using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordinateToSceneHandler : MonoBehaviour
{
    public int dungeon;
    public List<(int, int)> tutorialDungeon = new List<(int, int)>
    {
        (0,0),
        (1,0),
        (2,0),
        (3,0),
        (4,0),
    };
    private Dictionary<(int, int), string> loadMainDungeonSceneMap(List<(int, int)> coordinateMap, int dungeon)
    {
        Dictionary<(int, int), string> sceneMap = new Dictionary<(int, int), string>();
        if (dungeon == 0)
        {
            sceneMap.Add(coordinateMap[0], "tutorialMapRoomOne");
            sceneMap.Add(coordinateMap[0], "tutorialMapRoomTwo");
            sceneMap.Add(coordinateMap[0], "tutorialMapRoomThree");
            sceneMap.Add(coordinateMap[0], "tutorialMapRoomFour");
            sceneMap.Add(coordinateMap[0], "tutorialMapRoomFive");
        }
        return sceneMap;
    }
}
