using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	public Transform character;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (character.position.x > -12f && character.position.x < 45f)
			if (character.position.x > transform.position.x + 5f)
				transform.position = new Vector3 (character.position.x - 5f, transform.position.y, transform.position.z);
			else if (character.position.x < transform.position.x - 5f)
				transform.position = new Vector3 (character.position.x + 5f, transform.position.y, transform.position.z);
		
		if (character.position.y > -3.1f && character.position.y < 11.5f)
			if (character.position.y > transform.position.y + 2.75f)
				transform.position = new Vector3 (transform.position.x, character.position.y -2.75f, transform.position.z);
			else if (character.position.y < transform.position.y -2.75f)
				transform.position = new Vector3 (transform.position.x, character.position.y +2.75f, transform.position.z);
	}
}
