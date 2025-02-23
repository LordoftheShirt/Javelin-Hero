using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [NonSerialized] public bool jumpTriggered = false;
    [NonSerialized] public bool onGround = true;
    [NonSerialized] public Vector2 velocityAmount;

    [SerializeField] private float runAnimationTilt = 1f;

    [SerializeField] private float jumpAnimationSquishSpeed = 1f;
    [SerializeField] private float jumpAnimationStretchSpeed = 1f;
    [SerializeField] private float jumpAnimationResetSpeed = 1f;


    [SerializeField] private Vector3 jumpSquishAmount = new Vector3(2, 0.5f);
    [SerializeField] private Vector3 jumpStretchAmount = new Vector3(0.5f, 2);

    [SerializeField] private Vector2 fallingStretchModifier = new Vector2(1, 1);
    [SerializeField] private float fallingStretchMaximum = 5f;

    private Vector3 startProportions;
    private bool squishFinish;
    private bool stretchFinish;

    // goes unused as of yet
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        squishFinish = false;
        stretchFinish = false;
        startProportions = transform.localScale;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        JumpAnimation();

        if (velocityAmount.x == 0 && onGround)
        {
            // Idle animation

        }
        else
        {
            // Tilt if running, snap to neutral if jumping. 
            if (!jumpTriggered && onGround)
            {
                transform.rotation = Quaternion.Euler(0, 0, -velocityAmount.x * runAnimationTilt);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    void JumpAnimation()
    {
        if (jumpTriggered)
        {
            // First squish, then stretch, then reset.
            if (!squishFinish)
            {
                if (gameObject.transform.localScale != jumpSquishAmount)
                {
                    gameObject.transform.localScale = Vector3.MoveTowards(gameObject.transform.localScale, jumpSquishAmount, jumpAnimationSquishSpeed * Time.deltaTime);
                }
                else
                {
                    squishFinish = true;
                }
            }
            else if (squishFinish && !stretchFinish)
            {
                if (gameObject.transform.localScale != jumpStretchAmount)
                {
                    gameObject.transform.localScale = Vector3.MoveTowards(gameObject.transform.localScale, jumpStretchAmount, jumpAnimationStretchSpeed * Time.deltaTime);
                }
                else
                {
                    stretchFinish = true;
                }
            }
            else if (stretchFinish)
            {
                if (gameObject.transform.localScale != startProportions)
                {
                    gameObject.transform.localScale = Vector3.MoveTowards(gameObject.transform.localScale, startProportions, jumpAnimationResetSpeed * Time.deltaTime);
                }
                else
                {
                    squishFinish = false;
                    stretchFinish = false;
                    jumpTriggered = false;
                }

            }
        }
        else if (velocityAmount.y < 0)
        {
            if (startProportions.y * -velocityAmount.y * fallingStretchModifier.y > startProportions.y)
                if (startProportions.y * -velocityAmount.y * fallingStretchModifier.y < fallingStretchMaximum)
                {
                    gameObject.transform.localScale = new Vector3(startProportions.x * fallingStretchModifier.x, startProportions.y * -velocityAmount.y * fallingStretchModifier.y, startProportions.z);
                }
                else
                {
                    gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, fallingStretchMaximum, startProportions.z); ;
                }
        }
        else if (transform.localScale != startProportions) 
        {
            gameObject.transform.localScale = startProportions;
        }
    }
}

