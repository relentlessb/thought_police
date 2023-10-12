using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] roomEnemyInfo roomEnemyInfo;
    GameObject playerObj;
    Player player;
    float spawnTimer = 0;
    void Update()
    {
        if (player != null && spawnTimer >= .5)
        {
            if (!player.clearedRooms.Contains(player.currentPos) && player.enemiesSpawned == false)
            {
                foreach (Vector2 position in roomEnemyInfo.enemyPositions)
                {
                    int randomEnemyNumber = Random.Range(0, roomEnemyInfo.enemies.Count);
                    GameObject newEnemy = Instantiate(roomEnemyInfo.enemies[randomEnemyNumber], position, Quaternion.identity, transform.parent = null);
                    player.roomEnemies++;

                }

            }
            player.enemiesSpawned = true;
            if (player.enemiesSpawned == true)
            {
                Destroy(gameObject);
            }
        }
        else if (player != null)
        {
            if (player.enemiesSpawned == false)
            {
                spawnTimer += Time.deltaTime;
            }
        }
        else
        {
            playerObj = GameObject.FindWithTag("Player");
            player = playerObj.GetComponent<Player>();
        }
    }
}
