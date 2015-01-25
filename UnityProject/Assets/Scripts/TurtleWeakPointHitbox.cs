using UnityEngine;
using System.Collections;

public class TurtleWeakPoint : EnemyHitbox {
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
    }

    override public void OnPull()
    {
    }

    override public void OnIncoming()
    {
    }
}
