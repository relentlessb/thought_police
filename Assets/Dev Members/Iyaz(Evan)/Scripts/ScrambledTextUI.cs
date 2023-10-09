using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScrambledTextUI : MonoBehaviour
{
    [SerializeField] private WordDataSO wordDataSO;
    [SerializeField] private TextMeshProUGUI scrambledText;
    public string SelectedWord { get; private set; }

    private void Start()
    {
        ShuffleWords();
    }
    private void ShuffleWords()
    {
        string[] words = wordDataSO.words; // Get the array of words from the WordDataSO
        int randomIndex = Random.Range(0, words.Length);
        string selectedWord = words[randomIndex];

        char[] charArray = selectedWord.ToCharArray(); //Converts the word to an array of character

        System.Random rng = new System.Random();

        int n = charArray.Length;
        while (n > 1)
        {
            //Shuffle characters using Fisher-Yates shuffle algorithm
            n--;
            int k = rng.Next(n + 1);
            char value = charArray[k];
            charArray[k] = charArray[n];
            charArray[n] = value;
        }
        string scrambledWord = new string(charArray);

        scrambledText.text = scrambledWord;

        SelectedWord = selectedWord;
    }
}
