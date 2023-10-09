using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attacks/Acid Scientist/ Acid Spill")]
public class AcidSpill : BaseAttack
{
    public override void attack(Vector2 attackDir, GameObject attackItem, Vector3 originPos)
    {
        GameObject acidPuddle;
        attackDir = attackDir.normalized;
        Vector3 attackDirV3 = new Vector3(attackDir.x, attackDir.y, 0);
        Vector3 inFrontPos = attackDirV3 + originPos;
        acidPuddle = Instantiate(attackItem, inFrontPos, Quaternion.identity);
    }
}
