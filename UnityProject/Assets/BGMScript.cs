using UnityEngine;
using System.Collections;

public class BGMScript : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        DontDestroyOnLoad(gameObject);
	}
	
}
