using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyringeMono : MonoBehaviour
{
    public float syringeAmount; // Amount of HP currently in syringe
    float siphonAmount; // Amount of HP to be siphoned

    void Start()
    {
        syringeAmount = 0;
        siphonAmount = 0;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Siphons HP from player
        if (collision.gameObject.CompareTag("Player"))
        {
            siphonAmount = collision.gameObject.GetComponent<HealthManager>().currentHP * 0.1f;
            collision.gameObject.GetComponent<HealthManager>().UpdateHealth(-siphonAmount);
            syringeAmount += siphonAmount;
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            gameObject.GetComponent<HealthManager>().UpdateHealth(syringeAmount);
            syringeAmount = 0;
        }
    }
}
