using UnityEngine;
using System.Collections;

public class EnemyAngent : MonoBehaviour {

	NavMeshAgent agent;
	public Vector3 targetPos = new Vector3(21.7f, 0.7f, 7.0f);

	private bool isDamage = false;
	private EnemyObejct enemy;
	private IEnemyDamage enemyDamage;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		agent.SetDestination (targetPos);

		enemy = GetComponent<EnemyObejct>();
		enemyDamage = enemy as IEnemyDamage;
	}
	
	// Update is called once per frame
	void Update () {
		if (isDamage == false && Vector3.Distance(transform.position, targetPos) <= 2f) {
			isDamage = true;
			enemyDamage.DamageMine();
		}
	}
}
