using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private PlayerAnimation playerAnimation;
    [SerializeField] private InputController input = null;
    [Header("Values:")]
    [SerializeField, Range(0f, 20f)] private float jumpHeight = 3f;
    [SerializeField, Range(0, 5)] private int maxAirJumps = 0;
    [SerializeField, Range(0f, 5f)] private float downwardMovementMultiplier = 3f;
    [SerializeField, Range(0f, 5f)] private float upwardMovementMultiplier = 1.7f;
    [SerializeField, Range(0f, 0.3f)] private float coyoteTime = 0.2f;
    [SerializeField, Range(0f, 0.3f)] private float jumpBufferTime = 0.2f;

    // private Controller 
    private Rigidbody2D body;
    private Ground ground;
    private Vector2 velocity;

    private int jumpPhase;
    private float defaultGravityScale;
    private float coyoteCounter;
    private float jumpBufferCounter;
    private float preventSpamJumpCounter;

    private bool desiredJump;
    private bool onGround;
    private bool isJumping;

    private float jumpSpeed;
    // Start is called before the first frame update
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        ground = GetComponent<Ground>();

        // The amount of gravity when standing (not moving along the y-axis).
        defaultGravityScale = 1f;
    }

    void Update()
    {
        desiredJump |= input.RetrieveJumpInput(); 
    }

    private void FixedUpdate()
    {
        onGround = ground.GetOnGround();
        velocity = body.velocity;

        if(onGround && preventSpamJumpCounter <= 0)
        {
            jumpPhase = 0;
            coyoteCounter = coyoteTime;
            isJumping = false;
        }
        else
        {
            coyoteCounter -= Time.deltaTime;
        }

        if (preventSpamJumpCounter > 0)
        {
            preventSpamJumpCounter -= Time.deltaTime;
        }

        // When input has been given that the player desires to jump, desiredJump returns true which then sets the jumpBufferCounter to its maximum value, which then ticks down with time.
        if(desiredJump) 
        {
            desiredJump = false;
            jumpBufferCounter = jumpBufferTime;
        }
        else if (!desiredJump && jumpBufferCounter > 0)
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        // In the window that the jumpBufferCounter is active, it invokes the jumpAction.
        if (jumpBufferCounter > 0)
        {
            //print("JUMP!");
            JumpAction();
        }

        // Tínkers with how gravity affects the entity when on a course moving travelling, downwards or remaining stationary along the y axis. Imagine a jump as a hill:
        if(input.RetrieveJumpHoldInput() && body.velocity.y > 0)
        {
            // This affects the upwards arch.
            body.gravityScale = upwardMovementMultiplier;
        }
        else if(!input.RetrieveJumpHoldInput() || body.velocity.y < 0) 
        {
            // this affects the downard arch.
            body.gravityScale = downwardMovementMultiplier;

        }
        else if(body.velocity.y == 0)
        {
            body.gravityScale = defaultGravityScale;
        }

        body.velocity = velocity;
    }

    private void JumpAction()
    {
        // A jump is only triggered if: coyoteCounter is positive (is always positive when standing), or if the entity is in the air with any double jumps left. 
        if (coyoteCounter > 0f || (jumpPhase < maxAirJumps && isJumping))
        {
                FindObjectOfType<AudioManager>().Play("Jump");
                if (isJumping)
                {
                    jumpPhase++;
                }

                // the existence of this if allows the stacking of jumpForces. Is that desirable? Can get extreme.
                if (velocity.y < 0)
                {
                    velocity.y = 0f;
                }

                jumpBufferCounter = 0;
                coyoteCounter = 0;
                jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * jumpHeight);
                isJumping = true;

                // had to add this in to prevent spam jumping: When is ground resets the coyoteCounter even before feet leave the ground.
                preventSpamJumpCounter = coyoteTime;
                if (playerAnimation != null)
                {
                playerAnimation.jumpTriggered = true;
                }
                // Checks so that the jumpSpeed never goes negative. That might make you start floating. Although I see no scenario where the jumpSpeed ever does go negative. One would have to on purpose make the JumpHeight negative (not within its range),
                // or make the gravity go negative (when would gravity ever go negative?).  
                if (velocity.y > 0f)
                {
                    jumpSpeed = Mathf.Max(jumpSpeed - velocity.y, 0f);
                }
                velocity.y += jumpSpeed;
        }
    }
}
