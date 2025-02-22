using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [NonSerialized] public bool jumpTriggered = false;
    [NonSerialized] public bool onGround = true;
    [NonSerialized] public float velocityAmount = 0;

    [SerializeField] private float runAnimationTilt = 1f;

    [SerializeField] private float jumpAnimationSquishSpeed = 1f;
    [SerializeField] private float jumpAnimationStretchSpeed = 1f;
    [SerializeField] private float jumpAnimationResetSpeed = 1f;
    [SerializeField] private Vector3 squishAmount = new Vector3(2, 0.5f);
    [SerializeField] private Vector3 stretchAmount = new Vector3(0.5f, 2);


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

        if (velocityAmount == 0 && onGround)
        {
            // Idle animation

        }
        else
        {
            // Tilt if running, snap to neutral if jumping. 
            if (!jumpTriggered && onGround)
            {
                transform.rotation = Quaternion.Euler(0, 0, -velocityAmount * runAnimationTilt);
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
                if (gameObject.transform.localScale != squishAmount)
                {
                    gameObject.transform.localScale = Vector3.MoveTowards(gameObject.transform.localScale, squishAmount, jumpAnimationSquishSpeed * Time.deltaTime);
                }
                else
                {
                    squishFinish = true;
                }
            }
            else if (squishFinish && !stretchFinish)
            {
                if (gameObject.transform.localScale != stretchAmount)
                {
                    gameObject.transform.localScale = Vector3.MoveTowards(gameObject.transform.localScale, stretchAmount, jumpAnimationStretchSpeed * Time.deltaTime);
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
    }
}

