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
    [SerializeField] private Collider2D standCol;
    [SerializeField] private Collider2D slidCol;
    private PlayableCharacter activeChar;
    private Animator anim;

    //private bool isJumping;
    //private bool isSliding;

    
    // Start is called before the first frame update
    void Start()
    {
        activeChar = GetComponent<PlayableCharacter>();
        //rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
       
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

    //private void Slide()
    //{
    //    if (isSliding)
    //    {
    //        anim.SetBool("isSliding", true);
    //        standCol.enabled = false;
    //        slidCol.enabled = true;
    //        isSliding = false;
    //    }
    //    else
    //    {
    //        standCol.enabled = true;
    //        slidCol.enabled = false;
    //        anim.SetBool("isSliding", false);
    //    }
    //}

    //private void Jump()
    //{
    //    if (isJumping)
    //    {
    //        rb.velocity = Vector2.up * jumpForce;
    //        anim.SetBool("isJumping", true);
    //    }
    //    //add something to check when player is touching the ground
    //    isJumping = false;
    //    anim.SetBool("isJumping", false);
    //}

    private void ProcessInputs()
    {
        if(Input.GetKeyDown("up"))
        {
            activeChar.isJumping = true;
        }
        if(Input.GetKey("down"))
        {
            activeChar.isSliding = true;
        }
    }
}
