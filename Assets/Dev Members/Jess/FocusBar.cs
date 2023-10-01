using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FocusBar : MonoBehaviour
{
    public Slider focusBar;
    public int focusLevel;
    public int maxFocus;

    private void Start()
    {
        // Retrieves and initialises focus bar object; avoids NullReferenceException
        focusBar = GetComponent<Slider>();

        focusBar.maxValue = maxFocus;
        focusBar.value = maxFocus;
    }

    // Updates focus bar value to correspond to focus level
    void SetFocusBar()
    {
        focusBar.value = focusLevel;
    }

    /* Changes the focus level.
    The bool parameter increase determines whether the focus level will increase or decrease.
    The int parameter amount determines the amount added to/subtracted from the focus level. */
    void ChangeFocus(bool increase, int amount)
    {
        // Changes focus level depending on whether increase is true
        focusLevel = increase ? focusLevel + amount : focusLevel - amount;
        SetFocusBar();
    }
}
