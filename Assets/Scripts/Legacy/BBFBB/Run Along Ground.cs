using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAlongGround : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    Rigidbody2D rgbd;
    public bool activatePlatform = false;

    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (activatePlatform == true)
        {
            moveGround();
        }
    }


    void moveGround()
    {
        rgbd.position += Vector2.left * moveSpeed * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            activatePlatform = true;
        }
    }
}
