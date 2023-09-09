using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class mainCharacterHandler : MonoBehaviour
{
    //mainCharacterParsers
    enum characterNames
    {
        pathos, ethos, logos,
    }
    enum wieldTypes
    {
        pathos, ethos, logos,
    }
    enum spriteSheet
    {
        pathos, ethos, logos,
    }
    //weaponParser
    enum pathosWeaponNames
    {
        excalibur,
        judgeGavel,
        greatsword,
        pistol,
    }
    enum ethosWeaponNames
    {
        ancientScripture,
        hamurabisCode,
        peonPager,
        sunStaff,
    }
    enum logosWeaponNames
    {
        robot,
        drone,
        biosuit,
    }

    //Create Player GameObject
    public GameObject player;
    public Sprite characterSprite;
    public GameObject weapon;
    public Sprite weaponSprite;
    public void createCharacter(Dictionary<string, int> characterSelection, Dictionary<string, int> weaponSelection, GameObject player, GameObject weapon, Sprite characterSprite, Sprite weaponSprite)
    {
        Instantiate(player).transform.parent = transform;
        player.GetComponent<SpriteRenderer>().sprite = characterSprite;
        Instantiate(weapon).transform.parent = player.transform;
        weapon.GetComponent<SpriteRenderer>().sprite = weaponSprite;
    }

    //Character at Runtime
    public bool characterSwitch;
    public bool loadCharacter;
    public bool destroyCharacter;
    void Update()
    {
        if (characterSwitch == true)
        {
            //switchCharacters();
            characterSwitch= false;
        }
        if (loadCharacter== true)
        {
            //createCharacter();
            loadCharacter= false;
        }
        if(destroyCharacter == true)
        {
            //destroyCharacter();
        }
    }
    public void switchCharacters(Dictionary<string, int> characterSelection, Dictionary<string, int> weaponSelection, GameObject player, GameObject weapon, Sprite characterSprite, Sprite weaponSprite)
    {
        player.GetComponent<SpriteRenderer>().sprite = characterSprite;
        weapon.GetComponent<SpriteRenderer>().sprite = weaponSprite;

    }
}
