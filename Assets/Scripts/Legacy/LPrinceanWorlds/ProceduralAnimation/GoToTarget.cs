using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToTarget : MonoBehaviour
{
    [SerializeField] private float teleportTargetLength;
    [SerializeField] private Transform target;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        // rb.velocity = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
    }
}
