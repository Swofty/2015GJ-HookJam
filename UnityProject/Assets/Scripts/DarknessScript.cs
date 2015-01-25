using UnityEngine;
using System.Collections;

public class DarknessScript : MonoBehaviour {

    public float transparency = 1.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            if (transparency > 0.0)
            {
                transparency -= 0.02f;
                Color oldColor = renderer.material.color;
                Color newColor = new Color(oldColor.r, oldColor.b, oldColor.g, transparency);
                renderer.material.SetColor("_Color", newColor);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
