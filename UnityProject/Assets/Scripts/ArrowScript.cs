using UnityEngine;
using System.Collections;

public class ArrowScript : MonoBehaviour {

    public float ARROW_FORCE = 4.0f;
    private float speed = 4.0f;
    private float timeout = 3.0f;

    void Awake()
    {
        timeout = 3.0f;
    }

    void Update()
    {
        timeout -= Time.deltaTime;
        if (timeout <= 0)
            Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameObject player = Globals.GetPlayer();
            HeroMovement s = player.GetComponent<HeroMovement>();
            s.TakeDamage(6);
            s.ApplyKnockback(ARROW_FORCE * (player.transform.position - transform.position).normalized);
            Destroy(this.gameObject);
        }
        else if(Globals.isWallTag(col.gameObject.tag))
            Destroy(this.gameObject);
    }

}
