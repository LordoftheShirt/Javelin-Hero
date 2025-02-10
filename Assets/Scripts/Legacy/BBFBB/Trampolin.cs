using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampolin : MonoBehaviour
{
    [SerializeField] private float jumpForce = 200f;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<Animator>().SetTrigger("jump");
            Rigidbody2D playerRigidBody = other.GetComponent<Rigidbody2D>();
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, 0);
            playerRigidBody.AddForce(new Vector2(0, jumpForce));
        } 
    }
}
