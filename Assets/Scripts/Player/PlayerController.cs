using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public static PlayerController _instance;

	public GUIStyle style;
	public float startMoney = 100f;
	private float curMoney = 0;

	public float width = 50;
	public float height = 20;

	bool needDialog = false;

	public float timeInterval = 3.0f;
	private float curTime = 0;

	void Awake()
	{
		_instance = this;
	}

	// Use this for initialization
	void Start () {
		curMoney = startMoney;
	}
	
	public void AddMoney(float num)
	{
		curMoney += num;
	}

	public bool SubMoney(float num)
	{
		if (num <= curMoney) 
		{
			curMoney -= num;
			return true;
		} 
		else 
		{
			needDialog = true;
			return false;
		}
	}

	void OnGUI()
	{
		GUILayout.Label ("your Money: " + curMoney);

		if (needDialog) {
			GUI.Label(new Rect(Screen.width/2 - width/2, Screen.height/2 - height / 2, width, height), "没钱玩你妈比!!", style);
			if (curTime >= timeInterval) {
				needDialog = false;
				curTime = 0;
			}
			else
			{
				curTime += Time.deltaTime;
			}
		}
	}
}
