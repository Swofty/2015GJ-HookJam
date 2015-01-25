using UnityEngine;
using System.Collections;

public class ChargeScript : MonoBehaviour {

	public float ANGULAR_SPEED = 0.1f;
	public float SWORD_SIZE = 0.2f;
	public float MAXIMUM_CHARGE_LENGTH = 1.5f;

	private Vector3 initPos;
	private GameObject staminaBar;
	private GameObject player;
	private float rotateTo;
	private float timeActive;

	public bool inCharge;
	public bool inSwing;
	private float timeCharged;
	
	void Awake()
	{
		staminaBar = GameObject.Find("Stamina Bar");
		player = transform.parent.gameObject;
		inCharge = false;
		timeActive = 0f;
		inSwing = false;
		timeCharged = 0f;
	}
	
	public void ActivateSword(Constants.Dir dir)
	{
		gameObject.SetActive(true);
		transform.localPosition = GetInitPosition(dir);
		transform.rotation = GetInitRotation(dir);
		Debug.Log("Sword enabled!");
	}
	
	void DisableSword()
	{
		gameObject.SetActive(false);
		Debug.Log("Sword disabled!");
	}
	
	Vector3 GetInitPosition(Constants.Dir dir)
	{
		// TODO
		Vector3 pos = (0.34f * Constants.getVectorFromDirection(dir));
		return pos;
	}
	
	Quaternion GetInitRotation(Constants.Dir dir)
	{
		// TODO
		Quaternion rot;
		switch (dir)
		{
		case Constants.Dir.N: rot = Quaternion.Euler(0.0f, 0.0f, 135.0f); break;
		case Constants.Dir.E: rot = Quaternion.Euler(0.0f, 0.0f, 45.0f); break;
		case Constants.Dir.S: rot = Quaternion.Euler(0.0f, 0.0f, 315.0f); break;
		case Constants.Dir.W: rot = Quaternion.Euler(0.0f, 0.0f, 225.0f); break;
		default: rot = new Quaternion(); break;
		}
		return rot;
	}


	public void StartCharge(Constants.Dir dir)
	{
		ActivateSword (dir);
		inCharge = true;
	}

	public void FinishCharge()
	{
		if (staminaBar.GetComponent<StaminaBar> ().DoAttack (Constants.Attack.CHARGE)) {
			inCharge = false;
			timeCharged = 0;
			inSwing = true;
		}
		else {
			inCharge = false;
			timeCharged = 0;
		}
	}

	public void CancelCharge()
	{
		inCharge = false;
		timeCharged = 0;
		inSwing = false;
		DisableSword ();
	}

	public void Update() {
		if (inCharge)
		{
			Debug.Log (timeCharged);
			timeCharged += Time.deltaTime;

			if (timeCharged >= MAXIMUM_CHARGE_LENGTH) {
					FinishCharge ();	
			}
		}
		else if (inSwing) {
			timeActive += Time.deltaTime;
			transform.Rotate(-Vector3.forward * Time.deltaTime * 360);
			
			// When to stop
			if(timeActive > 0.25f)
			{
				DisableSword();
				timeActive = 0f;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		other.gameObject.GetComponent<EnemyHitbox>().OnAttackHit();
	}


	// DisableControls
	// EnableControls
}
