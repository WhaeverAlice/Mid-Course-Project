using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float moveSpeed;
    [SerializeField] private CharSwitcher charSwitcher;
    [SerializeField] private GameObject pauseMenu;
    private PlayableCharacter activeChar;
    private Rigidbody2D rb;

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
    }

    private void FixedUpdate()
    {
        //keep track of current active character
        charSwitcher.activeChar.Move(jumpForce, moveSpeed);
    }

    private void ProcessInputs() //proccess player inputs
    {
        if(Input.GetKeyDown("up"))
        {
            //activates jump / teleport
            charSwitcher.activeChar.isJumping = true;
        }
        if (charSwitcher.activeChar.canJumpAndSlide && Input.GetKey("down"))
        {
            //activates teleport
            charSwitcher.activeChar.isSliding = true;
        }
        if (!charSwitcher.activeChar.canJumpAndSlide && Input.GetKeyDown("down"))
        {
            //activates slide
            charSwitcher.activeChar.isSliding = true;
        }
        if (Input.GetKeyDown("right"))
        {
            //switches to character to the right
            charSwitcher.SwitchChar("right");
        }
        if(Input.GetKeyDown("left"))
        {
            //switches to character to the left
            charSwitcher.SwitchChar("left");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //activates special ability
            charSwitcher.activeChar.SpecialAbility();
        }
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            //opens pause window
            pauseMenu.SetActive(true);

            //puases game
            Time.timeScale = 0;
        }
    }
}
