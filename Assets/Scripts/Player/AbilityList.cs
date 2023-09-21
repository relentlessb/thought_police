using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AbilityList
{
    public static Dictionary<string, float> getStatAbility(int abilityID)
    {
        Dictionary<string, float> stats = new Dictionary<string, float>();
        switch (abilityID)
        {
            case 0: stats.Add("Wit",1); break;
        }
        return stats;
    }
}
