using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuNavigator : MonoBehaviour
{
    [SerializeField] AudioSource buttonPress;
    [SerializeField] Image newGameBackground;
    [SerializeField] Image exitBackground;
    [SerializeField] SceneHandler sceneHandler;
    List<Image> menuButtons;
    SceneHandler sceneHandlerObj;
    int chosen = 0;
    Color lightYellow = new Color(1f, .91f, .62f);
    Color white = Color.white;
    private void Start()
    {
        menuButtons = new List<Image> { };
        menuButtons.Add(newGameBackground); menuButtons.Add(exitBackground);
        menuButtons[chosen].color = lightYellow;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            buttonPress.Play();
            menuButtons[chosen].color = white;
            if (chosen == menuButtons.Count-1)
            {
                chosen = 0;
            }
            else
            {
                chosen += 1;
            }
            menuButtons[chosen].color = lightYellow;

        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            buttonPress.Play();
            menuButtons[chosen].color = white;
            if (chosen == 0)
            {
                chosen = menuButtons.Count-1;
            }
            else
            {
                chosen -= 1;
            }
            menuButtons[chosen].color = lightYellow;
        }
        else if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            buttonPress.Play();
            if (chosen == 0)
            {
                DontDestroyOnLoad(Camera.main);
                sceneHandlerObj = Instantiate(sceneHandler);
            }
            else if (chosen == 1)
            {
                Application.Quit();
                Debug.Log("If the game hasn't quit, it's because it is running in the editor.");
            }
        }
    }
}
