using UnityEngine;
using System.Collections;

public class StateEntity : MonoBehaviour{

    public delegate void Trigger2DEvent(Collider2D col);
    public delegate void Collision2DEvent(Collision2D coll);

    public event Trigger2DEvent TriggerEnter2D;
    public event Trigger2DEvent TriggerStay2D;
    public event Trigger2DEvent TriggerExit2D;

    public event Collision2DEvent CollisionEnter2D;
    public event Collision2DEvent CollisionStay2D;
    public event Collision2DEvent CollisionExit2D;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (TriggerEnter2D != null)
            TriggerEnter2D(col);
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (TriggerStay2D != null)
            TriggerStay2D(col);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (TriggerExit2D != null)
            TriggerExit2D(col);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (CollisionEnter2D != null)
            CollisionEnter2D(coll);
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (CollisionExit2D != null)
            CollisionExit2D(coll);
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        if (CollisionStay2D != null)
            CollisionStay2D(coll);
    }

}
