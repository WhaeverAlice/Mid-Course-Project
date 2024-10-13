using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float jumpForce;
    [SerializeField] private float moveSpeed;
    [SerializeField] private CharSwitcher charSwitcher;
    private PlayableCharacter activeChar;
   
    // Start is called before the first frame update
    void Start()
    {
      charSwitcher.SetRandomCharacter();
      //activeChar = charSwitcher.activeChar;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
    }

    private void FixedUpdate()
    {
        charSwitcher.activeChar.Move(jumpForce, moveSpeed);
    }

    private void ProcessInputs()
    {
        if(Input.GetKeyDown("up"))
        {
            charSwitcher.activeChar.isJumping = true;
        }
        if (charSwitcher.activeChar.canJumpAndSlide && Input.GetKey("down"))
        {
            charSwitcher.activeChar.isSliding = true;
        }
        if (!charSwitcher.activeChar.canJumpAndSlide && Input.GetKeyDown("down"))
        {
            charSwitcher.activeChar.isSliding = true;
        }
        if (Input.GetKeyDown("right"))
        {
            charSwitcher.SwitchChar("right");
        }
        if(Input.GetKeyDown("left"))
        {
            charSwitcher.SwitchChar("left");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            charSwitcher.activeChar.SpecialAbility();
        }
    }
}
