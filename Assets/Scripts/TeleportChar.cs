using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class TeleportChar : PlayableCharacter
{
    private int currentLane = 1;
    [SerializeField] private Transform above;
    [SerializeField] private Transform bellow;
    [SerializeField] private LayerMask blockLayer;
    [SerializeField] private Transform[] lanes;

    //public void Awake()
    //{
    //    rb = GetComponent<Rigidbody2D>();
    //    anim = GetComponent<Animator>();
    //    currentHP = maxHP;
    //   
    //}

    void Start()
    {
        canJumpAndSlide = false;
    }

    //void FixedUpdate()
    //{
    //    if (!dead) score += 1;
    //}
    public override void SpecialAbility()
    {
        //jump replaces special ability - maybe add a random teleport or a teleport projectiles
    }

    public override void Jump(float jumpForce) //jump ability is replaced with teleport up
    {
        if(isJumping) 
        {
            //teleports player to the lane above them if its exists
           


            if (currentLane == 0) currentLane = 2;
            else currentLane--;
            if (currentLane == 2) return;
            for (int i = 0; i < lanes.Length; i++)
            {
                if (i == currentLane)
                {
                    if (canTeleportUp())
                    {
                        transform.position = new Vector3(transform.position.x, lanes[i].transform.position.y, transform.position.z);
                        //play ability animation
                        anim.SetTrigger("abilityActive");
                        isJumping = false;
                    }
                    else currentLane++;
                    
                }
                else continue;
            }
        }
    }

    private bool canTeleportUp()
    {
        if (Physics2D.OverlapCircle(above.position, 0.5f, blockLayer))
        {
            return false;
        }
        else return true;
    }
    private bool canTeleportDown()
    {
        if (Physics2D.OverlapCircle(bellow.position, 0.5f, blockLayer))
        {
            return false;
        }
        else return true;
    }

    public override void Slide() //slide ability is replaced with teleport down
    {
        if (isSliding)
        {
            //teleports player to the lane below them if its exists
            if (currentLane == lanes.Length - 1) currentLane = 0;
            else currentLane++;
            if (currentLane == 0) return;

            for (int i = 0; i < lanes.Length; i++)
            {
                if (i == currentLane && canTeleportDown())
                {
                    if (canTeleportDown())
                    {
                        transform.position = new Vector3(transform.position.x, lanes[i].transform.position.y, transform.position.z);
                        //play ability animation
                        anim.SetTrigger("abilityActive");
                    }
                    else currentLane++;
                }
                else continue;
            } 
        }
        isSliding = false;
    }

    //public override void OnCollisonEnter2D(Collider2D collider)
    //{
    //    //collide with all types of traps 

    //    //apply damage to char
    //    this.ApplyDamage();
    //}

    //public override void OnTriggerEnter2D(Collider2D col)
    //{
    //    //collide with all types of traps 

    //    //apply damage to char
    //    this.ApplyDamage();
    //}
}
