using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InteractableBase : ScriptableObject
{
    public string type;
    public string puzzleSceneName;
    public virtual void OnPuzzleInteract(string puzzleSceneName, SceneHandler sceneHandler)
    {
        sceneHandler.loadTemporaryScene(puzzleSceneName);
    }
}
