using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AbilityHandler
{
    public static List<string> weaponAbilities = new List<string>()
    {
        "Ghost Hand",
        "Iron Fist",
        "Sucker Punch",
        "Invisible Current"
    };

    public static List<string> playerAbilities = new List<string>()
    {
        "Black Coffee",
        "Invisibility Cloak",
        "Family Crest",
        "A+"
    };
    public static Dictionary<string, int> getAbility(string AbilityName)
    {
        Dictionary<string, int> abilityInfo = new Dictionary<string, int>();
        int abilityType = 0;
        if (weaponAbilities.Contains(AbilityName))
        {
            abilityType = 1;
        }
        else abilityType = 2;

        return abilityInfo;
    }
}
