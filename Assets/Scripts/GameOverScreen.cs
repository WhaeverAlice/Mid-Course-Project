using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] ScoreTracker scoreTracker;


    public void SetUp(int score, int highscore)
    {
        gameObject.SetActive(true);
    }
}
