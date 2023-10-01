using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseSwordScript : MonoBehaviour
{
    Animator animator;
    SpriteRenderer spriteVis;
    [SerializeField] EffectHolder attackEffectHolder;  // this is the effect of attacks dealt by this weapon
    [SerializeField] EffectHolder holdingEffectHolder; // this is the effect of holding the weapon (on the holder)

    public void onEquip(Player player)
    {
        player.registerStatus(holdingEffectHolder);
    }


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
    public void hitEnemy(GameObject enemyCollider, BaseEnemyScript enemyObject)
    {
        // TODO: apply damage
        // TODO: apply weapon effect based on proc percentage
        if (UnityEngine.Random.Range(0f, 1.0f) <= attackEffectHolder.effect.hitPercentage)
        {
            // add the effect to the enemy
            enemyObject.effects.Add(attackEffectHolder);
        }
    }
}
