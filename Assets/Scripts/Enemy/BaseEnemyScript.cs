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
    public float movementTime; // Enemy runs movement script when timer runs out.


    public float standDistance;   // Distance at which enemy will preferrentially stand from player (to attack or do whatever)
    public float pursuitDistance; // Distance until which enemy will pursue player
    public float pursuitDelay;    // Time which the enemy will wait before tracking player when in standing state



    public virtual void enemyMovement(GameObject enemyObject, Rigidbody2D enemyPhys, GameObject player, float movementTime, float localTimer)
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
