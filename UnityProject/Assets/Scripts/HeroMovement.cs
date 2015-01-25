using UnityEngine;
using System.Collections;

public class HeroMovement : MonoBehaviour
{
    public enum State { FREE, STUNNED, GRAPPLE, SWORD, DASH };

    //public float HANG_TIME = 0.5f;
    public float MOVE_STR = 20.0f;
    public float WEAK_MOVE_STR = 2.0f;
    public float GRAVITY = 10.0f;
    public float GROUND_MIU = 1.0f;
    public float TOP_SPEED = 1.0f;
    public float MIN_FRICTION_SPEED = 0.2f;

    public bool grounded = true;
    public State state = State.FREE;
    public Constants.Dir direction;

    private Animator anim;
    private bool allowKeyMovement = true;
    private bool allowActions = true;
    private GameObject hook;
    private GameObject sword;
    private float startHang = -1.0f;
    private GameObject staminaBar;
    private GameObject bodyCollider;

    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        hook = transform.FindChild("Hook").gameObject;
        sword = transform.FindChild("Sword").gameObject;
        staminaBar = GameObject.Find("Stamina Bar");
        bodyCollider = transform.FindChild("BodyCollider").gameObject;
    }


    void Update()
    {
        // Set internal direction
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (h > 0.0f && v > 0.0f) direction = Constants.Dir.NE;
        else if (h > 0.0f && v < 0.0f) direction = Constants.Dir.SE;
        else if (h < 0.0f && v < 0.0f) direction = Constants.Dir.SW;
        else if (h < 0.0f && v > 0.0f) direction = Constants.Dir.NW;
        else if (v > 0.0f) direction = Constants.Dir.N;
        else if (h > 0.0f) direction = Constants.Dir.E;
        else if (v < 0.0f) direction = Constants.Dir.S;
        else if (h < 0.0f) direction = Constants.Dir.W;

        switch (direction)
        {
            case Constants.Dir.N: anim.SetFloat("Horizontal", 0.0f); anim.SetFloat("Vertical", 1.0f); break;
            case Constants.Dir.E: anim.SetFloat("Horizontal", 1.0f); anim.SetFloat("Vertical", 0.0f); break;
            case Constants.Dir.S: anim.SetFloat("Horizontal", 0.0f); anim.SetFloat("Vertical", -1.0f); break;
            case Constants.Dir.W: anim.SetFloat("Horizontal", -1.0f); anim.SetFloat("Vertical", 0.0f); break;
            case Constants.Dir.NE: anim.SetFloat("Horizontal", 1.0f); anim.SetFloat("Vertical", 0.0f); break;
            case Constants.Dir.SE: anim.SetFloat("Horizontal", 1.0f); anim.SetFloat("Vertical", 0.0f); break;
            case Constants.Dir.SW: anim.SetFloat("Horizontal", -1.0f); anim.SetFloat("Vertical", 0.0f); break;
            case Constants.Dir.NW: anim.SetFloat("Horizontal", -1.0f); anim.SetFloat("Vertical", 0.0f); break;
        }


        switch (state)
        {
            case State.FREE:
                if (Mathf.Abs(h) > 0.9f || Mathf.Abs(v) > 0.9f)
                    anim.SetInteger("Move", 1);
                else
                    anim.SetInteger("Move", 0);
                if (Input.GetKeyDown(Constants.HookKey))
                {
                    if (staminaBar.GetComponent<StaminaBar>().DoAttack(Constants.Attack.HOOK))
                    {
                        hook.GetComponent<HookScript>().ShootHook(direction, false);
                    }
                }
                else if (Input.GetKeyDown(Constants.DashKey))
                {
                    state = State.DASH;
                    ForceGround();
                }
                else if (Input.GetKeyDown(Constants.SwordKey))
                {
                    if (staminaBar.GetComponent<StaminaBar>().DoAttack(Constants.Attack.SWORD))
                    {
                        state = State.SWORD;
                        sword.GetComponent<SwordScript>().ActivateSword(direction);
                    }
                }
                break;

            case State.DASH:
                if (gameObject.GetComponent<DashScript>().finished())
                {
                    state = State.FREE;
                    grounded = true;
                }
                else
                {
                    anim.SetInteger("Move", 2);
                    grounded = false;
                }
                break;

            case State.SWORD:
                if (Input.GetKeyDown(Constants.DashKey))
                {
                    state = State.DASH;
                    ForceGround();
                    break;
                }
                if (sword.GetComponent<SwordScript>().isFinished())
                    state = State.FREE;
                break;
        }

    }

    void FixedUpdate()
    {
        Vector2 currVel = rigidbody2D.velocity;
        float currSpeed = currVel.magnitude;
        Vector2 currVelNorm = currVel / currSpeed;

        Vector2 totalForces = new Vector2();

        if (currSpeed > MIN_FRICTION_SPEED)
        {
            Vector2 frictionForce = new Vector2();
            if (grounded) frictionForce = -GROUND_MIU * (GRAVITY * rigidbody2D.mass) * currVelNorm;
            totalForces += frictionForce;
        }
        else
        {
            rigidbody2D.velocity = new Vector2();
        }

        if (state == State.DASH)
        {
            gameObject.GetComponent<DashScript>().Dash(direction);
        }
        else if (state == State.SWORD)
        {

        }
        else if (state == State.FREE)
        {
            float moveStrength = MOVE_STR;

            if (currSpeed > TOP_SPEED || !grounded) moveStrength = WEAK_MOVE_STR;
            else if (currSpeed < 0.5) moveStrength *= 3.0f;
            float horizontalDir = Input.GetAxis("Horizontal");
            float verticalDir = Input.GetAxis("Vertical");
            Vector2 moveForce = new Vector2(moveStrength * horizontalDir, moveStrength * verticalDir);
            Debug.Log("Move force" + moveForce + "CurrSPeed: " + currSpeed + "Topspeed:" + TOP_SPEED);
            totalForces += moveForce;

            if (currSpeed > TOP_SPEED)
            {
                if (Vector2.Dot(totalForces, currVelNorm) > 0.0)
                {
                    totalForces -= Vector2.Dot(totalForces, currVelNorm) * currVelNorm;
                    if (Mathf.Abs(Vector2.Dot(totalForces, currVelNorm)) < 0.01)
                        Debug.Log("Total force is not orthogonal");
                }
            }
        }

        rigidbody2D.AddForce(totalForces);
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Hole")
        {
            Debug.Log("Player nside a hole...");
            if (grounded)
            {
                if (startHang < 0.0f) startHang = Time.time;

                if (Time.time - startHang >= rigidbody2D.velocity.magnitude)
                    TakeDamage(GetCurrHealth());
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Hole")
        {
            Debug.Log("Leave hole");
            startHang = -1.0f;
        }
    }

    public void ApplyPull(Vector3 sourcePos, float pullStrength)
    {
        Debug.Log("Player getting pulled by " + sourcePos + ". Str: " + pullStrength);
        rigidbody2D.AddForce(pullStrength * (sourcePos - transform.position).normalized);
    }

    public void SetGrounded(bool b)
    {
        grounded = b;
    }

    public void ForceGround()
    {
        grounded = true;
        hook.GetComponent<HookScript>().DisableHook();
    }

    public float GetCurrHealth()
    {
        return 1.0f;
    }

    public void TakeDamage(float amount)
    {
        if (state == State.DASH) return;
        Debug.Log("Player has taken " + amount + "damage");
        Invoke("Reset", 2.0f);
        gameObject.SetActive(false);
    }

    private void Reset()
    {
        gameObject.SetActive(true);
        transform.position = Vector3.zero;
        direction = Constants.Dir.N;
        grounded = true;
        state = State.FREE;
    }
}
