using UnityEngine;
using System.Collections;

public class ShellHitboxScript : EnemyHitbox
{
    override public void OnAttackHit()
    {
        if (!transform.parent.GetComponent<SnailEnemyScript>().isInvulnerable())
        {
            Vector3 direction_vector = transform.parent.position - transform.position;
            Constants.Dir direction = Constants.getDirectionFromVector(direction_vector);

            transform.parent.GetComponent<SnailEnemyScript>().setDirection(direction);

            transform.parent.GetComponent<SnailEnemyScript>().hit();
        }
    }

    override public void OnChargedAttackhit()
    {
        if (!transform.parent.GetComponent<SnailEnemyScript>().isInvulnerable())
        {
            //Want to have it so that if the enemy dies, we shake the camera

            Vector3 direction_vector = transform.parent.position - transform.position;
            Constants.Dir direction = Constants.getDirectionFromVector(direction_vector);

            transform.parent.GetComponent<SnailEnemyScript>().setDirection(direction);
        }
    }

    override public void OnGrappleHit()
    {
        if (!transform.parent.GetComponent<SnailEnemyScript>().isInvulnerable())
        {
            Vector3 direction_vector = transform.parent.position - transform.position;
            Constants.Dir direction = Constants.getDirectionFromVector(direction_vector);

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

            Vector3 direction_vector = transform.parent.position - transform.position;
            Constants.Dir direction = Constants.getDirectionFromVector(direction_vector);

            transform.parent.GetComponent<SnailEnemyScript>().setDirection(direction);
        }
    }

    override public void OnIncoming()
    {
        if (!transform.parent.GetComponent<SnailEnemyScript>().isInvulnerable())
        {
            Vector3 direction_vector = transform.parent.position - transform.position;
            Constants.Dir direction = Constants.getDirectionFromVector(direction_vector);

            transform.parent.GetComponent<SnailEnemyScript>().setDirection(direction);
        }
    }
}

