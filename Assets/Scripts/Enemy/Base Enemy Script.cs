using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyScript : ScriptableObject
{
    public new string name;

    public int health;
    public float speed;
    public float standDistance;
    public virtual void enemyMovement(GameObject enemyObject, Rigidbody2D enemyPhys, GameObject player)
    {
        Vector2 moveDirection = (player.transform.position - enemyObject.transform.position).normalized;
        if(Vector2.Distance(enemyObject.transform.position, player.transform.position) > standDistance)
        {
            enemyPhys.MovePosition(new Vector2(enemyObject.transform.position.x, enemyObject.transform.position.y) + moveDirection * speed * Time.fixedDeltaTime);
        }
    }
}
