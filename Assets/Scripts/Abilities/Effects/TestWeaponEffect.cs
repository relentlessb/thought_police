using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TestWeaponEffect : BaseEffect
{
    private void Awake()
    {
        loadStats();
        type = BaseEffect.effectType.weapon;
    }
    public override void loadStats()
    {
        base.loadStats();
    }
}
