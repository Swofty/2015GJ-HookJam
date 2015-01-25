using UnityEngine;
using System.Collections;

public class HookScript : MonoBehaviour {

    public float SPEED = 10.0f;
    public float MAX_HOOK_DISTANCE = 5.0f;
    public float HOOK_PULL_FORCE = 50.0f;
    public bool fired = false;
    public bool latched = false;
    public bool charged = false;

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
        hookHead = transform.FindChild("HookHead").gameObject;
        hookHeadCollider = hookHead.transform.FindChild("Collider").gameObject;
        hookHeadDetach = hookHead.transform.FindChild("DetachArea").gameObject;
        rope = transform.FindChild("Rope").gameObject;
    }

    public void ShootHook(Constants.Dir dir, bool isChargedShot)
    {
        if (fired) return;
        fired = true;

        // Turn hook on
        gameObject.SetActive(true);
        charged = isChargedShot;

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

        Debug.Log("Hook enabled!");
    }

    public void DisableHook()
    {
        fired = false;

        rigidbody2D.velocity = Vector2.zero;
        latched = false;
        charged = false;

        gameObject.SetActive(false);
        hookHeadCollider.SetActive(false);
        hookHeadDetach.SetActive(false);

        // Reset everything
        rope.transform.localScale = Vector3.zero;
        gameObject.transform.localPosition = Vector3.zero;

        player.GetComponent<HeroMovement>().SetGrounded(true);

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
        if(!latched && playerToRopeDist > MAX_HOOK_DISTANCE)
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

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            print(col.gameObject.name);
            print(col.gameObject.GetComponent<EnemyHitbox>());
            col.gameObject.GetComponent<EnemyHitbox>().OnAttackHit();
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

                // Latch and stop
                latched = true;
                latchedPos = transform.position;
                rigidbody2D.velocity = Vector2.zero;

                // Put player in the air
                player.GetComponent<HeroMovement>().SetGrounded(false);
                
                // Disable hook hitbox. Enable detachment area.
                hookHeadCollider.SetActive(false);
                hookHeadDetach.SetActive(true);
            }
        }

        if(latched)
        {
            if (!hookHeadDetach.activeSelf) Debug.Log("WARNING: deatchArea is not active when latched!");
            if(col.gameObject.tag == "Player")
            {
                Debug.Log("Player is in detach radius");
                DisableHook();
            }
        }
    }
}
