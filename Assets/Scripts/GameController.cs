using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
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
    }
}
