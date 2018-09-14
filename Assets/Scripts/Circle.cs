using UnityEngine;

public class Circle : MonoBehaviour {
	private SpriteRenderer _sr;
	/// <summary>
	/// высота диска в юнитах
	/// </summary>
	/// <value>высота</value>
	public static float Height{
		get{
			return 0.42f;
			}
		}

	private void Awake () {
		_sr = GetComponentInChildren<SpriteRenderer>();
	}
	/// <summary>
	/// перемещает диск на первый план
	/// </summary>
	public void StartMove(){
		_sr.sortingOrder = 10;
	}
	/// <summary>
	/// перемещает диск на общий слой
	/// </summary>	
	public void StopMove(){
		_sr.sortingOrder = 1;
	}

}
