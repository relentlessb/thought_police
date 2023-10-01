using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    // scene variables
    [SerializeField] SceneHandler sceneHandler;
    public Dictionary<(int, int), string> sceneMap;

    // UI variables
    [SerializeField] UIMapHandler map;
    UIMapHandler mapObj;

    // camera variables
    [SerializeField] Camera cameraObject;
    Camera MainCamera;

    // door variables
    public List<GameObject> doorList;
    public List<((int, int), (int, int))> doorDictionary;
    public int doorEnterDistance;
    [SerializeField] UnityEvent<List<((int, int), (int, int))>, Dictionary<(int, int), string>, (int, int), List<GameObject>> touchedDoor;

    // player stat variables
    public Dictionary<string, float> currentStats;
    public List<Dictionary<string, float>> charStats;
    public List<Dictionary<string, float>> charStatsBase;

    // player ability variables
    public List<BaseAbility> passiveAbilitiesPathos = new List<BaseAbility>();
    public List<BaseAbility> passiveAbilitiesEthos = new List<BaseAbility>();
    public List<BaseAbility> passiveAbilitiesLogos = new List<BaseAbility>();
    public List<List<BaseAbility>> passiveAbilities;
    int passiveNum;

    // player active effect variables
    public List<EffectHolder> statusEffects = new List<EffectHolder>();  // Array of status effects currently attached to the player
    public List<EffectHolder> weaponEffects = new List<EffectHolder>();  // Array of weapon effects currently attached to the player
    int statusNum;

    // equipment variables
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject weaponAngleObject;
    GameObject weaponAngleObj;
    GameObject straightSword;

    // position variables
    public (int, int) currentPos;

    // physics variables
    public bool canMove = true;
    Rigidbody2D playerPhys;

    // graphics variables
    SpriteRenderer objectSprite;
    int spriteTimer = 0;
    public List<Sprite> charSprites;
    [SerializeField] UnityEvent<Dictionary<string, float>, Sprite, Player> startCharSwitch;

    [SerializeField] ActiveHolder activeHolder;


    private void Start()
    {
        playerPhys = gameObject.GetComponent<Rigidbody2D>();
        MainCamera = Instantiate(cameraObject, transform.parent = this.transform);
        weaponAngleObj = Instantiate(weaponAngleObject, transform.parent = this.transform);
        mapObj = Instantiate(map, transform.parent = this.transform);
        mapObj.sceneMap = sceneMap;
        mapObj.currentPos = currentPos;
        mapObj.updateMap = true;
        straightSword = Instantiate(weapon, transform.parent = weaponAngleObj.transform);
        objectSprite = gameObject.GetComponent<SpriteRenderer>();
        passiveAbilities = new List<List<BaseAbility>>
        {passiveAbilitiesPathos, passiveAbilitiesEthos, passiveAbilitiesLogos};
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

        // TODO-Deviant: test code for equipping and adding effect
        weapon.GetComponent<BaseSwordScript>().onEquip(this);
    }
    public int currentChar = 0;

    private void Update()
    {
        //Movement Script
        if (canMove)
        {
            Vector2 playerMovement = new Vector2((Input.GetAxis("Horizontal") * currentStats["Wit"]), (Input.GetAxis("Vertical") * currentStats["Wit"]));
            playerPhys.velocity = playerMovement;
        }
        //Reload Sprite and Camera after Scene Change
        if (objectSprite.enabled == false)
        {
            spriteTimer += 1;
            if (spriteTimer > 30)
            {
                objectSprite.enabled = true;
                MainCamera.enabled = true;
                spriteTimer = 0;
            }
        }
        //Character Swap Script
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            switch (currentChar)
            {
                case 0: currentChar = 1; break;
                case 1: currentChar = 2; break;
                case 2: currentChar = 0; break;
            }
            startCharSwitch.Invoke(charStats[currentChar], charSprites[currentChar], this);
        }
        //UnityEngine.Debug.Log("got here1");
        if (passiveAbilities[0].Count + passiveAbilities[1].Count + passiveAbilities[2].Count != passiveNum )
        {
            recalculateStats(charStatsBase, passiveAbilities, charStats);
            currentStats = charStats[currentChar];
            passiveNum = passiveAbilities[0].Count + passiveAbilities[1].Count + passiveAbilities[2].Count;
        }
        recalculateStats(charStatsBase, passiveAbilities, charStats);
        // check for application/expiry of active effects
        // Effects can be registered for two? reasons:
        //   1. Player is afflicted with a status (good or bad)
        //   2. Player swaps weapons

        // Effects can expire for two? reasons?
        //   1. Timer for status has expired
        //   2. Player swaps weapons

    }
    void OnTriggerEnter2D(UnityEngine.Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Ability":
                {
                    if (other.GetComponent<AbilityHolder>().heldAbility != null)
                    {
                        BaseAbility ability = other.GetComponent<AbilityHolder>().heldAbility;
                        string type = ability.type;
                        switch (type)
                        {
                            case "Passive":
                                if (!passiveAbilities[currentChar].Contains(ability))
                                {
                                    passiveAbilities[currentChar].Add(ability);
                                    other.GetComponent<AbilityHolder>().heldAbility = null;
                                }
                                break;
                            case "Active":
                                if (activeHolder.ability == null)
                                {
                                    activeHolder.ability = ability;
                                }
                                break;
                        }
                    }
                    break;
                }
            case "Door":
                {
                    Vector3 newPosition = Vector3.zero;
                    switch (other.gameObject.name)
                    {
                        case "North Door": currentPos = (currentPos.Item1, currentPos.Item2 + 1); break;
                        case "South Door": currentPos = (currentPos.Item1, currentPos.Item2 - 1); break;
                        case "East Door": currentPos = (currentPos.Item1 - 1, currentPos.Item2); break;
                        case "West Door": currentPos = (currentPos.Item1 + 1, currentPos.Item2); break;
                    }
                    mapObj.currentPos = currentPos;
                    mapObj.updateMap = true;
                    touchedDoor.Invoke(doorDictionary, sceneMap, currentPos, doorList);
                    switch (other.gameObject.name)
                    {
                        case "North Door": newPosition = new Vector3(transform.position.x, doorList[1].transform.position.y + doorEnterDistance); break;
                        case "South Door": newPosition = new Vector3(transform.position.x, doorList[0].transform.position.y - doorEnterDistance); break;
                        case "East Door": newPosition = new Vector3(doorList[3].transform.position.x - doorEnterDistance, transform.position.y); break;
                        case "West Door": newPosition = new Vector3(doorList[2].transform.position.x + doorEnterDistance, transform.position.y); break;
                    }
                    MainCamera.enabled = false;
                    objectSprite.enabled = false;
                    transform.position = newPosition;
                    break;
                }
        }
    }

    // recalculates stats for all characters on the fly
    public void recalculateStats(List<Dictionary<string, float>> charStatsBase, List<List<BaseAbility>> passiveAbilities, List<Dictionary<string, float>> charStats)
    {
        // loop through all 3
        for (int i = 0; i <= 2; i++)
        {
            // grab the base stats
            charStats = charStatsBase;

            // for each stat, cycle through all passive abilities, active effects, and weapon effects
            foreach (string statName in charStats[i].Keys.ToList())
            {
                foreach (BaseAbility ability in passiveAbilities[i])
                {
                    charStats[i][statName] += ability.statChange[statName];
                }
                // apply status effects
                foreach (EffectHolder effectHolder in statusEffects)
                {
                    charStats[i][statName] += effectHolder.effect.statChange[statName];
                }

                //TODO-Deviant: copy from status above

            }
        }
    }

    public void registerStatus(EffectHolder caller)
    {
        statusEffects.Add(caller);
        caller.listIndex = statusEffects.Count;
        statusNum++;
    }

    public void removeStatus(EffectHolder caller)
    {
        statusEffects.RemoveAt(caller.listIndex);
        statusNum--;
    }
}