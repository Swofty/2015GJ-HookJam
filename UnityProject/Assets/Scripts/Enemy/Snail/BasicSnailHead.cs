using UnityEngine;
using System.Collections;

namespace Snail
{

    public class BasicSnailHead : HittableSubpart<BasicSnailCore>
    {

        override public void OnAttackHit(GameObject source, float damageHint)
        {
            if (!core.Invulnerable)
            {
                core.TakeDamage(1.0f);
            }
        }

        override public void OnChargedAttackHit(GameObject source, float damageHint)
        {
            if (!core.Invulnerable)
            {
                core.TakeDamage(1.5f);
            }
        }

    }
}