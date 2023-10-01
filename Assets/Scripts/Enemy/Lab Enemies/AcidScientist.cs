using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/Lab/Acid Scientist")]
public class AcidScientist : BaseEnemyScript
{
    public override void enemyMovement(GameObject enemyObject, Rigidbody2D enemyPhys,GameObject player)
    {
        base.enemyMovement(enemyObject, enemyPhys, player);
    }
}
