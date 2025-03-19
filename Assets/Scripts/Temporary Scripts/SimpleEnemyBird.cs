using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SimpleEnemyBird : MonoBehaviour
{
    [SerializeField, Range(0, 50)] private float moveSpeed = 5f;
    [SerializeField] private LayerMask findWallsLayerMask, deathAvoidLayerMask;
    [SerializeField] private GameObject deathZone;

    private int rayDirection = 1;
    private RaycastHit2D avoidWallsHit;
    private Vector2 normal;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;
    private bool isAlive = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isAlive)
        {
            avoidWallsHit = Physics2D.Raycast(transform.position, new Vector2(rayDirection, 0), 3f, findWallsLayerMask);
            if (avoidWallsHit)
            {
                rayDirection *= -1;
                moveSpeed *= -1;
            }

            transform.Translate(new Vector2(moveSpeed, 0) * Time.fixedDeltaTime);
            //transform.position = Vector2.MoveTowards(transform.position, new Vector2(rayLength, 0), Time.deltaTime * moveSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")&& isAlive)
        {
            normal = collision.GetContact(0).normal;

            // if player comes in from the sides or below (Below shouldn't actually be here): take 1 dmg and deal this amount of knockback.
            if (normal.x > 0.9f || normal.x < -0.9f || normal.y > 0.9f)
            {
                if (collision.gameObject.TryGetComponent<Move>(out Move moveScript))
                {
                    moveScript.KnockBack(normal, 0.3f, 500, 1000);
                }
                if (collision.gameObject.TryGetComponent<HealthManager>(out HealthManager healthScript))
                {
                    healthScript.InputDamage(1);
                }
            }
            else if (normal.y < -0.9f)
            {
                if (collision.gameObject.TryGetComponent<Move>(out Move moveScript))
                {
                    moveScript.KnockBack(normal, 0, 1000, 0);
                    //print("fly now");

                }
                // bird gets jumped on. Dies.
                //print("DIE!");
                Die();
            }
        }
    }

    public void Die()
    {
        if (boxCollider.enabled == true)
        {
            FindObjectOfType<AudioManager>().Play("DieBird");
        }
        boxCollider.enabled = false;
        isAlive = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 1;
        rb.angularDrag = 0.05f;
        rb.rotation = 45f;
        rb.excludeLayers = deathAvoidLayerMask;
        if (deathZone != null)
            deathZone.SetActive(false);
    }
}


