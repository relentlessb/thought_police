using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security;
using System.Threading;
using UnityEngine;

public class BaseEnemyScript : ScriptableObject
{
    public new string name;

    public int   health;          // Enemy health
    public float speed;           // Enemy speed
    public float movementTime;    // Enemy runs movement script when timer runs out.
    public float mass;            // Enemy mass, which should affect knockback

    public float standDistance;   // Distance at which enemy will preferrentially stand from player (to attack or do whatever)
    public float pursuitDistance; // Distance until which enemy will pursue player
    public float pursuitDelay;    // Time which the enemy will wait before tracking player when in standing state



    public virtual void enemyMovement(GameObject enemyObject, Rigidbody2D enemyPhys, GameObject player, float movementTime, EnemyMovementTimer localTimer)
    {
        // grab the player position so we can figure out where to point
        Vector2 moveDirection = (player.transform.position - enemyObject.transform.position).normalized;

        // if the enemy is outside of its preferred standing distance, move towards the player using the enemy's speed stat
        /* TODO-DEVIANT: figure out how to get this to work with debuffs and scriptable objects, since we'll probably have issues
        //       if multiple copies of the same enemy are made, unless we deepcopy these stats. Look into it more. Same issue with health.
        //       (looks like SOs are basically data stores, so only one instance is made. changes to one entity propagate to all.
        //        which might be neat to mess with the rats)*/
        if( (Vector2.Distance(enemyObject.transform.position, player.transform.position) > standDistance) && (localTimer.localMovementTimer == 0) )
        {
            enemyPhys.MovePosition(new Vector2(enemyObject.transform.position.x, enemyObject.transform.position.y) + moveDirection * speed * Time.fixedDeltaTime);
        }
        else if ((Vector2.Distance(enemyObject.transform.position, player.transform.position) <= standDistance) && (localTimer.localMovementTimer != 0))
        {
            localTimer.localMovementTimer = movementTime;
        }
    }

    // 
    public virtual void applyKnockback(GameObject enemyObject, Rigidbody2D enemyPhys, GameObject player, BasePlayerWeapon weapon, EnemyMovementTimer localTimer)
    {
        // grab the player position/vector in relation to enemy so we can figure out where we got hit from/where to knock back to
        Vector2 knockbackDirection = -(player.transform.position - enemyObject.transform.position).normalized*(weapon.knockbackStrength/mass);

        //set movement timer to 0.25s to simulate knockback stun
        localTimer.localMovementTimer = 0.25f;

        enemyPhys.MovePosition(new Vector2(enemyObject.transform.position.x, enemyObject.transform.position.y) + knockbackDirection * speed * Time.fixedDeltaTime);
    }
}
