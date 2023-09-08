using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class straightSword
{
    static Dictionary<string, int> weaponAttributes = new Dictionary<string, int>()
    {
        {"name", 0 },
        {"type", 0 },
        {"weight", 1},
    };
    static Vector2 hitboxOffset = new Vector2(0,0);
    static Vector2 hitboxScale = new Vector2(1,1);
    static Vector2 hitboxPosition = new Vector2(0,0);
}
