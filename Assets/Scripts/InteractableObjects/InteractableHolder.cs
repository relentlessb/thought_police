using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableHolder : MonoBehaviour
{
    [SerializeField] KeyCode interactionKey;
    [SerializeField] InteractableBase interactableScript;
    [SerializeField] SceneHandler sceneHandler;
    bool interacted = false;
    bool canInteract = false;
    GameObject player;

    private void Update()
    {
        if (Input.GetKeyDown(interactionKey))
        {
            switch (interactableScript.type)
            {
                case "puzzle":
                    {
                        if (!interacted && canInteract)
                        {
                            interacted = true;
                            player.SetActive(false);
                            interactableScript.OnPuzzleInteract(interactableScript.puzzleSceneName, sceneHandler);
                        }
                        break;
                    }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            canInteract = true;
            player = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        canInteract = false;
    }
}
