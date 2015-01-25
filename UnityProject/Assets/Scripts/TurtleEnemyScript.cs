using UnityEngine;
using System.Collections;

public class TurtleEnemyScript : MonoBehaviour {

    private float invulnerable = 0; //Used to deal with invincibility frame timing

    private int stun = 3;

    private int health = 12;

    private float rotation_speed = 1.0f;

    private Animator anim;
    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();

        transform.rigidbody2D.angularVelocity = Mathf.PI;
        invulnerable = 0;
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

        GameObject player = GameObject.Find("Hero");

        Vector2 self_to_player = player.transform.position - transform.position;

        float angle = Mathf.Asin(self_to_player.x / self_to_player.magnitude);
        if(self_to_player.y < 0)
            angle = Mathf.PI - angle;

        print(transform.rigidbody2D.rotation);

        if(transform.rigidbody2D.rotation != -Mathf.Rad2Deg * angle);
           transform.rigidbody2D.angularVelocity = rotation_speed * ((-Mathf.Rad2Deg * angle)-transform.rigidbody2D.rotation);
    }

    void FixedUpdate()
    {
    }

    public void hit(int damage)
    {
        if (this.stun < 0)
        {
            this.health -= damage;
            this.invulnerable = 0.10f;
        }
        else
        {
            this.stun -= damage;
            this.invulnerable = 0.15f;
        }



        //Want to have it so that if the enemy dies, we shake the camera
        if (health <= 0)
        {
            GameObject.Find("Main Camera").GetComponent<CameraControls>().shake(0.15f, 0.25f);
            Destroy(this.gameObject);
        }
    }

    public bool isInvulnerable()
    {
        return (invulnerable > 0);
    }
}
