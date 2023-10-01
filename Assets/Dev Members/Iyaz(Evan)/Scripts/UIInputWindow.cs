using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.UI;
using UnityEngine.UI;

public class UIInputWindow : MonoBehaviour
{
    public event EventHandler<OnPlayerAnswerEventArgs> OnPlayerAnswer;
    public class OnPlayerAnswerEventArgs : EventArgs
    {
        public string feedbackText;
    }
    public event EventHandler<OnAnswerCorrectEventArgs> OnAnswerCorrect;
    public class OnAnswerCorrectEventArgs : EventArgs
    {
        public bool isAnswerCorrect;
    }

    [SerializeField] private TMP_InputField answerInputField;
    [SerializeField] private Button submitButton;
    [SerializeField] private ScrambledTextUI scrambledTextUI;
    [SerializeField] private TextMeshProUGUI feedbackText;
  
    private void Update()
    {
        submitButton.onClick.AddListener(CheckAnswer);
    }

    private void CheckAnswer()
    {
        string playerAnswer = answerInputField.text;

        if(playerAnswer == scrambledTextUI.SelectedWord)
        {
            // Correct Answer
            feedbackText.text = "Correct!";

            OnPlayerAnswer?.Invoke(this, new OnPlayerAnswerEventArgs
            {
                feedbackText = feedbackText.text
            });

            Hide();
        }

        else
        {
            // Wrong answer
            feedbackText.text = "Try Again!";
            OnPlayerAnswer?.Invoke(this, new OnPlayerAnswerEventArgs
            {
                feedbackText = feedbackText.text
            });
            Hide();
            Invoke(nameof(Show), 1f);
        }

        // We listen to this event in the Scene Manager
        OnAnswerCorrect?.Invoke(this, new OnAnswerCorrectEventArgs
        {
            isAnswerCorrect = playerAnswer == scrambledTextUI.SelectedWord 
        });
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
