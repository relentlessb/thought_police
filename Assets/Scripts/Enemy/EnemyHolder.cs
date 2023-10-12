using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHolder : MonoBehaviour
{
    public BaseEnemyScript enemyScript;
    public EnemyMovementTimer enemyMovementTimer;
    public BaseAttack attackScript;
    [SerializeField] GameObject attackItem;
    [SerializeField] EnemyHealth enemyHealth;
    public List<EffectHolder> effects = new List<EffectHolder>(); // Array of effects currently attached to the enemy

    float speed;
    float baseSpeed;
    float movementTime;

    public GameObject player;
    public GameObject thisEnemy;
    public Rigidbody2D enemyPhys;
    float cooldownTimer;
    public int maxHealth;
    enum attackState
    {
        ready,
        cooldown,
    }
    attackState state = attackState.ready;

    public float timeOfLastEffectProcess; // last time that an effect was applied
    public float effectProcessRate; // rate at which effects are applied

    public void Awake()
    {
        speed = enemyScript.speed;
        baseSpeed = enemyScript.baseSpeed;
        timeOfLastEffectProcess = 0;
        effectProcessRate = 1;
        maxHealth = enemyScript.health;
        movementTime = enemyScript.movementTime;
        enemyMovementTimer = new EnemyMovementTimer();
        enemyMovementTimer.localMovementTimer = 0;

        //make sure all enemies have mass so we don't have 0 division errors
        if (enemyScript.mass == 0)
        {
            enemyScript.mass = 1;
        }
    }
    private void Start()
    {
        cooldownTimer = attackScript.cooldownTime;
        player = GameObject.FindWithTag("Player");
        enemyPhys = GetComponent<Rigidbody2D>();
        enemyPhys.mass = enemyScript.mass;
        thisEnemy = this.gameObject;
    }
    private void FixedUpdate()
    {
        if (player != null)
        {
            if (enemyMovementTimer.localMovementTimer > 0)
            {
                enemyMovementTimer.localMovementTimer -= Time.deltaTime;
            }
            else
            {
                enemyMovementTimer.localMovementTimer = 0;
            }

            enemyScript.enemyMovement(thisEnemy, enemyPhys, player, movementTime, enemyMovementTimer);
        }
    }
    private void Update()
    {
        // process effect updates every "effectProcessRate" seconds-ish
        if (Time.time > timeOfLastEffectProcess + effectProcessRate)
        {
            // this is sort of hacky, but set the current speed to base speed so we can subtract whatever the effects are later
            speed = baseSpeed;

            foreach (EffectHolder effectHolder in effects)
            {
                if (effectHolder.effect.damage != 0)
                {
                    // note: damage is negative by default, but stored as positive
                    enemyHealth.currentHealth = enemyHealth.currentHealth - effectHolder.effect.damage;
                }
                if (effectHolder.effect.speed != 0)
                {
                    speed = speed + effectHolder.effect.speed;
                }
            }
            timeOfLastEffectProcess = Time.time;
        }
        switch (state)
        {
            case attackState.ready:
                {
                    if (Vector2.Distance(transform.position, player.transform.position) <= enemyScript.standDistance)
                    {
                        Vector2 attackDir = player.transform.position - transform.position;
                        Vector3 originPos = transform.position;
                        attackScript.attack(attackDir, attackItem, originPos);
                        state = attackState.cooldown;
                    }
                    break;
                }
            case attackState.cooldown:
                {
                    cooldownTimer -= Time.deltaTime;
                    if (cooldownTimer <= 0)
                    {
                        cooldownTimer = attackScript.cooldownTime;
                        state = attackState.ready;
                    }
                break;
                }
        }
    }
}
