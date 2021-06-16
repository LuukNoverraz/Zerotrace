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
        levelKeys[amountOfKeys].sprite = keySprites[1];
        amountOfKeys++;

        if (amountOfKeys == keySprites.Length)
        {
            NextLevel();
        }
    }

    public void NextLevel()
    {
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
