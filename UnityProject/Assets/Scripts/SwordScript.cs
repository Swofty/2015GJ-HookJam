using UnityEngine;
using System.Collections;

public class SwordScript : MonoBehaviour {

    public float MIN_CHARGE_TIME = 0.5f;
    public float MAX_CHARGE_TIME = 1.5f;
	
	private GameObject player;
    private GameObject staminaBar;
    private float startTime;

    public bool inCharge;
    public bool inSwing;
    public bool charged;
    private bool finished = false;
	
	void Awake()
	{
        staminaBar = GameObject.Find("Stamina Bar");
		player = transform.parent.gameObject;
        finished = true;
	}
	
	public void ActivateSword()
	{
		gameObject.SetActive(true);
        finished = false;
        startTime = Time.time;
        inCharge = true;
        inSwing = false;
		Debug.Log("Sword enabled!");
	}

	
	public void DisableSword()
	{
        finished = true;
        startTime = 0.0f;
        inCharge = false;
        inSwing = false;
        charged = false;
		gameObject.SetActive(false);
		Debug.Log("Sword disabled!");
	}

    public void FinishRegular()
    {
        if (staminaBar.GetComponent<StaminaBar>().DoAttack(Util.Attack.SWORD))
        {
            inCharge = false;
            inSwing = true;
            charged = false;
        }
    }

    public void FinishCharge()
    {
        if (staminaBar.GetComponent<StaminaBar>().DoAttack(Util.Attack.CHARGE))
        {
            inCharge = false;
            inSwing = true;
            charged = true;
        }
    }
    public void CancelCharge()
    {
        inCharge = false;
        inSwing = false;
        DisableSword();
    }

    public bool InSwing()
	{
        return inSwing;
    }

    public bool isFinished()
    {
        return finished;
    }

    public bool IsChargedAttack()
    {
        return Time.time - startTime > MIN_CHARGE_TIME;
    }

    public bool IsMaxTime()
    {
        return Time.time - startTime > MAX_CHARGE_TIME;
    }


	void OnTriggerEnter2D(Collider2D other) {
        HittablePart p = Util.GetHittablePart(other);
        if (p)
        {
            staminaBar.GetComponent<StaminaBar>().AddStamina(0.05f);
            Debug.Log("Got other enemy");
            if (charged)
                p.OnChargedAttackHit(player, 2.0f);
            else
            {
                p.OnAttackHit(player, 1.0f);
            }
        }
	}
	
}