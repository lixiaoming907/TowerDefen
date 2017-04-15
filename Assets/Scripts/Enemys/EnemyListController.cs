using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyListController : MonoBehaviour {

	public static EnemyListController _instance = null;

	public List<EnemyObejct> enemyLst = new List<EnemyObejct>();

	void Awake()
	{
		_instance = this;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
