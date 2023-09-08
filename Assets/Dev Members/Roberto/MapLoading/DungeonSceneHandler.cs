using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonSceneHandler : MonoBehaviour
{
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
    }
}
