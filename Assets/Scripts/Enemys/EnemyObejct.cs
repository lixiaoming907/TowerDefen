using UnityEngine;
using System.Collections;

public abstract class EnemyObejct : MonoBehaviour {

	public abstract float health {
		set;
		get;
	}

	public abstract float curHealth {
		set;
		get;
	}

	public abstract float moveSpeed {
		set;
		get;
	}

	public abstract float damage{ 
		set;
		get; 
	}

	public abstract void EnemyInit();
}
