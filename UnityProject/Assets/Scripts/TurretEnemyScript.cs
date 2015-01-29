using UnityEngine;
using System.Collections;

public class TurretEnemyScript : MonoBehaviour {

    public GameObject player;
    private float aggro_range = 10.0f;//Guess

    private bool firing;
    private float cooldown = 0.0f;

    private float invulnerable = 0; //Used to deal with invincibility frame timing

    private int health = 4;

    private Util.Dir direction;

    private Animator anim;
    //private AudioClip[] sfx;

    void Awake()
    {
        player = GameObject.Find("Hero");
        anim = gameObject.GetComponent<Animator>();

        invulnerable = 0;

        cooldown = 0.0f;

        direction = Util.Dir.S;

        //sfx = gameObject.GetComponents<AudioClip>();
    }

    void Start()
    {
        player = GameObject.Find("Hero");
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


        if(player == null)
        {
            return;
        }
        Vector3 offset_vector = (Vector2) (transform.position - player.transform.position);

        if (offset_vector.magnitude < aggro_range)
        {
            direction = Util.GetDirectionFromVector(-offset_vector);
            firing = true;
        }
        else
        {
            firing = false;
        }

        switch (direction)
        {
            case Util.Dir.N: anim.SetFloat("Horizontal", 0.0f); anim.SetFloat("Vertical", 1.0f);  break;
            case Util.Dir.E: anim.SetFloat("Horizontal", 1.0f); anim.SetFloat("Vertical", 0.0f);  break;
            case Util.Dir.S: anim.SetFloat("Horizontal", 0.0f); anim.SetFloat("Vertical", -1.0f); break;
            case Util.Dir.W: anim.SetFloat("Horizontal", -1.0f); anim.SetFloat("Vertical", 0.0f); break;
        }

        if (firing && cooldown <= 0.0f)
        {
            //audio.PlayOneShot();
            cooldown = 5.0f;
            Transform child = transform.FindChild("Arrow");
            //child.GetComponent<ArrowScript>().Fire(direction);
        }
        if (cooldown >= 0)
        {
            cooldown -= Time.deltaTime;
            if (cooldown <= 0)
            {
                Transform child = transform.FindChild("Arrow");
                //child.GetComponent<ArrowScript>().Reset();
            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            print("hi");
            col.transform.rigidbody2D.velocity = Vector2.zero;
        }
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

    public void setDirection(Util.Dir direction)
    {
        this.direction = direction;
    }
}
