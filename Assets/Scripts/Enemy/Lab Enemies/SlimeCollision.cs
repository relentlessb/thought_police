using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeCollision : MonoBehaviour
{
    public int damage;
    public int knockback;
    private void Awake()
    {
        damage = GetComponent<EnemyHolder>().attackScript.damage;
        knockback = GetComponent<EnemyHolder>().attackScript.knockback;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity.normalized * knockback;
            collision.gameObject.GetComponent<HealthManager>().currentHP -= damage;
            collision.gameObject.GetComponent<HealthManager>().SetHealthBar();
        }
    }
}
