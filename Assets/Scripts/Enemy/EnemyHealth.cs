using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] EnemyHolder enemyHolder;
    [SerializeField] SpriteRenderer sprite;
    int currentHealth;
    int maxHealth;
    float timer = 0;
    bool canHurt = true;
    private void Start()
    {
        currentHealth = enemyHolder.maxHealth;
        maxHealth = enemyHolder.maxHealth;
    }
    private void Update()
    {
        if(sprite.color == Color.red)
        {
            timer += Time.deltaTime;
            if(timer > .25)
            {
                sprite.color = Color.white;
                timer = 0;
                canHurt= true;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "FriendlyWeapon" && canHurt)
        {
            sprite.color = Color.red;
            currentHealth -= collision.gameObject.GetComponentInParent<PlayerWeaponHolder>().weaponScript.attackDamage;
            canHurt= false;
            if(currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
