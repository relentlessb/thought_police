using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TestStatusEffect : BaseEffect
{
    private void Awake()
    {
        loadStats();
        type = BaseEffect.effectType.status;
    }
    public override void loadStats()
    {
        base.loadStats();
    }
}
