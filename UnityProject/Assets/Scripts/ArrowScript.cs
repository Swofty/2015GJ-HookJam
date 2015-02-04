using UnityEngine;
using System.Collections;

public class ArrowScript : MonoBehaviour {

    public float ARROW_FORCE = 4.0f;
    private float speed = 4.0f;
    private float timeout = 3.0f;

    void Awake()
    {
        timeout = 3.0f;
    }

    void Update()
    {
        timeout -= Time.deltaTime;
        if (timeout <= 0)
            Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            GameManager.Player.TakeDamage(6);
            Vector3 impulse = ARROW_FORCE * (GameManager.Player.transform.position - transform.position).normalized;
            GameManager.Player.ApplyImpulse(impulse);
            Destroy(this.gameObject);
        }
        else if(Util.IsWallTag(col.gameObject.tag))
            Destroy(this.gameObject);
    }

}
