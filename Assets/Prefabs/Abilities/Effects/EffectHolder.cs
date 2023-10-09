using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectHolder : MonoBehaviour
{
    [SerializeField] public BaseEffect effect;
    public int listIndex; // index of the effect in a particular list

    // On update, we check to see if we need to clobber the effect
    void Update()
    {
        // we check to see if the effect needs to persist - if not, kill it after the duration expires
        if (effect.isPersistent == false)
        {
            Destroy(effect, effect.effectDuration);
        }

        // if we have a persistent effect, make sure the duration is set to 0 so it's destroyed immediately
        // after we turn it off
        else
        {
            if (effect.effectDuration != 0)
            {
                effect.effectDuration = 0;
            }
            
        }
    }
}
