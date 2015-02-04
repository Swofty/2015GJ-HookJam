using UnityEngine;
using System.Collections;

public class HeroMovement : MonoBehaviour
{
    public enum State { FREE, STUNNED, GRAPPLE, SWORD, DASH, CHARGE };

    //public float HANG_TIME = 0.5f;
    public float MOVE_STR = 20.0f;
    public float WEAK_MOVE_STR = 2.0f;
    public float GRAVITY = 10.0f;
    public float GROUND_MIU = 1.0f;
    public float TOP_SPEED = 1.0f;
    public float MIN_FRICTION_SPEED = 0.1f;
    public float FALL_DAMAGE = 4.0f;
    public float SAVEPOINT_TIMER = 15.0f;
    public float MAX_HEALTH = 48.0f;
    private float INVULN_TIME = 1.6f;

    public bool grounded = true;
    public State state = State.FREE;
    public Util.Dir direction;
    public Vector3 spawnPoint = Vector3.zero;
    public float health = 48.0f;

    private Animator anim;
    private bool allowKeyMovement = true;
    private bool allowActions = true;
    private GameObject hook;
    private GameObject sword;
    private float startHang = -1.0f;
    private GameObject staminaBar;
    private GameObject bodyCollider;
    private bool invulnerable;

    // Optimization
    private Transform _transform;
    public new Transform transform
    {
        get
        {
            if (_transform == null)
                _transform = base.transform;
            return _transform;
        }
    }

    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        hook = transform.FindChild("Hook").gameObject;
        sword = transform.FindChild("Sword").gameObject;
        staminaBar = GameObject.Find("Stamina Bar");
        bodyCollider = transform.FindChild("BodyCollider").gameObject;

