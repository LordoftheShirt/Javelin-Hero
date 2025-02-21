using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwap : MonoBehaviour
{
    [Header("Lerp or Jump Cut")]
    [SerializeField] bool smoothCamera = true;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera roomCamera;
    [Header("Values")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float zoomSpeed = 2f;
    [SerializeField] private float roomNumber;
    
    private static float roomPriority;
    private void Start()
    {
        roomPriority = 1;
    }

    void LateUpdate()
    {
        if (roomPriority == roomNumber && smoothCamera)
        {
            if (mainCamera.orthographicSize != roomCamera.orthographicSize)
            {
                mainCamera.orthographicSize = Mathf.LerpUnclamped(mainCamera.orthographicSize, roomCamera.orthographicSize, zoomSpeed * Time.deltaTime);
            }

            if (mainCamera.transform.position != roomCamera.transform.position)
            {
                mainCamera.transform.position = Vector3.LerpUnclamped(mainCamera.transform.position, roomCamera.transform.position, moveSpeed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            roomPriority = roomNumber;
            if (!smoothCamera)
            {
                mainCamera.orthographicSize = roomCamera.orthographicSize;
                mainCamera.transform.position = roomCamera.transform.position;
            }
        }
    }
}
