using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/Lab/Slime Jump")]
public class PetriSlime : BaseEnemyScript
{
    public override void enemyMovement(GameObject enemyObject, Rigidbody2D enemyPhys,GameObject player, float movementTime, EnemyMovementTimer localTimer)
    {
        if (localTimer.localMovementTimer >= movementTime*.75)
        {
            enemyPhys.velocity = (new  Vector3(player.transform.position.x, player.transform.position.y+2,0)-enemyPhys.transform.position)*1.5f;
        }
        else if (localTimer.localMovementTimer >= movementTime * .55)
        {
            enemyPhys.velocity = new Vector2(enemyPhys.velocity.x, enemyPhys.velocity.y-.15f);
        }
        else if (localTimer.localMovementTimer < movementTime * .55)
        {
            enemyPhys.velocity = Vector2.zero;
        }
    }
}
