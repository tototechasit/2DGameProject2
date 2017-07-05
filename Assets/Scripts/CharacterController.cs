using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour {
	public float speed;
	Animator animator;
	Rigidbody2D rb;
	float x, distanceToGround;
	int jumpCounter, directionX;
	LayerMask groundLayer, enemyLayer;


	private float pAtk = 5;
	private float maxHealth = 50;
	private float remainingHealth;
	private float exp;
	private int level = 1;

	void Start () {
		animator = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
		jumpCounter = 0;
		directionX = 1;
		distanceToGround = GetComponent<Collider2D> ().bounds.extents.y;
		groundLayer = 1 << 10;
		enemyLayer = 1 << 9;

		remainingHealth = maxHealth;
	}

	void Update () {
		// get speed from input
		x = Input.GetAxis ("Horizontal");
		animator.SetFloat ("speed", FindAbs (x));

		// set jump
		if (Input.GetButtonDown ("Jump") && jumpCounter < 2) {
			animator.SetBool ("jump", true);
			rb.velocity = new Vector2 (rb.velocity.x, 12f);
			jumpCounter++;
		}

		// set attack
		if (Input.GetKeyDown (KeyCode.A) && (!animator.GetBool("attack") && !animator.GetBool("throw"))) {
			StartCoroutine(Attack());
		}

		// set throw
		if (Input.GetKeyDown (KeyCode.S) && (!animator.GetBool("attack") && !animator.GetBool("throw"))) {
			StartCoroutine(Throw());
		}

		// can move whenever jumpattack or don't attack only
		if ((!animator.GetBool("attack") && !animator.GetBool("throw")) || (animator.GetBool("attack") && animator.GetBool("jump")) ||
			animator.GetBool("throw") && animator.GetBool("jump"))
			// set character can't out of map and move
			if ((transform.position.x > -13f || (transform.position.x <= -13f && x > 0f)) && (transform.position.x < 45f || (transform.position.x >= 45f && x < 0f)))
				rb.velocity = new Vector2 (x * speed, rb.velocity.y);

		// set character turn around
		if (x > 0f) {
			directionX = 1;
			this.GetComponent<SpriteRenderer> ().flipX = false;
		} else if (x < 0f) {
			directionX = -1;
			this.GetComponent<SpriteRenderer> ().flipX = true;
		}

		if (GetEXP () >= 100f) {
			LevelUp ();
		}
	}

	// call when character colliding obj
	void OnCollisionEnter2D (Collision2D coll) {
		if (Physics2D.Raycast (transform.position, -Vector2.up, distanceToGround + 1f, groundLayer)) {
			animator.SetBool ("jump", false);
			jumpCounter = 0;
		}
	}

	IEnumerator Attack() {
		animator.SetBool ("attack", true);

		yield return new WaitForSeconds (0.4f);
		rb.velocity = new Vector2 (0, rb.velocity.y);
		RaycastHit2D obj = Physics2D.Raycast (transform.position, directionX * Vector2.right, 2f, enemyLayer);
		if (obj != null) {
			DamageManager.HealthCalculate (obj.collider.gameObject, pAtk);
			StartCoroutine (DamageManager.ShowDamage (transform.position, pAtk));
		}
	}
	// use in animator for set don't attack
	void StopAttack() {
		animator.SetBool ("attack", false);
	}

	IEnumerator Throw() {
		animator.SetBool ("throw", true);

		yield return new WaitForSeconds (0.2f);
		Vector2 kunaiThrowPosition = new Vector2 (transform.position.x + (1.5f*directionX), transform.position.y);

		GameObject kunai = Instantiate (Resources.Load ("Kunai", typeof(GameObject)), kunaiThrowPosition, Quaternion.identity) as GameObject;
		kunai.GetComponent<KunaiBehavior> ().isLeft = this.GetComponent<SpriteRenderer> ().flipX;
		kunai.GetComponent<KunaiBehavior> ().kunaiAtk = pAtk * 1.5f;

	}
	// use in animator for set don't throw
	void StopThrow() {
		animator.SetBool ("throw", false);
	}

	void LevelUp() {
		SetPAtk (GetPAtk() * 1.2f);
		SetMaxHealth (GetMaxHealth() * 1.3f);
		SetEXP (GetEXP() - 100f);
		SetLevel (GetLevel () + 1);
	}

	// find speed
	float FindAbs(float x){
		if (x > 0f)
			return x;
		return -x;
	}


	public float GetPAtk() {
		return pAtk;
	}
	public void SetPAtk(float pAtk) {
		this.pAtk = pAtk;
	}
	public float GetMaxHealth() {
		return maxHealth;
	}
	public void SetMaxHealth(float maxHealth) {
		this.maxHealth = maxHealth;
	}
	public float GetRemainingHealth() {
		return remainingHealth;
	}
	public void SetRemainingHealth(float remainingHealth) {
		this.remainingHealth = remainingHealth;
	}
	public float GetEXP() {
		return exp;
	}
	public void SetEXP(float exp) {
		this.exp = exp;
	}
	public int GetLevel() {
		return level;
	}
	public void SetLevel(int level) {
		this.level = level;
	}
}
