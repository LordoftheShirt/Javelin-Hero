using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwap : MonoBehaviour
{
[SerializeField] private Camera mainCamera;
[SerializeField] private Camera roomCamera;
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            mainCamera.orthographicSize = roomCamera.orthographicSize;
            mainCamera.transform.position = roomCamera.transform.position;
            print("Triggered");
        }
    }
}
