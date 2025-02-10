using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessRoad : MonoBehaviour
{
    [SerializeField] private Transform start, end;
    [SerializeField] private float moveSpeed = 2f;
    void Start()
    {
        
    }

    
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, end.position, moveSpeed * Time.fixedDeltaTime);
        if (transform.position == end.position)
        {
            Instantiate(gameObject).transform.position = start.position;
            Destroy(gameObject);
        }
    }
}
