using UnityEngine;
using System.Collections;

public class TabletScript : MonoBehaviour {
	private bool textOn;
	// Use this for initialization
	void Start () {
		textOn = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		textOn = true;
	}

	void OnTriggerExit2D(Collider2D col)
	{
		textOn = false; 
    }

    public void OnGUI()
    {
		if (textOn == true) 
        GUI.Box(new Rect(150, 150, Screen.width - 300, Screen.height - 300), "AAAAAAAAAAH");
    }

}
