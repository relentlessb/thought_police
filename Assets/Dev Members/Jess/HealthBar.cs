using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider healthBar;
    public int currentHP;
    public int maxHP = 100;

    private void Start()
    {
        // Retrieves and initialises health bar object; avoids NullReferenceException
        healthBar = GetComponent<Slider>();

        healthBar.maxValue = maxHP;
        healthBar.value = maxHP;
    }

    // Updates health bar value to correspond to player's health
    void SetHealthBar()
    {
        healthBar.value = currentHP;
    }

    // Reduces player's HP when attacked
    public void DamagePlayer(int damage)
    {
        currentHP -= damage;
        SetHealthBar();
    }
}
