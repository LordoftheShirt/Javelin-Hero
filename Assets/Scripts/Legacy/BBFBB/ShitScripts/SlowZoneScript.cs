using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowZoneScript : MonoBehaviour
{
    /*
    [SerializeField] private float slowEffect = 0.5f;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rgbd))
        {
           
            rgbd.velocity = rgbd.velocity.x - rgbd.velocity.x * slowEffect, 0;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rgbd))
        {

        }
    }*/
}

// TODO: FIND EFFECTOR BULLSHIT WHICH SLOWS ENEMIES AND BOXES AND TWEAK IT TO SIMILAR SPEED OF PLAYER. IDEA: FRICTION?