using UnityEngine;
using System.Collections;

public class TurretEnemyScript : MonoBehaviour {

    public float invulnerable; //Used to deal with invincibility frame timing

    public int health;

    public Constants.Dir direction;

    public GameObject player;
    public float aggro_range = 10.0f;//Guess

    public bool firing;
    public float cooldown;

    private Animator anim;
    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();

        invulnerable = 0;

        direction = Constants.Dir.S;
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


        Vector3 offset_vector = (Vector2) (transform.position - player.transform.position);
        print(offset_vector);

        if (offset_vector.magnitude < aggro_range)
        {
            direction = Constants.getDirectionFromVector(offset_vector);
            firing = true;
        }
        else
        {
            firing = false;
        }

        switch (direction)
        {
            case Constants.Dir.N: anim.SetFloat("Horizontal", 0.0f); anim.SetFloat("Vertical", 1.0f);  break;
            case Constants.Dir.E: anim.SetFloat("Horizontal", 1.0f); anim.SetFloat("Vertical", 0.0f);  break;
            case Constants.Dir.S: anim.SetFloat("Horizontal", 0.0f); anim.SetFloat("Vertical", -1.0f); break;
            case Constants.Dir.W: anim.SetFloat("Horizontal", -1.0f); anim.SetFloat("Vertical", 0.0f); break;
        }
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

    public void setDirection(Constants.Dir direction)
    {
        this.direction = direction;
    }
}
