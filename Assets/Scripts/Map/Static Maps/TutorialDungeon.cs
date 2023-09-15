using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TutorialDungeon
{
    public static Dictionary<(int, int), string> sceneMap = new Dictionary<(int, int), string>
    {
        {(0,0),"TutorialRoom1"},
        {(1,0),"TutorialRoom2"},
        {(2,0),"TutorialRoom3"},
        {(3,0),"TutorialRoom4"},
        {(4,0),"TutorialRoom5"},
    };
    public static Dictionary<(int, int), (int, int)> doorDictionary = new Dictionary<(int, int), (int, int)>
    {
        {(0,0),(1,0)},
        {(1,0),(2,0)},
        {(2,0),(3,0)},
        {(3,0),(4,0)},
        {(4,0),(5,0)}
    };
}
