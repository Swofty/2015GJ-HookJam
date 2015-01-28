using UnityEngine;
using System.Collections;

public class StationTurretEnemyScript : MonoBehaviour, EnemyReactions {

    public float COOLDOWN = 1.0f;
    public int SHOTS_PER_BURST = 1;
    public float TIME_PER_SHOT = 0.5f;
    public float AGGRO_RANGE = 5.0f;

    public float health = 4.0f;
    public Globals.Dir direction;
    public Rigidbody2D arrow;

    private bool aggro = false;
    private float timeToNextBurst = 0.0f;
    private float timeToNextShot = 0.0f;
    private int shotsFired = 0;
    private float invulnerable = 0.0f;

    private GameObject player;
    private Animator anim;
    private AudioSource[] sfx;

    void Awake()
    {
        player = Globals.GetPlayer();
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (invulnerable > 0.0)
        {
            invulnerable -= Time.deltaTime;
        }
        else
        {
            invulnerable = 0.0f;
            anim.SetBool("Invulnerable", false);
        }

        aggro = (transform.position - player.transform.position).magnitude < AGGRO_RANGE;

        switch (direction)
        {
            case Globals.Dir.N: anim.SetFloat("Horizontal", 0.0f); anim.SetFloat("Vertical", 1.0f);  break;
            case Globals.Dir.E: anim.SetFloat("Horizontal", 1.0f); anim.SetFloat("Vertical", 0.0f);  break;
            case Globals.Dir.S: anim.SetFloat("Horizontal", 0.0f); anim.SetFloat("Vertical", -1.0f); break;
            case Globals.Dir.W: anim.SetFloat("Horizontal", -1.0f); anim.SetFloat("Vertical", 0.0f); break;
        }

        if (aggro)
        {
            timeToNextBurst -= Time.deltaTime;
            timeToNextShot -= Time.deltaTime;
            if (timeToNextBurst <= 0.0f)
            {
                shotsFired = 0;
                timeToNextShot = 0.0f;
                timeToNextBurst = COOLDOWN + TIME_PER_SHOT * SHOTS_PER_BURST;
            }
            if (timeToNextShot <= 0.0f && shotsFired < SHOTS_PER_BURST)
            {
                // Instantiate arrow
                timeToNextShot = TIME_PER_SHOT;
                shotsFired++;
            }
        }
    }

    public void OnAttackHit(GameObject source, float damageHint)
    {
        if (invulnerable <= 0.0f)
        {
            health -= damageHint;
            invulnerable = Globals.ENEMY_INVULN_TIME;

            if (health <= 0)
            {
                //sfx[2].Play();
                GameObject.Find("Main Camera").GetComponent<CameraControls>().shake();
                Destroy(this.gameObject);
            }
        }
    }

    public void OnChargedAttackHit(GameObject source, float damageHint)
    {
        OnAttackHit(source, damageHint);
    }

    public void OnGrappleHit(GameObject source)
    {
        // No reaction
    }

    public void OnPull(GameObject source)
    {
        // No reaction
    }


}
