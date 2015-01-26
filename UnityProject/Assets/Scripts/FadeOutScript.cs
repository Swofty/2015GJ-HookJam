using UnityEngine;
using System.Collections;

public class FadeOutScript : MonoBehaviour {

    public float transparency = 0.0f;

    public bool end;
	// Use this for initialization
	void Start () {
        Color oldColor = renderer.material.color;
        Color newColor = new Color(oldColor.r, oldColor.b, oldColor.g, transparency);
        renderer.material.SetColor("_Color", newColor);
        end = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (!end)
            return;
        else
        {
            if (transparency < 1.0)
            {
                transparency += 0.01f;
                Color oldColor = renderer.material.color;
                Color newColor = new Color(oldColor.r, oldColor.b, oldColor.g, transparency);
                renderer.material.SetColor("_Color", newColor);
            }
            else {
                transparency += 0.01f;
                if (transparency > 2.0)
                    Application.LoadLevel("titlescreen");
            }
        }
	}

}
