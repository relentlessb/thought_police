using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseSwordScript : MonoBehaviour
{
    Animator animator;
    SpriteRenderer spriteVis;
    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteVis = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("swordSwing", true);
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("SwordSwing"))
        {
            animator.SetBool("swordSwing", false);
        }
    }
    public void hitEnemy(GameObject enemyCollider)
    {
        Component enemy = enemyCollider.GetComponent<BaseEnemyScript>();
    }
}
