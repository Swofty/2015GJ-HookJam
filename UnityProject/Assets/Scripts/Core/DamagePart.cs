using UnityEngine;
using System.Collections;

public class DamagePart : MonoBehaviour {

    public float DAMAGE = 5.0f;
    public float KNOCKBACK = 1.0f;

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Trigger enter");
        if (col.CompareTag("Player"))
        {
            Debug.Log("Trigger enter is player");
            GameManager.Player.TakeDamage(12);
            GameManager.Player.ApplyImpulse(
                1.0f * (GameManager.Player.transform.position
                         - transform.position).normalized);
        }
    }
}
