using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private PlayableCharacter character;
    [SerializeField] public Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
           
          hearts[i].sprite = emptyHeart;
           
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < hearts.Length; i++) 
        {
         if(i < character.currentHP)
            {
                hearts[i].sprite = fullHeart;
            }
         else 
            {
                hearts[i].sprite = emptyHeart;
            }
        }

        //add portion to gray out player icon when theyre dead
    }
}
