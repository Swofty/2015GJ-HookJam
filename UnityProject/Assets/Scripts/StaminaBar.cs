﻿using UnityEngine;
using System.Collections;

public class StaminaBar : MonoBehaviour {

	public float MAXIMUM = 1f; // Maximum value of charge possible
	public float CHARGE_MODIFIER = 0.2f; // How much the bar charges per second
	public float DELAY = 3f; // Seconds to wait after last attack before charging

	public float HOOK_COST = 0.1f;
	public float SWORD_COST = 0.1f;
	public float DASH_COST = 0.1f;

	public float length;
	public float timer;
	public GameObject player;
	public RectTransform rectTransform;

	void Awake() {
		length = MAXIMUM;
		timer = 0f;
		player = GameObject.Find ("Hero");
		rectTransform = this.GetComponent<RectTransform>();
	}

	// Returns true if attack possible, otherwise false
	public bool CanAttack(Constants.Attack attack) {
		switch (attack) {
			case Constants.Attack.HOOK: return (length >= HOOK_COST * 0.99999f);
			case Constants.Attack.SWORD: return (length >= SWORD_COST * 0.99999f);
			case Constants.Attack.DASH: return (length >= DASH_COST * 0.99999f);
			default: return false;
		}
	}

	// Adjusts bar for attack, returns true if successful, otherwise false
	public bool DoAttack(Constants.Attack attack) {
		if (CanAttack (attack)) {
			float cost;

			switch (attack) {
				case Constants.Attack.HOOK: cost = HOOK_COST; break;
				case Constants.Attack.SWORD: cost = SWORD_COST; break;
				case Constants.Attack.DASH: cost = DASH_COST; break;
				default: return false;
			}

			length = (length - cost >= 0f) ? length - cost : 0f;
			timer = DELAY;

			return true;
		}

		return false;
	}

	void Update() {
		float timePassed = Time.deltaTime;
		float chargeTime = 0f;

		/*if ((Input.GetKeyDown(KeyCode.RightShift)
		     || Input.GetKeyDown(KeyCode.Z))
		    && length >= spaceCost * 0.99999f) {
			length = (length - spaceCost >= 0f) ? length - spaceCost : 0f;
			timer = delay;
		}
		else {*/
			if (timePassed > timer) {
				chargeTime = timePassed - timer;
				timer = 0f;
			}
			else {
				timer -= timePassed;
			}

			length = ((length + chargeTime * CHARGE_MODIFIER > MAXIMUM)
			          ? MAXIMUM
			          : length + chargeTime * CHARGE_MODIFIER);
		//}
		Debug.Log (length);
		rectTransform.sizeDelta = new Vector2(((float)Screen.width) * 0.3f * length, ((float)Screen.height) * 0.05f);
		//rectTransform.rect.width = Screen.width * 0.3 * length;
		//rectTransform.rect.height = Screen.height * 0.05;
		rectTransform.position = new Vector3(25 + rectTransform.rect.width / 2,
		                                 	 Screen.height - 25 - rectTransform.rect.height / 2,
		                                 	 0);
	}
}
