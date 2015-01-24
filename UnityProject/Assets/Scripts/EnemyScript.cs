using UnityEngine;using System.Collections;public class EnemyScript : MonoBehaviour {    public float speed;
    public Constants.Dir direction;	// Use this for initialization	void Start () {
        direction = Constants.Dir.S;	}		// Update is called once per frame	void Update () {	    	}    void FixedUpdate()    {
        rigidbody2D.velocity = Constants.getDirectionVector(direction) * speed;    }}