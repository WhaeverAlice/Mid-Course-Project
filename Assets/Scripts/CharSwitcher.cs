using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject[] characters;
    public PlayableCharacter activeChar;
    int currentActive = 0;
    public int avialableChars = 3;

    public void SwitchChar(string dir)
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
            if (i == currentActive)
            {
                if (characters[i].GetComponent<PlayableCharacter>().dead)
                {
                    SwitchChar(dir);
                }
                else 
                {
                    characters[i].transform.position = activeChar.transform.position;
                    characters[i].SetActive(true);
                    activeChar = characters[i].GetComponent<PlayableCharacter>();
                    activeChar.BecomeInvulnerable(); //charater get damage when switched
                }
            }
            else characters[i].SetActive(false);
        }
       // return activeChar;
    }

    public void SetRandomCharacter()
    {
        foreach (GameObject chara in characters)
        {
            chara.SetActive(false);
        }
        GameObject character = characters[Random.Range(0, characters.Length)];
        character.SetActive(true);
        activeChar = character.GetComponent<PlayableCharacter>();
       // return activeChar;
    }

    public GameObject GetActiveCharacter()
    {
        GameObject character = activeChar.gameObject;
        return character;
    }
}
