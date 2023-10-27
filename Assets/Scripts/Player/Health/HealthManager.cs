using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image healthBar;
    public float currentHP;
    public float maxHP;

    private void Start()
    {

        healthBar.fillAmount = currentHP / maxHP;
    }

    public void UpdateHealth(float healthChange)
    {
        currentHP += healthChange;

        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
        else if (currentHP < 0) 
        { 
            currentHP = 0;
        }
    }

    // Updates health bar value to correspond to player's health
    public void SetHealthBar()
    {
        healthBar.fillAmount = (currentHP / maxHP);
    }
}