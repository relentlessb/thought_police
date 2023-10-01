using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //Player Info
    [SerializeField] SpriteRenderer objectSprite;
    [SerializeField] Rigidbody2D playerPhys;
    [SerializeField] GameObject weapon;
    [SerializeField] HealthManager healthManager;

    //Stats
    public Dictionary<string, float> currentStats;

    //Scene
    [SerializeField] SceneHandler sceneHandler;
    public Dictionary<(int, int), string> sceneMap;
    public (int, int) currentPos;
    int spriteLoadTimer = 10; //Frames
    float timer = 0;

    //Doors
    public List<GameObject> doorList;
    public List<((int, int), (int, int))> doorDictionary;
    [SerializeField] UnityEvent<List<((int, int), (int, int))>, Dictionary<(int, int), string>, (int, int), List<GameObject>> touchedDoor;
    public float doorEnterDistance;

    //Children Objects
    [SerializeField] Camera cameraObject;
    Camera MainCamera;
    GameObject straightSword;
    [SerializeField] UIMapHandler map;
    UIMapHandler mapObj;
    [SerializeField] GameObject weaponAngleObject;
    GameObject weaponAngleObj;
    [SerializeField] Canvas healthUIObj;
    Canvas healthUI;

    //Character Switching
    [SerializeField] UnityEvent<Dictionary<string, float>, Sprite, Player> startCharSwitch;
    public List<Dictionary<string, float>> charStats;
    public List<Dictionary<string, float>> charStatsBase;
    public List<Sprite> charSprites;
    public int currentChar = 0;

    //Character Abilities
    public List<BaseAbility> passiveAbilitiesPathos= new List<BaseAbility>();
    public List<BaseAbility> passiveAbilitiesEthos = new List<BaseAbility>();
    public List<BaseAbility> passiveAbilitiesLogos = new List<BaseAbility>();
    public List<List<BaseAbility>> passiveAbilities;
    int passiveNum;
    [SerializeField] ActiveHolder activeHolder;

    //Player Effects
    // player active effect variables
    public List<BaseEffect> statusEffects = new List<BaseEffect>();  // Array of status effects currently attached to the player
    public List<BaseEffect> weaponEffects = new List<BaseEffect>();  // Array of weapon effects currently attached to the player
    int statusNum;

    //Player Statuses
    public bool canMove = true;

    //Map Info
    List<(int,int)> clearedRooms = new List<(int,int)> ();
    List<(int,int)> enteredRooms= new List<(int,int)> ();
    bool nextRoom = false;

    //Enemy Info
    public int roomEnemies = 0;

    //Runtime
    private void Start()
    {
        MainCamera = Instantiate(cameraObject, transform.parent = this.transform);
        weaponAngleObj = Instantiate(weaponAngleObject, transform.parent = this.transform);
        healthUI = Instantiate(healthUIObj, transform.parent = this.transform);
        healthManager.healthBar = healthUI.gameObject.transform.Find("HealthBar").gameObject.GetComponentInChildren<Image>();
        mapObj = Instantiate(map, transform.parent=this.transform);
        clearedRooms.Add(currentPos);
        enteredRooms.Add(currentPos);
        mapObj.enteredRooms = enteredRooms;
        mapObj.currentPos = currentPos;
        mapObj.updateMap = true;
        straightSword = Instantiate(weapon, transform.parent = weaponAngleObj.transform);
        passiveAbilities = new List<List<BaseAbility>> 
        {passiveAbilitiesPathos, passiveAbilitiesEthos, passiveAbilitiesLogos};
        healthManager.maxHP = currentStats["Determination"] * 20;
        healthManager.currentHP = healthManager.maxHP;
        // TODO-Deviant: test code for equipping and adding effect
        weapon.GetComponent<BaseSwordScript>().onEquip(this);
    }

    private void Update()
    {
        //Movement Script
        if (canMove)
        {
            Vector2 playerMovement = new Vector2((Input.GetAxis("Horizontal") * currentStats["Wit"]), (Input.GetAxis("Vertical") * currentStats["Wit"]));
            playerPhys.velocity = playerMovement;
        }
        //New Room Calls
        if (nextRoom)
        {
            timer += Time.deltaTime;
            if(spriteLoadTimer*Time.deltaTime-timer < 0)
            {
                MainCamera.enabled = true;
                objectSprite.enabled = true;
                nextRoom = false;
                timer = 0;
            }
        }
        //Character Swap Code
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
        if(passiveAbilities[0].Count+passiveAbilities[1].Count+passiveAbilities[2].Count != passiveNum)
        {
            recalculateStats(charStatsBase, passiveAbilities, charStats);
            currentStats = charStats[currentChar];
            healthManager.maxHP = currentStats["Determination"] * 20;
            passiveNum = passiveAbilities[0].Count + passiveAbilities[1].Count + passiveAbilities[2].Count;
        }
    }
    //Collision Detection
    void OnTriggerEnter2D(UnityEngine.Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Ability":
                {
                    if(other.GetComponent<AbilityHolder>().heldAbility!=null)
                    {
                        BaseAbility ability = other.GetComponent<AbilityHolder>().heldAbility;
                        string type = ability.type;
                        switch (type)
                        {
                            case "Passive": if (!passiveAbilities[currentChar].Contains(ability)) 
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
                    switch (other.gameObject.name)
                    {
                        case "North Door": currentPos = (currentPos.Item1, currentPos.Item2 + 1); break;
                        case "South Door": currentPos = (currentPos.Item1, currentPos.Item2 - 1);  break;
                        case "East Door": currentPos = (currentPos.Item1 - 1, currentPos.Item2);  break;
                        case "West Door": currentPos = (currentPos.Item1 + 1, currentPos.Item2); break;
                    }
                    enteredRooms.Add(currentPos);
                    mapObj.currentPos = currentPos;
                    mapObj.enteredRooms = enteredRooms;
                    mapObj.updateMap = true;
                    touchedDoor.Invoke(doorDictionary, sceneMap, currentPos, doorList);
                    switch (other.gameObject.name)
                    {
                        case "North Door": transform.position = new Vector3(transform.position.x, doorList[1].transform.position.y + doorEnterDistance); break;
                        case "South Door": transform.position = new Vector3(transform.position.x, doorList[0].transform.position.y - doorEnterDistance); break;
                        case "East Door": transform.position = new Vector3(doorList[3].transform.position.x - doorEnterDistance, transform.position.y); break;
                        case "West Door": transform.position = new Vector3(doorList[2].transform.position.x + doorEnterDistance, transform.position.y); break;
                    }
                    MainCamera.enabled = false;
                    objectSprite.enabled = false;
                    nextRoom = true;
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
                foreach (BaseEffect baseEffect in statusEffects)
                {
                    charStats[i][statName] += baseEffect.statChange[statName];
                }

                //TODO-Deviant: copy from status above

            }
        }
    }

    // Add a status effect to the list of effects
    public void registerStatus(EffectHolder caller)
    {
        statusEffects.Add(Instantiate(caller.effect));
        caller.listIndex = statusEffects.Count;
        statusNum++;
    }

    //Enemy Methods
    public void OnEnemyKilled(GameObject enemy)
    {
        
    }
}
