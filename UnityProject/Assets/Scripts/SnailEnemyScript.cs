﻿using UnityEngine;
using System.Collections;

public class SnailEnemyScript : MonoBehaviour {

    private float speed = 0.5f;
    private float invulnerable = 0; //Used to deal with invincibility frame timing

    private float next_turn = 2.0f;

    private bool armored = true; //Used to tell if the enemy still has armor on him

    private int health = 12;

    public Globals.Dir direction;

    private Animator anim;
    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();

        //invulnerable = 0;
        //armored = true;

        direction = Globals.Dir.S;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Wall" || col.tag == "Enemy")
        {
            switch (direction)
            {
                case Globals.Dir.N: direction = Globals.Dir.S; break;
                case Globals.Dir.E: direction = Globals.Dir.E; break;
                case Globals.Dir.S: direction = Globals.Dir.N; break;
                case Globals.Dir.W: direction = Globals.Dir.W; break;
            }

            switch (direction)
            {
                case Globals.Dir.N: anim.SetFloat("Horizontal", 0.0f); anim.SetFloat("Vertical", 1.0f); break;
                case Globals.Dir.E: anim.SetFloat("Horizontal", 1.0f); anim.SetFloat("Vertical", 0.0f); break;
                case Globals.Dir.S: anim.SetFloat("Horizontal", 0.0f); anim.SetFloat("Vertical", -1.0f); break;
                case Globals.Dir.W: anim.SetFloat("Horizontal", -1.0f); anim.SetFloat("Vertical", 0.0f); break;
            }
        }
        if (col.tag == "Player")
        {
            col.GetComponent<HeroMovement>().TakeDamage(12);
            col.GetComponent<HeroMovement>().ApplyKnockback(GameObject.Find("Hero").transform.position - transform.parent.position);
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
                case Globals.Dir.N: anim.SetFloat("Horizontal", 0.0f); anim.SetFloat("Vertical", 1.0f); break;
                case Globals.Dir.E: anim.SetFloat("Horizontal", 1.0f); anim.SetFloat("Vertical", 0.0f); break;
                case Globals.Dir.S: anim.SetFloat("Horizontal", 0.0f); anim.SetFloat("Vertical", -1.0f); break;
                case Globals.Dir.W: anim.SetFloat("Horizontal", -1.0f); anim.SetFloat("Vertical", 0.0f); break;
            }
            next_turn = 2.0f;
        }
	}

    void FixedUpdate()
    {
        rigidbody2D.velocity = Globals.getVectorFromDirection(direction) * speed;
    }

    public void hit(int damage)
    {
        this.health -= damage;
        this.invulnerable = 0.5f;

        //Want to have it so that if the enemy dies, we shake the camera
        if (health <= 0)
        {
            GameObject.Find("Main Camera").GetComponent<CameraControls>().shake();
            Destroy(this.gameObject);
        }
    }

    public bool isInvulnerable()
    {
        return (invulnerable > 0);
    }

    public bool isArmored()
    {
        return armored;
    }

    public void setDirection(Globals.Dir direction)
    {
        this.direction = direction;
    }

    public void setArmored(bool armored)
    {
        this.armored = armored;
    }
}
