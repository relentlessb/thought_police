using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoorTrigger : MonoBehaviour
{
    public event EventHandler<OnPlayerHitDoorEventArgs> OnPlayerHitDoor;
    public class OnPlayerHitDoorEventArgs : EventArgs
    {
        public bool hitDoor;
    }
    [SerializeField] private LayerMask doorLayerMask;
    void Update()
    {
        float playerTriggerHeight = 2f;
        float playerTriggerRadius = 0.5f;
        float distance = 2f;
        bool hitDoor = Physics2D.CapsuleCast(transform.position, new Vector2(playerTriggerHeight, playerTriggerRadius), CapsuleDirection2D.Horizontal, 0f, Vector2.right, distance, doorLayerMask);

        OnPlayerHitDoor?.Invoke(this, new OnPlayerHitDoorEventArgs
        {
            hitDoor = hitDoor
        });
    }
}
