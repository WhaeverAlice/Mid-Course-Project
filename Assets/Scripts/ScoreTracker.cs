using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    [SerializeField] public TMP_Text currentScoreText;
    [SerializeField] public TMP_Text finalScoreText;
    [SerializeField] public TMP_Text highScoreText;
    private int currentScore = 0;
    public int finalScore;
    public int highScore;


    void Update()
    {
        currentScoreText.text = currentScore.ToString();
        finalScore = currentScore;
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

        finalScoreText.text = finalScore.ToString();
        highScoreText.text = PlayerPrefs.GetInt("SavedHighScore").ToString();
    }

    public void IncreaseScore(int byThisMuch)
    {
        currentScore += byThisMuch;
    }
}
