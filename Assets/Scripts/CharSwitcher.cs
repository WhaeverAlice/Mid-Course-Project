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
        SetRandomCharacter();
        //currentActive = activeChar.charNum;
    }

    public void SwitchChar(string dir)
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
                if (i == currentActive)
                {
                    //if (characters[i].GetComponent<PlayableCharacter>().dead)
                    //{
                    //    SwitchChar(dir);
                    //}
                    //else 
                    //{
                    characters[i].transform.position = activeChar.transform.position;
                    characters[i].SetActive(true);
                    //Debug.Log("character set active");
                    activeChar = characters[i].GetComponent<PlayableCharacter>();
                    Physics2D.IgnoreLayerCollision(11, 7, false); //return collison between player and traps
                    activeChar.isInvulnerable = false;
                    //activeChar.BecomeInvulnerable(); //charater get damage when switched
                    //Debug.Log("switched character supposed to be invulnerabel");
                    //}
                }
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
        //GameObject character = characters[Random.Range(0, characters.Length)];
        character.SetActive(true);
        activeChar = character.GetComponent<PlayableCharacter>();
        currentActive = randomIndex;
    }

    public GameObject GetActiveCharacter()
    {
        GameObject character = activeChar.gameObject;
        return character;
    }
}
