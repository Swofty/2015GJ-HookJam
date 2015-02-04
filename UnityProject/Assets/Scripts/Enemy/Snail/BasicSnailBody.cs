using UnityEngine;
using System.Collections;

namespace Snail
{
    public class BasicSnailBody : HittableSubpart<BasicSnailCore>
    {
        public float KNOCKBACK_FORCE = 10.0f;

        override public void OnAttackHit(GameObject source, float damageHint)
        {
            if (core.Armored)
            {
                GameManager.Player.ApplyImpulse(KNOCKBACK_FORCE *
                    (source.transform.position - core.transform.position).normalized);
                return;
            }
            if (!core.Invulnerable)
            {
                if (!core.Armored) core.TakeDamage(1.0f);
            }
        }

        override public void OnChargedAttackHit(GameObject source, float damageHint)
        {
            if (core.Armored)
            {
                GameManager.Player.ApplyImpulse(KNOCKBACK_FORCE *
                    (source.transform.position - core.transform.position).normalized);
                return;
            }
            if (!core.Invulnerable)
            {
                if (!core.Armored) core.TakeDamage(1.5f);
            }
        }
    }

}