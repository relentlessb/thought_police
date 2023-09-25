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
    [SerializeField] Camera cameraObject;
    public int doorEnterDistance;
    SpriteRenderer objectSprite;
    int spriteTimer = 0;
    Camera MainCamera;
    [SerializeField] UnityEvent<Dictionary<string, float>, Sprite, Player> startCharSwitch;
    Rigidbody2D playerPhys;
    public List<Dictionary<string, float>> charStats;
    public List<Sprite> charSprites;

    private void Start()
    {
        playerPhys = gameObject.GetComponent<Rigidbody2D>();
        MainCamera = Instantiate(cameraObject, transform.parent = this.transform);
        objectSprite = gameObject.GetComponent<SpriteRenderer>();
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
        //Movement Script
        Vector2 playerMovement = new Vector2((Input.GetAxis("Horizontal") * currentStats["Wit"]), (Input.GetAxis("Vertical") * currentStats["Wit"]));
        playerPhys.velocity = playerMovement;
        //Reload Sprite and Camera after Scene Chane
        if(objectSprite.enabled == false)
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
            switch(currentChar)
            {
                case 0: currentChar = 1; break;
                case 1: currentChar = 2; break;
                case 2: currentChar = 0; break;
            }
            startCharSwitch.Invoke(charStats[currentChar], charSprites[currentChar], this);
        }
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
                    Vector3 newPosition = Vector3.zero;
                    switch (other.gameObject.name)
                    {
                        case "North Door": currentPos = (currentPos.Item1, currentPos.Item2 + 1); break;
                        case "South Door": currentPos = (currentPos.Item1, currentPos.Item2 - 1);  break;
                        case "East Door": currentPos = (currentPos.Item1 - 1, currentPos.Item2);  break;
                        case "West Door": currentPos = (currentPos.Item1 + 1, currentPos.Item2); break;
                    }
                    touchedDoor.Invoke(doorDictionary, sceneMap, currentPos, doorList);
                    switch (other.gameObject.name)
                    {
                        case "North Door": newPosition = new Vector3(transform.position.x, doorList[1].transform.position.y - doorEnterDistance); break;
                        case "South Door": newPosition = new Vector3(transform.position.x, doorList[0].transform.position.y + doorEnterDistance); break;
                        case "East Door": newPosition = new Vector3(doorList[3].transform.position.x - doorEnterDistance, transform.position.y); break;
                        case "West Door": newPosition = new Vector3(doorList[2].transform.position.x + doorEnterDistance, transform.position.y); break;
                    }
                    MainCamera.enabled = false;
                    objectSprite.enabled = false;
                    transform.position= newPosition;
                    break;
                }
        }
    }
}
