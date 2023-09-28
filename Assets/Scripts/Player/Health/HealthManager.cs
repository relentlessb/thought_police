using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] Image healthBar;
    public float currentHP;
    public float maxHP;

    private void Start()
    {

        healthBar.fillAmount = currentHP / maxHP;
    }

    // Updates health bar value to correspond to player's health
    public void SetHealthBar()
    {
        healthBar.fillAmount = (currentHP / maxHP);
    }
}