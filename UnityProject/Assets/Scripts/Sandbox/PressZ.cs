using UnityEngine;
using System.Collections;

public class PressZ : MonoBehaviour {

    public static bool instantiated = false;

    void Awake()
    {
        if (!instantiated)
        {
            instantiated = true;
            Debug.Log("Not instantiated yet!");
        }
        else
            Debug.Log("Already instantiated");
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.M))
        {
            GameObject turret = GameObject.Find("Stationary Turret");
            EnemyBase b = turret.gameObject.GetComponent<EnemyBase>();
            if (b = Util.GetEnemy(turret))
                Debug.Log("Is enemy base");
            else
                Debug.Log("Not enemy base");
        }

        if (Input.GetKeyDown(KeyCode.N))
            Application.LoadLevel("Sandbox3");

	}
}
