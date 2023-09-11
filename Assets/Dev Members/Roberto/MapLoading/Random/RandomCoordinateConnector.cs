using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public static class RandomCoordinateConnector
{
    public static List<((int, int), (int, int))> dungeonPathsFromOrigin(List<(int, int)> randomMap)
    {
        Dictionary<int, List<(int, int)>> originPaths = new Dictionary<int, List<(int, int)>> { };
        int totalPaths = 0;
        for (int i = 0; i < 50; i++)
        {
            bool retry = false;
            List<(int, int)> createdPath = createOriginPath(randomMap);
            totalPaths += 1;
            originPaths.Add(totalPaths, createdPath);
            foreach ((int, int) coordinate in randomMap)
            {
                bool found = false;
                foreach (List<(int, int)> path in originPaths.Values)
                {
                    if (path.Contains(coordinate))
                    {
                        found = true;  break;
                    }
                }
                if (found == false) { retry = true; break; }
            }
            if (retry == false) break;
        }
        //Merge Connection Paths to Door Dictionary
        List<((int, int), (int, int))> doorConnectionsList = createDoorDictionary(originPaths, randomMap);
        return doorConnectionsList;
    }
    static List<(int, int)> createOriginPath(List<(int, int)> randomMap)
    {
        List<(int, int)> path = new List<(int, int)> { };
        bool end = false;
        path.Add((0, 0));
        bool checkedX = false;
        bool checkedY = false;
        int x = 0;
        int y = 0;
        int direction = UnityEngine.Random.Range(0, 4);
        switch (direction)
        {
            case 0: x = 1; y = 1; break;
            case 1: x = 1; y = -1; break;
            case 2: x = -1; y = -1; break;
            case 3: x = -1; y = 1; break;
        }
        while (end == false)
        {
            int item = UnityEngine.Random.Range(0, 2);
            (int, int) checkRoom = (100, 100);
            switch (item)
            {
                case 0: checkRoom = (path.Last().Item1 + x, path.Last().Item2); checkedX = true; break;
                case 1: checkRoom = (path.Last().Item1, path.Last().Item2 + y); checkedY = true; break;
            }
            if (randomMap.Contains(checkRoom))
            {
                path.Add(checkRoom);
                checkedX = false; checkedY = false;
            }
            else
            {
                if (checkedX == true && checkedY == true)
                {
                    end = true;
                }
            }

        }
        return path;
    }
    public static List<((int, int), (int, int))> createDoorDictionary(Dictionary<int, List<(int, int)>> originPaths, List<(int, int)> randomMap)
    {
        List<((int, int), (int, int))> doorList = new List<((int, int), (int, int))>{};
        foreach(List<(int, int)> path in originPaths.Values)
        {
            (int, int) lastValue = (0, 0);
            foreach((int, int) coordinate in path)
            {
                if (coordinate != lastValue)
                {
                    doorList.Add((lastValue, coordinate));
                    lastValue = coordinate;
                }
            }
        }
        //Delete Unneccessary Connections
        doorList = doorList.Distinct().ToList();
        List<(int, int)> entryDoors = new List<(int, int)>();
        List<(int, int)> exitDoors = new List<(int, int)>();
        foreach (((int, int), (int, int)) coordinatePair in doorList)
        {
            entryDoors.Add(coordinatePair.Item1);
            exitDoors.Add(coordinatePair.Item2);
        }
        List<(int, int)> listEntryDoorsCopy = entryDoors;
        List<(int, int)> listExitDoorsCopy = exitDoors;
        foreach ((int, int) coordinate in exitDoors.ToList())
        {
            var coordinateQuery = exitDoors.Where(coordinates => coordinates == coordinate).ToList();
            int random25Chance = UnityEngine.Random.Range(0, 5);
            if (coordinateQuery.Count() > 1 && random25Chance!=0)
            {
                int index = listExitDoorsCopy.IndexOf(coordinate);
                listEntryDoorsCopy.RemoveAt(index);
                listExitDoorsCopy.RemoveAt(index);
            }
        }
        //Add last minute connections
        foreach ((int, int) coordinate in randomMap)
        {
            if (!exitDoors.Contains(coordinate))
            {
                bool madeConnection = false;
                while (madeConnection == false)
                {
                    int x = 1;
                    int y = 0;
                    int checkDirection = UnityEngine.Random.Range(0, 4);
                    (int, int) checkRoom;
                    switch (checkDirection)
                    {
                        case 0: x = 1; y = 0; break;
                        case 1: x = 0; y = 1; break;
                        case 2: x = -1; y = 0; break;
                        case 3: x = 0; y = -0; break;


                    }
                    checkRoom = (coordinate.Item1 + x, coordinate.Item2 + y);
                    if (exitDoors.Contains(checkRoom))
                    {
                        entryDoors.Add(checkRoom);
                        exitDoors.Add(coordinate);
                        madeConnection = true;
                    }
                }
            }
        }
        doorList.Clear();
        for (int index = 0; index < listEntryDoorsCopy.Count(); index++)
        {
            doorList.Add((listEntryDoorsCopy[index], listExitDoorsCopy[index]));
        }
        return doorList;
    }
}
