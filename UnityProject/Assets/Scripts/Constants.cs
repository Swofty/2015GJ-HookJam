using UnityEngine;
using System.Collections;

public static class Globals {

    public enum Dir { N, E, S, W, NE, SE, SW, NW };

    public static KeyCode HookKey = KeyCode.Z;
    public static KeyCode SwordKey = KeyCode.X;
	public static KeyCode ChargeKey = KeyCode.C;
    public static KeyCode DashKey = KeyCode.Space;

    public static float ENEMY_INVULN_TIME = 2.0f;

    public static GameObject GetPlayer()
    {
        return GameObject.Find("Hero");
    }

    public static Vector2 getVectorFromDirection(Dir direction)
    {
        switch (direction)
        {
            case Dir.N: return new Vector2(0.0f, 1.0f);
            case Dir.E: return new Vector2(1.0f, 0.0f);
            case Dir.S: return new Vector2(0.0f, -1.0f);
            case Dir.W: return new Vector2(-1.0f, 0.0f);
            case Dir.NE: return new Vector2(0.70710678118f, 0.70710678118f);
            case Dir.SE: return new Vector2(0.70710678118f, -0.70710678118f);
            case Dir.SW: return new Vector2(-0.70710678118f, -0.70710678118f);
            case Dir.NW: return new Vector2(-0.70710678118f, 0.70710678118f);
        }
        return new Vector2(0.0f, 0.0f);
    }

    public static Dir getDirectionFromVector(Vector3 direction_vector)
    {
        if (Mathf.Abs(direction_vector.y) > Mathf.Abs(direction_vector.x) && direction_vector.y >= 0)
        {
            return Dir.N;
        }
        else if (Mathf.Abs(direction_vector.y) > Mathf.Abs(direction_vector.x)  && direction_vector.y < 0)
        {
            return Dir.S;
        }
        if (Mathf.Abs(direction_vector.y) <= Mathf.Abs(direction_vector.x) && direction_vector.x >= 0)
        {
            return Dir.E;
        }
        else if (Mathf.Abs(direction_vector.y) <= Mathf.Abs(direction_vector.x) && direction_vector.x < 0)
        {
            return Dir.W;
        }

        //The above cases should be sufficient
        return Dir.N;
    }

    public static bool isWallTag(string str)
    {
        if (str == "GWall")
            return true;
        return false;
    }

    public static bool isEnemyTag(string str)
    {
        if (str == "Enemy")
            return true;
        if (str == "GEnemy")
            return true;
        return false;
    }

	public enum Attack { HOOK, SWORD, DASH, CHARGE, HOOKCHARGE };
}
