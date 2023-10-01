using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public IEnumerator ShowDoor()
    {
        yield return new WaitForSeconds(1.0F);
        gameObject.SetActive(true);
    }

    public void HideDoor()
    {
        gameObject.SetActive(false);
    }
}
