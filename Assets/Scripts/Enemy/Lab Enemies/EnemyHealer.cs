using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealer : BaseEnemyScript
{
    public override void enemyMovement(GameObject enemyObject, Rigidbody2D enemyPhys, GameObject player)
    {
        base.enemyMovement(enemyObject, enemyPhys, player);
    }
}
