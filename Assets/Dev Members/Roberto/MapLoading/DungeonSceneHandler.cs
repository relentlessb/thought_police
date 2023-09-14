using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor.U2D.Path;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class DungeonSceneHandler : MonoBehaviour
{
    public static int amountOfRooms;
    public static int mapSize;
    public List<(int, int)> randomMap = CoordinateMapGenerator.GenerateRandomMap(amountOfRooms, mapSize);
    public GameObject southDoor;
    public GameObject roomTilemap;
    BoxCollider2D doorSTrigger;
    public Collider2D playerCollider;
    public (int, int) currentRoom;
    public Dictionary<(int, int), string> sceneMap;
    public GameObject player;
    public List<((int, int), (int, int))> doorDictionary;
    public void Start()
    {
        roomTilemap = GameObject.FindGameObjectWithTag("RoomTilemap");
        roomTilemap.GetComponent<Tilemap>().CompressBounds();
        List<((int, int), (int, int))> doorDictionary = RandomCoordinateConnector.dungeonPathsFromOrigin(randomMap);
        doorSTrigger = southDoor.GetComponent<BoxCollider2D>();
        
    }
    public void Update()
    {
        if (doorSTrigger.IsTouching(playerCollider))        
        {
            string direction = "south";
            loadRoom(sceneMap, direction, currentRoom, player, doorDictionary);
        }
    }
    public void loadRoom(Dictionary<(int, int), string> sceneMap, string direction, (int, int) currentRoom, GameObject player, List<((int, int), (int, int))> doorDictionary)
    {
        (int, int) destinationRoom = currentRoom;
        switch (direction)
        {
            default: destinationRoom = currentRoom; break;
            case "north": destinationRoom = (currentRoom.Item1, currentRoom.Item2 + 1); break;
            case "south": destinationRoom = (currentRoom.Item1, currentRoom.Item2 - 1); break;
            case "west": destinationRoom = (currentRoom.Item1 + 1, currentRoom.Item2); break;
            case "east": destinationRoom = (currentRoom.Item1 - 1, currentRoom.Item2); break;
        }
        string sceneName = sceneMap[destinationRoom];
        DontDestroyOnLoad(player);
        SceneManager.LoadScene(sceneName);
        bool loadDoorN = false;
        bool loadDoorS = false;
        bool loadDoorE = false;
        bool loadDoorW = false;
        foreach(((int, int), (int, int)) coordinatePair in doorDictionary)
        {
            if (coordinatePair.Item1 == destinationRoom)
            {
                (int, int) coordDirection = (coordinatePair.Item2.Item1-coordinatePair.Item1.Item1, coordinatePair.Item2.Item2 - coordinatePair.Item1.Item2);
                switch (coordDirection)
                {
                    case (0, 1): loadDoorN = true; break;
                    case (0, -1): loadDoorS = true; break;
                    case (-1, 0): loadDoorE = true; break;
                    case (1, 0): loadDoorW = true; break;
                }
            }
            if(coordinatePair.Item2 == destinationRoom)
            {
                {
                    (int, int) coordDirection = (coordinatePair.Item1.Item1 - coordinatePair.Item2.Item1, coordinatePair.Item1.Item2 - coordinatePair.Item2.Item2);
                    switch (coordDirection)
                    {
                        case (0, 1): loadDoorN = true; break;
                        case (0, -1): loadDoorS = true; break;
                        case (-1, 0): loadDoorE = true; break;
                        case (1, 0): loadDoorW = true; break;
                    }
                }
            }
        }
        if (loadDoorS == true)
        {
            GameObject doorS = Instantiate(southDoor, transform.parent = roomTilemap.transform);
            Vector3 roomSize = roomTilemap.GetComponent<Tilemap>().size;
            doorS.transform.localPosition = new Vector3(0, -(roomSize.y / 2));
        }
    }
}
