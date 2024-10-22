using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public abstract class PlayableCharacter : MonoBehaviour, IDamageable, IAnimated
{
    protected static int maxHP = 9;
    public static int currentHP { get; protected set; }
    public static bool dead = false;
    public Rigidbody2D rb;
    private SpriteRenderer rbSprite;
    [SerializeField] CharSwitcher characterSwitcher;
    [SerializeField] private Collider2D standCol;
    [SerializeField] private Collider2D slidCol;
    [SerializeField] private float knockbackStrength;
    [SerializeField] private float knockbackDelay;
    [SerializeField] public ScoreTracker scoreTracker;
    [SerializeField] private GameObject gameOverScreen;
    public Animator anim;
    private Color spriteColor;
    public bool isJumping = false;
    public bool isSliding = false;
    //public static int score { get; protected set; } = 0;
    public bool canJumpAndSlide { get; protected set; } = true;
    public bool abilityActive = false;
    public bool isInvulnerable = false;
    public bool isTouchingTrap = false;

    public void Awake()
    {
        //resetCharacters();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rbSprite = GetComponent<SpriteRenderer>();
        spriteColor = rbSprite.color;
        currentHP = maxHP;
    }

    void FixedUpdate()
    {
        if (!dead) scoreTracker.IncreaseScore(1);
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
        if (!dead) 
        {
        transform.position += Vector3.right * moveSpeed * Time.fixedDeltaTime;
        }
        anim.SetBool("isJumping", false);
        Jump(jumpForce);
        Slide();
    }

    //public void IncreaseScore(int byThisMuch)
    //{
    //    score += byThisMuch;
    //}

    //public string GetScore()
    //{
    //    string currentScore = score.ToString();
    //    return currentScore;
    //}

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

    public IEnumerator BecomeInvulnerable()
    {
        isInvulnerable = true;
        Physics2D.IgnoreLayerCollision(11, 7, true); //ignore collison between player and traps
        //rbSprite.material.color = Color.white; //change sprite color to white to indicate hit
        //Debug.Log("invincible on");
        yield return new WaitForSeconds(2f);
        Physics2D.IgnoreLayerCollision(11, 7, false); //return collison between player and traps
        isInvulnerable = false;
        //Debug.Log("invincible off");

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
        //Debug.Log($"Damage take. current hp: {currentHP}");
        if (currentHP <= 0)
        {
            Die();
        }
        else
        {
            //add taking hit animation
            anim.SetTrigger("playerHit");
            StartCoroutine(WaitForAnimation());
        }
    }
    public void Die()
    {
        dead = true;
        scoreTracker.UpdateHighScore();
        gameOverScreen.SetActive(true);
        //characterSwitcher.avialableChars--;

        

       // if (characterSwitcher.avialableChars <= 0)
       // {

            //make player stop
            rb.velocity = Vector3.zero; 
            anim.SetBool("isDying", true);
       // }

        //else
        //{
        //    //force a switch to another char thats alive
        //    StartCoroutine(ForceSwitch());
        //    //Debug.Log("switch triggered");
        //    //characterSwitcher.SwitchChar("right");
        //}
    }

    public IEnumerator ForceSwitch()
    {
        yield return new WaitForSeconds(knockbackDelay);
        characterSwitcher.SwitchChar("right");
    }

    public void resetCharacters()
    {
        dead = false;
        scoreTracker.ResetCurrentScore();
    }
}
