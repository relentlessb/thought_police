using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidSpillMono : MonoBehaviour
{
    float timer;
    [SerializeField] float cooldown; // For balance, enemy must wait a certain length of time between attacks
    [SerializeField] int damage;

    private void Update()
    {
        timer += Time.deltaTime;
    }

    // Damages player when touched
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<HealthManager>().currentHP -= damage;
            collision.gameObject.GetComponent<HealthManager>().SetHealthBar();
        }
    }
}
