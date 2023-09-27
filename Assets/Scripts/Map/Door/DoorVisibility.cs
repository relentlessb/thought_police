using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorVisibility : MonoBehaviour
{
    private void Start()
    {
        PlayerDoorTrigger playerDoorTrigger = GameObject.FindWithTag("Player").GetComponent<PlayerDoorTrigger>();

        playerDoorTrigger.OnPlayerHitDoor += PlayerToDoorAngle_OnPlayerHitDoor;
    }

    private void PlayerToDoorAngle_OnPlayerHitDoor(object sender, PlayerDoorTrigger.OnPlayerHitDoorEventArgs e)
    {
        if (e.hitDoor)
        {
            Hide();
            Invoke(nameof(Show), 2f);
        }
    }

   private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
