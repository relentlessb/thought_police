using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CutsceneDirector : MonoBehaviour
{
    //Editor Inserted Actors and Animation Setup
    [SerializeField] List<Actor> actorsOnScreen = new List<Actor>();
    [SerializeField] Animator dialogueAnimator;
    [SerializeField] TextMeshProUGUI dialogueInsert;
    [SerializeField] KeyCode passText;
    [SerializeField] float textTypeTime;

    Queue<string> dialogueSentences;
    string fullSentence = "";
    char[] splitSentence = new char[0];
    int charArrayIndex = 0;
    int lastCharArrayIndex = -1;

    private void Start()
    {
        //Instantiate Necessary Actors
        foreach(Actor actorObj in actorsOnScreen)
        {
            actorObj.transform.position = actorObj.startPosition;
        }
    }
    private void Update()
    {
        foreach(Actor actorObj in actorsOnScreen)
        {
            if (actorObj.currentAction <= actorObj.actions.Count - 1)
            {
                switch (actorObj.actions[actorObj.currentAction].type)
                {
                    case "move":
                        {
                            switch (actorObj.state)
                            {
                                case Actor.actionState.ready:
                                    {
                                        actorObj.startPosition = actorObj.transform.position;
                                        actorObj.totalMoveTime = actorObj.actionTimes[actorObj.currentAction];
                                        actorObj.actionTimer = actorObj.actionTimes[actorObj.currentAction];
                                        actorObj.state = Actor.actionState.inProgress;
                                        break;
                                    }
                                case Actor.actionState.inProgress:
                                    {
                                        actorObj.actionTimer -= Time.deltaTime;
                                        actorObj.actorPhys.MovePosition(Vector2.Lerp(actorObj.startPosition, actorObj.actions[actorObj.currentAction].destinationPos, (actorObj.totalMoveTime - actorObj.actionTimer) / actorObj.totalMoveTime));
                                        if (actorObj.actionTimer <= 0) { actorObj.currentAction++; actorObj.state = Actor.actionState.ready; actorObj.transform.position = actorObj.actions[actorObj.currentAction - 1].destinationPos; }
                                        break;
                                    }
                            }
                            break;
                        }
                    case "dialogue":
                        {
                            switch (actorObj.state)
                            {
                                case Actor.actionState.ready:
                                    {
                                        dialogueAnimator.SetBool("showDialogue", true);
                                        actorObj.actionTimer += Time.deltaTime;
                                        if (actorObj.actionTimer > 1)
                                        {
                                        dialogueInsert.text = "";
                                        dialogueSentences = new Queue<string>();
                                        foreach (string sentence in actorObj.actions[actorObj.currentAction].sentences)
                                        {
                                            dialogueSentences.Enqueue(sentence);
                                        }
                                        actorObj.state = Actor.actionState.slowText;
                                        fullSentence = dialogueSentences.Dequeue();
                                        splitSentence = fullSentence.ToCharArray();                                        
                                        actorObj.actionTimer = 0;
                                        }

                                        break;
                                    }
                                case Actor.actionState.inProgress:
                                    {
                                        if (dialogueSentences.Count <= 0 && Input.GetKeyDown(passText))
                                        {
                                            dialogueAnimator.SetBool("showDialogue", false);
                                            actorObj.currentAction++;
                                            actorObj.state = Actor.actionState.ready;
                                        }
                                        else if (Input.GetKeyDown(passText))
                                        {
                                            dialogueInsert.text = "";
                                            fullSentence = dialogueSentences.Dequeue();
                                            splitSentence = fullSentence.ToCharArray();
                                            actorObj.state = Actor.actionState.slowText;
                                        }
                                        break;
                                    }
                                case Actor.actionState.slowText:
                                    {
                                        if(dialogueInsert.text == fullSentence)
                                        {
                                            actorObj.state = Actor.actionState.inProgress;
                                            charArrayIndex = 0;
                                            break;
                                        }
                                        actorObj.actionTimer += Time.deltaTime;
                                        if (actorObj.actionTimer > textTypeTime)
                                        {
                                            actorObj.actionTimer = 0;
                                            charArrayIndex++;
                                        }
                                        if (charArrayIndex != lastCharArrayIndex)
                                        {
                                            lastCharArrayIndex = charArrayIndex;
                                            dialogueInsert.text += splitSentence[charArrayIndex];
                                        }
                                        if (Input.GetKeyDown(passText))
                                        {
                                            actorObj.state = Actor.actionState.inProgress;
                                            dialogueInsert.text = fullSentence;
                                            charArrayIndex = 0;
                                        }
                                        break;
                                    }
                            }
                            break;
                        }


                    case "wait":
                        {
                            switch (actorObj.state)
                            {
                                case Actor.actionState.ready:
                                    {
                                        actorObj.actionTimer = actorObj.actionTimes[actorObj.currentAction];
                                        actorObj.state = Actor.actionState.inProgress;
                                        break;
                                    }
                                case Actor.actionState.inProgress:
                                    {
                                        actorObj.actionTimer -= Time.deltaTime;
                                        
                                        if (actorObj.actionTimer <= 0) { actorObj.currentAction++; actorObj.state = Actor.actionState.ready; }
                                        break;
                                    }
                            }
                            break;
                        }
                }
            }
        }
    }
}
