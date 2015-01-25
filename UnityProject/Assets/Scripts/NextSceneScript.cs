using UnityEngine;
using System.Collections;

public class NextSceneScript : MonoBehaviour {

    public string nextScene = "";

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            Application.LoadLevel(nextScene);
        }
    }
}
