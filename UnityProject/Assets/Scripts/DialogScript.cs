using UnityEngine;
using System.Collections;

public class DialogScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnGUI()
    {
        GUI.Box(new Rect(5, 5, Screen.width - 5, Screen.height - 5), "My box");
    }
}
