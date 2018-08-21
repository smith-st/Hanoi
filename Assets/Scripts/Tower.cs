using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    List<Circle> allCircles = new List<Circle>(0);//хранит все диски
	/// <summary>
	/// возвращает позицию для нового диска
	/// </summary>
	/// <value>позиция</value>
    public Vector3 positionForNewCircle
    {
        get
        {
            if (allCircles.Count == 0)
            {
                return this.transform.position;
            }
            else
            {
                Circle last = allCircles[allCircles.Count - 1];
                return new Vector3(
                    last.transform.position.x,
                    last.transform.position.y + last.height,
                    last.transform.position.z
                );
            }
        }
    }
	
	///<summary>Добавляет диск на эту башню</summary>
	///<param name="circle">диск</param>
	public void AddCircles(Circle circle)
    {
        circle.transform.position = positionForNewCircle;
        allCircles.Add(circle);
    }


	///<summary>Возвращает диск из башни</summary>
    public Circle GetCircle()
    {
        if (allCircles.Count == 0)
            return null;
        else
            return allCircles.Pop();
    }

	///<summary>Показывает диски на башне </summary>
	///<param name="to">до какого диска показать, все после этого будут спрятаны</param>
    public void ShowCircle(int to)
    {
        for (int i = 0; i < allCircles.Count; i++)
        {
            if (i < to)
                allCircles[i].gameObject.SetActive(true);
            else
                allCircles[i].gameObject.SetActive(false);


        }
    }
	///<summary>Удаляет все диски с башни</summary>
    public void Reset()
    {
        for (int i = 0; i < allCircles.Count; i++)
        {
            Destroy(allCircles[i].gameObject);
        }
        allCircles.Clear();
    }


}
