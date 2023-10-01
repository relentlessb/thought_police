using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TutorialDungeon
{
    public static Dictionary<(int, int), string> sceneMap = new Dictionary<(int, int), string>
    {
        {(0,0),"Tutorial Room 1"},
        {(1,0),"Tutorial Room 2"},
        {(2,0),"Tutorial Room 3"},
        {(3,0),"Tutorial Room 4"},
        {(4,0),"Tutorial Room 5"},
    };
    public static List<((int, int), (int, int))> doorDictionary = new List<((int, int), (int, int))>
    {
        {((0,0),(1,0))},
        {((1, 0),(2, 0))},
        {((2, 0),(3, 0))},
        {((3, 0),(4, 0))}
    };
}
