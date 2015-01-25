using UnityEngine;
using System.Collections;

public class SnailEnemyScript : MonoBehaviour {

    public float speed;
    public float invulnerable; //Used to deal with invincibility frame timing

    public float next_turn = 2.0f;

    public bool armored; //Used to tell if the enemy still has armor on him

    public int health = 12;

    public Constants.Dir direction;

    private Animator anim;
    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();

        invulnerable = 0;
        armored = true;

        direction = Constants.Dir.S;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Wall" || col.tag == "Enemy")
        {
            switch (direction)
            {
                case Constants.Dir.N: direction = Constants.Dir.S; break;
                case Constants.Dir.E: direction = Constants.Dir.E; break;
                case Constants.Dir.S: direction = Constants.Dir.N; break;
                case Constants.Dir.W: direction = Constants.Dir.W; break;
            }

            switch (direction)
            {
                case Constants.Dir.N: anim.SetFloat("Horizontal", 0.0f); anim.SetFloat("Vertical", 1.0f); break;
                case Constants.Dir.E: anim.SetFloat("Horizontal", 1.0f); anim.SetFloat("Vertical", 0.0f); break;
                case Constants.Dir.S: anim.SetFloat("Horizontal", 0.0f); anim.SetFloat("Vertical", -1.0f); break;
                case Constants.Dir.W: anim.SetFloat("Horizontal", -1.0f); anim.SetFloat("Vertical", 0.0f); break;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
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
                case Constants.Dir.N: anim.SetFloat("Horizontal", 0.0f); anim.SetFloat("Vertical", 1.0f); break;
                case Constants.Dir.E: anim.SetFloat("Horizontal", 1.0f); anim.SetFloat("Vertical", 0.0f); break;
                case Constants.Dir.S: anim.SetFloat("Horizontal", 0.0f); anim.SetFloat("Vertical", -1.0f); break;
                case Constants.Dir.W: anim.SetFloat("Horizontal", -1.0f); anim.SetFloat("Vertical", 0.0f); break;
            }
            next_turn = 2.0f;
        }
	}

    void FixedUpdate()
    {
        rigidbody2D.velocity = Constants.getVectorFromDirection(direction) * speed;
    }

    public void hit(int damage)
    {
        this.health -= damage;
        this.invulnerable = 0.5f;

        //Want to have it so that if the enemy dies, we shake the camera
        if (health <= 0)
            print("dead");
    }

    public bool isInvulnerable()
    {
        return (invulnerable > 0);
    }

    public bool isArmored()
    {
        return armored;
    }

    public void setDirection(Constants.Dir direction)
    {
        this.direction = direction;
    }

    public void setArmored(bool armored)
    {
        this.armored = armored;
    }
}
