using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayableCharacter : MonoBehaviour
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

    public virtual void Slide(bool slide)
    {
        if (isSliding)
        {
            anim.SetBool("isSliding", true);
            standCol.enabled = false;
            slidCol.enabled = true;
            isSliding = false;
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
    }

    private void Die()
    {
        gameObject.SetActive(false);
        dead = true;
        //add dying animation
    }
}
