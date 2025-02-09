using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private bool onGround;
    private float friction;

    private Vector2 normal;
    private PhysicsMaterial2D material;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EvaluateCollision(collision);
        RetrieveCollision(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        EvaluateCollision(collision);
        RetrieveCollision(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        onGround = false;
        friction = 0;
    }

    private void EvaluateCollision(Collision2D collision)
    {
        // Looks at each collision and checks at which angle thus collision occured. A normal is a vector which points perpindicular, directly out from the surface of which it detects.
        // So to say, a horizontal flat surface should have a normal of y = 1. A normal only has 1 in length, meaning, it's pointing directly upwards. Some leeway is given: if y is 0.7 or more, set the onGround bool to true.
        for (int i = 0; i < collision.contactCount; i++) 
        { 
            normal = collision.GetContact(i).normal;
            onGround |= normal.y >= 0.7f;
            //print(normal.y);
        }
    }


    // Gathers the material of the other object and checks whether it's null. If it is, assume friction stays at 0. Otherwise, sample the friction value.
    private void RetrieveCollision(Collision2D collision) 
    {
        material = collision.rigidbody.sharedMaterial;

        friction = 0;

        if (material != null) 
        {
            friction = material.friction;
            //print(collision + " Has material");
        }
        //print("Ground friction:" +friction);
    }

    public bool GetOnGround()
    {
        return onGround;
    }

    public float GetFriction()
    {
        return friction;
    }
}
