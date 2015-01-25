using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

    public float speed;
    public float invulnerable; //Used to deal with invincibility frame timing
    public bool armored; //Used to tell if the enemy still has armor on him

    public int health;

    public Constants.Dir direction;

    private Animator anim;
    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();

        invulnerable = 0;
        armored = true;

        direction = Constants.Dir.S;
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


        switch (direction)
        {
            case Constants.Dir.N: anim.SetFloat("Horizontal", 0.0f); anim.SetFloat("Vertical", 1.0f); break;
            case Constants.Dir.E: anim.SetFloat("Horizontal", 1.0f); anim.SetFloat("Vertical", 0.0f); break;
            case Constants.Dir.S: anim.SetFloat("Horizontal", 0.0f); anim.SetFloat("Vertical", -1.0f); break;
            case Constants.Dir.W: anim.SetFloat("Horizontal", -1.0f); anim.SetFloat("Vertical", 0.0f); break;
        }
	}

    void FixedUpdate()
    {
        rigidbody2D.velocity = Constants.getVectorFromDirection(direction) * speed;
    }

    public void hit()
    {
        this.health -= 1;
        this.invulnerable = 0.5f;
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
