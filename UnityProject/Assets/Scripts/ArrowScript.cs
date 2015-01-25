﻿using UnityEngine;
using System.Collections;

public class ArrowScript : MonoBehaviour {

    public float speed = 4.0f;

	// Use this for initialization
	void Start () {
        Color oldColor = renderer.material.color;
        Color newColor = new Color(oldColor.r, oldColor.b, oldColor.g, 0.0f);
        renderer.material.SetColor("_Color", newColor);

        this.transform.position = transform.parent.GetComponent<TurretEnemyScript>().transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void Fire(Constants.Dir direction)
    {
        Color oldColor = renderer.material.color;
        Color newColor = new Color(oldColor.r, oldColor.b, oldColor.g, 100.0f);
        renderer.material.SetColor("_Color", newColor);

        switch (direction)
        {
            case Constants.Dir.N: this.transform.rigidbody2D.rotation = 0.0f; break;
            case Constants.Dir.W: this.transform.rigidbody2D.rotation = 90.0f; break;
            case Constants.Dir.S: this.transform.rigidbody2D.rotation = 180.0f; break;
            case Constants.Dir.E: this.transform.rigidbody2D.rotation = 270.0f; break;
        }

        this.transform.rigidbody2D.velocity = Constants.getVectorFromDirection(direction) * speed;
    }

    public void Reset()
    {
        Color oldColor = renderer.material.color;
        Color newColor = new Color(oldColor.r, oldColor.b, oldColor.g, 0.0f);
        renderer.material.SetColor("_Color", newColor);

        this.transform.position = transform.parent.GetComponent<TurretEnemyScript>().transform.position;
    }
}
