using UnityEngine;
using System.Collections;

public class HookScript : MonoBehaviour
{

    public float SPEED = 10.0f;
    public float MAX_HOOK_DISTANCE = 5.0f;
    public float HOOK_PULL_FORCE = 50.0f;
    public float MIN_CHARGE_TIME = 0.6f;
    public float PULL_DELAY = 0.5f;
    public float PULL_SPEED = 1.0f;
    public float PULL_END = 0.2f;
    public float HOOK_LAND_REWARD = 0.1f;

    public bool fired = false;
    public bool latched = false;
    public bool charged = false;

    private float startTime = -1.0f;

    private GameObject player;
    private GameObject hookHead;
    private GameObject hookHeadCollider;
    private GameObject hookHeadDetach;
    private GameObject rope;
    private Vector3 latchedPos;

    void Awake()
    {
        fired = false;
        latched = false;
        charged = false;
        rigidbody2D.velocity = new Vector2();


        player = transform.parent.gameObject;
        Debug.Log("aw transform: " + transform);
        Debug.Log("aw transform.parent: " + transform.parent);
        hookHead = transform.FindChild("HookHead").gameObject;
        hookHeadCollider = hookHead.transform.FindChild("Collider").gameObject;
        hookHeadDetach = hookHead.transform.FindChild("DetachArea").gameObject;
        rope = transform.FindChild("Rope").gameObject;
    }

    public void WakeUp()
    {
        Awake();
    }


    public void StartHook()
    {
        startTime = Time.time;
    }

    public void ReleaseHook(Constants.Dir dir)
    {
        if (startTime < 0.0f) return;

        if (fired) return;
        fired = true;

        charged = Time.time - startTime >= MIN_CHARGE_TIME;

        // Turn hook on
        gameObject.SetActive(true);

        // Enable hook head hitbox
        hookHeadCollider.SetActive(true);

        // Not hitting anything yet
        latched = false;
        hookHeadDetach.SetActive(false);

        // Place hook in correct relative position to start
        transform.localPosition = GetInitPosition(dir);
        transform.rotation = GetInitRotation(dir);

        // Give hook a velocity
        rigidbody2D.velocity = SPEED * Constants.getVectorFromDirection(dir);

        Debug.Log("Hook fired!");
    }

    public void DisableHook()
    {
        fired = false;

        rigidbody2D.velocity = Vector2.zero;
        latched = false;
        charged = false;
        startTime = -1.0f;

        Debug.Log(player);
        player.GetComponent<HeroMovement>().SetGrounded(true);
        gameObject.SetActive(false);

        hookHeadCollider.SetActive(false);
        hookHeadDetach.SetActive(false);

        // Reset everything
        rope.transform.localScale = Vector3.zero;
        gameObject.transform.localPosition = Vector3.zero;


        Debug.Log("Hook disabled!");
    }

    Vector3 GetInitPosition(Constants.Dir dir)
    {
        // TODO
        Vector3 pos = (0.34f * Constants.getVectorFromDirection(dir));
        return pos;
    }

    Quaternion GetInitRotation(Constants.Dir dir)
    {
        // TODO
        Quaternion rot;
        switch (dir)
        {
            case Constants.Dir.N: rot = Quaternion.Euler(0.0f, 0.0f, 90.0f); break;
            case Constants.Dir.E: rot = Quaternion.Euler(0.0f, 0.0f, 0.0f); break;
            case Constants.Dir.S: rot = Quaternion.Euler(0.0f, 0.0f, -90.0f); break;
            case Constants.Dir.W: rot = Quaternion.Euler(0.0f, 0.0f, 180.0f); break;
            case Constants.Dir.NE: rot = Quaternion.Euler(0.0f, 0.0f, 45.0f); break;
            case Constants.Dir.SE: rot = Quaternion.Euler(0.0f, 0.0f, -45.0f); break;
            case Constants.Dir.SW: rot = Quaternion.Euler(0.0f, 0.0f, -135.0f); break;
            case Constants.Dir.NW: rot = Quaternion.Euler(0.0f, 0.0f, 135.0f); break;
            default: rot = new Quaternion(); break;
        }
        return rot;
    }

    void Update()
    {
        Vector3 playerToRopeVec = (transform.position - player.transform.position);
        float playerToRopeDist = playerToRopeVec.magnitude;

        // Rope tracking
        rope.transform.localScale = new Vector3(playerToRopeDist, 1.0f, 1.0f);
        Vector3 q = Quaternion.AngleAxis(90, Vector3.forward) * playerToRopeVec;
        rope.transform.rotation = Quaternion.LookRotation(Vector3.forward, q);

        // When to stop
        if (!latched && playerToRopeDist > MAX_HOOK_DISTANCE)
        {
            DisableHook();
        }


        /*
        // Bug with getting stuck against wall
        if(latched && player.rigidbody2D.velocity.sqrMagnitude == 0.0f)
        {
            DisableHook();
        }
         */
    }

    void FixedUpdate()
    {
        if (latched)
        {
            if (!charged)
            {
                // Ensure no movement
                rigidbody2D.velocity = Vector2.zero;
                transform.position = latchedPos;

                player.GetComponent<HeroMovement>().ApplyPull(transform.position, HOOK_PULL_FORCE);
            }
        }
    }

    void Latch()
    {
        // Latch and stop
        latched = true;
        latchedPos = transform.position;
        rigidbody2D.velocity = Vector2.zero;

        player.GetComponent<HeroMovement>().RestoreStamina(HOOK_LAND_REWARD);

        // Put player in the air
        player.GetComponent<HeroMovement>().SetGrounded(false);

        // Disable hook hitbox. Enable detachment area.
        hookHeadCollider.SetActive(false);
        hookHeadDetach.SetActive(true);
    }

    void Tug()
    {
        rigidbody2D.velocity = PULL_SPEED * (player.transform.position - transform.position).normalized;
        Invoke("DisableHook", PULL_END);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            var sc = col.gameObject.GetComponent<EnemyHitbox>();
            sc.OnGrappleHit();
            if (charged)
            {
                sc.OnPull();
                hookHeadCollider.SetActive(false);
                rigidbody2D.velocity = Vector2.zero;
                Invoke("Tug", PULL_DELAY);
            }
            else
            {
                sc.OnIncoming();
                Latch();
            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (hookHeadCollider.activeSelf && !latched)
        {
            if (hookHeadDetach.activeSelf)
            {
                Debug.Log("WARNING: detachArea is active when hook is not latched!");
            }

            Debug.Log("Hook latched to " + col.gameObject.tag);

            if (col.gameObject.tag == "GWall")
            {
                // Regardless if it was charged, it's not anymore
                charged = false;

                Latch();
            }
        }

        if (latched)
        {
            if (!hookHeadDetach.activeSelf) Debug.Log("WARNING: deatchArea is not active when latched!");
            if (col.gameObject.tag == "Player")
            {
                Debug.Log("Player is in detach radius");
                DisableHook();
            }
        }
    }
}
