using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CoordinateToSceneHandler
{
    public static Dictionary<(int, int), string> loadRandomGeneratedMap(List<(int, int)> coordinateMap)
    {
        List<string> all = LabRandomRooms.allDirPossible;
        List<string> allButNorth = (List<string>)LabRandomRooms.allDirButNorthPossible; allButNorth.AddRange(all);
        List<string> allButSouth = (List<string>)LabRandomRooms.allDirButSouthPossible; allButSouth.AddRange(all);
        List<string> allButEast = (List<string>)LabRandomRooms.allDirButEastPossible; allButEast.AddRange(all);
        List<string> allButWest = (List<string>)LabRandomRooms.allDirButWestPossible; allButWest.AddRange(all);
        List<string> NAndE = (List<string>)LabRandomRooms.northAndEastEntrancePossible; NAndE.AddRange(allButSouth);
        List<string> NAndW = (List<string>)LabRandomRooms.northAndWestEntrancePossible; NAndW.AddRange(allButEast);
        List<string> NAndS = (List<string>)LabRandomRooms.northAndSouthEntrancePossible; NAndS.AddRange(allButWest);
        List<string> SAndE = (List<string>)LabRandomRooms.southAndEastEntrancePossible; SAndE.AddRange(allButNorth);
        List<string> SAndW = (List<string>)LabRandomRooms.southandWestEntrancePossible; SAndW.AddRange(allButEast);
        List<string> EAndW = (List<string>)LabRandomRooms.eastAndWestEntrancePossible; EAndW.AddRange(allButSouth);
        List<string> northEnts = (List<string>)LabRandomRooms.northEntrancePossible; northEnts.AddRange(NAndS);
        List<string> southEnts = (List<string>)LabRandomRooms.southEntrancePossible; southEnts.AddRange(SAndE);
        List<string> eastEnts = (List<string>)LabRandomRooms.eastEntrancePossible; eastEnts.AddRange(EAndW);
        List<string> westEnts = (List<string>)LabRandomRooms.westEntrancePossible; westEnts.AddRange(NAndW);


        Dictionary<(int, int), string> sceneMap = new Dictionary<(int, int), string>();
        {
            foreach ((int, int) coordinate in coordinateMap)
            {
                (int, int) north = (coordinate.Item1, coordinate.Item2 + 1);
                (int, int) south = (coordinate.Item1, coordinate.Item2 - 1);
                (int, int) west = (coordinate.Item1 - 1, coordinate.Item2);
                (int, int) east = (coordinate.Item1 + 1, coordinate.Item2);
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
                (int, int, int, int) roomEntrancesNeeded = (northEntrance, southEntrance, westEntrance, eastEntrance);

                string roomSceneToLoad = "LabEnv 26";
                //switch (roomEntrancesNeeded)
                //{
                //    case (0, 0, 0, 0): roomSceneToLoad = null; break;
                //    case (1, 0, 0, 0): roomSceneToLoad = northEnts[Random.Range(0, northEnts.Count - 1)]; break;
                //    case (1, 1, 0, 0): roomSceneToLoad = NAndS[Random.Range(0, NAndS.Count - 1)]; break;
                //    case (1, 1, 1, 0): roomSceneToLoad = allButEast[Random.Range(0, allButEast.Count - 1)]; break;
                //    case (0, 1, 0, 0): roomSceneToLoad = southEnts[Random.Range(0, southEnts.Count - 1)]; break;
                //    case (0, 1, 1, 0): roomSceneToLoad = SAndW[Random.Range(0, SAndW.Count - 1)]; break;
                //    case (0, 1, 1, 1): roomSceneToLoad = allButNorth[Random.Range(0, allButNorth.Count - 1)]; break;
                //    case (0, 0, 1, 0): roomSceneToLoad = eastEnts[Random.Range(0, eastEnts.Count - 1)]; break;
                //    case (0, 0, 1, 1): roomSceneToLoad = EAndW[Random.Range(0, EAndW.Count - 1)]; break;
                //    case (1, 0, 1, 1): roomSceneToLoad = allButSouth[Random.Range(0, allButSouth.Count - 1)]; break;
                //    case (0, 0, 0, 1): roomSceneToLoad = eastEnts[Random.Range(0, eastEnts.Count - 1)]; break;
                //    case (1, 0, 0, 1): roomSceneToLoad = NAndE[Random.Range(0, NAndE.Count - 1)]; break;
                //    case (1, 1, 0, 1): roomSceneToLoad = allButWest[Random.Range(0, allButWest.Count - 1)]; break;
                //    case (1, 1, 1, 1): roomSceneToLoad = all[Random.Range(0, all.Count - 1)]; break;
                //    case (1, 0, 1, 0): roomSceneToLoad = NAndW[Random.Range(0, NAndW.Count - 1)]; break;
                //    case (0, 1, 0, 1): roomSceneToLoad = SAndE[Random.Range(0, SAndE.Count - 1)]; break;
                //}
                sceneMap.Add(coordinate, roomSceneToLoad);
            }
        }
        return sceneMap;
    }
}
