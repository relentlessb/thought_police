using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeypadPuzzle : MonoBehaviour
{
    [SerializeField] private Text Answer;
    public Text colorChangingFont;
    private string Ans = "1984";
    // Update is called once per frame
    public void Number(int number)
    {
        Answer.text += number.ToString();
    }
    public void Enter()
    {
        if (Answer.text == Ans)
        {
            colorChangingFont.color = Color.green;
            Answer.text = "CORRECT";
        }
        else
        {
           colorChangingFont.color = Color.red;
            Answer.text = "INCORRECT";
        }
    }
    public void Clear()
    {
        Answer.text = string.Empty;
    }

}
