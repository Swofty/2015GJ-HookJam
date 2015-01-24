using UnityEngine;
using System.Collections;

public static class Constants{

    public enum Dir { N, E, S, W };

    public static Vector2 getDirectionVector(Dir direction)
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
}
