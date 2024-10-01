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
        activeChar = charSwitcher.SetRandomCharacter();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
    }

    private void FixedUpdate()
    {
        activeChar.Move(jumpForce, moveSpeed);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            activeChar.SpecialAbility();
        }
    }

    private void ProcessInputs()
    {
        if(Input.GetKeyDown("up"))
        {
            activeChar.isJumping = true;
        }
        if(Input.GetKeyDown("right"))
        {
            activeChar = charSwitcher.SwitchChar("right");
        }
        if(Input.GetKeyDown("left"))
        {
            activeChar = charSwitcher.SwitchChar("left");
        }
    }
}
