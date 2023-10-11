using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Weapon")]
public class BasePlayerWeapon : ScriptableObject
{
    public int attackDamage;
    public float leftClickAttackTime;
    public float rightClickAttackTime;
    public string leftClickAnimBool;
    public string rightClickAnimBool;
    public string speedMultiplierAnimName;
    public GameObject weaponObject;
    [SerializeField] public EffectHolder attackEffectHolder;  // this is the effect of attacks dealt by this weapon
    [SerializeField] public EffectHolder holdingEffectHolder; // this is the effect of holding the weapon (on the holder)
    [SerializeField] public float knockbackStrength;          // the knockback strength of a given weapon


    // this should be called for all weapons on pickup - it registers weapon effects on the player object
    public void onEquip(Player player)
    {
        player.registerStatus(holdingEffectHolder);
    }

    public virtual void weaponLeftClick(GameObject weaponObject, string leftClickAnimBool, string speedMultiplierAnimName, float animationSpeedMultiplier)
    {
        weaponObject.GetComponent<BoxCollider2D>().enabled = true;
        Animator weaponAnimator = weaponObject.GetComponent<Animator>();
        weaponAnimator.SetFloat(speedMultiplierAnimName, animationSpeedMultiplier);
        weaponAnimator.SetTrigger(leftClickAnimBool);    
    }
    public virtual void weaponLeftClickEnd(GameObject weaponObject, string leftClickAnimBool)
    {
        weaponObject.GetComponent<BoxCollider2D>().enabled = false;
        Animator weaponAnimator = weaponObject.GetComponent<Animator>();
        weaponAnimator.ResetTrigger(leftClickAnimBool);
    }

    public virtual void weaponRightClick() 
    {
    }
    public virtual void weaponRightClickEnd()
    {
    }


}
