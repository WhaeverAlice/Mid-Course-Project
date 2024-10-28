using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour, IAnimated
{
    [SerializeField] private Animator bgAnim;

    public void GoToGameScene()
    {
        SceneManager.LoadScene("Game");
    } 
    public void GoToGameHardScene()
    {
        SceneManager.LoadScene("GameHard");
    }  
    public void GoToGameFromMenu()
    {
        //play loading animation
        bgAnim.SetTrigger("LoadGame");
        StartCoroutine(WaitForAnimation());
    }
    public void GoToGameFromMenuHard()
    {
        //play loading animation
        bgAnim.SetTrigger("LoadGame");
        StartCoroutine(WaitForAnimationHard());
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public IEnumerator WaitForAnimation()
    {
        //Wait until the "LoadGame" animation is done playing
        yield return new WaitUntil(() => bgAnim.GetCurrentAnimatorStateInfo(0).IsName("SkyWipe") &&
                                       bgAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);

        //load the new scene after the animation is complete
        SceneManager.LoadScene("Game");
    }
    public IEnumerator WaitForAnimationHard()
    {
        //Wait until the "LoadGame" animation is done playing
        yield return new WaitUntil(() => bgAnim.GetCurrentAnimatorStateInfo(0).IsName("SkyWipe") &&
                                       bgAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);

        //load the new scene after the animation is complete
        SceneManager.LoadScene("GameHard");
    }
}
