using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayableCharacter : MonoBehaviour, IDamageable, IAnimated
{
    //private int speed;
    protected int maxHP = 3;
    public int currentHP { get; protected set; }
    public bool dead { get; protected set; } = false;
    public Rigidbody2D rb;
    [SerializeField] private Collider2D standCol;
    [SerializeField] private Collider2D slidCol;
    [SerializeField] private float knockbackStrength;
    [SerializeField] private float knockbackDelay;
    public Animator anim;
    public bool isJumping = false;
    public bool isSliding = false;
    public static int score { get; protected set; } = 0;
    public bool canJumpAndSlide { get; protected set; } = true;
    public bool abilityActive = false;
    public bool isInvulnerable = false;

    public abstract void SpecialAbility();

    public void Move(float jumpForce, float moveSpeed)
    {
        if (!dead) 
        {
        transform.position += Vector3.right * moveSpeed * Time.fixedDeltaTime;
        }
        anim.SetBool("isJumping", false);
        Jump(jumpForce);
        Slide();
    }

    //public abstract void OnCollisonEnter2D(Collider2D collider);
    //public abstract void OnTriggerEnter2D(Collider2D col);

    public void IncreaseScore(int byThisMuch)
    {
        score += byThisMuch;
    }

    public virtual void Slide()
    {
        if (isSliding)
        {
            anim.SetBool("isSliding", true);
            standCol.enabled = false;
            slidCol.enabled = true;

        }
        else
        {
            anim.SetBool("isSliding", false);
            WaitForAnimation();
            standCol.enabled = true;
            slidCol.enabled = false;
        }
        isSliding = false;
    }

    public void ApplyKnockback()
    {
        //StopAllCoroutines();
        //new Vector3(transform.position.x + 1, transform.position.y, transform.position.z)
        Vector2 direction = (-transform.position).normalized;
        rb.AddForce(direction * knockbackStrength, ForceMode2D.Impulse);
        RecoverFromKnockback();
        StartCoroutine(RecoverFromKnockback());
    }

    public IEnumerator WaitForAnimation() //waits for animation transition to end
    {
        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f);
    }

    private IEnumerator RecoverFromKnockback()
    {
        yield return new WaitForSeconds(knockbackDelay);
        rb.velocity = Vector3.zero;
    }

    private IEnumerator BecomeInvulnerable()
    {
        isInvulnerable = true;
        Physics2D.IgnoreLayerCollision(11, 7, true); //ignore collison between player and traps
        //rbSprite.material.color = Color.white; //change sprite color to white to indicate hit
        Debug.Log("invincible on");
        yield return new WaitForSeconds(1f);
        Physics2D.IgnoreLayerCollision(11, 7, false); //return collison between player and traps
        isInvulnerable = false;
        Debug.Log("invincible off");

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
        if(isInvulnerable) 
        {
        return;
        }
        //add something to make haracter invinsible for a couple of seconds
        StartCoroutine(BecomeInvulnerable());

        //add something to make character knockback from hit
        ApplyKnockback();
        
        currentHP -= 1;
        Debug.Log("damage taken");
        if (currentHP <= 0)
        {
            Die();
        }

        //add taking hit animation
        anim.SetTrigger("playerHit");
        StartCoroutine(WaitForAnimation());

        //add somehting that tells the UI to show one less heart

       

       
    }
    public void Die()
    {
        //add if for when char dies to force a switch to other char

        //add if for when its the last char to do dying animation instead of inactive and to trigger game over

        //make player stop
        rb.velocity = Vector3.zero;

        gameObject.SetActive(false);
        dead = true;
    }
}
