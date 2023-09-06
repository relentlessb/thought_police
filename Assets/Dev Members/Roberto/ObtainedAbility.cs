using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ObtainedAbility : MonoBehaviour
{
    public GhostHandAbility ghostHandAbility;
    public int abilityType;
    void Update()
    {
        weaponOffsetIncrease = ghostHandAbility.weaponOffsetIncrease;
    }
    public Vector2 weaponOffsetIncrease;
}
