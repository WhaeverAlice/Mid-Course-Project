using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class TeleportChar : PlayableCharacter
{
    private int currentLane = 0;
    [SerializeField] private Transform[] lanes;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    public override void SpecialAbility()
    {
        //jump replaces special ability - maybe add a random teleport or a teleport projectiles
    }

    public override void Jump(float jumpForce) //jump ability is replaced with teleport up
    {
        if(isJumping) 
        {
            if (currentLane == 0) currentLane = 2;
            else currentLane--;

            for (int i = 0; i < lanes.Length; i++)
            {
                if (i == currentLane)
                {
                    //if (i == 0) { break; } //add animation to indicate you cant teleport? or let them teleport in  a loop?
                    transform.position = new Vector3(transform.position.x, lanes[i].transform.position.y, transform.position.z);
                    //add animation for teleporting
                    break;
                }
                else continue;
            }
        }
       isJumping = false;
    }

    public override void Slide(bool slide) //slide ability id replaced with teleport down
    {
       if(Input.GetKeyDown("down")) //need to figure out why input lags - or feels like lagging
        {
           
                //teleports player to the lane above them if its exists
                if (currentLane == lanes.Length - 1) currentLane = 0;
                else currentLane++;

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

    public override void OnCollisonEnter2D(Collider2D collider)
    {
        //collide with all types of traps 

        //apply damage to char
        this.ApplyDamage();

        return;
    }

    public override void OnTriggerEnter2D(Collider2D col)
    {
        return;
    }
}
