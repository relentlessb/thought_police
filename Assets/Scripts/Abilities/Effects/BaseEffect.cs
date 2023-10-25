using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseEffect : ScriptableObject
{
    public new string name;
    public string desc;

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

    public int healthChange;      // change to health over effectDuration - numerical if healthChangeIsPercentage is false
    public bool healthChangeIsPercentage; // determines whether health change is numerical (false) or percentage of total health (true)
    public float effectDuration;  // this is the duration of the effect (when it expires)
    public float hitPercentage;   // this is the percentage (as 0-1.0) likelihood that the effect will proc
    public bool isPersistent;     // for effects that persist (such as weapon effects), setting this to true prevents destruction of the effect

    public effectType type;       // type of effect for proper connection to entities

    public Dictionary<string, float> statChange;

    public enum effectType
    {
        status,
        weapon,
    }

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
}
