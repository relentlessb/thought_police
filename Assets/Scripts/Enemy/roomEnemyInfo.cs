using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DungeonScene ScrObj/Room Enemy Info")]
public class roomEnemyInfo : ScriptableObject
{
    public List<Vector2> enemyPositions;
    public List<GameObject> enemies;
}
