using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HintUI : MonoBehaviour
{
    [SerializeField] private ColorMatchingPuzzle colorMatchingPuzzle;
    [SerializeField] private TextMeshProUGUI redtargetColorTextHint;
    [SerializeField] private TextMeshProUGUI greentargetColorTextHint;
    [SerializeField] private TextMeshProUGUI bluetargetColorTextHint;
    [SerializeField] private TargetColorDisplay targetColorDisplay;

    private int showHintAttemptMax = 3;

    private void Start()
    {
        colorMatchingPuzzle.OnAttempt += ColorMatchingPuzzle_OnAttempt;
        Hide();
    }

    private void ColorMatchingPuzzle_OnAttempt(object sender, ColorMatchingPuzzle.OnAttemptEventArgs e)
    {
        if (e.attempt > showHintAttemptMax)
        {
            Show();
            HintDisplay();
        }
    }

    private void HintDisplay()
    {
        redtargetColorTextHint.text = targetColorDisplay.GetTargetColor().r.ToString("F2");
        greentargetColorTextHint.text = targetColorDisplay.GetTargetColor().g.ToString("F2");
        bluetargetColorTextHint.text = targetColorDisplay.GetTargetColor().b.ToString("F2");
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
