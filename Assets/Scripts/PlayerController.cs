using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer myPlayerRender;

    //--Input System--//
    private MyInputSystem inputSet;
    private InputAction move;
    private InputAction jump;

    //--Movement--//
    private Vector2 movement;
    [SerializeField] private float topSpeed = 20f;
    [SerializeField] private float accelRate = 4f;
    [SerializeField] private float turnMultiplier = 1.5f;
    private Vector2 playerDirection;

    //--Jump--//
    // Jump Physics
    [SerializeField] private float jumpPower = 5f;
    [SerializeField] private float jumpCancelMultiplier = 0.5f;
    [SerializeField] private float fallMultiplier = 2.5f; 
    [SerializeField] private float lowJumpMultiplier = 2f; 
    [SerializeField] private float maxFallSpeed = -10f;
    // Ground Detection
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.3f;
    // Coyote Time, Jump Buffer and Jump Canceling
    private float coyoteCounter;
    [SerializeField] private float coyoteTime = 0.2f;
    private float jumpBufferCounter;
    [SerializeField] private float jumpBuffer = 0.2f;
    private bool isJumpPressed = false;
    private bool isJumpReleased = false;
    private bool isJumping = false;
    private bool isJumpHeld = false;

    private void OnEnable()
    {
        move = inputSet.Player.Move;
        move.Enable();
        jump = inputSet.Player.Jump;
        jump.Enable();
        jump.performed += onJumpPerformed;
        jump.canceled += onJumpCanceled;
    }

    private void OnDisable()
    {
        jump.performed -= onJumpPerformed;
        jump.canceled -= onJumpCanceled; 
        move.Disable();
        jump.Disable();
    }
    private bool isGrounded()
    {
        //I chose the overlap circle method, as it is a quick single line of code and is not prone to errors.

        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }
    void onJumpPerformed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isJumpPressed = true;
            isJumpHeld = true;
        }
    }
    void onJumpCanceled(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            isJumpReleased = true;
            isJumpHeld = false;
        }
    }
    void RunningCheck()
    {
        //I decided to use a velocity manipulation method as I found it easier to add acceleration and deceleration this way.

        //Sprite Flip 
        if (movement.x != 0f)
        {
            myPlayerRender.flipX = movement.x < 0;
            playerDirection = new Vector2(movement.x, 0);
        } 

        //Accelerating movement
        
        float targetSpeed = movement.x * topSpeed;

        float newAccelRate;
        //Faster Changing Directions
        if (Mathf.Sign(targetSpeed) != Mathf.Sign(rb.linearVelocityX) && movement.x != 0)
        {
            newAccelRate = accelRate * turnMultiplier;
        }
        else
        {
            newAccelRate = accelRate;
        }

            float newXspeed = Mathf.MoveTowards(rb.linearVelocityX, targetSpeed, newAccelRate * Time.fixedDeltaTime);

        rb.linearVelocity = new Vector2(newXspeed, rb.linearVelocityY); 

    }
    void JumpCheck()
    {
        //I decided to use both Coyote Time and an Input Check, as I felt comfortable enough to implement both.

        //Coyote Check
        if (isGrounded())
        { coyoteCounter = coyoteTime;
            isJumping = false;
        }
        else
        { coyoteCounter -= Time.deltaTime; }

        //Jump Input
        if (isJumpPressed)
        {
            jumpBufferCounter = jumpBuffer;
            isJumpPressed = false;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
        //Start the Jump
        if (jumpBufferCounter > 0f && coyoteCounter > 0f && !isJumping)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower); 
            isJumping = true; 
            coyoteCounter = 0f; 
            jumpBufferCounter = 0f; 
        }
        //Cancel the Jump Height
        if (isJumpReleased && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * jumpCancelMultiplier);
            isJumpReleased = false;
        }

        isJumpReleased = false;
    }
    void ApplyGravity()
    {
        //Gravity adjusting
        if (rb.linearVelocity.y < 0f)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (rb.linearVelocity.y > 0f && isJumpHeld)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
        //Clamping terminal velocity
        if (rb.linearVelocity.y < maxFallSpeed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, maxFallSpeed);
        }
    }
    private void Awake()
    {
        inputSet = new MyInputSystem();
        rb = GetComponent<Rigidbody2D>();
        myPlayerRender = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        Debug.DrawRay(transform.position, Vector2.down * 10, Color.red);

        movement = move.ReadValue<Vector2>();
        JumpCheck();
    }
    private void FixedUpdate()
    {
        ApplyGravity();
        RunningCheck();
    }
}
