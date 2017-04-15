using UnityEngine;
using System.Collections;

public enum EnemyType
{
	Rabbit,
	BigRabbit,
	Elephant
}

public class Enemy : EnemyObejct , IEnemyStudio, IEnemyDamage{

	public float TotalHealth;
	public float MoveSpeed;
	public float DamageOfMine;
	public float enemyMoney;
	public EnemyType type;

	public Vector3 offset;
	public float width = 20;
	public float height = 10;
	public float bloodDis = 10;

	private Animator anim;
	private NavMeshAgent agent;
	private Collider col;

	private Texture bloodTexture;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		agent = GetComponent<NavMeshAgent>();
		col = GetComponent<Collider>();
		bloodTexture = Resources.Load<Texture>("Textures/Blood");
		EnemyInit ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	private float _health;
	private float _curHealth;
	private float _moveSpeed;
	private float _damageOfMine;

	#region implemented abstract members of EnemyObejct
	public override float health {
		get {
			return _health;
		}
		set {
			if (value <= 0) {
				Debug.LogError("Are you SB?");
			}
			_health = value;
		}
	}

	public override float curHealth {
		get {
			return _curHealth;
		}
		set {
			_curHealth = value;
		}
	}

	public override float moveSpeed {
		get {
			return _moveSpeed;
		}
		set {
			if (value < 0) {
				Debug.LogError("Are you SB?");
			}
			agent.speed = value;
			_moveSpeed = value;
		}
	}

	#region implemented abstract members of EnemyObejct

	public override float damage {
		get {
			return _damageOfMine;
		}
		set {
			if (value < 0) {
				Debug.LogWarning("Are you SB?");
			}
			_damageOfMine = value;
		}
	}

	#endregion

	public override void EnemyInit()
	{
		health = TotalHealth;
		curHealth = health;
		moveSpeed = MoveSpeed;
		damage = DamageOfMine;
	}
	#endregion

	#region IEnemyStudio implementation

	public void TakeDamage (float damage)
	{
		curHealth -= damage;
		if (curHealth <= 0) {
			BeingDead();
		}
	}

	public void BeingDead ()
	{
		if (anim == null) {
			return;
		}
		//todo: enemy is die!!
		anim.SetTrigger ("Dead");
		moveSpeed = 0;
		col.enabled = false;
		agent.enabled = false;
		EnemyListController._instance.enemyLst.Remove (this);
		PlayerController._instance.AddMoney (enemyMoney);
		StartCoroutine (EnemyTranslate());
	}

	#endregion

	#region IEnemyDamage implementation

	public void DamageMine ()
	{
		//todo:
//		Debug.Log (" o o o! Ya Me Die!!" + damage);
		EnemyListController._instance.enemyLst.Remove (this);
		Destroy (this.gameObject);
	}

	#endregion

	IEnumerator EnemyTranslate ()
	{
		float totalTime = 2;
		float curTime = 0;
		yield return new WaitForSeconds(1.5f);
		while (curTime < totalTime) {
//			Debug.Log("this is IEnumerator");
			transform.Translate(Vector3.down * Time.deltaTime * 5.0f, Space.World);
			curTime += Time.deltaTime;
			yield return null;
		}
		Destroy (this.gameObject);
	}

	void OnGUI()
	{
		if (curHealth <= 0) {
			return;
		}
		float t = bloodDis/(Vector3.Distance(transform.position,Camera.main.transform.position));
		Vector3 pos = Camera.main.WorldToScreenPoint (transform.position + offset);
		GUI.DrawTexture (new Rect(pos.x - width/2 * t, Screen.height - pos.y - height/2, width * curHealth/TotalHealth * t, height * t),bloodTexture);
	}
}
