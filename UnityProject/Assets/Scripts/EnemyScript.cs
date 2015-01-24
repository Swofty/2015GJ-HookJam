using UnityEngine;using System.Collections;public class EnemyScript : MonoBehaviour {    public float speed;    public Constants.Dir direction;	// Use this for initialization	void Start () {        direction = Constants.Dir.S;	}		// Update is called once per frame	void Update () {	}    void OnTriggerEnter(Collider other)    {
//        if (other.gameObject.tag == "Player")
//        {
            Vector3 direction_vector = other.transform.position - transform.position;
            direction = Constants.getDirectionFromVector(direction_vector);
//        }
            print("hi");    }    void FixedUpdate()    {        rigidbody2D.velocity = Constants.getVectorFromDirection(direction) * speed;    }}