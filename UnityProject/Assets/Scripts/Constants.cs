﻿using UnityEngine;
using System.Collections;

public static class Constants{

    public enum Dir { N, E, S, W, NE, SE, SW, NW };

    public static KeyCode HookKey = KeyCode.Z;
    public static KeyCode SwordKey = KeyCode.X;

    public static Vector2 getVectorFromDirection(Dir direction)
    {
        switch (direction)
        {
            case Dir.N: return new Vector2(0.0f, 1.0f);
            case Dir.E: return new Vector2(1.0f, 0.0f);
            case Dir.S: return new Vector2(0.0f, -1.0f);
            case Dir.W: return new Vector2(-1.0f, 0.0f);
        }
        return new Vector2(0.0f, 0.0f);
    }

    public static Dir getDirectionFromVector(Vector3 direction_vector)
    {
        if (direction_vector.y >= direction_vector.x && direction_vector.y >= 0)
        {
            return Dir.N;
        }
        else if (direction_vector.y >= direction_vector.x && direction_vector.y < 0)
        {
            return Dir.S;
        }
        if (direction_vector.x > direction_vector.y && direction_vector.x >= 0)
        {
            return Dir.E;
        }
        else if (direction_vector.x > direction_vector.y && direction_vector.x < 0)
        {
            return Dir.W;
        }

        //The above cases should be sufficient
        return Dir.N;
    }

    public static bool isWallTag(string str)
    {
        return false;
    }	public enum Attack { HOOK, SWORD, DASH };}
