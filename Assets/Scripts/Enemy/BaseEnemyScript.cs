using System.Collections;
using System.Collections.Generic;
using System.Security;
using System.Threading;
using UnityEngine;

public class BaseEnemyScript : ScriptableObject
{
    public new string name;

    public int health;            // Enemy health
    public float speed;           // Enemy speed
    public float baseSpeed;       // Enemy base speed (without modifiers)
 
    public List<EffectHolder> effects = new List<EffectHolder>(); // Array of effects currently attached to the enemy

    public float standDistance;   // Distance at which enemy will preferrentially stand from player (to attack or do whatever)
    public float pursuitDistance; // Distance until which enemy will pursue player
    public float pursuitDelay;    // Time which the enemy will wait before tracking player when in standing state

    public float mass;            // enemy mass, which should affect knockback

    public float timeOfLastEffectProcess; // last time that an effect was applied
    public float effectProcessRate; // rate at which effects are applied

    public void Awake()
    {
        timeOfLastEffectProcess = 0;
        effectProcessRate = 1;
    }

    public virtual void Update()
    {
        // process effect updates every "effectProcessRate" second-ish
        if (Time.time > timeOfLastEffectProcess + effectProcessRate)
        {
            // this is sort of hacky, but set the current speed to base speed so we can subtract whatever the effects are later
            speed = baseSpeed;

            foreach (EffectHolder effectHolder in effects)
            {
                if (effectHolder.effect.damage != 0)
                {
                    // note: damage is negative by default, but stored as positive
                    health = health - effectHolder.effect.damage;
                }
                if (effectHolder.effect.speed != 0)
                {
                    speed = speed + effectHolder.effect.speed;
                }
            }
            timeOfLastEffectProcess = Time.time;
        }
    }

    public virtual void enemyMovement(GameObject enemyObject, Rigidbody2D enemyPhys, GameObject player)
    {
        // grab the player position so we can figure out where to point
        Vector2 moveDirection = (player.transform.position - enemyObject.transform.position).normalized;

        // if the enemy is outside of its preferred standing distance, move towards the player using the enemy's speed stat
        /* TODO-DEVIANT: figure out how to get this to work with debuffs and scriptable objects, since we'll probably have issues
        //       if multiple copies of the same enemy are made, unless we deepcopy these stats. Look into it more. Same issue with health.
        //       (looks like SOs are basically data stores, so only one instance is made. changes to one entity propagate to all.
        //        which might be neat to mess with the rats)*/
        if(Vector2.Distance(enemyObject.transform.position, player.transform.position) > standDistance)
        {
            enemyPhys.MovePosition(new Vector2(enemyObject.transform.position.x, enemyObject.transform.position.y) + moveDirection * speed * Time.fixedDeltaTime);
        }
    }

    // 
    /*public virtual void applyKnockback(GameObject enemyObject, Rigidbody2D enemyPhys, GameObject player)
    {
        // grab the player position/vector in relation to enemy so we can figure out where we got hit from/where to knock back to

        // TODO-DEVIANT: figure out where to put knockback
        // this isn't exact, but it should be good enough to get this working for now //
        Vector2 knockbackDirection = -(player.transform.position - enemyObject.transform.position).normalized*(player.weapon.attackEffectHolder.effect.knockback/mass));

        enemyPhys.MovePosition(new Vector2(enemyObject.transform.position.x, enemyObject.transform.position.y) + knockbackDirection * speed * Time.fixedDeltaTime);
    }*/
}
