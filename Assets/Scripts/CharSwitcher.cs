using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject[] characters;
    private PlayableCharacter activeChar;
    int currentActive = 0;

    public PlayableCharacter SwitchChar(string dir)
    {
        switch (dir)
        {
            case "right":
                if (currentActive == characters.Length - 1) currentActive = 0;
                else currentActive++;
                for (int i = 0; i < characters.Length; i++)
                {
                    if (i == currentActive)
                    {
                        characters[i].SetActive(true);
                        activeChar = characters[i].GetComponent<PlayableCharacter>();
                    }
                    else characters[i].SetActive(false);
                }
                break;
               
            case "left":
                if (currentActive == 0) currentActive = 2;
                else currentActive--;
                for (int i = 0; i < characters.Length; i++)
                {
                    if (i == currentActive)
                    {
                        characters[i].SetActive(true);
                        activeChar = characters[i].GetComponent<PlayableCharacter>();
                    }
                    else characters[i].SetActive(false);
                }
                break;
        }
        return activeChar;
    }

    public PlayableCharacter SetRandomCharacter()
    {
        GameObject character = characters[Random.Range(0, characters.Length)];
        character.SetActive(true);
        activeChar = character.GetComponent<PlayableCharacter>();
        return activeChar;
    }
}