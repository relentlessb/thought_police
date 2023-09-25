using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] PlayerToDoorAngle playerToDoorAngle;
    private void Start()
    {
        playerToDoorAngle.OnPlayerHitDoor += PlayerToDoorAngle_OnPlayerHitDoor;
    }

    private void PlayerToDoorAngle_OnPlayerHitDoor(object sender, PlayerToDoorAngle.OnPlayerHitDoorEventArgs e)
    {
        if (e.hitDoor)
        {
            Debug.Log("SphereCast Hitting door");
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
