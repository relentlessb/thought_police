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
    [SerializeField] PlayerWeaponHolder weaponHolder;
    [SerializeField] HealthManager healthManager;

    //Stats
    public Dictionary<string, float> currentStats;

    //Scene
    public SceneHandler sceneHandler;
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
    PlayerWeaponHolder weapon;
    [SerializeField] UIMapHandler map;
    UIMapHandler mapObj;
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

    //Player Statuses
    public bool canMove = true;

    //Map Info
    public List<(int,int)> clearedRooms = new List<(int,int)> ();
    List<(int,int)> enteredRooms= new List<(int,int)> ();
    bool nextRoom = false;

    //Enemy Info
    public int roomEnemies = 0;

    //Runtime
    private void Start()
    {
        MainCamera = Instantiate(cameraObject, transform.parent = this.transform);
        weapon = Instantiate(weaponHolder, transform.parent = this.transform);
        healthUI = Instantiate(healthUIObj, transform.parent = this.transform);
        healthManager.healthBar = healthUI.gameObject.transform.Find("HealthBar").gameObject.transform.Find("HP").GetComponent<Image>();
        mapObj = Instantiate(map, transform.parent=this.transform);
        clearedRooms.Add(currentPos);
        enteredRooms.Add(currentPos);
        mapObj.enteredRooms = enteredRooms;
        mapObj.currentPos = currentPos;
        mapObj.updateMap = true;
        passiveAbilities = new List<List<BaseAbility>> 
        {passiveAbilitiesPathos, passiveAbilitiesEthos, passiveAbilitiesLogos};
        healthManager.maxHP = currentStats["Determination"] * 20;
        weapon.attackSpeed = 1 + currentStats["Speed"] / 4;
        healthManager.currentHP = healthManager.maxHP;
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
            if(spriteLoadTimer*Time.deltaTime-timer < 0 && MainCamera.enabled == false)
            {
                MainCamera.enabled = true;
                objectSprite.enabled = true;
                timer = 0;
                nextRoom= false;
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
            weapon.attackSpeed = 1 + currentStats["Speed"] / 4;
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
                        case "East Door": currentPos = (currentPos.Item1 + 1, currentPos.Item2);  break;
                        case "West Door": currentPos = (currentPos.Item1 - 1, currentPos.Item2); break;
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
                        case "East Door": transform.position = new Vector3(doorList[2].transform.position.x + doorEnterDistance, transform.position.y); break;
                        case "West Door": transform.position = new Vector3(doorList[3].transform.position.x - doorEnterDistance, transform.position.y); break;
                    }
                    MainCamera.enabled = false;
                    objectSprite.enabled = false;
                    nextRoom = true;
                    break;
                }
        }
    }
    //Stat Methods
    public void recalculateStats(List<Dictionary<string,float>> charStatsBase, List<List<BaseAbility>> passiveAbilities, List<Dictionary<string, float>> charStats)
    {
        for(int i = 0; i<=2; i++)
        {
            charStats = charStatsBase;
            foreach(string statName in charStats[i].Keys.ToList())
            {
                foreach(BaseAbility ability in passiveAbilities[i])
                {
                    charStats[i][statName] += ability.statChange[statName];
                }
                
            }
        }
    }
    //Enemy Methods
    public void OnEnemyKilled(GameObject enemy)
    {
        
    }
}
