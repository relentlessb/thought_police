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

    public virtual void weaponLeftClick(GameObject weaponObject, string leftClickAnimBool, string speedMultiplierAnimName, float animationSpeedMultiplier)
    {
        weaponObject.GetComponent<BoxCollider2D>().enabled = true;
        Animator weaponAnimator = weaponObject.GetComponent<Animator>();
        weaponAnimator.SetFloat(speedMultiplierAnimName, animationSpeedMultiplier);
        weaponAnimator.SetTrigger(leftClickAnimBool);    }
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
