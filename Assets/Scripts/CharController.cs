using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float jumpForce = 400f;                          // Amount of force added when the player jumps.
    //[Range(0, 1)][SerializeField] private float m_CrouchSpeed = .36f;           // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [Range(0, .3f)][SerializeField] private float movementSmoothing = .05f;   // How much to smooth out the movement
    //[SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask whatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform groundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform ceilingCheck;                          // A position marking where to check for ceilings
    [SerializeField] private Collider2D slideDisableCollider;                // A collider that will be disabled when sliding

    const float groundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool grounded;            // Whether or not the player is grounded.
    const float ceilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    private Rigidbody2D rb;
    //private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 velocity = Vector3.zero;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnSlideEvent;
    private bool wasSliding = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnSlideEvent == null)
            OnSlideEvent = new BoolEvent();
    }

    private void FixedUpdate()
    {
        bool wasGrounded = grounded;
        grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }


    public void Move(float move, bool slide, bool jump)
    {
        // If sliding, check to see if the character can stand up
        if (!slide)
        {
            // If the character has a ceiling preventing them from standing up, keep them sliding
            if (Physics2D.OverlapCircle(ceilingCheck.position, ceilingRadius, whatIsGround))
            {
                slide = true;
            }
        }

        //only control the player if grounded
        if (grounded)
        {

            // If sliding
            if (slide)
            {
                if (!wasSliding)
                {
                    wasSliding = true;
                    OnSlideEvent.Invoke(true);
                }

                //// Reduce the speed by the crouchSpeed multiplier
                //move *= m_CrouchSpeed;

                // Disable one of the colliders when crouching
                if (slideDisableCollider != null)
                    slideDisableCollider.enabled = false;
            }
            else
            {
                // Enable the collider when not crouching
                if (slideDisableCollider != null)
                    slideDisableCollider.enabled = true;

                if (wasSliding)
                {
                    wasSliding = false;
                    OnSlideEvent.Invoke(false);
                }
            }

            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, rb.velocity.y);
            // And then smoothing it out and applying it to the character
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmoothing);

            //// If the input is moving the player right and the player is facing left...
            //if (move > 0 && !m_FacingRight)
            //{
            //    // ... flip the player.
            //    Flip();
            //}
            //// Otherwise if the input is moving the player left and the player is facing right...
            //else if (move < 0 && m_FacingRight)
            //{
            //    // ... flip the player.
            //    Flip();
            //}
        }
        // If the player should jump...
        if (grounded && jump)
        {
            // Add a vertical force to the player.
            grounded = false;
            rb.AddForce(new Vector2(0f, jumpForce));
        }
    }


    //private void Flip()
    //{
    //    // Switch the way the player is labelled as facing.
    //    m_FacingRight = !m_FacingRight;

    //    // Multiply the player's x local scale by -1.
    //    Vector3 theScale = transform.localScale;
    //    theScale.x *= -1;
    //    transform.localScale = theScale;
    //}
}