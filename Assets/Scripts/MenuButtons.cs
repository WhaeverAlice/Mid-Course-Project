using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
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
