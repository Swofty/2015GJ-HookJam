using UnityEngine;

public interface EnemyReactions
{
    // Reactions to player caused effects
    void OnAttackHit(GameObject source, float damageHint);
    void OnChargedAttackHit(GameObject source, float damageHint);
    void OnGrappleHit(GameObject source);
    void OnPull(GameObject source);
}