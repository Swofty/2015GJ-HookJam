using UnityEngine;
using System.Collections;

public class DashScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            dash();
        }

	}

    void dash()
    {
        float topSpeed = 1.0f; //Replace with the top speed in HeroMovement
    }
}
