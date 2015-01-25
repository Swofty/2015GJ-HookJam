using UnityEngine;
using System.Collections;

public class DashScript : MonoBehaviour {

    public float DASH_TIME_LENGTH= 0.2f;
    public float DASH_SPEED;

    private float timeActive = 0f;

    public void Dash(Constants.Dir dir)
    {
        Vector2 new_velocity = Constants.getVectorFromDirection(dir);
        rigidbody2D.velocity = new Vector2(new_velocity.x * DASH_SPEED, new_velocity.y * DASH_SPEED);
        print("testing");

        timeActive += Time.deltaTime;
    }

    public bool finished()
    {
        if (timeActive > DASH_TIME_LENGTH)
            return true;
        else
            return false;
    }
    // DisableControls
    // EnableControls
}
