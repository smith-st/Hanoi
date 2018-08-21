using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	
public GameObject[] circles;
public Tower[] towers;
public Text txtCircleCount;
public Text txtSteps;
public Text txtStepsOnComplete;
public GameObject setupCont;
public GameObject progressCont;
public GameObject completeCont;

struct HanoiPoint{
	public byte from;
	public byte to;
	public HanoiPoint(byte from, byte to){
		this.from = from;
		this.to = to;
	}
}

	List<HanoiPoint> points = new List<HanoiPoint>();
	int circleCount = 1;
	float speed = 1f;

/// <summary>
///Инициализация
/// </summary>
	void Start(){
		setupCont.SetActive(true);
		progressCont.SetActive(false);
		completeCont.SetActive(false);
		
		for(int i=0;i<10;i++){
			GameObject go = Instantiate(circles[i],Vector3.zero,Quaternion.identity);
			towers[0].AddCircles(go.GetComponent<Circle>());
		}
		towers[0].ShowCircle(1);
	}

	/// <summary>
	/// Запуск решения задачи
	/// </summary>
	public void StartHanoi(){
		setupCont.SetActive(false);
		progressCont.SetActive(true);
		towers[0].Reset();
		HanoiTowers(circleCount,0,1,2);
		for(int i=0;i<circleCount;i++){
			GameObject go = Instantiate(circles[i],Vector3.zero,Quaternion.identity);
			towers[0].AddCircles(go.GetComponent<Circle>());
		}
		txtStepsOnComplete.text = points.Count.ToString();
		txtSteps.text = points.Count.ToString();
		HanoiStep();
	}
	/// <summary>
	/// Получение цепочки перемещений дисков
	/// </summary>
	/// <param name="q">количество дисков</param>
	/// <param name="from">с какой башни брать диск</param>
	/// <param name="to">куда перемещать</param>
	/// <param name="buf">буфферная башня</param>
	void HanoiTowers(int q, byte from, byte to, byte buf){
		if (q != 0){
			HanoiTowers(q-1,from,buf,to);
			// print (from.ToString() + " - " + to.ToString());
			points.Add(new HanoiPoint(from,to));
			HanoiTowers(q-1,buf,to,from);
		}
	}
/// <summary>
/// Выполняет одно перемещение дисков
/// </summary>
	void HanoiStep(){
		if (points.Count>0){
			HanoiPoint point = points.Shift();
			txtSteps.text = points.Count.ToString();
			MoveCircle(towers[point.from],towers[point.to]);
		}else{
			progressCont.SetActive(false);
			completeCont.SetActive(true);
		}
	}
	/// <summary>
	/// Двигает диск
	/// </summary>
	/// <param name="from">с какой башни</param>
	/// <param name="to">на какую</param>
	void MoveCircle(Tower from, Tower to){
		Circle circle = from.GetCircle();
		circle.StartMove();
		circle.transform.DOMove(to.positionForNewCircle,speed).OnComplete(
			()=>{
				to.AddCircles(circle); 
				circle.StopMove();
				HanoiStep();
			}
		);
	}

	
/// <summary>
/// Задает изначальное количество дисков
/// </summary>
/// <param name="count">количество</param>
	public void SetCircleCount(float count){
		circleCount = (int)count;
		txtCircleCount.text = count.ToString();
		towers[0].ShowCircle(circleCount);
	}
	/// <summary>
	/// Скорость перемещения
	/// </summary>
	/// <param name="speed">скорость</param>
	public void SetStepSpeed(float speed){
		this.speed = speed;
	}
	/// <summary>
	/// Перезапуск программы
	/// </summary>
	public void Restart(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}


	
	

	
}
