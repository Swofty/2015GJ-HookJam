using UnityEngine;
using System.Collections;

public class EnemyBase : MonoBehaviour {

    // Reactions to player caused effects
    public virtual void OnAttackHit(GameObject source, float damageHint) {

    }

    public virtual void OnChargedAttackHit(GameObject source, float damageHint)
    {

    }

    public virtual void OnGrappleHit(GameObject source)
    {

    }

    public virtual void OnPull(GameObject source)
    {

    }
}
