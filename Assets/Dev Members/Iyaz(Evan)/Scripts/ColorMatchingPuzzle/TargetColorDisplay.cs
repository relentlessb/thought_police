using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TargetColorDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI targetColorText;
    [SerializeField] private Image targetColorImage;

    private Color targetColor;

    private void Awake()
    {
        targetColorText.text = "Target Color";
    }

    private void Start()
    {
        GenerateRandomTargetColor();
    }

    private void GenerateRandomTargetColor()
    {
        Color targetColorDisplay = new Color(Random.value, Random.value, Random.value);
        targetColor = targetColorDisplay;
        targetColorImage.color = targetColorDisplay;
    }

    public Color GetTargetColor()
    {
        return targetColor;
    }
}
