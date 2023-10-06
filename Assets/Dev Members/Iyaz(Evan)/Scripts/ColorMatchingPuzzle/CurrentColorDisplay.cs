using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CurrentColorDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentColorText;
    [SerializeField] private Image currentColorImage;
    [SerializeField] private ColorSliders colorSliders;
    [SerializeField] private ColorMatchingPuzzle colorMatchingPuzzle;
    private void Awake()
    {
        currentColorText.text = "Current Color";
    }

    private void Start()
    {
        colorMatchingPuzzle.OnButtonPressed += ColorMatchingPuzzle_OnButtonPressed;
    }

    private void ColorMatchingPuzzle_OnButtonPressed(object sender, ColorMatchingPuzzle.OnButtonPressedEventArgs e)
    {
        if(e.colorMatch)
        {
            Hide();
        }
        else
        {
            Hide();
            Invoke(nameof(Show), 2f);
        }
    }

    private void Update()
    {
        currentColorImage.color = colorSliders.GetCurrentColor();
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
