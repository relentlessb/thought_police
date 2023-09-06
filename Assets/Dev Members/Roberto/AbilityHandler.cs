using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHandler : MonoBehaviour
{
    public ObtainedAbility obtainedAbility;
    public enum abilityTypes
    {
        hitboxSizeChange,
        damageChange,
        hitboxOffsetChange,
        weaponOffsetChange,
    }
    void Update()
    {
        if (Input.GetKeyDown("a"))
        {

        }
    }
}
