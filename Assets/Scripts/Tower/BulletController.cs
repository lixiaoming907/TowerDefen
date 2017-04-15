using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	public float t;
	public GameObject boomEffect;

	Transform target;
	float damage;

	bool canFollow = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (target == null) {
			transform.Translate(transform.forward * 2.0f,Space.World);
			return;
		}

		if (canFollow && target != null) {
			transform.position = Vector3.Lerp(transform.position,target.position,t);
			if (Vector3.Distance(transform.position,target.position) <= 0.3f) {
				canFollow = false;
				Instantiate(boomEffect,transform.position,Quaternion.identity);
				EnemyTakeDamage();
			}
		}
	}

	public void BeginFollow(Transform target, TowerType towerType, float damage)
	{
		this.target = target;
		this.damage = damage;
		if (towerType == TowerType.radar) {
			EnemyTakeDamage();
		}
		else {
			canFollow = true;
		}
	}

	void EnemyTakeDamage()
	{
		EnemyObejct enemy = this.target.GetComponent<EnemyObejct>();
		IEnemyStudio studio = enemy as IEnemyStudio;
		studio.TakeDamage(this.damage);
	}
}
