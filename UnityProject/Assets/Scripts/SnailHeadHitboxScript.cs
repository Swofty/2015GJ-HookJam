using UnityEngine;
using System.Collections;

public class SnailHeadHitboxScript : EnemyHitbox 
{
    override public void OnAttackHit()
    {
        if (!transform.parent.GetComponent<SnailEnemyScript>().isInvulnerable())
        {
            Vector3 direction_vector = GameObject.Find("Hero").transform.position - transform.parent.position;
            Globals.Dir direction = Globals.getDirectionFromVector(direction_vector);

            transform.parent.GetComponent<SnailEnemyScript>().setDirection(direction);

            transform.parent.GetComponent<SnailEnemyScript>().hit(3);
        }
    }

    override public void OnChargedAttackhit()
    {
        if (!transform.parent.GetComponent<SnailEnemyScript>().isInvulnerable())
        {
            Vector3 direction_vector = GameObject.Find("Hero").transform.position - transform.parent.position;
            Globals.Dir direction = Globals.getDirectionFromVector(direction_vector);

            transform.parent.GetComponent<SnailEnemyScript>().setDirection(direction);

            transform.parent.GetComponent<SnailEnemyScript>().hit(6);
        }
    }

    override public void OnGrappleHit()
    {
        if (!transform.parent.GetComponent<SnailEnemyScript>().isInvulnerable())
        {
            Vector3 direction_vector = GameObject.Find("Hero").transform.position - transform.parent.position;
            Globals.Dir direction = Globals.getDirectionFromVector(direction_vector);

            transform.parent.GetComponent<SnailEnemyScript>().setDirection(direction);
        }
    }

    override public void OnPull()
    {
        if (!transform.parent.GetComponent<SnailEnemyScript>().isInvulnerable())
        {
            if (transform.parent.GetComponent<SnailEnemyScript>().isArmored())
            {
                transform.parent.GetComponent<SnailEnemyScript>().setArmored(false);
                //We want to play an armor poofing animation at this point (noooootteee)
                ((Animator)transform.parent.gameObject.GetComponent<Animator>()).SetTrigger("BreakArmor");
            }
        }
    }

    override public void OnIncoming()
    {
        if (!transform.parent.GetComponent<SnailEnemyScript>().isInvulnerable())
        {
        }
    }
}
