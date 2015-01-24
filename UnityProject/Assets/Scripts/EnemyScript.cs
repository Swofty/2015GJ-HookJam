using UnityEngine;using System.Collections;public class EnemyScript : MonoBehaviour {    public float speed;    public Constants.Dir direction;

    private Animator anim;
    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        direction = Constants.Dir.S;
    }		// Update is called once per frame	void Update () {
        // Idle state
        // TODO: Move to its own
        switch (direction)
        {
            case Constants.Dir.N: anim.SetFloat("Horizontal", 0.0f); anim.SetFloat("Vertical", 1.0f); break;
            case Constants.Dir.E: anim.SetFloat("Horizontal", 1.0f); anim.SetFloat("Vertical", 0.0f); break;
            case Constants.Dir.S: anim.SetFloat("Horizontal", 0.0f); anim.SetFloat("Vertical", -1.0f); break;
            case Constants.Dir.W: anim.SetFloat("Horizontal", -1.0f); anim.SetFloat("Vertical", 0.0f); break;
        }	}    void OnTriggerEnter2D(Collider2D col)    {//        if (other.gameObject.tag == "Player")//        {            Vector3 direction_vector = col.transform.position - transform.position;            direction = Constants.getDirectionFromVector(direction_vector);//        }            print(direction);    }    void FixedUpdate()    {        rigidbody2D.velocity = Constants.getVectorFromDirection(direction) * speed;    }}