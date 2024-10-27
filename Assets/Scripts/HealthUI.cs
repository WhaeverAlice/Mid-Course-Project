using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] public Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;
    private Color iconColor;
    
    void Start()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
          hearts[i].sprite = fullHeart;
        }
    }

    void Update()
    {
        //cahnge heart sprites according to current player health
        for (int i = 0; i < hearts.Length; i++) 
        {
         if(i < PlayableCharacter.currentHP)
            {
                hearts[i].sprite = fullHeart;
            }
         else 
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
}
