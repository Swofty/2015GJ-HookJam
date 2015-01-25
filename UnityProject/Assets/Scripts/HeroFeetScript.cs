using UnityEngine;
using System.Collections;

public class HeroFeetScript : MonoBehaviour {

    public float FALL_DAMAGE = 5.0f;
    public float MIN_FLOAT_TIME = 0.3f;

    private GameObject player;
    private HeroMovement playerScript;
    float startHang = -1.0f;

	void Awake()
    {
        player = transform.parent.gameObject;
        playerScript = player.GetComponent<HeroMovement>();
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Hole")
        {
            Debug.Log("Player nside a hole...");
            if (playerScript.grounded)
            {
                if (startHang < 0.0f) startHang = Time.time;

                float airTime = player.GetComponent<Rigidbody2D>().velocity.magnitude;

                if (Time.time - startHang >= airTime)
                    playerScript.Fall();
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Hole")
        {
            Debug.Log("Leave hole");
            startHang = -1.0f;
        }
    }
}
