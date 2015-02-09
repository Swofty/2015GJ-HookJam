using UnityEngine;
using System.Collections;

namespace Enemy.Snail
{
    public class BasicSnailCore : StateEntity, IAware
    {
        public float TURN_FREQUENCY = 2.0f;
        public float SPEED = 0.5f;
        public float INVULN_TIME = 0.2f;
        public float PAUSE_DURATION = 2.0f;
        public float MOVE_DURATION = 3.0f;

        public float AGGRO_CHASE_DURATION = 5.0f;
        public float AGGRO_PAUSE_DURATION = 2.0f;
        public float AGGRO_ATTACK_RADIUS = 1.5f;

        public float ATTACK_SPEED = 12.0f;
        public float ATTACK_PREP_DURATION = 2.0f;
        public float ATTACK_JUMP_DURATION = 0.2f;
        public float ATTACK_RECOVER_DURATION = 3.0f;

        public Util.Dir Direction
        {
            get { return direction; }
            set
            {
                direction = value;
                SelectAnimationDirection(direction);
            }
        }
        public bool Armored { get { return armored; } }
        public bool Invulnerable { get { return invulnerable > 0.0f; } }
        public bool Aggro { get { return aggro; } }
        public bool DamageHitboxEnable
        {
            get
            {
                return damageHitbox.activeSelf;
            }
            set
            {
                damageHitbox.SetActive(value);
            }
        }

        [SerializeField]
        private Util.Dir direction;

        [SerializeField]
        private float health = 12.0f;

        [SerializeField]
        private bool armored = true;

        [SerializeField]
        private bool aggro = false;

        /// <summary>
        /// Time for invulnerability
        /// </summary>
        private float invulnerable = 0.0f;
        private Animator anim;
        private GameObject damageHitbox;
        private StateMachine<BasicSnailCore> fsm;

        void Awake()
        {
            anim = gameObject.GetComponent<Animator>();
            anim.SetBool("Moving", false);
            anim.SetBool("Armored", true);
            fsm = new StateMachine<BasicSnailCore>(this);
            fsm.InitialState(WanderState.Instance(this, fsm));
            damageHitbox = transform.FindChild("DamageHitbox").gameObject;
        }

        void Start()
        {
            damageHitbox.SetActive(false);
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            // TODO: Change to support whatever Wall/Hole tagger we use later
            if (col.gameObject.CompareTag("GWall") || col.gameObject.CompareTag("Hole"))
            {
                Direction = Util.FlipDirection(direction);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (invulnerable > 0.0f) invulnerable -= Time.deltaTime;;
            anim.SetBool("Invulnerable", invulnerable > 0.0f);
            anim.SetBool("Moving", rigidbody2D.velocity.sqrMagnitude > 0.0f);
            anim.SetBool("Attacking", DamageHitboxEnable);

            fsm.Execute();
        }

        void FixedUpdate()
        {
            fsm.FixedExecute();
        }
        
        public void TakeDamage(float damage)
        {
            aggro = true;
            anim.SetTrigger("Hurt");
            health -= damage;
            invulnerable = INVULN_TIME;

            //Want to have it so that if the enemy dies, we shake the camera
            if (health <= 0)
            {
                GameManager.EnemyDeath();
                Destroy(this.gameObject);
            }
        }
        
        private void SelectAnimationDirection(Util.Dir dir)
        {
            switch (dir)
            {
                case Util.Dir.N:
                case Util.Dir.NE:
                    anim.SetFloat("Horizontal", 0.0f); anim.SetFloat("Vertical", 1.0f); break;
                case Util.Dir.E:
                case Util.Dir.SE:
                    anim.SetFloat("Horizontal", 1.0f); anim.SetFloat("Vertical", 0.0f); break;
                case Util.Dir.S:
                case Util.Dir.SW:
                    anim.SetFloat("Horizontal", 0.0f); anim.SetFloat("Vertical", -1.0f); break;
                case Util.Dir.W:
                case Util.Dir.NW:
                    anim.SetFloat("Horizontal", -1.0f); anim.SetFloat("Vertical", 0.0f); break;
            }
        }

        public void SetAwareness(bool aware)
        {
            aggro = aware;
        }

        public void EnterAwareness(GameObject go)
        {
            aggro = true;
        }

        public void LeaveAwareness(GameObject go)
        {
            aggro = false;
        }

    }
}