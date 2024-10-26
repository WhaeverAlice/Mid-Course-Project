using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    //[SerializeField] private GameObject menu;
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResetHighScore()
    {
        PlayerPrefs.SetInt("SavedHighScore", 0);
    }

    public void DisableGameObject()
    {
        this.gameObject.SetActive(false);
    }
    public void EnableGameObject()
    {
        this.gameObject.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
    }
}
