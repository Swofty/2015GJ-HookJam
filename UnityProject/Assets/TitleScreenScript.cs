using UnityEngine;
using System.Collections;

public class TitleScreenScript : MonoBehaviour {


	void OnGUI()
    {
        GUI.color = new Color(.3f, .3f, .3f, .3f);
        if (GUI.Button(new Rect(Screen.width * 0.015f, Screen.height * 0.87f, Screen.width * 0.2f, Screen.height * 0.11f), "    "))
            Application.LoadLevel("map0_outside");
    }
}
