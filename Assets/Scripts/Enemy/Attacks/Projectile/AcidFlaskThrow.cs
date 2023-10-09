using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attacks/Projectile")]
public class AcidFlaskThrow : BaseAttack
{
    public override void attack(Vector2 splashDir, GameObject attackItem, Vector3 originPos)
    {
        base.attack(splashDir, attackItem, originPos);
    }
}
