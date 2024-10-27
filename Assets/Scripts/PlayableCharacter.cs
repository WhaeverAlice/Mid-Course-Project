using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public abstract class PlayableCharacter : MonoBehaviour, IDamageable, IAnimated
{
    [SerializeField] CharSwitcher characterSwitcher;
    [SerializeField] private Collider2D standCol;
    [SerializeField] private Collider2D slidCol;
    [SerializeField] private float knockbackStrength;
    [SerializeField] private float knockbackDelay;
    [SerializeField] public ScoreTracker scoreTracker;
    [SerializeField] private GameObject gameOverScreen;
    public Rigidbody2D rb;
    private SpriteRenderer rbSprite;
    public Animator anim;
    private Color spriteColor;
    protected static int maxHP = 9;
    public static int currentHP { get; protected set; }
    public static bool dead = false;
    public bool isJumping = false;
    public bool isSliding = false;
    public bool canJumpAndSlide { get; protected set; } = true;
    public bool abilityActive = false;
    public bool isInvulnerable = false;
    public bool isTouchingTrap = false;

    public void Awake()
    {
        //get character components
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rbSprite = GetComponent<SpriteRenderer>();
        spriteColor = rbSprite.color;
        currentHP = maxHP;
    }

    private void OnEnable()
    {
        //reset character actions
        abilityActive = false;
        isJumping = false;
        isSliding = false;
    }
    void FixedUpdate()
    {
        //increase score by 1 each fixed frame
        if (!dead) scoreTracker.IncreaseScore(1);
        
        //change sprite alpha value if charcter is invulnerable
        if (isInvulnerable)
        {
            spriteColor.a = 0.7f;
        }
        else
        {
            spriteColor.a = 1f;
        }
        rbSprite.color = spriteColor; 
    }
    public abstract void SpecialAbility();
    public void Move(float jumpForce, float moveSpeed)
    {
        //move character right at a constant speed when player is alive 
        if (!dead) 
        {
        transform.position += Vector3.right * moveSpeed * Time.fixedDeltaTime;
        }

        //reset animation after jumping
        anim.SetBool("isJumping", false);
        Jump(jumpForce);
        Slide();
    }
    public virtual void Jump(float jumpForce)
    {
        if (isJumping)
        {
            rb.velocity = Vector2.up * jumpForce;
            anim.SetBool("isJumping", true);
        }
        isJumping = false;
    }
    public virtual void Slide()
    {
        if (isSliding)
        {
            //play sliding animation
            anim.SetBool("isSliding", true);

            //disable standing collider and enable sliding collider
            standCol.enabled = false;
            slidCol.enabled = true;
        }
        else
        {
            //exit sliding animation
            anim.SetBool("isSliding", false);
            WaitForAnimation();

            //enable standing collider and disable sliding collider
            standCol.enabled = true;
            slidCol.enabled = false;
        }
        isSliding = false;
    }
    public void ApplyDamage()
    {
        if (isInvulnerable)
        {
            return;
        }

        //make haracter invulnerable for a couple of seconds
        StartCoroutine(BecomeInvulnerable());

        //apply knockback from hit
        ApplyKnockback();

        //reduce hp
        currentHP -= 1;

        //trigger death if hp is at 0
        if (currentHP <= 0)
        {
            Die();
        }
        else
        {
            //play taking hit animation
            anim.SetTrigger("playerHit");
            StartCoroutine(WaitForAnimation());
        }
    }
    public IEnumerator BecomeInvulnerable()
    {
        isInvulnerable = true;
        Physics2D.IgnoreLayerCollision(11, 7, true); //ignore collison between player and traps

        //remain active for 2 seconds
        yield return new WaitForSeconds(2f);
        Physics2D.IgnoreLayerCollision(11, 7, false); //return collison between player and traps
        isInvulnerable = false;
    }
    public void ApplyKnockback()
    {
        //add opposite force to create knockback
        Vector2 direction = (-transform.position).normalized;
        rb.AddForce(direction * knockbackStrength, ForceMode2D.Impulse);
        StartCoroutine(RecoverFromKnockback());
    }
    private IEnumerator RecoverFromKnockback() //stops force of knockback after a time
    {
        yield return new WaitForSeconds(knockbackDelay);
        rb.velocity = Vector3.zero;
    }
    public void Die()
    {
        //set character as dead
        dead = true;

        //stop player movement
        rb.velocity = Vector3.zero;

        //play death animation
        anim.SetBool("isDying", true);

        //update final score
        scoreTracker.UpdateHighScore();

        //open game over screen
        gameOverScreen.SetActive(true);
    }
    public IEnumerator WaitForAnimation() //waits for animation transition to end
    {
        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f);
    }
   
    public void resetCharacters() //reset character and score before a new run
    {
        dead = false;
        scoreTracker.ResetCurrentScore();
    }
}
