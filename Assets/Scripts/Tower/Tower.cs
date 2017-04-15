using UnityEngine;
using System.Collections;

public enum TowerType
{
	radar,
	laser
}

public class Tower : MonoBehaviour
{

	public TowerType type;
	public GameObject attactEffect;
	public Transform shootPos;

	public float attactRadius = 10;
	public float timeInterval = 1.0f;
	public float rotaSpeed = 20.0f;

	private float curTime = 0;

	private EnemyObejct targetEnemy;


	// Use this for initialization
	void Start ()
	{
		curTime = timeInterval;
	}
	
	// Update is called once per frame
	void Update ()
	{
		targetEnemy = CheckEnemy ();
		if (targetEnemy == null) {
			StopAllCoroutines();
			return;
		} else {
			if (curTime >= timeInterval) {
				ShootEnemy ();
				curTime = 0;
			} else {
				curTime += Time.deltaTime;
			}
		}
	}

	private void ShootEnemy ()
	{
		if (type == TowerType.radar) {
			GameObject effect = Instantiate (attactEffect, shootPos.position, Quaternion.LookRotation (targetEnemy.transform.position - shootPos.position)) as GameObject;
			effect.GetComponent<BulletController> ().BeginFollow (targetEnemy.transform, type, 50.0f);
		} else {
			StartCoroutine (LaserShoot ());
		}
//		Debug.Log (targetEnemy.name);
	}

	IEnumerator LaserShoot ()
	{	
		while (targetEnemy!= null && Vector3.Angle(transform.forward, new Vector3 (targetEnemy.transform.position.x, transform.position.y, targetEnemy.transform.position.z) - transform.position) > 2f) {
			//Debug.Log(targetEnemy);
			Vector3 targetPos = new Vector3 (targetEnemy.transform.position.x, transform.position.y, targetEnemy.transform.position.z);
			Vector3 dir = targetPos - transform.position;
			transform.rotation = Quaternion.RotateTowards (transform.rotation, Quaternion.LookRotation (dir), rotaSpeed);
			yield return null;
		}
		if (targetEnemy == null) {
			StopAllCoroutines();
		}
		GameObject effect = Instantiate (attactEffect, shootPos.position, Quaternion.LookRotation (targetEnemy.transform.position - shootPos.position)) as GameObject;
		effect.GetComponent<BulletController> ().BeginFollow (targetEnemy.transform, type, 50.0f);
	}

	private EnemyObejct CheckEnemy ()
	{
		if (EnemyListController._instance.enemyLst.Count <= 0) {
			return null;
		} else {
			if (EnemyListController._instance.enemyLst.Count <= 0) {
				return null;
			}
			for (int i = 0; i < EnemyListController._instance.enemyLst.Count; i++) {
				if (Vector3.Distance (EnemyListController._instance.enemyLst [i].transform.position, transform.position) <= attactRadius) {
					if (EnemyListController._instance.enemyLst.Count <= 0) {
						return null;
					}
					return EnemyListController._instance.enemyLst [i];
				}
			}
			return null;
		}
	}


	void OnDrawGizmos ()
	{
		Gizmos.color = new Color (1, 0, 0, 0.2f);
		Gizmos.DrawSphere (transform.position, attactRadius);
	}
}
