using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageManager : MonoBehaviour {

	public static void HealthCalculate(GameObject obj, float damage) {
		float remainingHealth;

		if (obj.tag == "Enemy") {
			remainingHealth = obj.GetComponent<MonsterAIController> ().GetRemainingHealth ();

			if (remainingHealth - damage <= 0f)
				obj.GetComponent<MonsterAIController> ().SetRemainingHealth (0);
			else
				obj.GetComponent<MonsterAIController> ().SetRemainingHealth (remainingHealth - damage);

		} else {
			remainingHealth = obj.GetComponent<MonsterAIController> ().GetRemainingHealth ();

			if (remainingHealth - damage <= 0f)
				obj.GetComponent<CharacterController> ().SetRemainingHealth (0);
			else
				obj.GetComponent<CharacterController> ().SetRemainingHealth (remainingHealth - damage);
		}
	}

	public static IEnumerator ShowDamage(Vector3 position, float damage){
		GameObject damageHit = Instantiate (Resources.Load ("DamageHit"), position, Quaternion.identity) as GameObject;
		damageHit.GetComponent<TextMesh> ().text += damage.ToString ();

		yield return new WaitForSeconds (1f);
		Destroy (damageHit);
	}
}
