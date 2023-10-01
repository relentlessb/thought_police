using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AcidFlaskThrow : BaseAttack
{
    public override void splashAttack(Vector2 splashDir, GameObject attackItem, Vector3 originPos)
    {
        base.splashAttack(splashDir, attackItem, originPos);
    }
}
