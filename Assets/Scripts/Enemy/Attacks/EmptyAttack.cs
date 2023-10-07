using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attacks/EmptyAttack")]
public class EmptyAttack : BaseAttack
{
    public override void attack(Vector2 attackDir, GameObject attackItem, Vector3 originPos)
    {
        
    }
}
