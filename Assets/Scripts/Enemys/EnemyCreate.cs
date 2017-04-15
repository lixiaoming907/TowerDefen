using UnityEngine;
using System.Collections;

public class EnemyCreate : MonoBehaviour , IEnemyCreate{

	public GameObject enemy;
	public float startTime = 5.0f;
	public float repeatTime = 2.0f;
	public int totalCount = 10;

	private Transform enemyList;
	private int curCount = 0;
	// Use this for initialization
	void Start () {

		GameObject find = GameObject.Find ("EnemyList");
		if (!find) {
			Debug.LogError("Are you Bitch?");
			return;
		}
		enemyList = find.transform;

		if (totalCount <= 0 || startTime <= 0 || repeatTime < 0) {
			Debug.LogWarning("Are you SB?");
			return;
		}

		InvokeRepeating ("CreateEnemy",startTime, repeatTime);
	}
	
//	// Update is called once per frame
//	void Update () {
//	
//	}

	public void CreateEnemy()
	{
		GameObject go = Instantiate (enemy,transform.position,Quaternion.identity) as GameObject;
		go.transform.parent = enemyList;
		curCount ++;

		EnemyListController._instance.enemyLst.Add (go.GetComponent<EnemyObejct>());

		if (curCount >= totalCount) {
			CancelInvoke();
		}
	}
}
