using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseAbility : ScriptableObject
{
    public new string name;
    public string desc;
    public string type;

    public int determination;
    public int confidence;
    public int wit;
    public int morale;
    public int focus;
    public int damage;
    public float size;
    public float offsetX;
    public float offsetY;
    public float speed;

    public float activeAbilityTime;
    public float activeCooldownTime;

    public Dictionary<string, float> statChange;

    public virtual void loadStats()
    {
        statChange = new Dictionary<string, float>()
        {
            { "Determination", determination},
            { "Confidence",confidence},
            { "Wit", wit},
            { "Morale", morale},
            { "Focus", focus},
            { "Damage", damage},
            { "Size", size},
            { "Offset X", offsetX},
            { "Offset Y", offsetY},
            { "Speed", speed}
        };
    }
    public virtual void ActiveAbility(Player player)
    {

    }
}
