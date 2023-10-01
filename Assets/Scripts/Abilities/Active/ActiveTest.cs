using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Active Abilities/Dash")]
public class ActiveTest : BaseAbility
{
    public override void ActiveAbility(Player player)
    {
        player.canMove = false;
        player.GetComponent<Rigidbody2D>().velocity *= 4;
    }
}
