using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToDoorAngle : MonoBehaviour
{
    public event EventHandler<OnPlayerHitDoorEventArgs> OnPlayerHitDoor;
    public class OnPlayerHitDoorEventArgs : EventArgs
    {
        public bool hitDoor;
    }

    [SerializeField] private Transform doorTransform;
    [SerializeField] private LayerMask doorLayerMask;

    private Vector2 doorPos;
    private Vector2 playerPos;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = gameObject.transform.position;
        doorPos = doorTransform.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerOpenDoor();
    }

    private Vector2 GetPlayerDistanceFromDoor()
    {
        Vector2 playerToDoorDistance = playerPos - doorPos;
        return playerToDoorDistance;
    }

    private float GetPlayerAngleFromDoor()
    {
        Vector2 playerDistanceFromDoor = GetPlayerDistanceFromDoor();
        float angleInRad = (playerDistanceFromDoor.y / playerDistanceFromDoor.x) * Mathf.Deg2Rad;
        float angle = Mathf.Atan(angleInRad);
        return angle;
    }

    private void PlayerOpenDoor()
    {
        float playerHeight = 2f; // To be changed later
        float playerRadius = 0.5f; // To be changed laterF
        float distance = 2f;
        bool hitDoor = Physics2D.CapsuleCast(transform.position, new Vector2(playerHeight, playerRadius), CapsuleDirection2D.Horizontal, 0f, Vector2.right, distance, doorLayerMask);

        OnPlayerHitDoor?.Invoke(this, new OnPlayerHitDoorEventArgs
        {
            hitDoor = hitDoor
        });
    }

}
