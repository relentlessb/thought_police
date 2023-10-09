using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class BaseCutsceneAction : ScriptableObject
{
    public new string name;
    public string type;

    //"direction" Action
    [TextArea(3, 10)]
    public string[] sentences;
    //"move" Action
    public Vector3 destinationPos;
    //"sound" Action
    public AudioSource audioClip;
    //"sprite" Action
    public Sprite sprite;
}
