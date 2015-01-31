using UnityEngine;
using System.Collections;

public class PressZ : MonoBehaviour {

    public static bool instantiated = false;
    public static PressZ instance;
    private GameObject test;

    void Awake()
    {
        if (!instance)
        {
            instantiated = true;
            instance = this;
            Debug.Log("Not instantiated yet!");
        }
        else
            Debug.Log("Already instantiated");

        test = GameObject.Find("Test");

    }

	// Update is called once per frame
	void Update () {
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            test.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            AwakeStartTest t = test.GetComponent<AwakeStartTest>();
            t.enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            //Destroy(this);
            //Debug.Log("Instance: " + instance);
            Application.LoadLevel("Sandbox3");
        }

	}
}
