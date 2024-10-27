using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharSwitcher : MonoBehaviour
{
    [SerializeField] public GameObject[] characters;
    public PlayableCharacter activeChar;
    int currentActive;
    public int avialableChars = 3;

    private void Awake()
    {
        //starts the game with a random character
        SetRandomCharacter();
    }

    public void SwitchChar(string dir) //changes active character 
    {
        if (!PlayableCharacter.dead)
        {
            switch (dir)
            {
                case "right": 
                    if (currentActive == characters.Length - 1) currentActive = 0;
                    else currentActive++;
                    break;

                case "left":
                    if (currentActive == 0) currentActive = 2;
                    else currentActive--;
                    break;
            }

            for (int i = 0; i < characters.Length; i++)
            {
                //enable new active character
                if (i == currentActive)
                {
                    //transforms new active character to current acitve character's position
                    characters[i].transform.position = activeChar.transform.position; 
                    characters[i].SetActive(true);

                    //sets new active character
                    activeChar = characters[i].GetComponent<PlayableCharacter>();
                    
                    //return collison between player and traps in case the inulnerable coroutine was interrupted by the switch
                    Physics2D.IgnoreLayerCollision(11, 7, false); 
                    activeChar.isInvulnerable = false;
                }
                //disable all other characters
                else characters[i].SetActive(false); 
            }
        }
    }
    
    public void SetRandomCharacter()
    {
        foreach (GameObject chara in characters)
        {
            chara.SetActive(false);
        }
        int randomIndex = Random.Range(0, characters.Length);
        GameObject character = characters[randomIndex];
        character.SetActive(true);
        activeChar = character.GetComponent<PlayableCharacter>();
        currentActive = randomIndex;
    }
}
