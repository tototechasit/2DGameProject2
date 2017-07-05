using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextMap : MonoBehaviour {
	public string sceneName;
	// Use this for initialization

	void OnTriggerStay2D (Collider2D coll) {
		if (coll.gameObject.tag == "Player" && Input.GetKeyDown (KeyCode.X)) {
			SceneManager.LoadScene (sceneName);
		}
	}
}
