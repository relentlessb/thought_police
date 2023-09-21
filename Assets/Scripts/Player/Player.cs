using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public Dictionary<string, float> currentStats;
    [SerializeField] SceneHandler sceneHandler;
    public Dictionary<(int, int), string> sceneMap;
    public List<GameObject> doorList;
    public List<((int, int), (int, int))> doorDictionary;
    [SerializeField] UnityEvent<List<((int, int), (int, int))>, Dictionary<(int, int), string>, (int, int), List<GameObject>> touchedDoor;
    public (int, int) currentPos;

    private void Start()
    {    
        currentStats = new Dictionary<string, float>()
        {
            { "Determination", 3 },
            { "Confidence", 5 },
            { "Wit", 3},
            { "Morale", 0 },
            { "Focus", 0 },
            { "Damage", 5 },
            { "Size", 1 },
            { "Offset X", 0 },
            { "Offset Y", 0 },
            { "Speed", 1 }
        };

}
    public int currentChar = 0;

    private void Update()
    {
        Vector3 playerMovement = new Vector3((Input.GetAxis("Horizontal") * currentStats["Wit"] * Time.deltaTime), (Input.GetAxis("Vertical") * currentStats["Wit"] * Time.deltaTime), 0);
        transform.Translate(playerMovement);
    }
    void OnTriggerEnter2D(UnityEngine.Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Ability":
                {
                    Dictionary<string, float> stats = AbilityList.getStatAbility((int)Variables.Object(other.gameObject).Get("abilityID"));
                    transform.parent.gameObject.GetComponent<MainCharacterHandler>().addStats(stats, currentStats); 
                    break;
                }
            case "Door":
                {
                    switch (other.gameObject.name)
                    {
                        case "North Door": currentPos = (currentPos.Item1, currentPos.Item2 + 1); break;
                        case "South Door": currentPos = (currentPos.Item1, currentPos.Item2 - 1); break;
                        case "East Door": currentPos = (currentPos.Item1 - 1, currentPos.Item2); break;
                        case "West Door": currentPos = (currentPos.Item1 + 1, currentPos.Item2); break;
                    }
                    touchedDoor.Invoke(doorDictionary, sceneMap, currentPos, doorList);
                    transform.position= Vector3.zero;
                    break;
                }
        }
    }
}
