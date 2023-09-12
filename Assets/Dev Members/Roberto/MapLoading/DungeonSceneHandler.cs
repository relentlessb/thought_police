using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonSceneHandler : MonoBehaviour
{
    public static int amountOfRooms;
    public static int mapSize;
    public List<(int, int)> randomMap = CoordinateMapGenerator.GenerateRandomMap(amountOfRooms, mapSize);
    public GameObject southDoor;
    private void Update()
    {
        List<((int, int), (int, int))> doorDictionary = RandomCoordinateConnector.dungeonPathsFromOrigin(randomMap);
    }
    
    public void loadRoom(Dictionary<(int, int), string> sceneMap, string direction, (int, int) currentRoom, GameObject player)
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
        GameObject doorS = Instantiate(southDoor);
    }
}
