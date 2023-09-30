using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttack : ScriptableObject
{
    public new string name;
    public float attackSpeed;
    public float cooldownTime;

    public virtual void splashAttack(Vector2 splashDir, GameObject attackItem, Vector3 originPos)
    {
        GameObject projectile;
        Quaternion rotation = Quaternion.identity;
        projectile = Instantiate(attackItem, originPos, rotation);
        Rigidbody2D projectilePhys = projectile.GetComponent<Rigidbody2D>();
        projectilePhys.velocity = splashDir.normalized * attackSpeed;
    }

    // Spawn acid puddle
    public virtual void spillAttack(GameObject attackItem, Vector3 originPos)
    {
        Quaternion rotation = Quaternion.identity;
        GameObject acidPuddle;
        acidPuddle = Instantiate(attackItem, originPos, rotation);
    }
}