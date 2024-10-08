using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class TeleportChar : PlayableCharacter
{
    private int currentLane = 1;
    [SerializeField] private Transform[] lanes;

    //public void Awake()
    //{
    //    rb = GetComponent<Rigidbody2D>();
    //    anim = GetComponent<Animator>();
    //    currentHP = maxHP;
    //    canJumpAndSlide = false;
    //}

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
                    //if (i == 0) { break; } //add animation to indicate you cant teleport? or let them teleport in  a loop?
                    transform.position = new Vector3(transform.position.x, lanes[i].transform.position.y, transform.position.z);
                    isJumping = false;
                    //add animation for teleporting
                    break;
                }
                else continue;
            }
        }
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
                if (i == currentLane)
                {
                    transform.position = new Vector3(transform.position.x, lanes[i].transform.position.y, transform.position.z);
                    //add animation for teleporting
                    isSliding = false;
                    break;
                }
                else continue;
            } 
        }
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
