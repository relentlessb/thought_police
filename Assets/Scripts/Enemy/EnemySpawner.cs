using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] roomEnemyInfo roomEnemyInfo;
    void Start()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if(playerObj != null)
        {
            Player player = playerObj.GetComponent<Player>();
            if (!player.clearedRooms.Contains(player.currentPos))
            {
                foreach(Vector2 position in roomEnemyInfo.enemyPositions)
                {
                    int randomEnemyNumber = Random.Range(0, roomEnemyInfo.enemies.Count);
                    GameObject newEnemy = Instantiate(roomEnemyInfo.enemies[randomEnemyNumber], position, Quaternion.identity);
                    player.roomEnemies++;
                }
            }
        }
    }
}
