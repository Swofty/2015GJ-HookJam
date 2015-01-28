using UnityEngine;
using System.Collections;

public class StaminaBar : MonoBehaviour {

	public float MAXIMUM = 1f; // Maximum value of charge possible
	public float NORMAL_RECHARGE_MODIFIER = 0.4f; // How much the bar recharges per second
	public float CHARGE_RECHARGE_MODIFIER = 0.05f; // How much the bar recharges per second when charging
	public float DELAY = 1f; // Seconds to wait after last attack before recharging

	public float HOOK_COST = 0.05f;
	public float SWORD_COST = 0.01f;
	public float DASH_COST = 0.01f;
	public float CHARGE_COST = 0.01f;

	public float length;
	public float timer;
	public float currentModifier;
	public GameObject player;
	public RectTransform rectTransform;

	void Awake() {
		length = MAXIMUM;
		timer = 0f;
		currentModifier = NORMAL_RECHARGE_MODIFIER;
		player = GameObject.Find ("Hero");
		rectTransform = this.GetComponent<RectTransform>();
	}

    public void AddStamina(float amount)
    {
        length = Mathf.Min(MAXIMUM, length + amount);
    }

	// Returns true if attack possible, otherwise false
	public bool CanAttack(Globals.Attack attack) {
		switch (attack) {
			case Globals.Attack.HOOK: return (length >= HOOK_COST * 0.99999f);
			case Globals.Attack.SWORD: return (length >= SWORD_COST * 0.99999f);
			case Globals.Attack.DASH: return (length >= DASH_COST * 0.99999f);
			case Globals.Attack.CHARGE: return (length >= CHARGE_COST * 0.99999f);
			default: return false;
		}
	}

	//Used for certain attacks, like charging and dashing
	public bool PrepareAttack(Globals.Attack attack) {
		if (CanAttack (attack)) {
			switch (attack) {
				case Globals.Attack.CHARGE: currentModifier = CHARGE_RECHARGE_MODIFIER; return true;
				default: return false;
			}
		}

		return false;
	}

	// Adjusts bar for attack, returns true if successful, otherwise false
	public bool DoAttack(Globals.Attack attack) {
		if (CanAttack (attack)) {
			float cost;

			switch (attack) {
				case Globals.Attack.HOOK: cost = HOOK_COST; break;
				case Globals.Attack.SWORD: cost = SWORD_COST; break;
				case Globals.Attack.DASH: cost = DASH_COST; break;
				case Globals.Attack.CHARGE: cost = CHARGE_COST; break;
				default: return false;
			}

			length = (length - cost >= 0f) ? length - cost : 0f;
			timer = DELAY;

			return true;
		}

		return false;
	}

	public bool CancelAttack(Globals.Attack attack) {
		switch (attack) {
			case Globals.Attack.CHARGE: currentModifier = NORMAL_RECHARGE_MODIFIER; return true;
			default: return false;
		}
		
		return false;
	}

	void Update() {
		float timePassed = Time.deltaTime;
		float rechargeTime = 0f;

		/*if ((Input.GetKeyDown(KeyCode.RightShift)
		     || Input.GetKeyDown(KeyCode.Z))
		    && length >= spaceCost * 0.99999f) {
			length = (length - spaceCost >= 0f) ? length - spaceCost : 0f;
			timer = delay;
		}
		else {*/
			if (timePassed > timer) {
				rechargeTime = timePassed - timer;
				timer = 0f;
			}
			else {
				timer -= timePassed;
			}

			length = ((length + rechargeTime * currentModifier > MAXIMUM)
			          ? MAXIMUM
			          : length + rechargeTime * currentModifier);
		//}
		//Debug.Log (length);
         rectTransform.sizeDelta = new Vector2(240 * length, 10);
        //rectTransform.rect.width = Screen.width * 0.3 * length;
		//rectTransform.rect.height = Screen.height * 0.05;
         rectTransform.position = new Vector3(80 + rectTransform.rect.width / 2,
                                              Screen.height - 42 - rectTransform.rect.height / 2,
                                              0);
	}
}
