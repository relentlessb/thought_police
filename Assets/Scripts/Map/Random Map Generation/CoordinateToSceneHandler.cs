using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CoordinateToSceneHandler
{
    public static Dictionary<(int, int), string> loadRandomGeneratedMap(List<(int, int)> coordinateMap, int dungeon)
    {
        List<string> all = RandomRooms.allDirPossible;
        List<string> allButNorth = ((List<string>)RandomRooms.allDirButNorthPossible.Concat(all));
        List<string> allButSouth = ((List<string>)RandomRooms.allDirButSouthPossible.Concat(all));
        List<string> allButEast = ((List<string>)RandomRooms.allDirButEastPossible.Concat(all));
        List<string> allButWest = ((List<string>)RandomRooms.allDirButWestPossible.Concat(all));
        List<string> NAndE = ((List<string>)RandomRooms.northAndEastEntrancePossible.Concat(allButSouth));
        List<string> NAndW = ((List<string>)RandomRooms.northAndWestEntrancePossible.Concat(allButEast));
        List<string> NAndS = ((List<string>)RandomRooms.northAndSouthEntrancePossible.Concat(allButWest));
        List<string> SAndE = ((List<string>) RandomRooms.southAndEastEntrancePossible.Concat(allButNorth));
        List<string> SAndW = ((List<string>)RandomRooms.southandWestEntrancePossible.Concat(allButEast));
        List<string> EAndW = ((List<string>)RandomRooms.eastAndWestEntrancePossible.Concat(allButSouth));
        List<string> northEnts = ((List<string>)RandomRooms.northEntrancePossible.Concat(NAndE));
        List<string> southEnts = ((List<string>)RandomRooms.southEntrancePossible.Concat(SAndW));
        List<string> eastEnts = ((List<string>)RandomRooms.eastEntrancePossible.Concat(EAndW));
        List<string> westEnts = ((List<string>)RandomRooms.westEntrancePossible.Concat(NAndW));


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
                    case (1, 0, 0, 0): roomSceneToLoad = northEnts[Random.Range(0, northEnts.Count - 1)]; break;
                    case (1, 1, 0, 0): roomSceneToLoad = NAndS[Random.Range(0, NAndS.Count - 1)]; break;
                    case (1, 1, 1, 0): roomSceneToLoad = allButWest[Random.Range(0, allButWest.Count - 1)]; break;
                    case (0, 1, 0, 0): roomSceneToLoad = southEnts[Random.Range(0, southEnts.Count - 1)]; break;
                    case (0, 1, 1, 0): roomSceneToLoad = SAndE[Random.Range(0, SAndE.Count - 1)]; break;
                    case (0, 1, 1, 1): roomSceneToLoad = allButNorth[Random.Range(0, allButNorth.Count - 1)]; break;
                    case (0, 0, 1, 0): roomSceneToLoad = eastEnts[Random.Range(0, eastEnts.Count - 1)]; break;
                    case (0, 0, 1, 1): roomSceneToLoad = EAndW[Random.Range(0, EAndW.Count - 1)]; break;
                    case (1, 0, 1, 1): roomSceneToLoad = allButSouth[Random.Range(0, allButSouth.Count - 1)]; break;
                    case (0, 0, 0, 1): roomSceneToLoad = westEnts[Random.Range(0, westEnts.Count - 1)]; break;
                    case (1, 0, 0, 1): roomSceneToLoad = NAndW[Random.Range(0, NAndW.Count - 1)]; break;
                    case (1, 1, 0, 1): roomSceneToLoad = allButEast[Random.Range(0, allButEast.Count - 1)]; break;
                    case (1, 1, 1, 1): roomSceneToLoad = all[Random.Range(0, all.Count - 1)]; break;
                    case (1, 0, 1, 0): roomSceneToLoad = NAndE[Random.Range(0, NAndE.Count - 1)]; break;
                    case (0, 1, 0, 1): roomSceneToLoad = SAndW[Random.Range(0, SAndW.Count - 1)]; break;
                }

            }
        }
        return sceneMap;
    }
}
