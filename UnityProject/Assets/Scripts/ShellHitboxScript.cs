using UnityEngine;
using System.Collections;

public class ShellHitboxScript : EnemyHitbox
{
    override public void OnAttackHit()
    {
        if (!transform.parent.GetComponent<EnemyScript>().isInvulnerable())
        {
            Vector3 direction_vector = transform.parent.position - transform.position;
            Constants.Dir direction = Constants.getDirectionFromVector(direction_vector);

            transform.parent.GetComponent<EnemyScript>().setDirection(direction);

            transform.parent.GetComponent<EnemyScript>().hit();
        }
    }

    override public void OnChargedAttackhit()
    {
        if (!transform.parent.GetComponent<EnemyScript>().isInvulnerable())
        {
            //Want to have it so that if the enemy dies, we shake the camera

            Vector3 direction_vector = transform.parent.position - transform.position;
            Constants.Dir direction = Constants.getDirectionFromVector(direction_vector);

            transform.parent.GetComponent<EnemyScript>().setDirection(direction);
        }
    }

    override public void OnGrappleHit()
    {
        if (!transform.parent.GetComponent<EnemyScript>().isInvulnerable())
        {
            Vector3 direction_vector = transform.parent.position - transform.position;
            Constants.Dir direction = Constants.getDirectionFromVector(direction_vector);

            transform.parent.GetComponent<EnemyScript>().setDirection(direction);
        }
    }

    override public void OnPull()
    {
        if (!transform.parent.GetComponent<EnemyScript>().isInvulnerable())
        {
            if (!transform.parent.GetComponent<EnemyScript>().isArmored())
            {
                transform.parent.GetComponent<EnemyScript>().setArmored(false);
                //We want to play an armor poofing animation at this point (noooootteee)
            }

            Vector3 direction_vector = transform.parent.position - transform.position;
            Constants.Dir direction = Constants.getDirectionFromVector(direction_vector);

            transform.parent.GetComponent<EnemyScript>().setDirection(direction);
        }
    }

    override public void OnIncoming()
    {
        if (!transform.parent.GetComponent<EnemyScript>().isInvulnerable())
        {
            Vector3 direction_vector = transform.parent.position - transform.position;
            Constants.Dir direction = Constants.getDirectionFromVector(direction_vector);

            transform.parent.GetComponent<EnemyScript>().setDirection(direction);
        }
    }
}

