using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHolder : MonoBehaviour
{
    [SerializeField] BaseEnemyScript enemyScript;
    [SerializeField] BaseAttack attackScript;
    [SerializeField] GameObject attackItem;
    GameObject player;
    Rigidbody2D enemyPhys;
    float cooldownTimer;
    enum attackState
    {
        ready,
        cooldown,
    }
    attackState state = attackState.ready;
    private void Start()
    {
        cooldownTimer = attackScript.cooldownTime;
        player = GameObject.FindWithTag("Player");
        enemyPhys = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (player != null)
        {
            enemyScript.enemyMovement(gameObject, enemyPhys, player);
        }
    }
    private void Update()
    {
        switch (state)
        {
            case attackState.ready:
                {
                    if (Vector2.Distance(transform.position, player.transform.position) <= enemyScript.standDistance)
                    {
                        Vector2 attackDir = player.transform.position - transform.position;
                        Vector3 originPos = transform.position;
                        attackScript.acidAttack(attackDir, attackItem, originPos);
                        state = attackState.cooldown;
                    }
                    break;
                }
            case attackState.cooldown:
                {
                    cooldownTimer -= Time.deltaTime;
                    if (cooldownTimer <= 0)
                    {
                        cooldownTimer = attackScript.cooldownTime;
                        state = attackState.ready;
                    }
                break;
                }
        }
    }
}
