using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHolder : MonoBehaviour
{
    public BaseEnemyScript enemyScript;
    public EnemyMovementTimer enemyMovementTimer;
    public BaseAttack attackScript;
    [SerializeField] GameObject attackItem;
    EnemyHealth enemyHealth;
    [SerializeField] EffectHolder attackEffect;
    public List<EffectPacket> statusEffects = new List<EffectPacket>(); // Array of effects currently attached to the enemy

    float speed;
    float baseSpeed;
    float movementTime;

    public GameObject player;
    public GameObject thisEnemy;
    public Rigidbody2D enemyPhys;
    float cooldownTimer;
    public int maxHealth;
    private bool effectsChanged;

    enum attackState
    {
        ready,
        cooldown,
    }
    attackState state = attackState.ready;

    public float processEffectsTimer; // timer for processing status effects

    public void Awake()
    {
        speed = enemyScript.speed;
        baseSpeed = enemyScript.speed;
        processEffectsTimer = 0.0f;
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
        enemyHealth = GetComponent<EnemyHealth>(); // the EnemyHealth class for this enemy
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
        // process effect updates every 1-ish seconds
        enemyHealth.updateHealth(-processHealthEffects(Time.deltaTime));

        // update status effects to see what's active/expired
        updateStatus(Time.deltaTime);

        // apply any status effects to enemy stats
        if (effectsChanged == true)
        {
            recalculateStats();
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

    // Add a status effect to the list of effects
    public void registerStatus(EffectHolder caller)
    {
        // make sure we don't crash the game if we accidentally pass in nothing
        if (caller != null)
        {
            statusEffects.Add(new EffectPacket(Instantiate(caller.effect), 0));

            effectsChanged = true;
        }
    }

    // calculate and return any health-related effects that happen once per second
    public float processHealthEffects(float deltaTime)
    {
        float damagePerSecondToApply = 0;

        processEffectsTimer += deltaTime;

        if (processEffectsTimer >= 1)
        {
            UnityEngine.Debug.Log("ENEMYHOLDER: Enter processHealthEffects");

            // reset the timer
            processEffectsTimer = 0;

            // apply status effects
            foreach (EffectPacket effectPacket in statusEffects)
            {
                // make sure the effect is time-based
                if (effectPacket.effect.effectDuration > 0)
                {
                    float tempDamageCalculation = (float)effectPacket.effect.healthChange;
                    UnityEngine.Debug.Log("ENEMYHOLDER-processHealthEffects status: " + (float)effectPacket.effect.healthChange);

                    // if we're working with a percent-based damage/heal, convert the health change into a percentage
                    if (effectPacket.effect.healthChangeIsPercentage == true)
                    {
                        tempDamageCalculation = tempDamageCalculation / 100 * (float)enemyHealth.maxHealth;
                    }

                    // calculate the damage or healing per second for the status effect in the current loop
                    damagePerSecondToApply += tempDamageCalculation / effectPacket.effect.effectDuration;
                }
            }
            UnityEngine.Debug.Log("ENEMYHOLDER-processHealthEffects: Done with status");

            UnityEngine.Debug.Log("ENEMYHOLDER-processHealthEffects: Health change this iteration: " + damagePerSecondToApply);
        }

        return damagePerSecondToApply;
    }

    // check the persistence and "on time" for each registered effect and deregister/delete stuff as it expires
    public void updateStatus(float deltaTime)
    {
        UnityEngine.Debug.Log("ENEMYHOLDER-updateStatus: items in statusEffects: " + statusEffects.Count);
        // run backwards through the lists of effects and remove elements that are past their expiration time with RemoveAt
        // we need to do this, otherwise we screw up the indices when we're iterating through and miss or mistakenly delete elements
        if (statusEffects.Count > 0)
        {
            for (int i = (statusEffects.Count - 1); i >= 0; i--)
            {
                if (statusEffects[i] != null)
                {
                    statusEffects[i].elapsedTime += deltaTime;

                    // cull the list if we have exceeded the time and mark effects as changed
                    if (statusEffects[i].effect.isPersistent == false)
                    {
                        if (statusEffects[i].elapsedTime >= statusEffects[i].effect.effectDuration)
                        {
                            UnityEngine.Debug.Log("ENEMYHOLDER-updateStatus: Removing statusEffect[" + i + "], items in statusEffects: " + statusEffects.Count);
                            statusEffects.RemoveAt(i);
                            UnityEngine.Debug.Log("ENEMYHOLDER-updateStatus: Removed statusEffect[" + i + "], items in statusEffects: " + statusEffects.Count);
                            effectsChanged = true;
                        }
                    }
                }
            }
        }
    }

    // recalculates stats for this enemy
    // note: this works with the global values for the instance
    public void recalculateStats()
    {
        UnityEngine.Debug.Log("ENEMY-recalculateStats: START");

        speed = baseSpeed;

        // for each stat, cycle through all passive abilities, active effects, and weapon effects
        foreach (EffectPacket effectPacket in statusEffects)
        {
            UnityEngine.Debug.Log("ENEMY-recalculateStats: Before status: speed + : " + speed);
            // apply status effects
            speed += effectPacket.effect.statChange["Speed"];
        }

        // make sure stats can't go below 1 - for now?
        // this is super relevant since we could really screw things up with negative stats
        //  such as: hitting enemy heals them for negative attack, controls are reversed for negative speed
        // and with 0 we wouldn't be able to move, which is neat and maybe useful for a stun effect, but sucks for the player
        enemyScript.speed = speed;
        UnityEngine.Debug.Log("ENEMY-recalculateStats: After zero and negative stats correction: speed : " + speed) ;

        effectsChanged = false;
        UnityEngine.Debug.Log("ENEMY-recalculateStats: END");
    }
}
