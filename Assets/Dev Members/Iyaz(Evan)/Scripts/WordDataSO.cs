using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWordData", menuName = "WordPuzzle")]
public class WordDataSO : ScriptableObject
{
    public string[] words;
}
