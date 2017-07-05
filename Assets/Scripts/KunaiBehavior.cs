using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunaiBehavior : MonoBehaviour {
	public float speed = 0.25f;
	public bool isLeft;
	float timeLeft = 1f;

	public float kunaiAtk;
	// Use this for initialization
	void Start () {
		
		if (isLeft) {
			speed = -speed;
			this.GetComponent<SpriteRenderer> ().flipX = true;
		}

	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector2 (transform.position.x + speed, transform.position.y);

		timeLeft -= Time.deltaTime;
		if (timeLeft < 0) {
			Destroy (this.gameObject);
		}
	}

	void OnCollisionEnter2D (Collision2D coll) {
		Destroy (this.gameObject);
	}
}
