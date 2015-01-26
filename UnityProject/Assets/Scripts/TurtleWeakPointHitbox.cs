using UnityEngine;
using System.Collections;

public class TurtleWeakPointHitbox : EnemyHitbox {

    public float DETACH_RADIUS = 1.0f;
    public float SLOWED_SPEED = 0.25f;

    private float latchedTime = -1.0f;
    private bool latched = false;

    void Update()
    {
        if(latched)
        {
            GameObject player = GameObject.Find("Hero");
            if (player == null) return;
            if ((player.transform.position - transform.position).magnitude < DETACH_RADIUS)
                player.rigidbody2D.velocity *= (SLOWED_SPEED / player.rigidbody2D.velocity.magnitude);
            latched = false;
        }
        if (Time.time - latchedTime > 5.0f)
            latched = false;

    }

    override public void OnAttackHit()
    {
        if (!transform.parent.GetComponent<TurtleEnemyScript>().isInvulnerable())
        {
            transform.parent.GetComponent<TurtleEnemyScript>().hit(1);
        }
    }

    override public void OnChargedAttackhit()
    {
        if (!transform.parent.GetComponent<TurtleEnemyScript>().isInvulnerable())
        {
            transform.parent.GetComponent<TurtleEnemyScript>().hit(2);
        }
    }

    override public void OnGrappleHit()
    {
        latched = true;
        latchedTime = Time.time;
    }

    override public void OnPull()
    {
    }

    override public void OnIncoming()
    {
    }
}
