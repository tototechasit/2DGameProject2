using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusTextController : MonoBehaviour {

	public Text AtkTxt, HealthTxt, LevelTxt;
	public GameObject character;

	void Start () {
		AtkTxt.text = character.GetComponent<CharacterController> ().GetPAtk ().ToString ();
		HealthTxt.text = character.GetComponent<CharacterController> ().GetMaxHealth ().ToString ();
		LevelTxt.text = character.GetComponent<CharacterController> ().GetLevel ().ToString ();
	}
}
