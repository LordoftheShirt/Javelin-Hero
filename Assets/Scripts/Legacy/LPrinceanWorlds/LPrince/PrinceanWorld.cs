using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PrinceanWorld : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D otherObject, myBody;
    private Vector2 gravity;
    [SerializeField] private Vector2 startVelocity;
    [SerializeField] private float gravityAmount = 1;

    private void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        myBody.AddForce(startVelocity);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Rigidbody2D>(out Rigidbody2D otherRigidBody))
        {
            otherObject = otherRigidBody;
        }
    }
    private void OnTriggerStay2D()
    {
        if (otherObject != null)
        {
            // Creates a vector stretching between its own pivot and pivot of OtherObject.
            gravity = new Vector2(otherObject.transform.position.x - transform.position.x, otherObject.transform.position.y - transform.position.y);

            // The two objects will always be facing each other with their up arrow (green, rotation z).
            otherObject.transform.up = gravity;



            // Turns the vectors into decimals so that rather than have the gravity increase as otherObject grow farther apart, gravity will decrease.
            //gravity = new Vector2(1/gravity.x, 1/gravity.y);

            // gravityAmount is negative to make otherObject move in contrary to direction of the vector (otherObject will fall toward the vectors base). 
            otherObject.AddForce(gravity * -gravityAmount);
            Debug.Log(gravity);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
