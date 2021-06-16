using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public string currentLevel;
    public Sprite[] keySprites;
    public Image[] levelKeys;
    public int amountOfKeys = 0;

    public void GotKey()
    {
        // Change sprite of key to unlocked
        levelKeys[amountOfKeys].sprite = keySprites[1];
        amountOfKeys++;

        if (amountOfKeys == keySprites.Length)
        {
            NextLevel();
        }
    }

    public void NextLevel()
    {
        // Goes to the next level or to the menu if playing last stage
        Debug.Log("Next Level");
        if (currentLevel == "Tutorial")
        { 
            SceneManager.LoadScene("Level 01", LoadSceneMode.Single);
        }
        if (currentLevel == "Level 01")
        {
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
    }
}
