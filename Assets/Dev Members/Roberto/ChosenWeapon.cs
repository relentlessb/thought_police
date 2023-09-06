using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChosenWeapon : MonoBehaviour
{
    //Imports
    public StraightSwordBase swordData;
    //Exports
    [HideInInspector] public Sprite weaponSprite;
    [HideInInspector] public Vector2 hitboxSize;
    [HideInInspector] public Vector2 hitboxOffset;

    //Weapon Types
    public enum weaponTypes
    {
        sword,
        weapon2,
        weapon3,
    }
    [HideInInspector] public weaponTypes weaponType;
    void Update()
    {
        if(weaponType==weaponTypes.sword)
        {
            weaponSprite = swordData.swordSprite;
            hitboxSize = swordData.hitboxSize;
            hitboxOffset = swordData.hitboxOffset;

        }
    }
}
