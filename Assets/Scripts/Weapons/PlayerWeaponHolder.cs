using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponHolder : MonoBehaviour
{
    [SerializeField] public BasePlayerWeapon weaponScript;
    public float attackSpeed;
    float timer = 0;
    GameObject weapon;
    bool weaponAttackStart;
    private void Start()
    {
        weapon = Instantiate(weaponScript.weaponObject, transform.parent = this.transform);
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) && !weaponAttackStart)
        {
            timer = weaponScript.leftClickAttackTime*attackSpeed;
            weaponAttackStart = true;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, -15));
            weaponScript.weaponLeftClick(weapon, weaponScript.leftClickAnimBool, weaponScript.speedMultiplierAnimName, attackSpeed);
        }
        if (Input.GetKey(KeyCode.DownArrow) && !weaponAttackStart)
        {
            timer = weaponScript.leftClickAttackTime*attackSpeed;
            weaponAttackStart = true;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 165));
            weaponScript.weaponLeftClick(weapon, weaponScript.leftClickAnimBool, weaponScript.speedMultiplierAnimName, attackSpeed);
        }
        if (Input.GetKey(KeyCode.LeftArrow) && !weaponAttackStart)
        {
            timer = weaponScript.leftClickAttackTime * attackSpeed;
            weaponAttackStart = true;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 75));
            weaponScript.weaponLeftClick(weapon, weaponScript.leftClickAnimBool, weaponScript.speedMultiplierAnimName, attackSpeed);
        }
        if (Input.GetKey(KeyCode.RightArrow) && !weaponAttackStart)
        {
            timer = weaponScript.leftClickAttackTime * attackSpeed;
            weaponAttackStart = true;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, -105));
            weaponScript.weaponLeftClick(weapon, weaponScript.leftClickAnimBool, weaponScript.speedMultiplierAnimName, attackSpeed);
        }
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        } else if(weaponAttackStart && timer <=0) 
        { 
            weaponAttackStart = false;
            weaponScript.weaponLeftClickEnd(weapon, weaponScript.leftClickAnimBool);
        }
    }
}
