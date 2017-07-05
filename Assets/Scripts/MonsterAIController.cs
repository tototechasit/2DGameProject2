using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAIController : MonoBehaviour {

	public float speed;
	Vector3 monsterBegin;
	Rigidbody2D rb;
	public int axis;
	float distanceToGround;
	LayerMask groundLayer;

	public string name;
	public float maxHealth;
	private float remainingHealth;

	public float expReturn;


	// Use this for initialization
	void Start () {
		monsterBegin = transform.position;
		axis = FindBeginAxis ();
		rb = GetComponent<Rigidbody2D> ();
		distanceToGround = GetComponent<Collider2D> ().bounds.extents.y;
		groundLayer = 1 << 10;

		remainingHealth = maxHealth;
	}

	// Update is called once per frame
	void Update () {
		
		Walk();

		this.gameObject.GetComponent<HealthBarController> ().ChangeHealthBar (maxHealth, remainingHealth);

		StartCoroutine (CheckHealth());


	}

	// random begin direction
	int FindBeginAxis() {
		int n = Random.Range (-1, 1);
		if (n < 0)
			return -1;
		return 1;
	}

	void Walk() {
		if (axis > 0) {
			this.GetComponent<SpriteRenderer> ().flipX = true;
			if (!Physics2D.Raycast (transform.position + new Vector3 (0.5f, 0f, 0f), -Vector2.up, distanceToGround+0.5f, groundLayer)) {
				axis = -axis;
			}
		} else {
			this.GetComponent<SpriteRenderer> ().flipX = false;
			if (!Physics2D.Raycast (transform.position + new Vector3 (-0.5f, 0f, 0f), -Vector2.up, distanceToGround+0.5f, groundLayer)) {
				axis = -axis;
			}
		}
		rb.velocity = new Vector2 (axis * speed, rb.velocity.y);
	}

	// check is it die ?
	IEnumerator CheckHealth () {
		if (remainingHealth <= 0) {

			GetComponent<SpriteRenderer> ().enabled = false;
			transform.Find ("Canvas").GetComponent<Canvas> ().enabled = false;
			GetComponent<Collider2D> ().isTrigger = true;


			yield return new WaitForSeconds (3f);
			GameObject.Find ("NinjaGirl").GetComponent<CharacterController> ().SetEXP (GameObject.Find ("NinjaGirl").GetComponent<CharacterController> ().GetEXP() + expReturn);
			Destroy (this.gameObject);


			BirthEnemy.Birth (name, monsterBegin);
		}
	}

	void OnCollisionEnter2D (Collision2D coll) {
		// colliding by kunai
		if (coll.gameObject.tag == "DamageObject") {
			float kunaiAtk = coll.gameObject.GetComponent<KunaiBehavior> ().kunaiAtk;
			DamageManager.HealthCalculate (this.gameObject, kunaiAtk);

			StartCoroutine (DamageManager.ShowDamage (transform.position, kunaiAtk));
		} else if (coll.gameObject.tag == "Enemy") {
			axis = -axis;
		}
	}


	public float GetRemainingHealth() {
		return remainingHealth;
	}
	public void SetRemainingHealth(float remainingHealth) {
		this.remainingHealth = remainingHealth;
	}
}
