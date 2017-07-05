using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour {

	public Transform releasePosition;

	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.collider.gameObject.tag == "Player") {
			coll.collider.gameObject.transform.position = releasePosition.position;
		}
	}
}
