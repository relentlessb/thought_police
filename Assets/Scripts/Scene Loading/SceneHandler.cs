using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class SceneHandler : MonoBehaviour
{
    public List<GameObject> doorList = new List<GameObject>();
    public GameObject door;
    public MainCharacterHandler characterHandler;
    public Player player;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        createRandomDungeon(door, doorList,characterHandler, player);
    }
    void createRandomDungeon(GameObject door, List<GameObject> doorList, MainCharacterHandler characterHandler, Player player)
    {
        List<(int,int)> randomMap = CoordinateMapGenerator.GenerateRandomMap(10,5);
        List<((int, int), (int, int))>  doorDictionary = RandomCoordinateConnector.dungeonPathsFromOrigin(randomMap);
        Dictionary<(int, int), string>  sceneMap = CoordinateToSceneHandler.loadRandomGeneratedMap(randomMap);
        CreateDirDoors(door, doorList);
        LoadNextScene(sceneMap[(0, 0)], doorList);
        (int, int) startPos = (0, 0);
        MainCharacterHandler characterHandlerObject = Instantiate(characterHandler);
        characterHandlerObject.sceneMap = sceneMap;
        characterHandlerObject.currentPos = startPos;
        characterHandlerObject.doorList = doorList;
        characterHandlerObject.doorDictionary = doorDictionary;
        DontDestroyOnLoad(characterHandlerObject);
        activateRoomDoors(doorDictionary, startPos, doorList);

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
        MainCharacterHandler characterHandlerObject = Instantiate(characterHandler);
        characterHandlerObject.sceneMap = sceneMap;
        characterHandlerObject.currentPos = startPos;
        characterHandlerObject.doorList = doorList;
        characterHandlerObject.doorDictionary = doorDictionary;
        DontDestroyOnLoad(characterHandlerObject);
        activateRoomDoors(doorDictionary, startPos, doorList);
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
            doorList.Add(newDoor);
            switch (i)
            {
                case 0: newDoor.name = "North Door"; break;
                case 1: newDoor.name = "South Door"; break;
                case 2: newDoor.name = "East Door"; break;
                case 3: newDoor.name = "West Door"; break;
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
                case "North Door": door.transform.position = new Vector3(0, roomSizeY / 2 - .25f, 0); break;
                case "South Door": door.transform.position = new Vector3(0, -(roomSizeY / 2) + .5f, 0); break;
                case "East Door": door.transform.position = new Vector3(-(roomSizeX / 2) + .25f, 0, 0); break;
                case "West Door": door.transform.position = new Vector3(roomSizeX / 2 - .25f, 0, 0); break;
            }
        }
    }
}