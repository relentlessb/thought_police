using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ColorMatchingPuzzle : MonoBehaviour
{
    public event EventHandler<OnButtonPressedEventArgs> OnButtonPressed;
    public class OnButtonPressedEventArgs : EventArgs
    {
        public bool colorMatch;
    }

    public event EventHandler<OnPuzzleSolvedEventArgs> OnPuzzleSoved;
    public class OnPuzzleSolvedEventArgs : EventArgs
    {
        public bool isPuzzleSolved;
    }

    public event EventHandler<OnAttemptEventArgs> OnAttempt;
    public class OnAttemptEventArgs : EventArgs
    {
        public int attempt;
    }

    [SerializeField] private TargetColorDisplay targetColorDisplay;
    [SerializeField] private ColorSliders colorSliders;
    [SerializeField] private Button submitButton;

    private float errorTolerance = 0.2f;
    private int attemptCount = 0;
    private void Start()
    {
        submitButton.onClick.AddListener(CheckForColorMatch);
    }

    private bool ColorMatch(TargetColorDisplay targetColorDisplay, ColorSliders coloSliders)
    {
        float difference = Mathf.Abs(targetColorDisplay.GetTargetColor().r - coloSliders.GetCurrentColor().r) + Mathf.Abs(targetColorDisplay.GetTargetColor().g - coloSliders.GetCurrentColor().g) + Mathf.Abs(targetColorDisplay.GetTargetColor().b - coloSliders.GetCurrentColor().b);
        return difference < errorTolerance;
    }

    private void CheckForColorMatch()
    {

        if (ColorMatch(targetColorDisplay, colorSliders))
        {
            Debug.Log("Puzzle Solved");

            OnButtonPressed?.Invoke(this, new OnButtonPressedEventArgs
            {
                colorMatch = ColorMatch(targetColorDisplay, colorSliders)
            });

            OnPuzzleSoved?.Invoke(this, new OnPuzzleSolvedEventArgs
            {
                isPuzzleSolved = ColorMatch(targetColorDisplay, colorSliders) // To be listened in the SceneManger 
            });
        }
        else
        {
            Debug.Log("Try Again!");

            OnButtonPressed?.Invoke(this, new OnButtonPressedEventArgs
            {
                colorMatch = ColorMatch(targetColorDisplay, colorSliders)
            });

            OnPuzzleSoved?.Invoke(this, new OnPuzzleSolvedEventArgs
            {
                isPuzzleSolved = ColorMatch(targetColorDisplay, colorSliders) // // To be listened in the SceneManger 
            });
        }

        attemptCount++;
        OnAttempt?.Invoke(this, new OnAttemptEventArgs
        {
            attempt = attemptCount
        });
    }
}
