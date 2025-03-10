using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenEnemy : MonoBehaviour
{
    [SerializeField] private float MoveSpeed = 2.0f;
    private SpriteRenderer rend;
    [SerializeField] private float Bounciness = 100f;
    [SerializeField] private float knockbackForce = 200f;
    [SerializeField] private float upwardForce = 100f;
    [SerializeField] private int damageGiven = 1;
    private bool canMove = true;


    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }
    void FixedUpdate()
    {
        if (!canMove)
            return;

        transform.Translate(new Vector2(MoveSpeed, 0) * Time.deltaTime);

        if (MoveSpeed > 0)
        {
            rend.flipX = true;
        }

        if (MoveSpeed < 0)
        {
            rend.flipX = false;
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("EnemyBlock"))
        {
            MoveSpeed = -MoveSpeed;
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            MoveSpeed = -MoveSpeed;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerMovement>().TakeDamage(damageGiven);

            if(other.transform.position.x > transform.position.x)
            {
                other.gameObject.GetComponent<PlayerMovement>().TakeKnockback(knockbackForce, upwardForce);
            }
            else
            {
                other.gameObject.GetComponent<PlayerMovement>().TakeKnockback(-knockbackForce, upwardForce);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(other.GetComponent<Rigidbody2D>().velocity.x, 0);
            other.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, Bounciness));
            GetComponent<Animator>().SetTrigger("hit");
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            canMove = false;
            Destroy(gameObject, 1f);
           
        }
    }
}
