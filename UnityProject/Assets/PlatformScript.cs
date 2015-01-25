using UnityEngine;
using System.Collections;

public class PlatformScript : MonoBehaviour {

    private float rotational_offset; //Used for when the boss is stacked on top of itself
    
	// Update is called once per frame
	void Update () {
        rigidbody2D.rotation += 300f;
	}

    void OnTriggerStay2D(Collider2D col)
    {
        Vector2 displacement = (transform.position - col.transform.position);

        float radians = rigidbody2D.rotation * Mathf.Deg2Rad;
        float sin = Mathf.Sin(radians);
        float cos = Mathf.Cos(radians);
        float tx = displacement.x;
        float ty = displacement.y;
        
        Vector2 new_position = (Vector2)(this.transform.position) + new Vector2(cos * tx - sin * ty, sin * tx + cos * ty);

        print(new_position);

        if(col.gameObject.tag == "Player" && col.gameObject.rigidbody2D.velocity == Vector2.zero)
        {
            col.gameObject.transform.position = new_position;
        }
        else
        {
            col.gameObject.rigidbody2D.rotation = this.rigidbody2D.rotation;
        }
    }
}
