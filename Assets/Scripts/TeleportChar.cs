using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class TeleportChar : PlayableCharacter
{
    private int currentLane = 1;
    [SerializeField] private Transform above;
    [SerializeField] private Transform bellow;
    [SerializeField] private LayerMask blockLayer;
    [SerializeField] private Transform[] lanes;
    [SerializeField] private AudioSource ability;

    void Start()
    {
        canJumpAndSlide = false;
    }

    public override void SpecialAbility()
    {
        //jump replaces special ability - maybe add a random teleport or a teleport projectiles
        return;
    }

    public override void Jump(float jumpForce) //jump ability is replaced with teleport up
    {
        if(isJumping) 
        {
            //teleports player to the lane above them if its exists
           

            int temp = currentLane;
            if (currentLane == 0) currentLane = 2;
            else currentLane--;
            //if (currentLane == 2) return;
            for (int i = 2; i > -1 ; i--)
            {
                if (i == currentLane)
                {
                    if (canTeleportUp())
                    {
                        transform.position = new Vector3(transform.position.x, lanes[i].transform.position.y, transform.position.z);
                        //play ability animation
                        anim.SetTrigger("abilityActive");
                        ability.Play();
                        //isJumping = false;
                    }
                    else
                    {
                        currentLane = temp;
                    }
                }
                else continue;
            }
            Debug.Log(currentLane);
        }
        isJumping = false;
    }

    private bool canTeleportUp()
    {
        if (IsInCameraView(above) && Physics2D.OverlapCircle(above.position, 0.3f, blockLayer))
        {
            return false;
        }
        else if (!IsInCameraView(above) && Physics2D.OverlapCircle(lanes[2].position, 0.3f, blockLayer))
        {
            return false;
        }
        else return true;
    }
    private bool canTeleportDown()
    {
         if (IsInCameraView(bellow) && Physics2D.OverlapCircle(bellow.position, 0.3f, blockLayer))
         {
             return false;
         }

         else if (!IsInCameraView(bellow) && Physics2D.OverlapCircle(lanes[0].position, 0.3f, blockLayer))
        {
            return false ;
        }
         else return true;
    }

    public override void Slide() //slide ability is replaced with teleport down
    {
        if (isSliding)
        {
            int temp = currentLane;
            //teleports player to the lane below them if its exists
            if (currentLane == lanes.Length - 1) currentLane = 0;
            else currentLane++;
            //if (currentLane == 0) return;

            for (int i = 0; i < lanes.Length; i++)
            {
                if (i == currentLane)
                {
                    if (canTeleportDown())
                    {
                        transform.position = new Vector3(transform.position.x, lanes[i].transform.position.y, transform.position.z);
                        //play ability animation
                        anim.SetTrigger("abilityActive");
                        ability.Play();
                    }
                    else
                    {
                        currentLane = temp;
                    }
                }
                else continue;
            }
            Debug.Log(currentLane);
        }
        isSliding = false;
       
    }

    private bool IsInCameraView(Transform target)
    {
        Vector3 viewportPoint = Camera.main.WorldToViewportPoint(target.position);
        return viewportPoint.x >= 0 && viewportPoint.x <= 1 &&
               viewportPoint.y >= 0 && viewportPoint.y <= 1 &&
               viewportPoint.z >= 0;
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
