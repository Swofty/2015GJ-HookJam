using UnityEngine;
using System.Collections;

public class HeadScript : EnemyHitbox {

    override public void OnAttackHit()
    {
        Debug.Log("SlugHeadScript");
    }
}
