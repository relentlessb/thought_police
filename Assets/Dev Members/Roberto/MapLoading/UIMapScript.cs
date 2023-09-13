using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMapScript : MonoBehaviour
{
    public float uiMapRoomDistance;
    public int mapScale;
    public GameObject uiRoomSquare;
    public GameObject uiRoomConnection;
    public float descale;
    public DungeonSceneHandler sceneHandler;
    void Start()
    {
        List<(int, int)> map = sceneHandler.randomMap;
       // List<((int, int), (int, int))> doorDictionary = sceneHandler.doorDictionary;
        float uiRoomScaleX = .1f;
        Vector3 uiRoomScale = new Vector3(uiRoomScaleX, uiRoomScaleX, 1);
        Transform mapPos = this.transform;
        foreach ((int, int) room in map)
        {
            Vector3 roomPos = new Vector3(room.Item1/descale, room.Item2/descale, 1);
            GameObject roomSquare = Instantiate(uiRoomSquare, transform.parent = mapPos);
            roomSquare.transform.localPosition = roomPos;
            roomSquare.transform.localScale = uiRoomScale;
        }
      //  foreach (((int,int),(int,int)) coordinatePair in doorDictionary)
        {
            Vector3 uiConnectionScale = new Vector3(.04f,.3f,1);
            GameObject roomSquare = Instantiate(uiRoomConnection, transform.parent = mapPos);
          //  Vector3 connectionPos = new Vector3(((coordinatePair.Item2.Item1+coordinatePair.Item1.Item1)/2f)/descale, ((coordinatePair.Item2.Item2+coordinatePair.Item1.Item2)/2f)/descale, 1);
            //roomSquare.transform.localPosition = connectionPos;
            //if (coordinatePair.Item1.Item1 != coordinatePair.Item2.Item1)
            {
                uiConnectionScale = new Vector3(.3f, .04f, 1);
            }
            roomSquare.transform.localScale = uiConnectionScale;
        }

    }
}
