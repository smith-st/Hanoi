using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    readonly List<Circle> _allCircles = new List<Circle>(0);//хранит все диски
	/// <summary>
	/// возвращает позицию для нового диска
	/// </summary>
	/// <value>позиция</value>
    public Vector3 PositionForNewCircle
    {
        get
        {
            if (_allCircles.Count == 0)
            {
                return transform.position;
            }else{
                var last = _allCircles[_allCircles.Count - 1];
                return new Vector3(
                    last.transform.position.x,
                    last.transform.position.y + Circle.Height,
                    last.transform.position.z
                );
            }
        }
    }
	
	///<summary>Добавляет диск на эту башню</summary>
	///<param name="circle">диск</param>
	public void AddCircles(Circle circle) {
        circle.transform.position = PositionForNewCircle;
        _allCircles.Add(circle);
    }


	///<summary>Возвращает диск из башни</summary>
    public Circle GetCircle(){
        if (_allCircles.Count == 0)
            return null;
        else
            return _allCircles.Pop();
    }

	///<summary>Показывает диски на башне </summary>
	///<param name="to">до какого диска показать, все после этого будут спрятаны</param>
    public void ShowCircle(int to) {
        for (var i = 0; i < _allCircles.Count; i++) {
            _allCircles[i].gameObject.SetActive(i < to);
        }
    }
	///<summary>Удаляет все диски с башни</summary>
    public void Reset() {
	    foreach (var circle in _allCircles) {
	        Destroy(circle.gameObject);
	    }
	    _allCircles.Clear();
	}


}
