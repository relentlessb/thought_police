using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FeedbackText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] private ColorMatchingPuzzle colorMatchingPuzzle;

    private void Start()
    {
        colorMatchingPuzzle.OnButtonPressed += ColorMatchingPuzzle_OnButtonPressed;
        Hide();
    }

    private void ColorMatchingPuzzle_OnButtonPressed(object sender, ColorMatchingPuzzle.OnButtonPressedEventArgs e)
    {
        if(e.colorMatch)
        {
            feedbackText.text = "Puzzle Solved!";
            Show();
            Invoke(nameof(Hide), 1f);
        }
        else
        {
            feedbackText.text = "TryAgain!";
            Show();
            Invoke(nameof(Hide), 1f);
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
