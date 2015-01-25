using UnityEngine;
using System.Collections;

public class ChargeScript : MonoBehaviour {

	public float ANGULAR_SPEED = 0.1f;
	public float SWORD_SIZE = 0.2f;

    public float MAXIMUM_CHARGE_LENGTH = 1.0f;

	private Vector3 initPos;
	private GameObject staminaBar;
	private GameObject player;
    private GameObject sword;
	private float rotateTo;
	private float timeActive;

	public bool inCharge;
	public bool inSwing;

    private float startTime;
	private float timeCharged;
	
	void Awake()
	{
		staminaBar = GameObject.Find("Stamina Bar");
		player = transform.parent.gameObject;
        sword = player.transform.FindChild("Sword").gameObject;
		inCharge = false;
		timeActive = 0f;
		inSwing = false;
		timeCharged = 0f;
	}
	
	public void ActivateSword(Constants.Dir dir)
	{
		sword.SetActive(true);
		Debug.Log("Sword enabled!");
	}
	
	void DisableSword()
	{
		sword.SetActive(false);
		Debug.Log("Sword disabled!");
	}


	public void StartCharge(Constants.Dir dir)
	{
		ActivateSword (dir);
		inCharge = true;
		startTime = Time.time;
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
