using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FeedBackTextUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] private UIInputWindow inputFieldWindow;

    private void Start()
    {
        inputFieldWindow.OnPlayerAnswer += InputFieldWindow_OnPlayerAnswer;
        Hide();
    }

    private void InputFieldWindow_OnPlayerAnswer(object sender, UIInputWindow.OnPlayerAnswerEventArgs e)
    {
        if(e.feedbackText == feedbackText.text)
        {
            Show();
            Invoke(nameof(Hide), 1.5f);
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
