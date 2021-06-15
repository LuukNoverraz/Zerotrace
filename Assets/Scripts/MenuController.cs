using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public Animation[] menuAnimations;
    public Button[] buttons;
    private bool inControlsMenu = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace) && inControlsMenu)
        {
            ControlsExit();
        }
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
    }

    public void ControlsButton()
    {
        menuAnimations[0].Play();
        buttons[0].interactable = false;
        buttons[1].interactable = false;
        buttons[2].interactable = false;
        buttons[3].interactable = true;
        inControlsMenu = true;
    }

    public void ControlsExit()
    {
        menuAnimations[1].Play();
        buttons[0].interactable = true;
        buttons[1].interactable = true;
        buttons[2].interactable = true;
        buttons[3].interactable = false;
        inControlsMenu = false;
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
