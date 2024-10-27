using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    [SerializeField] public TMP_Text currentScoreText;
    [SerializeField] public TMP_Text currentScoreTextPause;
    [SerializeField] public TMP_Text finalScoreText;
    [SerializeField] public TMP_Text highScoreText;
    [SerializeField] public TMP_Text highScoreTextPause;
    private int currentScore = 0;


    void Update()
    {
        currentScoreText.text = currentScore.ToString();
        currentScoreTextPause.text = currentScore.ToString();
        highScoreTextPause.text = PlayerPrefs.GetInt("SavedHighScore").ToString();
    }

    public void UpdateHighScore()
    {
        if (PlayerPrefs.HasKey("SavedHighScore"))
        {
            if (currentScore > PlayerPrefs.GetInt("SavedHighScore"))
            {
                PlayerPrefs.SetInt("SavedHighScore", currentScore);
            }
        }
        else
        {
            PlayerPrefs.SetInt("SavedHighScore", currentScore);
        }

        finalScoreText.text = currentScore.ToString();
        highScoreText.text = PlayerPrefs.GetInt("SavedHighScore").ToString();
    }

    public void OverwriteHighScore()
    {
        PlayerPrefs.SetInt("SavedHighScore", currentScore);
        highScoreText.text = PlayerPrefs.GetInt("SavedHighScore").ToString();
    }

    public void IncreaseScore(int byThisMuch)
    {
        currentScore += byThisMuch;
        if (byThisMuch > 1) 
        {
            currentScoreText.color = Color.yellow;
            StartCoroutine(DelayColorReturn());
        }
    }

    public void ResetCurrentScore()
    {
        currentScore = 0;
    }

    IEnumerator DelayColorReturn()
    {
        yield return new WaitForSeconds(0.2f);
        currentScoreText.color = Color.white;
    }
}
