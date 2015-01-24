using UnityEngine;
using System.Collections;

public class HookScript : MonoBehaviour {

    public float SPEED = 10.0f;
    public float MAX_HOOK_DISTANCE = 5.0f;

    private Vector3 initPos;
    private GameObject player;

    void Awake()
    {
        player = transform.parent.gameObject;
        rigidbody2D.velocity = new Vector2();
    }

    public void ShootHook(Constants.Dir dir)
    {
        gameObject.SetActive(true);
        transform.localPosition = GetInitPosition(dir);
        transform.rotation = GetInitRotation(dir);
        rigidbody2D.velocity = SPEED * Constants.getVectorFromDirection(dir);
        Debug.Log("Hook enabled!");
    }

    void DisableHook()
    {
        rigidbody2D.velocity = new Vector2();
        gameObject.SetActive(false);
        Debug.Log("Hook disabled!");
    }

    Vector3 GetInitPosition(Constants.Dir dir)
    {
        // TODO
        Vector3 pos = (0.34f * Constants.getVectorFromDirection(dir));
        return pos;
    }

    Quaternion GetInitRotation(Constants.Dir dir)
    {
        // TODO
        Quaternion rot;
        switch (dir)
        {
            case Constants.Dir.N: rot = Quaternion.Euler(0.0f, 0.0f, 90.0f); break;
            case Constants.Dir.E: rot = Quaternion.Euler(0.0f, 0.0f, 0.0f); break;
            case Constants.Dir.S: rot = Quaternion.Euler(0.0f, 0.0f, -90.0f); break;
            case Constants.Dir.W: rot = Quaternion.Euler(0.0f, 0.0f, 180.0f); break;
            default: rot = new Quaternion(); break;
        }
        return rot;
    }
	
    void Update()
    {
        Vector3 playerToRopeVec = (transform.position- player.transform.position);
        float playerToRopeDist = playerToRopeVec.magnitude;

        // Rope attachment
        Transform rope = transform.FindChild("Rope");
        rope.localScale = new Vector3(playerToRopeDist, 1.0f, 1.0f);
        Vector3 q = Quaternion.AngleAxis(90, Vector3.forward) * playerToRopeVec;
        rope.rotation = Quaternion.LookRotation(Vector3.forward, q);

        // When to stop
        if(playerToRopeDist > MAX_HOOK_DISTANCE)
        {
            DisableHook();
        }
    }
}
