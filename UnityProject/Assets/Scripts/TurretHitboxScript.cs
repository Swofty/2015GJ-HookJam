using UnityEngine;
using System.Collections;

public class TurretHitboxScript : EnemyHitbox {

    override public void OnAttackHit()
    {
        if (!transform.parent.GetComponent<TurretEnemyScript>().isInvulnerable())
        {
            transform.parent.GetComponent<TurretEnemyScript>().hit(1);
        }
    }

    override public void OnChargedAttackhit()
    {
        if (!transform.parent.GetComponent<TurretEnemyScript>().isInvulnerable())
        {
            transform.parent.GetComponent<TurretEnemyScript>().hit(4);
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
