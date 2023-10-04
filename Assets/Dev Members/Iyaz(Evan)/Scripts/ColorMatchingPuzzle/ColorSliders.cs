using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ColorSliders : MonoBehaviour
{
    [SerializeField] private Slider redSlider;
    [SerializeField] private Slider greenSlider;
    [SerializeField] private Slider blueSlider;
    [SerializeField] private TextMeshProUGUI redValueText;
    [SerializeField] private TextMeshProUGUI greenValueText;
    [SerializeField] private TextMeshProUGUI blueValueText;
    [SerializeField] private ColorMatchingPuzzle colorMatchingPuzzle;

    private Color currentColor;

    private void Awake()
    {
        InitializeSliderValue();
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
        UpdateColorValues();
    }

    private void InitializeSliderValue()
    {
        // Initializes the redSlider value
        redSlider.value = 0f;
        greenSlider.value = 0f;
        blueSlider.value = 0f;
    }

    private void UpdateColorValues()
    {
        // Stores the value for the updated slider
        float r = redSlider.value;
        float g = greenSlider.value;
        float b = blueSlider.value;

        Color colorValue = new Color(r, g, b);

        // Updates the color value text
        redValueText.text = $"{r:F2}";
        greenValueText.text = $"{g:F2}";
        blueValueText.text = $"{b:F2}";

        currentColor = colorValue;
    }

    public Color GetCurrentColor()
    {
        return currentColor;
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
