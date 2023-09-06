using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D weaponHitbox;
    public ChosenWeapon chosenWeapon;
    public ObtainedAbility obtainedAbility;
    void changeWeapon()
    {
        spriteRenderer.sprite = chosenWeapon.weaponSprite;
        weaponHitbox.size = chosenWeapon.hitboxSize;
        weaponHitbox.offset = chosenWeapon.hitboxOffset;
    }
    void addAbilityEffect()
    {
        transform.position += new Vector3(obtainedAbility.weaponOffsetIncrease.x, obtainedAbility.weaponOffsetIncrease.y, 0);
    }
    void Update()
    {
        if(Input.GetKeyDown("w"))
        {
            changeWeapon();
        }
        if(Input.GetKeyDown("g")) 
        { 
            addAbilityEffect();
        }
    }
}
