using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TestAbility : BaseAbility
{
    private void Awake()
    {
        loadStats();
    }
    public override void loadStats()
    {
        base.loadStats();
    }
}
