using UnityEngine;
using System.Collections;

public class TurtleLimbHitbox: EnemyHitbox {

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            GameObject.Find("Hero").GetComponent<HeroMovement>().TakeDamage(16);
            GameObject.Find("Hero").GetComponent<HeroMovement>().ApplyKnockback(GameObject.Find("Hero").transform.position - transform.parent.position);
        }
    }

    override public void OnAttackHit()
    {
        if (!transform.parent.GetComponent<TurtleEnemyScript>().isInvulnerable())
        {
            transform.parent.GetComponent<TurtleEnemyScript>().stun_action(1);
        }
    }

    override public void OnChargedAttackhit()
    {
        if (!transform.parent.GetComponent<TurtleEnemyScript>().isInvulnerable())
        {
            transform.parent.GetComponent<TurtleEnemyScript>().stun_action(2);
        }
    }

    override public void OnGrappleHit()
    {
    }

    override public void OnPull()
    {
    }

    override public void OnIncoming()
    {
        if (!transform.parent.GetComponent<TurtleEnemyScript>().isInvulnerable())
        {
        }
    }
}
