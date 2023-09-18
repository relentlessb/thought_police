using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MainCharacterHandler : MonoBehaviour
{
    //Switch Character Functionality
    public int character;
    public void switchCharacter(int character, Dictionary<string, float> stats)
    {

    }
    void Start()
    {
        //Initial Character Attributes and Abilities
        Dictionary<string, float> pathosStats = new Dictionary<string, float>()
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
        instantiatePlayer(pathosStats);
    }
    //Create Player Instance
    Player player;
    public Player basePlayer;
    void instantiatePlayer(Dictionary<string, float> defaultStats)
    {
        player = Instantiate(basePlayer, transform.parent = this.transform);
        player.currentStats = defaultStats;
        
    }
    public void addStats(Dictionary<string,float> addedStats, Dictionary<string, float> currentStats)
    {
        Debug.Log("Adding Stats");
        foreach(string statName in currentStats.Keys.ToList())
        {
            //Calculate Current Attributes
            if (addedStats.Keys.Contains(statName))
            {
                currentStats[statName] += addedStats[statName];
            }
        }
    }
}