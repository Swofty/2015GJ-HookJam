using UnityEngine;
using System.Collections;

namespace Snail
{
    public class BasicSnailCore : MonoBehaviour, IAware
    {
        public float SPEED = 0.5f;
        public float TURN_FREQUENCY = 2.0f;

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

        /// <summary>
        /// Time to next direction change update
        /// </summary>
        private float nextTurn = 0.0f;

        private Animator anim;

        void Awake()
        {
            anim = gameObject.GetComponent<Animator>();
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            // TODO: Change to support whatever Wall/Hole tagger we use later
            if (col.gameObject.CompareTag("GWall") || col.gameObject.CompareTag("Hole"))
            {
                Direction = Util.FlipDirection(direction);
            }

            // TODO: Change to figure out player collider
            if (col.gameObject.tag == "Player")
            {
                GameManager.Player.TakeDamage(12);
                GameManager.Player.ApplyImpulse(
                    1.0f * 
                    (GameManager.Player.transform.position - transform.position).normalized);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (invulnerable > 0.0f)
            {
                invulnerable -= Time.deltaTime;
                anim.SetBool("Invulnerable", true);
            }
            else
            {
                anim.SetBool("Invulnerable", false);
            }

            nextTurn -= Time.deltaTime;

            if (nextTurn <= 0.0f)
            {
                if (aggro)
                {
                    Direction = Util.GetDirectionFromVector(
                        GameManager.Player.transform.position - transform.position);
                }
                else
                {
                    Direction = (Util.Dir)(int)(Random.value * 8);
                }
                nextTurn = TURN_FREQUENCY;
            }
        }

        void FixedUpdate()
        {
            rigidbody2D.velocity = Util.GetVectorFromDirection(direction) * SPEED;
        }
        
        public void TakeDamage(float damage)
        {
            aggro = true;
            anim.SetTrigger("Hurt");
            health -= damage;
            invulnerable = 0.2f;
            nextTurn = 0.0f;

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
            throw new System.NotImplementedException();
        }

        public void EnterAwareness(GameObject go)
        {
            throw new System.NotImplementedException();
        }

        public void LeaveAwareness(GameObject go)
        {
            throw new System.NotImplementedException();
        }
    }
}