using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public static class RandomCoordinateConnector
{
    public static Dictionary<int, List<(int, int)>> dungeonPathsFromOrigin(List<(int, int)> randomMap)
    {
        Dictionary<int, List<(int, int)>> originPaths = new Dictionary<int, List<(int, int)>> { };
        bool fullyConnected = false;
        int totalPaths = 0;
        while(fullyConnected == false)
        {
            List<(int, int)> createdPath = createOriginPath(randomMap);
            totalPaths += 1;
            originPaths.Add(totalPaths, createdPath);
            foreach((int, int) coordinate in randomMap)
            {
                bool found = false;
                while (found == false)
                {
                    foreach (List<(int, int)> path in originPaths.Values)
                    {
                        if (path.Contains(coordinate))
                        {
                            found = true;
                        }
                    }
                    break;
                }
                if (found == false)
                {
                    break;
                }
                fullyConnected = true;
            }
        }
        return originPaths;
    }
    static List<(int, int)> createOriginPath(List<(int, int)> randomMap)
    {
        List<(int, int)> path = new List<(int, int)> { };
        int direction = Random.Range(0, 3);
        int x = 0;
        int y = 0;
        switch (direction)
        {
            case 0: x = 1; y = 1; break;
            case 1: x = 1; y = -1; break;
            case 2: x = -1; y = -1; break;
            case 3: x = -1; y = 1; break;
        }
        bool end = false;
        path.Add((0, 0));
        while (end == false)
        {
            int item = Random.Range(0, 1);
            (int, int) checkRoom = (100, 100);
            bool checkedX = false;
            bool checkedY = false;
            switch (item)
            {
                case 0: checkRoom = (path.Last().Item1 + x, path.Last().Item2); checkedX = true; break;
                case 1: checkRoom = (path.Last().Item1, path.Last().Item2 + y);  checkedY = true;  break;
            }
            if (randomMap.Contains(checkRoom))
            {
                path.Add(checkRoom);
                checkedX = false; checkedY = false;
            }
            else
            {
                if(checkedX == true && checkedY == true)
                {
                    end = true;
                }
            }

        }
        return path;
    }
}
