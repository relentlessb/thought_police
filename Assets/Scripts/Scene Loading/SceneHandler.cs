using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.Events;

public class SceneHandler : MonoBehaviour
{
    List<GameObject> doorList = new List<GameObject>();
    public Dictionary<(int, int), string> sceneMap;
    List<((int, int), (int, int))> doorDictionary;
    [SerializeField] GameObject door;
    [SerializeField] MainCharacterHandler characterHandler;
    [SerializeField] Player player;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        StartPremadeDungeon(0, sceneMap, doorDictionary, door, doorList, characterHandler, player);
    }
    void StartPremadeDungeon(int dungeonNum, Dictionary<(int, int), string> sceneMap, List<((int, int), (int, int))> doorDictionary, GameObject door, List<GameObject> doorList, MainCharacterHandler characterHandler, Player player)
    {
        switch (dungeonNum)
        {
            case 0: sceneMap = TutorialDungeon.sceneMap; doorDictionary = TutorialDungeon.doorDictionary; break;
        }
        CreateDirDoors(door, doorList);
        LoadNextScene(sceneMap[(0, 0)], doorList);
        (int, int) startPos = (0, 0);
        activateRoomDoors(doorDictionary, startPos, doorList);
        MainCharacterHandler characterHandlerObject = Instantiate(characterHandler);
        characterHandlerObject.sceneMap = sceneMap;
        characterHandlerObject.currentPos = startPos;
        characterHandlerObject.doorList = doorList;
        characterHandlerObject.doorDictionary = doorDictionary;
        DontDestroyOnLoad(characterHandlerObject);
    }
    void LoadNextScene(string sceneName, List<GameObject> doorList)
    {
        SceneManager.LoadScene(sceneName);
        foreach (GameObject door in doorList)
        {
            door.SetActive(false);
        }
    }
    void CreateDirDoors(GameObject door, List<GameObject> doorList)
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject newDoor = Instantiate(door);
            DontDestroyOnLoad(newDoor);
            Tilemap tilemap = GameObject.FindWithTag("RoomTilemap").GetComponent<Tilemap>();
            tilemap.CompressBounds();
            float roomSizeX = tilemap.size.x;
            float roomSizeY = tilemap.size.y;
            doorList.Add(newDoor);
            switch (i)
            {
                case 0: newDoor.name = "North Door"; newDoor.transform.position = new Vector3(0, roomSizeY / 2 - 1, 0); break;
                case 1: newDoor.name = "South Door"; newDoor.transform.position = new Vector3(0, -(roomSizeY / 2) + 1, 0); break;
                case 2: newDoor.name = "East Door"; newDoor.transform.position = new Vector3(-(roomSizeX / 2) + 1, 0, 0); break;
                case 3: newDoor.name = "West Door"; newDoor.transform.position = new Vector3(roomSizeX / 2 - 1, 0, 0); break;
            }
        }
    }
    public void getDoorScene(List<((int, int), (int, int))> doorDictionary, Dictionary<(int, int), string> sceneMap, (int, int) currentPos, List<GameObject> doorList)
    {
        string sceneName = sceneMap[currentPos];
        LoadNextScene(sceneName, doorList);
        activateRoomDoors(doorDictionary, currentPos, doorList);
    }
    void activateRoomDoors(List<((int, int), (int, int))> doorDictionary, (int, int) currentPos, List<GameObject> doorList)
    {
        foreach (((int, int), (int, int)) doorConnector in doorDictionary)
        {
            if (doorConnector.Item1 == currentPos)
            {
                switch ((doorConnector.Item2.Item1 - currentPos.Item1, doorConnector.Item2.Item2 - currentPos.Item2))
                {
                    case (0, 1): doorList[0].SetActive(true); break;
                    case (0, -1): doorList[1].SetActive(true); break;
                    case (-1, 0): doorList[2].SetActive(true); break;
                    case (1, 0): doorList[3].SetActive(true); break;

                }
            }
            if (doorConnector.Item2 == currentPos)
            {
                switch ((doorConnector.Item1.Item1 - currentPos.Item1, doorConnector.Item1.Item2 - currentPos.Item2))
                {
                    case (0, 1): doorList[0].SetActive(true); break;
                    case (0, -1): doorList[1].SetActive(true); break;
                    case (-1, 0): doorList[2].SetActive(true); break;
                    case (1, 0): doorList[3].SetActive(true); break;

                }
            }
        }
        Tilemap tilemap = GameObject.FindWithTag("RoomTilemap").GetComponent<Tilemap>();
        tilemap.CompressBounds();
        float roomSizeX = tilemap.size.x;
        float roomSizeY = tilemap.size.y;
        foreach(GameObject door in doorList)
        {
            switch (door.name)
            {
                case "North Door": door.transform.position = new Vector3(0, roomSizeY / 2 - 1, 0); break;
                case "South Door": door.transform.position = new Vector3(0, -(roomSizeY / 2) + 1, 0); break;
                case "East Door": door.transform.position = new Vector3(-(roomSizeX / 2) + 1, 0, 0); break;
                case "West Door": door.transform.position = new Vector3(roomSizeX / 2 - 1, 0, 0); break;
            }
        }
    }
}