using UnityEngine;
using System.Collections;

namespace Enemy.Snail
{
    public class BasicSnailBody : HittablePart
    {
        public float KNOCKBACK_FORCE = 3.0f;

        private BasicSnailCore core;

        void Awake()
        {
            core = GetComponentInParent<BasicSnailCore>();
        }

        override public void OnAttackHit(GameObject source, float damageHint)
        {
            core.EnterAwareness(source);
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
            core.EnterAwareness(source);
            if (core.Armored)
            {
                GameManager.Player.ApplyImpulse(2 * KNOCKBACK_FORCE *
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