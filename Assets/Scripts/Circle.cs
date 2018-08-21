using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour {

	SpriteRenderer sr;
	/// <summary>
	/// высота диска в юнитах
	/// </summary>
	/// <value>высота</value>
	public float height{
		get{
			return 0.42f;
			}
		}
	
	void Awake () {
		sr = GetComponentInChildren<SpriteRenderer>();
	}
/// <summary>
/// перемещает диск на первый план
/// </summary>
	public void StartMove(){
		sr.sortingOrder = 10;
	}
/// <summary>
/// перемещает диск на общий слой
/// </summary>	
	public void StopMove(){
		sr.sortingOrder = 1;
	}

}
