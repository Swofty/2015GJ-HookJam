using UnityEngine;
using System.Collections;

namespace Snail
{
    public class BasicSnailCore : MonoBehaviour, IAware
    {
        public float SPEED = 0.5f;
        public Util.Dir direction;
        public bool test = true;

        [SerializeField]
        private float health = 12.0f;

        [SerializeField]
        private bool armored = true;


        public bool pArmored { get { return armored; } }


        private float invulnerable = 0.0f; //Used to deal with invincibility frame timing
        private float next_turn = 2.0f;
        private bool aggro = false;

        private Animator anim;

        void Awake()
        {
            anim = gameObject.GetComponent<Animator>();
        }

        void OnTriggerStay2D(Collider2D col)
        {
            if (!aggro)
            {
                if (col.CompareTag("GWall") || col.CompareTag("Hole"))
                {
                    direction = Util.FlipDirection(direction);
                    SelectAnimationDirection(direction);
                }
            }


            if (col.tag == "Player")
            {
                col.GetComponent<HeroMovement>().TakeDamage(12);
                col.GetComponent<HeroMovement>().ApplyKnockback(GameObject.Find("Hero").transform.position - transform.parent.position);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (invulnerable > 0)
            {
                invulnerable -= Time.deltaTime;
                Color oldColor = renderer.material.color;
                Color newColor = new Color(oldColor.r, oldColor.b, oldColor.g, 0.2f);
                renderer.material.SetColor("_Color", newColor);
            }
            else
            {
                Color oldColor = renderer.material.color;
                Color newColor = new Color(oldColor.r, oldColor.b, oldColor.g, 1.0f);
                renderer.material.SetColor("_Color", newColor);
            }

            next_turn -= Time.deltaTime;

            if (next_turn <= 0)
            {
                switch (direction)
                {
                    case Util.Dir.N: anim.SetFloat("Horizontal", 0.0f); anim.SetFloat("Vertical", 1.0f); break;
                    case Util.Dir.E: anim.SetFloat("Horizontal", 1.0f); anim.SetFloat("Vertical", 0.0f); break;
                    case Util.Dir.S: anim.SetFloat("Horizontal", 0.0f); anim.SetFloat("Vertical", -1.0f); break;
                    case Util.Dir.W: anim.SetFloat("Horizontal", -1.0f); anim.SetFloat("Vertical", 0.0f); break;
                }
                next_turn = 2.0f;
            }
        }

        void FixedUpdate()
        {
            rigidbody2D.velocity = Util.GetVectorFromDirection(direction) * SPEED;
        }

        public void GetHit(float damage, bool headHit)
        {
            if (headHit)
            {
                TakeDamage(3.0f);
                return;
            }
            else
            {
                if(pArmored)
                {
                    // Bounce off
                }
            }
        }

        public void TakeDamage(float damage)
        {
            anim.SetTrigger("Hurt");
            this.health -= damage;
            this.invulnerable = 0.5f;

            //Want to have it so that if the enemy dies, we shake the camera
            if (health <= 0)
            {
                GameManager.EnemyDeath();
                Destroy(this);
            }
        }

        public bool isInvulnerable()
        {
            return (invulnerable > 0);
        }

        public void setDirection(Util.Dir direction)
        {
            this.direction = direction;
        }

        public void setArmored(bool armored)
        {
            this.armored = armored;
        }

        private void SelectAnimationDirection(Util.Dir dir)
        {
            switch (dir)
            {
                case Util.Dir.N: anim.SetFloat("Horizontal", 0.0f); anim.SetFloat("Vertical", 1.0f); break;
                case Util.Dir.E: anim.SetFloat("Horizontal", 1.0f); anim.SetFloat("Vertical", 0.0f); break;
                case Util.Dir.S: anim.SetFloat("Horizontal", 0.0f); anim.SetFloat("Vertical", -1.0f); break;
                case Util.Dir.W: anim.SetFloat("Horizontal", -1.0f); anim.SetFloat("Vertical", 0.0f); break;
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