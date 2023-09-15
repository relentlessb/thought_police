using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

public static class CoordinateMapGenerator
{
    public static List<(int, int)> GenerateRandomMap(int amountOfRooms, int mapSize)
    {
        List<(int, int)> roomCoordinates = new List<(int, int)>
        {
            (0, 0)
        };
        //Select Map Rooms
        while (amountOfRooms > roomCoordinates.Count)
        {
            //Create Filter Lists
            List<(int, int)> connectingRooms = new List<(int, int)>();
            List<(int, int)> potentialRoomCoordinates = new List<(int, int)>();

            //Find Connecting Rooms
            foreach ((int, int) coordinate in roomCoordinates)
            {
                (int, int) north = (coordinate.Item1, coordinate.Item2 + 1);
                (int, int) south = (coordinate.Item1, coordinate.Item2 - 1);
                (int, int) east = (coordinate.Item1 - 1, coordinate.Item2);
                (int, int) west = (coordinate.Item1 + 1, coordinate.Item2);
                connectingRooms.Add(north);
                connectingRooms.Add(south);
                connectingRooms.Add(east);
                connectingRooms.Add(west);
            }

            //Filter Out Rooms Outside of Limit and Already Made Rooms
            foreach ((int, int) room in connectingRooms)
            {
                if (!roomCoordinates.Contains(room) && room.Item1<mapSize && -mapSize<room.Item1 && room.Item2 < mapSize && -mapSize < room.Item2)
                {
                    potentialRoomCoordinates.Add(room);
                }
            }
            connectingRooms.Clear();

            //Select Room from Potential Rooms
            int randomRoomNum = UnityEngine.Random.Range(0, potentialRoomCoordinates.Count-1);
            (int, int) selectedRoom = potentialRoomCoordinates[randomRoomNum];
            roomCoordinates.Add(selectedRoom);
            potentialRoomCoordinates.Clear();
        }
        return roomCoordinates;
    }
}
