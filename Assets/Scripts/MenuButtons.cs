using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResetHighScore()
    {
        PlayerPrefs.SetInt("SavedHighScore", 0);
    }
}
