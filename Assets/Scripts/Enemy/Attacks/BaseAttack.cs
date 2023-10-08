using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttack : ScriptableObject
{
    public new string name;
    public float attackSpeed;
    public float cooldownTime;
    public int damage;
    public int knockback;

    public virtual void attack(Vector2 attackDir, GameObject attackItem, Vector3 originPos)
    {
        GameObject projectile;
        Quaternion rotation = Quaternion.identity;
        projectile = Instantiate(attackItem, originPos, rotation);
        Rigidbody2D projectilePhys = projectile.GetComponent<Rigidbody2D>();
        projectilePhys.velocity = attackDir.normalized * attackSpeed;
    }
}
