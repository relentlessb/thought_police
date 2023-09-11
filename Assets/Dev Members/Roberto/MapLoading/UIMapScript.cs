using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMapScript : MonoBehaviour
{
    public int amountOfRooms;
    public int mapSize;
    public float uiMapRoomDistance;
    public int mapScale;
    public GameObject uiRoomSquare;
    public GameObject uiRoomConnection;
    List<(int, int)> randomMap;
    List<((int, int), (int, int))> roomConnections;
    void Start()
    {
        randomMap = CoordinateMapGenerator.GenerateRandomMap(amountOfRooms, mapSize);
        float uiRoomScaleX = .5f;
        Vector3 uiRoomScale = new Vector3(uiRoomScaleX, uiRoomScaleX, 1);
        foreach ((int, int) room in randomMap)
        {
            Vector3 roomPos = new Vector3(room.Item1, room.Item2, 1);
            GameObject roomSquare = Instantiate(uiRoomSquare, transform.parent);
            roomSquare.transform.position = roomPos;
            roomSquare.transform.localScale = uiRoomScale;
        }
        List<((int, int), (int, int))> doorDictionary = RandomCoordinateConnector.dungeonPathsFromOrigin(randomMap);
        foreach (((int,int),(int,int)) coordinatePair in doorDictionary)
        {
            Vector3 uiConnectionScale = new Vector3(.1f,.8f,1);
            GameObject roomSquare = Instantiate(uiRoomConnection);
            Vector3 connectionPos = new Vector3((coordinatePair.Item2.Item1+coordinatePair.Item1.Item1)/2f, (coordinatePair.Item2.Item2+coordinatePair.Item1.Item2)/2f, 1);
            roomSquare.transform.position = connectionPos;
            if (coordinatePair.Item1.Item1 != coordinatePair.Item2.Item1)
            {
                uiConnectionScale = new Vector3(.8f, .1f, 1);
            }
            roomSquare.transform.localScale = uiConnectionScale;
        }

    }
}
