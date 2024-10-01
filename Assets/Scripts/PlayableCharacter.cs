using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayableCharacter : MonoBehaviour, IDamageable
{
    //private int speed;
    private int maxHP;
    private int currentHP;
    public bool dead = false;
    public Rigidbody2D rb;
    [SerializeField] private Collider2D standCol;
    [SerializeField] private Collider2D slidCol;
    public Animator anim;
    public bool isJumping = false;
    public bool isSliding = false;

    public abstract void SpecialAbility();

    public void Move(float jumpForce, float moveSpeed)
    {
        transform.position += Vector3.right * moveSpeed * Time.fixedDeltaTime;
        anim.SetBool("isJumping", false);
        Jump(jumpForce);
        Slide(isSliding);
    }

    public abstract void OnCollisonEnter2D(Collider2D collider);
    public abstract void OnTriggerEnter2D(Collider2D col);
    

    public virtual void Slide(bool slide)
    {
        if (Input.GetKey("down"))
        {
            anim.SetBool("isSliding", true);
            standCol.enabled = false;
            slidCol.enabled = true;
        }

        else
        {
            standCol.enabled = true;
            slidCol.enabled = false;
            anim.SetBool("isSliding", false);
        }
    }

    public virtual void Jump(float jumpForce)
    {
        if (isJumping)
        {
            rb.velocity = Vector2.up * jumpForce;
            anim.SetBool("isJumping", true);
        }
        //add something to check when player is touching the ground
        isJumping = false;
    }

    public void ApplyDamage()
    {
        currentHP--;
        if (currentHP <= 0) Die();
        //add taking hit animation

        //add somethin g to make character knockback from hit

        //add something to make haracter invinsible for a couple of seconds
    }

    private void Die()
    {
        //add if for when char dies to force a switch to other char

        //add if for when its the last char to do dying animation instead of inactive and to trigger game over

        gameObject.SetActive(false);
        dead = true;
        
    }
}
