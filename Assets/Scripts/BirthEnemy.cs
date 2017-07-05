using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirthEnemy : MonoBehaviour {

	public static void Birth(string name, Vector3 beginPosition) {
		GameObject slime = Instantiate (Resources.Load (name), beginPosition, Quaternion.identity) as GameObject;

	}
}
