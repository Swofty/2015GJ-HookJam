using UnityEngine;
using System.Collections;

public class HeroFeetScript : MonoBehaviour {

    public float FALL_DAMAGE = 3.0f;

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

                if (Time.time - startHang >= player.GetComponent<Rigidbody2D>().velocity.magnitude)
                    playerScript.TakeDamage(FALL_DAMAGE);
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
