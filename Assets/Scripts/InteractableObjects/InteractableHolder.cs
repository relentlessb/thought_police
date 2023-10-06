using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class InteractableHolder : MonoBehaviour
{
    [TextAreaAttribute]
    [SerializeField] string sentence;
    [SerializeField] List<Sprite> alphaNumerImages = new List<Sprite>();
    [SerializeField] List<string> alphaNumerStringKey = new List<string>();
    [SerializeField] KeyCode interactionKey;
    [SerializeField] InteractableBase interactableScript;
    [SerializeField] SceneHandler sceneHandler;
    [SerializeField] GameObject letter;
    bool interacted = false;
    bool canInteract = false;
    bool letterListCreated = false;
    List<GameObject> letterList = new List<GameObject>();
    GameObject player;
    bool notFirst = false;

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
                case "randomDungeon":
                    {
                        if (!interacted && canInteract)
                        {
                            interacted = true;
                            Camera.main.transform.parent = null;
                            Destroy(player);
                            interactableScript.OnRandomDungeonInteract(sceneHandler);
                        }
                        break;
                    }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canInteract = true;
            player = collision.gameObject;
            char[] sentenceChars = sentence.ToCharArray();
            Vector3 letterStart = new Vector3(transform.position.x +.25f, transform.position.y-.1f, 0);
            if (!letterListCreated)
            {
                foreach (char character in sentenceChars)
                {
                    if (char.IsWhiteSpace(character))
                    {
                        letterStart = new Vector3(transform.position.x+.5f, letterStart.y-.25f, 0);
                    }
                    else
                    {
                        letterStart = new Vector3(letterStart.x + .25f, letterStart.y, 0);
                        GameObject newLetterObj = Instantiate(letter, letterStart, Quaternion.identity);
                        newLetterObj.SetActive(false);
                        newLetterObj.GetComponentInChildren<SpriteRenderer>().sprite = alphaNumerImages[alphaNumerStringKey.IndexOf(character.ToString())];
                        letterList.Add(newLetterObj);
                    }
                }
                letterListCreated= true;
                StartCoroutine(displayLetterList(letterList, notFirst));
            }
            else
            {
                StartCoroutine(displayLetterList(letterList, notFirst));
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        canInteract = false;
        StopAllCoroutines();
        foreach (GameObject letter in letterList)
        {
            letter.SetActive(false);
            letter.GetComponentInChildren<Animator>().ResetTrigger("reactivate");
        }
    }

    IEnumerator displayLetterList(List<GameObject> letterList, bool notFirst)
    {
        foreach (GameObject letterObj in letterList)
        {
            if (notFirst)
            {
                letterObj.GetComponentInChildren<Animator>().SetTrigger("reactivate");
            }
            letterObj.SetActive(true);
            yield return new WaitForSeconds(.1f);
        }
    }
}
