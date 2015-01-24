using UnityEngine;
using System.Collections;

public class ShellScript : EnemyHitbox
{

    override public void OnAttackHit()
    {
        Debug.Log("SlugShellScript");
    }
}
