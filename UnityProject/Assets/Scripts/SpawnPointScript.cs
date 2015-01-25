using UnityEngine;
using System.Collections;

public class SpawnPointScript : MonoBehaviour {

    public bool firstSpawn = false;
    public bool forceSpawn = false;

    private GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Hero");
        if (firstSpawn) player.GetComponent<HeroMovement>().SetSpawnPoint(transform.position);

        if(forceSpawn)
            player.transform.position = transform.position;
	}

    public void SaveSpawnPoint()
    {
        player.GetComponent<HeroMovement>().SetSpawnPoint(transform.position);
    }

    public void ReloadMap()
    {
        Invoke("ReloadMapHelp", 2.0f);
    }

    public void ReloadMapHelp()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
