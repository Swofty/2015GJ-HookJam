using UnityEngine;
using System.Collections;

public static class Util
{

    public enum Dir { N, E, S, W, NE, SE, SW, NW };

    public static KeyCode HOOK_KEY = KeyCode.Z;
    public static KeyCode SWORD_KEY = KeyCode.X;
    public static KeyCode CHARGE_KEY = KeyCode.C;
    public static KeyCode DASH_KEY = KeyCode.Space;

    public static float ENEMY_INVULN_TIME = 0.7f;

    public static Vector2 North = new Vector2(0.0f, 1.0f);
    public static Vector2 East = new Vector2(1.0f, 0.0f);
    public static Vector2 South = new Vector2(0.0f, -1.0f);
    public static Vector2 West = new Vector2(-1.0f, 0.0f);
    public static Vector2 NorthEast = new Vector2(0.70710678118f, 0.70710678118f);
    public static Vector2 SouthEast = new Vector2(0.70710678118f, -0.70710678118f);
    public static Vector2 SouthWest = new Vector2(-0.70710678118f, -0.70710678118f);
    public static Vector2 NorthWest = new Vector2(-0.70710678118f, 0.70710678118f);

    public static Vector2 GetVectorFromDirection(Dir direction)
    {
        switch (direction)
        {
            case Dir.N: return North;
            case Dir.E: return East;
            case Dir.S: return South;
            case Dir.W: return West;
            case Dir.NE: return NorthEast;
            case Dir.SE: return SouthEast;
            case Dir.SW: return SouthWest;
            case Dir.NW: return NorthWest;
        }
        return Vector2.zero;
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

    public static Dir GetDirection4FromVector(Vector3 v)
    {
        if (Mathf.Abs(v.y) > Mathf.Abs(v.x) && v.y >= 0)
        {
            return Dir.N;
        }
        else if (Mathf.Abs(v.y) > Mathf.Abs(v.x) && v.y < 0)
        {
            return Dir.S;
        }
        if (Mathf.Abs(v.y) <= Mathf.Abs(v.x) && v.x >= 0)
        {
            return Dir.E;
        }
        else if (Mathf.Abs(v.y) <= Mathf.Abs(v.x) && v.x < 0)
        {
            return Dir.W;
        }

        //The above cases should be sufficient
        return Dir.N;
    }

    public static Dir GetDirection8FromVector(Vector2 v)
    {
        if (v.x == 0.0f)
        {
            return v.y > 0.0f ? Dir.N : Dir.S;
        }

        float ratio = Mathf.Abs(v.y / v.x);
        if (v.y > 0.0f)
        {
            if (ratio > 2.414f) // 67.5 to 112.5 degrees
            {
                return Dir.N;
            }
            else if (ratio > 0.414f) // 22.5 to 67.5, and 112.5 to 157.5
            {
                return v.x >= 0.0f ? Dir.NE : Dir.NW;
            }
            else
            {
                return v.x >= 0.0 ? Dir.E : Dir.W;
            }
        }
        else
        {
            if (ratio > 2.414f) // 67.5 to 112.5 degrees
            {
                return Dir.S;
            }
            else if (ratio > 0.414f) // 22.5 to 67.5, and 112.5 to 157.5
            {
                return v.x >= 0.0f ? Dir.SE : Dir.SW;
            }
            else
            {
                return v.x >= 0.0 ? Dir.E : Dir.W;
            }
        }
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

    internal static HookablePart GetHookablePart(Collider2D col)
    {
        return col.GetComponent<HookablePart>()
            ?? col.GetComponentInParent<HookablePart>();
    }

    public enum Attack { HOOK, SWORD, DASH, CHARGE, HOOKCHARGE };

}
