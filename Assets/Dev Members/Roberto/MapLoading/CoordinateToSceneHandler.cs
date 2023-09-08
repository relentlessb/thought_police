using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
            sceneMap.Add(coordinateMap[1], "tutorialMapRoomTwo");
            sceneMap.Add(coordinateMap[2], "tutorialMapRoomThree");
            sceneMap.Add(coordinateMap[3], "tutorialMapRoomFour");
            sceneMap.Add(coordinateMap[4], "tutorialMapRoomFive");
        }
        return sceneMap;
    }
    private Dictionary<(int, int), string> loadRandomGeneratedMap(List<(int, int)> coordinateMap, int dungeon)
    {
        Dictionary<(int, int), string> sceneMap = new Dictionary<(int, int), string>();
        {
            foreach ((int, int) coordinate in coordinateMap)
            {
                (int, int) north = (coordinate.Item1, coordinate.Item2 + 1);
                (int, int) south = (coordinate.Item1, coordinate.Item2 - 1);
                (int, int) east = (coordinate.Item1 - 1, coordinate.Item2);
                (int, int) west = (coordinate.Item1 + 1, coordinate.Item2);
                int northEntrance = 0;
                int southEntrance = 0;
                int eastEntrance = 0;
                int westEntrance = 0;
                if (coordinateMap.Contains(north))
                {
                    northEntrance = 1;
                }
                if (coordinateMap.Contains(south))
                {
                    southEntrance = 1;
                }
                if (coordinateMap.Contains(east))
                {
                    eastEntrance = 1;
                }
                if (coordinateMap.Contains(west))
                {
                    westEntrance = 1;
                }
                (int, int, int, int) roomEntrancesNeeded = (northEntrance, southEntrance, eastEntrance, westEntrance);

                string roomSceneToLoad;
                switch (roomEntrancesNeeded)
                {
                    case (0, 0, 0, 0): roomSceneToLoad = null; break;
                    case (1, 0, 0, 0): roomSceneToLoad = northEntrancePossible[Random.Range(0, northEntrancePossible.Count - 1)]; break;
                    case (1, 1, 0, 0): roomSceneToLoad = northAndSouthEntrancePossible[Random.Range(0, northAndSouthEntrancePossible.Count - 1)]; break;
                    case (1, 1, 1, 0): roomSceneToLoad = allDirButWestPossible[Random.Range(0, allDirButWestPossible.Count - 1)]; break;
                    case (0, 1, 0, 0): roomSceneToLoad = southEntrancePossible[Random.Range(0, southEntrancePossible.Count - 1)]; break;
                    case (0, 1, 1, 0): roomSceneToLoad = southAndEastEntrancePossible[Random.Range(0, southAndEastEntrancePossible.Count - 1)]; break;
                    case (0, 1, 1, 1): roomSceneToLoad = allDirButNorthPossible[Random.Range(0, allDirButNorthPossible.Count - 1)]; break;
                    case (0, 0, 1, 0): roomSceneToLoad = eastEntrancePossible[Random.Range(0, eastEntrancePossible.Count - 1)]; break;
                    case (0, 0, 1, 1): roomSceneToLoad = eastAndWestEntrancePossible[Random.Range(0, eastAndWestEntrancePossible.Count - 1)]; break;
                    case (1, 0, 1, 1): roomSceneToLoad = allDirButSouthPossible[Random.Range(0, eastAndWestEntrancePossible.Count - 1)]; break;
                    case (0, 0, 0, 1): roomSceneToLoad = westEntrancePossible[Random.Range(0, westEntrancePossible.Count - 1)]; break;
                    case (1, 0, 0, 1): roomSceneToLoad = northAndWestEntrancePossible[Random.Range(0, northAndWestEntrancePossible.Count - 1)]; break;
                    case (1, 1, 0, 1): roomSceneToLoad = allDirButEastPossible[Random.Range(0, allDirButEastPossible.Count - 1)]; break;
                    case (1, 1, 1, 1): roomSceneToLoad = allDirPossible[Random.Range(0, allDirPossible.Count - 1)]; break;
                    case (1, 0, 1, 0): roomSceneToLoad = northAndEastEntrancePossible[Random.Range(0, allDirPossible.Count - 1)]; break;
                    case (0, 1, 0, 1): roomSceneToLoad = southandWestEntrancePossible[Random.Range(0, allDirPossible.Count - 1)]; break;
                }

            }
        }
        return sceneMap;
    }
    static List<string> northEntrancePossible = new List<string>
    {
        "Room1"
    };
    static List<string> southEntrancePossible = new List<string>
    {
        "Room1"
    };
    static List<string> eastEntrancePossible = new List<string>
    {
        "Room1"
    };
    static List<string> westEntrancePossible = new List<string>
    {
        "Room1"
    };
    static List<string> northAndSouthEntrancePossible = new List<string>
    {
        "Room1"
    };
    static List<string> northAndWestEntrancePossible = new List<string>
    {
        "Room1"
    };
    static List<string> northAndEastEntrancePossible = new List<string>
    {
        "Room1"
    };
    static List<string> southandWestEntrancePossible = new List<string>
    {
        "Room1"
    };
    static List<string> southAndEastEntrancePossible = new List<string>
    {
        "Room1"
    };
    static List<string> eastAndWestEntrancePossible = new List<string>
    {
        "Room1"
    };
    static List<string> allDirButWestPossible = new List<string>
    {
        "Room1"
    };
    static List<string> allDirButEastPossible = new List<string>
    {
        "Room1"
    };
    static List<string> allDirButNorthPossible = new List<string>
    {
        "Room1"
    };
    static List<string> allDirButSouthPossible = new List<string>
    {
        "Room1"
    };
    static List<string> allDirPossible = new List<string>
    {
        "Room1"
    };
}
