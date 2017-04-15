using UnityEngine;
using System.Collections;

public class CameraChoose : MonoBehaviour {

	private Ray ray;
	private RaycastHit hitInfo;
	private int mask;
	private TowerBase towerBase;
	// Use this for initialization
	void Start () {
		mask = 1 << 8;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hitInfo, 500, mask)) 
			{
				towerBase = hitInfo.transform.GetComponent<TowerBase>();
				if (towerBase.tower == null) {
					ChooseTowerType();
				}
				else
				{
					//up level
				}
			}
			else {
				canShowDialog = false;
			}
		}
	}

	void ChooseTowerType()
	{
		canShowDialog = true;
	}
	private bool canShowDialog = false;
	void OnGUI()
	{
		if(canShowDialog)
		{
			GUILayout.Label ("Plase input '1' or '2' to choose the Tower!");
			if (Input.GetKeyDown(KeyCode.Alpha1)) {
				towerBase.CreateTower(TowerType.laser);
				canShowDialog = false;
			}
			else if (Input.GetKeyDown(KeyCode.Alpha2)) {
				towerBase.CreateTower(TowerType.radar);
				canShowDialog = false;
			}
		}

	}
}
