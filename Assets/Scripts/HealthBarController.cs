using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour {
	public Image healthBar;

	public void ChangeHealthBar(float maxHealth, float remainingHealth) {
		healthBar.fillAmount = remainingHealth / maxHealth ;
	}

}
