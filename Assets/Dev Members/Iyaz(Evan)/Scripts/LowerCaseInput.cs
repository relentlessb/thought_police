using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LowerCaseInput : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;

    private void Start()
    {
        // Attach a custom validation function to the InputField's onValidateInput event.
        inputField.onValidateInput += InputField_OnValidateInput; 
    }

    private char InputField_OnValidateInput(string text, int charIndex, char addedChar)
    {
        //Converts the added character to lower case
        char lowerCaseChar = char.ToLower(addedChar);

        // Checks if the character is a lower case letter between a-z
        if(lowerCaseChar >= 'a' && lowerCaseChar <= 'z')
        {
            // Allows the character
            return lowerCaseChar;
        }
        else
        {
            return '\0';
        }
    }

    private void OnDisable()
    {
        if (inputField != null)
        {
            inputField.onValidateInput -= InputField_OnValidateInput;
        }
    }
}
