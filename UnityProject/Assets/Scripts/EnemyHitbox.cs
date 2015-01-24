using UnityEngine;
using System.Collections;

public abstract class EnemyHitbox : MonoBehaviour {

	abstract public void OnAttackHit();
    abstract public void OnChargedAttackhit();

    abstract public void OnGrappleHit();
    abstract public void OnPull();
    abstract public void OnIncoming();
}
