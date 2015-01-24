using UnityEngine;
using System.Collections;

public class HeroMovement : MonoBehaviour {

    public bool grounded = true;
    public Dir direction;

    public enum Dir { N, E, S, W };

    private Animator anim;
    private bool allowKeyMovement = true;
    private bool allowActions = true;

    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        direction = Dir.N;
    }

    void Update()
    {
        if (Input.GetAxis("Vertical") > 0.0f) direction = Dir.N;
        else if (Input.GetAxis("Vertical") < 0.0f) direction = Dir.S;
        else if (Input.GetAxis("Horizontal") > 0.0f) direction = Dir.E;
        else if (Input.GetAxis("Horizontal") < 0.0f) direction = Dir.W;
        switch(direction)
        {
            case Dir.N: anim.SetFloat("Horizontal", 0.0f); anim.SetFloat("Vertical", 1.0f); break;
            case Dir.E: anim.SetFloat("Horizontal", 1.0f); anim.SetFloat("Vertical", 0.0f); break;
            case Dir.S: anim.SetFloat("Horizontal", 0.0f); anim.SetFloat("Vertical", -1.0f); break;
            case Dir.W: anim.SetFloat("Horizontal", -1.0f); anim.SetFloat("Vertical", 0.0f); break;
        }
    }

	void FixedUpdate()
    {
        Vector2 currVel = rigidbody2D.velocity;
        float currSpeed = currVel.magnitude;
        Vector2 currVelNorm = currVel / currSpeed;

        float moveStrength = 20.0f;
        float weakMoveStrength = 2.0f;
        float gravity = 10.0f;
        float groundMiu = 1.0f;
        float topSpeed = 1.0f;

        float MIN_FRICTION_SPEED = 0.2f;

        Vector2 totalForces = new Vector2();

        if (currSpeed > MIN_FRICTION_SPEED)
        {
            Vector2 frictionForce = new Vector2();
            if (grounded) frictionForce = -groundMiu * (gravity * rigidbody2D.mass) * currVelNorm;
            totalForces += frictionForce;
        }
        else
        {
            rigidbody2D.velocity = new Vector2();
        }

        if (allowKeyMovement)
        {
            if (currSpeed > topSpeed) moveStrength = weakMoveStrength;
            else if (currSpeed < 0.5) moveStrength *= 3.0f;
            float horizontalDir = Input.GetAxis("Horizontal");
            float verticalDir = Input.GetAxis("Vertical");
            Vector2 moveForce = new Vector2(moveStrength * horizontalDir, moveStrength * verticalDir);
            totalForces += moveForce;

            if(currSpeed > topSpeed)
            {
                if(Vector2.Dot(totalForces, currVelNorm) > 0.0)
                {
                    totalForces -= Vector2.Dot(totalForces, currVelNorm) * currVelNorm;
                    if (Mathf.Abs(Vector2.Dot(totalForces, currVelNorm)) < 0.01)
                        Debug.Log("Total force is not orthogonal");
                }
            }
        }

        rigidbody2D.AddForce(totalForces);
    }
}
