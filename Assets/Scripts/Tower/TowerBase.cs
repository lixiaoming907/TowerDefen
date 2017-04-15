using UnityEngine;
using System.Collections;

public class TowerBase : MonoBehaviour {

	private Transform towerPos;

	[HideInInspector]
	public Tower tower;

	private GameObject laserPrefab;
	private GameObject radarPrefab;

	// Use this for initialization
	void Start () {
		radarPrefab = Resources.Load<GameObject>("TowerPrefabs/Tow_Radar3");
		laserPrefab = Resources.Load<GameObject>("TowerPrefabs/Tow_Plasma3");

		towerPos = transform.GetChild (0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CreateTower(TowerType type)
	{
		if (!PlayerController._instance.SubMoney(100)) {
			return;
		}
		GameObject prefab = null;
		switch (type) {
		case TowerType.laser:
			prefab = laserPrefab;
			break;
		case TowerType.radar:
			prefab = radarPrefab;
			break;
		default:
			break;
		}
		GameObject go = Instantiate(prefab,towerPos.position,Quaternion.identity) as GameObject;
		go.transform.parent = towerPos;
		go.transform.localPosition = Vector3.zero;

		tower = go.GetComponent<Tower>();
	}
}
