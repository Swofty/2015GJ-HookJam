using UnityEngine;
using System.Collections;

public class SwordScript : MonoBehaviour {

	public float ANGULAR_SPEED = 0.1f;
	public float SWORD_SIZE = 0.2f;
	
	private Vector3 initPos;
	private GameObject player;
	private float rotateTo;
    private float startTime;

	private float timeActive = 0f;

    private bool finished = false;
	
	void Awake()
	{
		player = transform.parent.gameObject;
        finished = true;
	}
	
	public void ActivateSword(Constants.Dir dir)
	{
		gameObject.SetActive(true);
        finished = false;
        startTime = Time.time;
		Debug.Log("Sword enabled!");
	}

    public bool isFinished()
    {
        return finished;
    }
	
	public void DisableSword()
	{
        finished = true;
        startTime = 0.0f;
		gameObject.SetActive(false);
		Debug.Log("Sword disabled!");
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		other.gameObject.GetComponent<EnemyHitbox>().OnAttackHit();
	}
	
}