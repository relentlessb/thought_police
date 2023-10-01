using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attacks/Acid Scientist/ Glass Throw")]
public class AcidFlaskThrow : BaseAttack
{
    public override void acidAttack(Vector2 splashDir, GameObject attackItem, Vector3 originPos)
    {
        base.acidAttack(splashDir, attackItem, originPos);
    }
}
