using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMapHandler : MonoBehaviour
{
    public GameObject mapRoom;
    public Dictionary<(int, int), string> sceneMap;
    public (int, int) currentPos;
    public bool updateMap = false;
    Dictionary<(int, int), GameObject> mapElementList = new Dictionary<(int, int), GameObject>();
    void GenerateMapElements(GameObject mapRoom, Dictionary<(int,int),GameObject> mapElementList, Dictionary<(int,int), string> sceneMap)
    {
        for (int i = -3; i <= 3; i++)
        {
            for (int c = -3; c <= 3; c++)
            {
                if (c != 0 || i != 0)
                {
                    GameObject mapCoordEle = Instantiate(mapRoom, transform.parent = this.transform);
                    Vector2 coordPos = new Vector2(c * .4f, i * .4f);
                    mapCoordEle.transform.localPosition = coordPos;
                    mapCoordEle.SetActive(false);
                    mapElementList.Add((c, i), mapCoordEle);
                    mapElementList[(c,i)].name = (c, i).ToString();
                }
            }
        }
    }
    private void Start()
    {
        GenerateMapElements(mapRoom, mapElementList, sceneMap);
    }
    private void Update()
    {
        if (updateMap == true)
        {
            foreach((int,int) room in mapElementList.Keys)
            {
                mapElementList[room].SetActive(false);
            }
            foreach((int,int) room in sceneMap.Keys)
            {
                (int, int) roomToActivate = (room.Item1 - currentPos.Item1, room.Item2 - currentPos.Item2);
                if (roomToActivate.Item1>=-3&& roomToActivate.Item1 <= 3 && roomToActivate.Item2 >= -3 && roomToActivate.Item2 <= 3 && roomToActivate!=(0,0))
                {
                    mapElementList[roomToActivate].SetActive(true);
                }
            }
            updateMap= false;
        }
    }
}
