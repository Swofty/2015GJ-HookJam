using UnityEngine;
using System.Collections;

public class StaminaBar : MonoBehaviour {

	public float maximum = 1f;
	public float length = 1f;
	public float chargeModifier = 0.2f;

	public float delay = 3f;
	public float timer = 0f;

	public float spaceCost = 0.1f;

	void Update() {
		float timePassed = Time.deltaTime;
		float chargeTime = 0f;

		if ((Input.GetKeyDown(KeyCode.RightShift)
		     || Input.GetKeyDown(KeyCode.Z))
		    && length >= spaceCost * 0.99999f) {
			length = (length - spaceCost >= 0f) ? length - spaceCost : 0f;
			timer = delay;
		}
		else {
			if (timePassed > timer) {
				chargeTime = timePassed - timer;
				timer = 0f;
			}
			else {
				timer -= timePassed;
			}

			length = ((length + chargeTime * chargeModifier > maximum)
			          ? maximum
			          : length + chargeTime * chargeModifier);
		}
		transform.localScale = new Vector3(length * 0.5f, 0.1f, 1f);
		transform.position = new Vector3((length - maximum) * 7.2f, 0, 0);
	}
}