        hook.GetComponent<HookScript>().WakeUp();
    }


    void Update()
    {
        if (grounded && Time.time % SAVEPOINT_TIMER < 0.1f)
            spawnPoint = transform.position;

        // Set internal direction
        float h = 0.0f;
        float v = 0.0f;

        if (state != State.CHARGE)
        {
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");

            if (h > 0.0f && v > 0.0f) direction = Util.Dir.NE;
            else if (h > 0.0f && v < 0.0f) direction = Util.Dir.SE;
            else if (h < 0.0f && v < 0.0f) direction = Util.Dir.SW;
            else if (h < 0.0f && v > 0.0f) direction = Util.Dir.NW;
            else if (v > 0.0f) direction = Util.Dir.N;
            else if (h > 0.0f) direction = Util.Dir.E;
            else if (v < 0.0f) direction = Util.Dir.S;
            else if (h < 0.0f) direction = Util.Dir.W;

            switch (direction)
            {
                case Util.Dir.N: anim.SetFloat("Horizontal", 0.0f); anim.SetFloat("Vertical", 1.0f); break;
                case Util.Dir.E: anim.SetFloat("Horizontal", 1.0f); anim.SetFloat("Vertical", 0.0f); break;
                case Util.Dir.S: anim.SetFloat("Horizontal", 0.0f); anim.SetFloat("Vertical", -1.0f); break;
                case Util.Dir.W: anim.SetFloat("Horizontal", -1.0f); anim.SetFloat("Vertical", 0.0f); break;
                case Util.Dir.NE: anim.SetFloat("Horizontal", 1.0f); anim.SetFloat("Vertical", 0.0f); break;
                case Util.Dir.SE: anim.SetFloat("Horizontal", 1.0f); anim.SetFloat("Vertical", 0.0f); break;
                case Util.Dir.SW: anim.SetFloat("Horizontal", -1.0f); anim.SetFloat("Vertical", 0.0f); break;
                case Util.Dir.NW: anim.SetFloat("Horizontal", -1.0f); anim.SetFloat("Vertical", 0.0f); break;
            }
        }

        switch (state)
        {
            case State.FREE:
                if (Mathf.Abs(h) > 0.9f || Mathf.Abs(v) > 0.9f)
                    anim.SetInteger("Move", 1);
                else
                    anim.SetInteger("Move", 0);
                if (Input.GetKeyDown(Util.HOOK_KEY))
                {
                    if (staminaBar.GetComponent<StaminaBar>().DoAttack(Util.Attack.HOOK))
                    {
                        anim.SetBool("Charge", true);
                        hook.GetComponent<HookScript>().StartHook();
                    }
                }
                else if (Input.GetKeyUp(Util.HOOK_KEY))
                {
                    anim.SetBool("Charge", false);
                    hook.GetComponent<HookScript>().ReleaseHook(direction);
                }
                else if (Input.GetKeyDown(Util.DASH_KEY))
                {
                    Debug.Log("Dash Key detected");
                    if (staminaBar.GetComponent<StaminaBar>().DoAttack(Util.Attack.DASH))
                    {
                        gameObject.GetComponent<DashScript>().StartDash(direction);
                        state = State.DASH;
                        ForceGround();
                        grounded = false;
                    }
                }
                else if (grounded && Input.GetKeyDown(Util.SWORD_KEY))
                {
                    if (staminaBar.GetComponent<StaminaBar>().PrepareAttack(Util.Attack.CHARGE))
                    {
                        state = State.SWORD;
                        sword.GetComponent<SwordScript>().ActivateSword();
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
                SwordScript sc = sword.GetComponent<SwordScript>();
                if (sc.inCharge)
                {
                    if (!sc.IsChargedAttack() && Input.GetKeyUp(Util.SWORD_KEY))
                    {
                        if (staminaBar.GetComponent<StaminaBar>().CancelAttack(Util.Attack.CHARGE))
                        {
                            anim.SetInteger("Move", 3);
                            sc.FinishRegular();
                        }
                    }
                    else if (sc.IsChargedAttack() && (Input.GetKeyUp(Util.SWORD_KEY) || sc.IsMaxTime()))
                    {
                        anim.SetInteger("Move", 3);
                        sc.FinishCharge();
                    }
                    else
                        anim.SetInteger("Move", 4);
                }
                if (Input.GetKeyDown(Util.DASH_KEY))
                {
                    state = State.DASH;
                    ForceGround();
                    sword.GetComponent<SwordScript>().DisableSword();
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
        else if (state == State.SWORD && !sword.GetComponent<SwordScript>().inCharge)
        {

        }
        else if (state == State.FREE || sword.GetComponent<SwordScript>().inCharge)
        {
            float moveStrength = MOVE_STR;

            if (currSpeed > TOP_SPEED || !grounded) moveStrength = WEAK_MOVE_STR;
            else if (currSpeed < 0.5) moveStrength *= 3.0f;
            float horizontalDir = Input.GetAxis("Horizontal");
            float verticalDir = Input.GetAxis("Vertical");
            Vector2 moveForce = new Vector2(moveStrength * horizontalDir, moveStrength * verticalDir);
            //Debug.Log("Move force" + moveForce + "CurrSPeed: " + currSpeed + "Topspeed:" + TOP_SPEED);
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
        anim.SetBool("Charge", false);
        grounded = true;
        hook.GetComponent<HookScript>().DisableHook();
    }

    public float GetCurrHealth()
    {
        return health;
    }

    public void TakeDamage(float amount)
    {
        if (state == State.DASH || invulnerable) return;
        health -= amount;
        
        if(health <= 0.0)
        {
            gameObject.SetActive(false);
            GameObject.Find("SpawnPoint").GetComponent<SpawnPointScript>().ReloadMap();
        }
        else
        {
            invulnerable = true;
            Invoke("DisableInvuln", INVULN_TIME);
        }
    }

    public void Fall()
    {
        TakeDamage(FALL_DAMAGE);
        ForceGround();
        Invoke("Respawn", 2.0f);
        this.gameObject.SetActive(false);
    }

    private void Respawn()
    {
        gameObject.SetActive(true);
        transform.position = spawnPoint;
        direction = Util.Dir.S;
        grounded = true;
        state = State.FREE;
    }


    public void DisableSword()
    {
        sword.GetComponent<SwordScript>().DisableSword();
    }

    public void DisableInvuln()
    {
        invulnerable = false;
    }

    public void ApplyImpulse(Vector2 deltaP)
    {
        rigidbody2D.velocity += deltaP;
    }

    public void SetSpawnPoint(Vector3 point)
    {
        spawnPoint = point;
    }

    public void RestoreStamina(float amount)
    {
        staminaBar.GetComponent<StaminaBar>().AddStamina(amount);
    }
}
