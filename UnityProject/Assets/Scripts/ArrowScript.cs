using UnityEngine;
using System.Collections;

public class ArrowScript : MonoBehaviour {

    public float ARROW_FORCE = 10.0f;

    private float speed = 4.0f;
    
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

   
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "GWall")
        {
            Reset();
        }
        if (col.gameObject.tag == "Player")
        {
            HeroMovement s = col.gameObject.transform.parent.gameObject.GetComponent<HeroMovement>();
            s.TakeDamage(6);
            s.ApplyKnockback(ARROW_FORCE * (GameObject.Find("Hero").transform.position - transform.parent.position).normalized);
            Reset();
        }
    }

    public void Fire(Globals.Dir direction)
    {
        Reset();
        Color oldColor = renderer.material.color;
        Color newColor = new Color(oldColor.r, oldColor.b, oldColor.g, 100.0f);
        renderer.material.SetColor("_Color", newColor);

        switch (direction)
        {
            case Globals.Dir.N: this.transform.rigidbody2D.rotation = 0.0f; break;
            case Globals.Dir.E: this.transform.rigidbody2D.rotation = 90.0f; break;
            case Globals.Dir.S: this.transform.rigidbody2D.rotation = 180.0f; break;
            case Globals.Dir.W: this.transform.rigidbody2D.rotation = 270.0f; break;
        }

        this.transform.rigidbody2D.velocity = Globals.getVectorFromDirection(direction) * speed;
    }

    public void Reset()
    {
        Color oldColor = renderer.material.color;
        Color newColor = new Color(oldColor.r, oldColor.b, oldColor.g, 0.0f);
        renderer.material.SetColor("_Color", newColor);

        this.transform.position = transform.parent.GetComponent<TurretEnemyScript>().transform.position;
    }
}
