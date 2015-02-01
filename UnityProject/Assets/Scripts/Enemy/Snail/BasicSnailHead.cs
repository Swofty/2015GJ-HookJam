using UnityEngine;
using System.Collections;

namespace Snail
{

    public class BasicSnailHead : HittableSubpart<BasicSnailCore>
    {

        public override void OnAttackHit(GameObject source, float damageHint)
        {
            if (!core.isInvulnerable())
            {
                Vector3 direction_vector = GameObject.Find("Hero").transform.position - transform.parent.position;
                Util.Dir direction = Util.GetDirectionFromVector(direction_vector);

                core.setDirection(direction);

                core.TakeDamage(3.0f);
            }
        }

        public override void OnChargedAttackHit(GameObject source, float damageHint)
        {
            if (!core.isInvulnerable())
            {
                Vector3 direction_vector = GameObject.Find("Hero").transform.position - transform.parent.position;
                Util.Dir direction = Util.GetDirectionFromVector(direction_vector);

                core.setDirection(direction);

                core.TakeDamage(6.0f);
            }
        }

    }
}