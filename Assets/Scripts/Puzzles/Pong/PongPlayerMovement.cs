using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongPlayerMovement : MonoBehaviour
{
    [SerializeField] float playerBaseSpeed;
    [SerializeField] Rigidbody2D playerPhys;

    void Update()
    {
        playerPhys.velocity += new Vector2(0, Input.GetAxis("Vertical")*playerBaseSpeed/100);
    }
}
