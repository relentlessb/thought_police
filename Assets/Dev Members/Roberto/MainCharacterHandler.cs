using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class MainCharacterHandler : MonoBehaviour
{
    //Switch Character Functionality
    //Create Player Instance
    //Character Attributes and Abilities

    //Calculate Current Stats
    public Dictionary<string, int> calculatePlayerStats(int character, List<int> gainedAbilities)
    {
        Dictionary<string, int> currentPlayerAttributes = new Dictionary<string, int>();
        //Calculate Stats
        int determination = 0;
        int confidence = 0;
        int wit = 0;
        int morale = 0;
        int focus = 0;
        currentPlayerAttributes.Add("Determination", determination);
        currentPlayerAttributes.Add("Confidence", confidence);
        currentPlayerAttributes.Add("Wit", wit);
        currentPlayerAttributes.Add("Morale", morale);
        currentPlayerAttributes.Add("Focus", focus);

        return currentPlayerAttributes;
    }
    public Dictionary<string, int> calculateWeaponStats(int weapon, List<int> gainedAbilities)
    {
        Dictionary<string, int> currentWeaponAttributes = new Dictionary<string, int>();
        //Calculate Stats
        int damage = 0;
        int size = 0;
        int offsetX = 0;
        int offsetY = 0;
        int speed = 0;
        currentWeaponAttributes.Add("Damage", damage);
        currentWeaponAttributes.Add("Size", size);
        currentWeaponAttributes.Add("Offset X", offsetX);
        currentWeaponAttributes.Add("Offset Y", offsetY);
        currentWeaponAttributes.Add("Speed", speed);

        return currentWeaponAttributes;
    }
}
