using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject LabRatEnemy;
    [SerializeField] private GameObject LabMonkeyEnemy;
    [SerializeField] private GameObject LabSecurityEnemy;
    [SerializeField] private GameObject LabScientistBase;
    private void Start()
    {
        List<GameObject> list = new List<GameObject>()
        {
            LabRatEnemy,
            LabMonkeyEnemy,
            LabSecurityEnemy,
            LabScientistBase
        };
        InstantiateEnemy(list);
    }
    void InstantiateEnemy(List<GameObject> EnemyList)
    {
        int randomEnemyNum = UnityEngine.Random.Range(0, EnemyList.Count);
        Instantiate(EnemyList[randomEnemyNum],transform.parent = gameObject.transform,false);
    }
}
