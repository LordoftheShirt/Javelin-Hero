using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private PlayerAnimation playerAnimation;

    [SerializeField] private InputController input = null;
    [Header ("Player Movement")]
    [SerializeField, Range(0f, 100f)] private float maxSpeed = 4f;
    [SerializeField, Range(0f, 100f)] private float maxAcceleration = 35f;
    [SerializeField, Range(0f, 100f)] private float maxAirAcceleration = 20f;

    private Vector2 direction;
    private Vector2 desiredVelocity;
    private Vector2 velocity;
    private Rigidbody2D body;
    private Ground ground;

    private float maxSpeedChange;
    private float acceleration;
    private float stunCounter = -69, knockbackCooldown = 0;
    private bool onGround;
    private bool canMove = true;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        ground = GetComponent<Ground>();
    }

    // Update is called once per frame
    void Update()
    {
        direction.x = input.RetrieveMoveInput();
        desiredVelocity = new Vector2(direction.x, 0f) * Mathf.Max(maxSpeed - ground.GetFriction(), 0f);
        //print("Move friction: " +ground.GetFriction());
        if (playerAnimation != null)
        {
            FeedVelocityData();
        }
    }

    private void FixedUpdate()
    {
        onGround = ground.GetOnGround();
        velocity = body.velocity;

        acceleration = onGround ? maxAcceleration : maxAirAcceleration;
        maxSpeedChange = acceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);

        if (canMove)
        {
            body.velocity = velocity;
        }

        if (stunCounter > 0) 
        {
            stunCounter -= Time.deltaTime;
        }
        else if (stunCounter != -69 && stunCounter <= 0)
        {
            // stunCounter <= 0 is the true condition. The other condition exists only because I needed some way of locking this "if" so that it isn't continually accessed more times than what it has to be.
            // Therefore I've made -69 into a resting number, locking the whole thing.
            stunCounter = -69;
            canMove = true;
        }

        if (knockbackCooldown > 0)
        {
            knockbackCooldown -= Time.deltaTime;
        }
    }

    public void KnockBack(Vector2 direction, float stunLength, int upwardsKnockback, int knockbackForce)
    {
        if (knockbackCooldown <= 0)
        {
            body.velocity = new Vector2(0, 0);
            knockbackCooldown = 1f;
            stunCounter = stunLength;
            canMove = false;
            body.AddForce(new Vector2(0, upwardsKnockback));
            body.AddForce(direction * -knockbackForce);
        }
    }

    private void FeedVelocityData()
    {
        playerAnimation.velocityAmount = body.velocity;
    }
}

