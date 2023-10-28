using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] EnemyHolder enemyHolder;
    [SerializeField] SpriteRenderer sprite;
    public int currentHealth;
    public int maxHealth;
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

        // destroy the enemy if their health is below 0
        if (currentHealth <= 0)
        {
            Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
            int roomEnemies = player.roomEnemies;
            List<(int, int)> clearedRooms = player.clearedRooms;
            (int, int) currentPos = player.currentPos;
            (roomEnemies, clearedRooms) = player.OnEnemyKilled(roomEnemies, clearedRooms, currentPos);
            player.roomEnemies = roomEnemies;
            player.clearedRooms = clearedRooms;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "FriendlyWeapon" && canHurt)
        {
            updateHealth(-collision.gameObject.GetComponentInParent<PlayerWeaponHolder>().weaponScript.attackDamage);
            
            // apply knockback
            enemyHolder.enemyScript.applyKnockback(enemyHolder.thisEnemy, enemyHolder.enemyPhys, GameObject.FindWithTag("Player"), collision.gameObject.GetComponentInParent<PlayerWeaponHolder>().weaponScript, enemyHolder.enemyMovementTimer);

            // check to see if the player's weapon ability inflicts a status effect
            if (collision.gameObject.GetComponentInParent<PlayerWeaponHolder>().getAttackEffect() != null)
            {
                // pull a random number from 0-1
                float randomPercentage = Random.Range(0.0f, 100.0f);
                float requiredPercentage = 100.0f - collision.gameObject.GetComponentInParent<PlayerWeaponHolder>().getAttackEffect().effect.hitPercentage;

                UnityEngine.Debug.Log("ENEMYHEALTH: roll = "+ randomPercentage);
                UnityEngine.Debug.Log("ENEMYHEALTH: required = " + requiredPercentage);

                // if the random number is less than or equal to the hit percentage for the weapon ability, register it on the enemy
                if (randomPercentage >= requiredPercentage)
                {
                    UnityEngine.Debug.Log("ENEMYHEALTH: Player attack effect applied");
                    enemyHolder.registerStatus(collision.gameObject.GetComponentInParent<PlayerWeaponHolder>().getAttackEffect());
                }
            }

            canHurt = false;
        }
    }

    private void flashOnHealthChange(float healthChangeValue)
    {
        if (healthChangeValue > 0)
        {
            sprite.color = Color.green;
        }
        else if (healthChangeValue < 0)
        {
            sprite.color = Color.red;
        }
    }

    public void updateHealth(float healthChangeValue)
    {
        flashOnHealthChange(healthChangeValue);

        currentHealth += (int)healthChangeValue;
    }
}
