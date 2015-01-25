using UnityEngine;
using System.Collections;

public class TabletScript : MonoBehaviour {
    public TextAsset textFile;

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
        float x0 = Screen.width * 0.06f;
        float y0 = Screen.height * 0.7f;
        float width = Screen.width * 0.88f;
        float height = Screen.height * 0.25f;
        Rect r = new Rect(x0, y0, width, height);
        string text = textFile.text;
		if (textOn == true)
            GUI.Box(r, text);
    }

}
