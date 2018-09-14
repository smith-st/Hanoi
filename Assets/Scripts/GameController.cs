using System;
using System.Collections.Generic;
using System.Globalization;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	
	[SerializeField] private GameObject[] _circles;
	[SerializeField] private Tower[] _towers;
	[SerializeField] private UIController _ui;

	private struct HanoiPoint{
	public readonly byte From;
	public readonly byte To;
	public HanoiPoint(byte from, byte to){
		From = from;
		To = to;
	}
}

	private readonly List<HanoiPoint> _points = new List<HanoiPoint>();
	private int _circleCount = 1;
	private float _speed = 1f;


	private void Awake() {
		_ui = FindObjectOfType<UIController>();
		if (_ui == null)
			throw new Exception("Ненайден UI контроллер");
	}

	/// <summary>
	///Инициализация
	/// </summary>
	private void Start(){
		_ui.ShowSetup();
		
		for(var i=0;i<10;i++){
			var go = Instantiate(_circles[i],Vector3.zero,Quaternion.identity);
			_towers[0].AddCircles(go.GetComponent<Circle>());
		}
		_towers[0].ShowCircle(1);
	}

	/// <summary>
	/// Задает изначальное количество дисков
	/// </summary>
	/// <param name="count">количество</param>
	public void SetCircleCount(float count){
		_circleCount = (int)count;
		_ui.CountCircle(count);
		_towers[0].ShowCircle(_circleCount);
	}
	/// <summary>
	/// Скорость перемещения
	/// </summary>
	/// <param name="speed">скорость</param>
	public void SetStepSpeed(float speed){
		_speed = speed;
	}
	/// <summary>
	/// Перезапуск программы
	/// </summary>
	public void Restart(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	/// <summary>
	/// Запуск решения задачи
	/// </summary>
	public void StartHanoi(){
		_ui.ShowProgress();
		_towers[0].Reset();
		HanoiTowers(_circleCount,0,1,2);
		for(var i=0;i<_circleCount;i++){
			var go = Instantiate(_circles[i],Vector3.zero,Quaternion.identity);
			_towers[0].AddCircles(go.GetComponent<Circle>());
		}
		_ui.CountSteps(_points.Count);
		_ui.TotalSteps(_points.Count);
		HanoiStep();
	}
	/// <summary>
	/// Получение цепочки перемещений дисков
	/// </summary>
	/// <param name="q">количество дисков</param>
	/// <param name="from">с какой башни брать диск</param>
	/// <param name="to">куда перемещать</param>
	/// <param name="buf">буфферная башня</param>
	private void HanoiTowers(int q, byte from, byte to, byte buf){
		if (q != 0){
			HanoiTowers(q-1,from,buf,to);
			// print (from.ToString() + " - " + to.ToString());
			_points.Add(new HanoiPoint(from,to));
			HanoiTowers(q-1,buf,to,from);
		}
	}
	/// <summary>
	/// Выполняет одно перемещение дисков
	/// </summary>
	private void HanoiStep(){
		if (_points.Count>0){
			var point = _points.Shift();
			_ui.CountSteps(_points.Count);
			MoveCircle(_towers[point.From],_towers[point.To]);
		}else{
			_ui.ShowComplete();
		}
	}
	/// <summary>
	/// Двигает диск
	/// </summary>
	/// <param name="from">с какой башни</param>
	/// <param name="to">на какую</param>
	private void MoveCircle(Tower from, Tower to){
		var circle = from.GetCircle();
		circle.StartMove();
		circle.transform.DOMove(to.PositionForNewCircle,_speed).OnComplete(
			()=>{
				to.AddCircles(circle); 
				circle.StopMove();
				HanoiStep();
			}
		);
	}

	


	
	

	
}
