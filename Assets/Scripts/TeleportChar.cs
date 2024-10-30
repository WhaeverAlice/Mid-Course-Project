using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;
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
        //'jump' and 'slide' are replaced by special ability instead
        return;
    }

    public override void Jump(float jumpForce) //jump ability is replaced with teleport up
    {
        //teleports player to the lane above them if possible
        if (isJumping) 
        {
            int temp = currentLane;

            //set target lane for teleportaion
            if (currentLane == 0) currentLane = 2;
            else currentLane--;

            for (int i = 2; i > -1 ; i--)
            {
                if (i == currentLane)
                {
                    if (canTeleportUp())
                    {
                        //move character 1 lane up (or to the bottom lane if player is in top lane)
                        transform.position = new Vector3(transform.position.x, lanes[i].transform.position.y, transform.position.z);

                        //play ability animation
                        anim.SetTrigger("abilityActive");

                        //play ability sound effect
                        ability.Play();
                    }
                    else
                    {
                        //reset current lane value if wasnt able to teleport
                        currentLane = temp;
                    }
                }
                else continue;
            }
        }
        isJumping = false;
    }

    private bool canTeleportUp() //checks if player can move to the lane above
    {
        //if area above player is within view, checks if it is blocked
        if (IsInCameraView(above) && Physics2D.OverlapCircle(above.position, 0.3f, blockLayer))
        {
            return false;
        }
        //if area above player is out of view, checks if bottom lane is blocked
        else if (!IsInCameraView(above) && Physics2D.OverlapCircle(lanes[2].position, 0.3f, blockLayer))
        {
            return false;
        }
        else return true;
    }
    
    public override void Slide() //slide ability is replaced with teleport down
    {
        //teleports player to the lane bellow them if possible
        if (isSliding)
        {
            int temp = currentLane;

            //set target lane for teleportaion
            if (currentLane == lanes.Length - 1) currentLane = 0;
            else currentLane++;

            for (int i = 0; i < lanes.Length; i++)
            {
                if (i == currentLane)
                {
                    if (canTeleportDown())
                    {
                        //move character 1 lane down (or to the top lane if player is in bottom lane)
                        transform.position = new Vector3(transform.position.x, lanes[i].transform.position.y, transform.position.z);
                        
                        //play ability animation
                        anim.SetTrigger("abilityActive");

                        //play ability sound effect
                        ability.Play();
                    }
                    else
                    {
                        //reset current lane value if wasnt able to teleport
                        currentLane = temp;
                    }
                }
                else continue;
            }
        }
        isSliding = false;
    }

    private bool canTeleportDown() //checks if player can move to the lane bellow
    {
        //if area bellow player is within view, checks if it is blocked
        if (IsInCameraView(bellow) && Physics2D.OverlapCircle(bellow.position, 0.3f, blockLayer))
        {
            return false;
        }
        //if area bellow player is out of view, checks if top lane is blocked
        else if (!IsInCameraView(bellow) && Physics2D.OverlapCircle(lanes[0].position, 0.3f, blockLayer))
        {
            return false;
        }
        else return true;
    }

    private bool IsInCameraView(Transform target) //checks if target is within view of the camera
    {
        Vector3 viewportPoint = Camera.main.WorldToViewportPoint(target.position);
        return viewportPoint.x >= 0 && viewportPoint.x <= 1 &&
               viewportPoint.y >= 0 && viewportPoint.y <= 1 &&
               viewportPoint.z >= 0;
    }
}
