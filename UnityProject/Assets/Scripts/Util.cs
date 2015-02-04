using UnityEngine;
using System.Collections;

public static class Util {

    public enum Dir { N, E, S, W, NE, SE, SW, NW };

    public static KeyCode HOOK_KEY = KeyCode.Z;
    public static KeyCode SWORD_KEY = KeyCode.X;
	public static KeyCode CHARGE_KEY = KeyCode.C;
    public static KeyCode DASH_KEY = KeyCode.Space;

    public static float ENEMY_INVULN_TIME = 0.7f;

    public static Vector2 GetVectorFromDirection(Dir direction)
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

    public static Dir FlipDirection(Dir dir)
    {
        switch (dir)
        {
            case Dir.N: return Dir.S;
            case Dir.E: return Dir.W;
            case Dir.S: return Dir.N;
            case Dir.W: return Dir.E;
            case Dir.NE: return Dir.SW;
            case Dir.NW: return Dir.SE;
            case Dir.SE: return Dir.NW;
            case Dir.SW: return Dir.NE;
        }
        return Dir.N;
    }

    public static Dir GetDirectionFromVector(Vector3 direction_vector)
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

    public static bool IsWallTag(string str)
    {
        if (str == "GWall")
            return true;
        return false;
    }

    public static HittablePart GetHittablePart(Component c)
    {
        return c.GetComponent<HittablePart>();
    }

	public enum Attack { HOOK, SWORD, DASH, CHARGE, HOOKCHARGE };
}
