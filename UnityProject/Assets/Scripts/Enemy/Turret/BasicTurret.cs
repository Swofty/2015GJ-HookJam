using UnityEngine;
using System.Collections;

namespace Turret
{
    public class BasicTurret : HittablePart, IAware
    {
        public float COOLDOWN = 1.0f;
        public int SHOTS_PER_BURST = 4;
        public float TIME_PER_SHOT = 0.5f;
        public float ARROW_SPEED = 4.0f;
        public bool FOLLOW_PLAYER = false;

        public float health = 4.0f;
        public Util.Dir direction = Util.Dir.E;
        public Rigidbody2D arrow;

        private bool aggro = true;
        private float timeToNextBurst = 0.0f;
        private float timeToNextShot = 0.0f;
        private int shotsFired = 0;
        private float invulnerable = 0.0f;

        private Animator anim;
        private AudioSource sfx;

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
            sfx = gameObject.GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            if (invulnerable > 0.0)
            {
                invulnerable -= Time.deltaTime;
                anim.SetBool("Invulnerable", true);
            }
            else
            {
                invulnerable = 0.0f;
                anim.SetBool("Invulnerable", false);
            }

            switch (direction)
            {
                case Util.Dir.N: anim.SetFloat("Horizontal", 0.0f); anim.SetFloat("Vertical", 1.0f); break;
                case Util.Dir.E: anim.SetFloat("Horizontal", 1.0f); anim.SetFloat("Vertical", 0.0f); break;
                case Util.Dir.S: anim.SetFloat("Horizontal", 0.0f); anim.SetFloat("Vertical", -1.0f); break;
                case Util.Dir.W: anim.SetFloat("Horizontal", -1.0f); anim.SetFloat("Vertical", 0.0f); break;
            }

            if (aggro)
            {
                if (FOLLOW_PLAYER)
                {
                    direction = Util.GetDirectionFromVector(
                        GameManager.Player.transform.position - transform.position);
                }

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
                    Vector3 pos = transform.position +
                        0.2f * (Vector3)Util.GetVectorFromDirection(direction);
                    Quaternion rot = Quaternion.LookRotation(Vector3.forward,
                         Util.GetVectorFromDirection(direction));
                    Rigidbody2D arrowInstance = (Rigidbody2D)Instantiate(arrow, pos, rot);
                    arrowInstance.transform.parent = transform;
                    arrowInstance.velocity = ARROW_SPEED * Util.GetVectorFromDirection(direction);
                    sfx.Play();
                    timeToNextShot = TIME_PER_SHOT;
                    shotsFired++;
                }
            }
        }


        public override void OnAttackHit(GameObject source, float damageHint)
        {
            if (invulnerable <= 0.0f)
            {
                health -= damageHint;
                invulnerable = Util.ENEMY_INVULN_TIME;

                if (health <= 0.0f)
                {
                    GameManager.EnemyDeath();
                    transform.DetachChildren();
                    Destroy(this.gameObject);
                }
            }
        }

        public override void OnChargedAttackHit(GameObject source, float damageHint)
        {
            OnAttackHit(source, damageHint);
        }


        public void SetAwareness(bool aware)
        {
            aggro = aware;
        }

        public void EnterAwareness(GameObject go)
        {
            if (go.CompareTag("Player"))
                aggro = true;
        }

        public void LeaveAwareness(GameObject go)
        {
            if (go.CompareTag("Player"))
                aggro = false;
        }
    }

}