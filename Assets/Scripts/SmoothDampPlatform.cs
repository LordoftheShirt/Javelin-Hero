using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothDampPlatform : MonoBehaviour
{
    [SerializeField] private Transform target1, target2;
    [SerializeField, Range(0, 100)] private float speed = 1;
    private bool forward = true;
    private Rigidbody2D rb;

    // Smoothdampers:
    //private float lerpedValue;
    //private float duration = 3;
    //private float velocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        //lerpedValue = Mathf.SmoothDamp(lerpedValue, 1, ref velocity, duration);
    }
    void FixedUpdate()
    {
        if (forward)
        {
            transform.position = Vector2.Lerp(transform.position, target1.position, speed * Time.deltaTime);
            /*if (rb.velocity.magnitude < 0.1f)
            {
                forward = false;
            }
        }
        else
        {
            transform.position = Vector2.Lerp(transform.position, target2.position, speed * Time.deltaTime);
            if (rb.velocity.magnitude < 0.1f)
            {
                forward = true;
            } */
        }

        

    }
}
