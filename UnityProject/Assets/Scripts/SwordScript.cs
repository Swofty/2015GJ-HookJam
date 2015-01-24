using UnityEngine;
using System.Collections;

public class SwordScript : MonoBehaviour {

	public float ANGULAR_SPEED = 0.1f;
	public float SWORD_SIZE = 0.2f;
	
	private Vector3 initPos;
	private GameObject player;
	private float rotateTo;

	private float timeActive = 0f;
	
	void Awake()
	{
		player = transform.parent.gameObject;
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
	
	void Update()
	{
		timeActive += Time.deltaTime;
		transform.Rotate(-Vector3.forward * Time.deltaTime * 360);

		// When to stop
		if(timeActive > 0.25f)
		{
			DisableSword();
			timeActive = 0f;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		other.gameObject.GetComponent<EnemyHitbox>().OnAttackHit();
	}
	
}