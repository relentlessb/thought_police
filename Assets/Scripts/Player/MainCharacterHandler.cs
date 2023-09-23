using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MainCharacterHandler : MonoBehaviour
{
    public Sprite pathosSprite;
    public Sprite ethosSprite;
    public Sprite logosSprite;
    List<Dictionary<string, float>> charStats = new List<Dictionary<string, float>>();
    List<Sprite> charSprites = new List<Sprite>();
    //Switch Character Functionality
    int startingCharacter = 0;
    public void switchCharacter(Dictionary<string, float> charStats, Sprite sprite, Player playerInstance)
    {
        playerInstance.GetComponent<SpriteRenderer>().sprite = sprite;
        playerInstance.currentStats = charStats;
    }
    void Start()
    {
        //Initial Character Attributes and Abilities
        Dictionary<string, float> pathosStats = new Dictionary<string, float>()
        {
            { "Determination", 3 },
            { "Confidence", 4},
            { "Wit", 3},
            { "Morale", 0 },
            { "Focus", 0 },
            { "Damage", 5 },
            { "Size", 1 },
            { "Offset X", 0 },
            { "Offset Y", 0 },
            { "Speed", 1 }
        };
        Dictionary<string, float> ethosStats = new Dictionary<string, float>()
        {
            { "Determination", 2 },
            { "Confidence", 8 },
            { "Wit", 5},
            { "Morale", 2},
            { "Focus", 0},
            { "Damage", 5},
            { "Size", 1},
            { "Offset X", 0},
            { "Offset Y", 0},
            { "Speed", 3}
        };
        Dictionary<string, float> logosStats = new Dictionary<string, float>()
        {
            { "Determination", 2 },
            { "Confidence", 3 },
            { "Wit", 6},
            { "Morale", 0},
            { "Focus", 2},
            { "Damage", 4},
            { "Size", 1 },
            { "Offset X", 4},
            { "Offset Y", 4},
            { "Speed", 5}
        };
        charStats.Add(pathosStats); charStats.Add(ethosStats); charStats.Add(logosStats);
        //Add Sprite to Sprite List
        charSprites.Add(pathosSprite); charSprites.Add(ethosSprite); charSprites.Add(logosSprite);
        // Instantiate Player Method
        instantiatePlayer(charStats[startingCharacter]);
    }
    //Create Player Instance
    Player player;
    public Player basePlayer;
    public Dictionary<(int, int), string> sceneMap;
    public (int, int) currentPos;
    public List<GameObject> doorList;
    public List<((int, int), (int, int))> doorDictionary;
    public void instantiatePlayer(Dictionary<string, float> defaultStats)
    {
        player = Instantiate(basePlayer, transform.parent = this.transform);
        player.currentStats = defaultStats;
        player.currentPos = currentPos;
        player.sceneMap= sceneMap;
        player.doorList = doorList;
        player.doorDictionary = doorDictionary;
        player.charStats = charStats;
        player.charSprites = charSprites;
        
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