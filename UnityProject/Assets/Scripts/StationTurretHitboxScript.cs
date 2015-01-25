using UnityEngine;
using System.Collections;

public class StationTurretHitboxScript : EnemyHitbox {

    override public void OnAttackHit()
    {
        if (!transform.parent.GetComponent<StationTurretEnemyScript>().isInvulnerable())
        {
            transform.parent.GetComponent<StationTurretEnemyScript>().hit(1);
        }
    }

    override public void OnChargedAttackhit()
    {
        if (!transform.parent.GetComponent<StationTurretEnemyScript>().isInvulnerable())
        {
            transform.parent.GetComponent<StationTurretEnemyScript>().hit(4);
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
