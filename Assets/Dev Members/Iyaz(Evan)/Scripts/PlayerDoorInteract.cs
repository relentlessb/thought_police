using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoorInteract : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Door door = collision.GetComponent<Door>();
        if (door != null)
        {
            door.HideDoor();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Door door = collision.GetComponent<Door>();
        if (door != null)
        {
            StartCoroutine(door.ShowDoor());
        }
    }
}
