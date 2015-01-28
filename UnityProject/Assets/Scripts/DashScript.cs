using UnityEngine;
using System.Collections;

public class DashScript : MonoBehaviour {

    public float DASH_TIME_LENGTH= 0.1f;
    public float DASH_SPEED = 10.0f;
    public float STOP_SPEED = 3.0f;

    private float timeStart = -1.0f;

    public void Dash(Globals.Dir dir)
    {
        if (timeStart < 0.0) timeStart = Time.time;
        Vector2 new_velocity = Globals.getVectorFromDirection(dir);
        rigidbody2D.velocity = new Vector2(new_velocity.x * DASH_SPEED, new_velocity.y * DASH_SPEED);
    }

    public bool finished()
    {
        if (Time.time - timeStart > DASH_TIME_LENGTH)
        {
            rigidbody2D.velocity = STOP_SPEED * rigidbody2D.velocity.normalized;
            timeStart = -1.0f;
            return true;
        }
        else
        {
            return false;
        }
    }
    // DisableControls
    // EnableControls
}
