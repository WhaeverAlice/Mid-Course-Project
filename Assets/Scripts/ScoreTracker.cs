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
    public bool hardMode;
    private int currentScore = 0;

    void Update()
    {
        currentScoreText.text = currentScore.ToString();
        currentScoreTextPause.text = currentScore.ToString();
        if (hardMode)
        {
            highScoreTextPause.text = PlayerPrefs.GetInt("SavedHighScoreHardMode").ToString();
        }
        else
        {
            highScoreTextPause.text = PlayerPrefs.GetInt("SavedHighScore").ToString();
        }
    }
    public void IncreaseScore(int byThisMuch)
    {
        currentScore += byThisMuch;

        //make score text flash yellow when bonus score is earned
        if (byThisMuch > 1)
        {
            currentScoreText.color = Color.yellow;
            StartCoroutine(DelayColorReturn());
        }
    }
    IEnumerator DelayColorReturn()
    {
        yield return new WaitForSeconds(0.2f);
        currentScoreText.color = Color.white;
    }
    public void UpdateHighScore()
    {
        if (hardMode)
        {
            //updates highscore if current score is higher than current higscore
            if (PlayerPrefs.HasKey("SavedHighScoreHardMode"))
            {
                if (currentScore > PlayerPrefs.GetInt("SavedHighScoreHardMode"))
                {
                    PlayerPrefs.SetInt("SavedHighScoreHardMode", currentScore);
                }
            }
            //if no previous highscore exists, sets new highscore
            else
            {
                PlayerPrefs.SetInt("SavedHighScoreHardMode", currentScore);
            }

            finalScoreText.text = currentScore.ToString();
            highScoreText.text = PlayerPrefs.GetInt("SavedHighScoreHardMode").ToString();
        }
        else
        {
            //updates highscore if current score is higher than current higscore
            if (PlayerPrefs.HasKey("SavedHighScore"))
            {
                if (currentScore > PlayerPrefs.GetInt("SavedHighScore"))
                {
                    PlayerPrefs.SetInt("SavedHighScore", currentScore);
                }
            }
            //if no previous highscore exists, sets new highscore
            else
            {
                PlayerPrefs.SetInt("SavedHighScore", currentScore);
            }

            finalScoreText.text = currentScore.ToString();
            highScoreText.text = PlayerPrefs.GetInt("SavedHighScore").ToString();
        }
    }

    public void OverwriteHighScore() //manually set new highscore
    {
        if (hardMode)
        {
            PlayerPrefs.SetInt("SavedHighScoreHardMode", currentScore);
            highScoreText.text = PlayerPrefs.GetInt("SavedHighScoreHardMode").ToString();
        }
        else
        {
            PlayerPrefs.SetInt("SavedHighScore", currentScore);
            highScoreText.text = PlayerPrefs.GetInt("SavedHighScore").ToString();
        }
    }
    public void ResetCurrentScore()
    {
        currentScore = 0;
    }
}
