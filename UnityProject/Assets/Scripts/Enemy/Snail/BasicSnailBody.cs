using UnityEngine;
using System.Collections;

namespace Snail {
public class BasicSnailBody : HittableSubpart<BasicSnailCore>
{
    override public void OnAttackHit(GameObject source, float damageHint)
    {
        if (!transform.parent.GetComponent<BasicSnailCore>().isInvulnerable())
        {
            Vector3 direction_vector = GameObject.Find("Hero").transform.position - transform.parent.position;
            Util.Dir direction = Util.GetDirectionFromVector(direction_vector);

            transform.parent.GetComponent<BasicSnailCore>().setDirection(direction);
            
            if(transform.parent.GetComponent<BasicSnailCore>().Armored)
                transform.parent.GetComponent<BasicSnailCore>().TakeDamage(1.0f);
            else
                transform.parent.GetComponent<BasicSnailCore>().TakeDamage(3.0f);
        }
    }

    override public void OnChargedAttackHit(GameObject source, float damageHint)
    {
        if (!transform.parent.GetComponent<BasicSnailCore>().isInvulnerable())
        {
            Vector3 direction_vector = GameObject.Find("Hero").transform.position - transform.parent.position;
            Util.Dir direction = Util.GetDirectionFromVector(direction_vector);

            transform.parent.GetComponent<BasicSnailCore>().setDirection(direction);

            if (transform.parent.GetComponent<BasicSnailCore>().Armored)
                transform.parent.GetComponent<BasicSnailCore>().TakeDamage(2.0f);
            else
                transform.parent.GetComponent<BasicSnailCore>().TakeDamage(6.0f);
        }
    }
}

}