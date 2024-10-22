using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour, IAnimated
{
    [SerializeField] private Animator bgAnim;

    public void GoToGameScene()
    {
        SceneManager.LoadScene("Game");
    }  
    public void GoToGameFromMenu()
    {
        bgAnim.SetTrigger("LoadGame");
        StartCoroutine(WaitForAnimation());
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public IEnumerator WaitForAnimation()
    {
        // Wait until the "LoadGame" animation is done playing
        yield return new WaitUntil(() => bgAnim.GetCurrentAnimatorStateInfo(0).IsName("SkyWipe") &&
                                       bgAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);

        // Now load the new scene after the animation is complete
        SceneManager.LoadScene("Game");
    }

    //public IEnumerator WaitForAnimation()
    //{
    //    yield return new WaitWhile(() => bgAnim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f);
    //    SceneManager.LoadScene("Game");

    //    //yield return new WaitForSeconds(2.3f);
    //}

    public void TestAnim()
    {
        bgAnim.SetTrigger("LoadGame");
        Debug.Log(bgAnim.GetCurrentAnimatorStateInfo(0).IsName("SkyWipe"));
    }
}
