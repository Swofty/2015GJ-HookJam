using UnityEngine;
using System.Collections;

public class DashScript : MonoBehaviour {

    public float DASH_TIME_LENGTH= 0.1f;
    public float DASH_SPEED = 10.0f;
    public float STOP_SPEED = 3.0f;

    private float duration;
    private bool inDash;

    public void StartDash(Util.Dir dir)
    {
        duration = DASH_TIME_LENGTH;
        inDash = true;
    }

    public void Dash(Util.Dir dir)
    {
        duration -= Time.deltaTime;
        if (!inDash) Debug.Log("Dash() called without being inDash!");
        rigidbody2D.velocity = DASH_SPEED * Util.GetVectorFromDirection(dir);
    }

    public bool finished()
    {
        if (duration <= 0.0f)
        {
            rigidbody2D.velocity = STOP_SPEED * rigidbody2D.velocity.normalized;
            return true;
        }
        else
        {
            return false;
        }
    }
}
