using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraLock : MonoBehaviour
{
    [SerializeField] private GameObject m_Camera, cameraCoordinates;
    [SerializeField] private Transform cameraCoordinatesTransform;
    private CameraFollowVer2 getFollowScript;

    private void Start()
    {
        getFollowScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollowVer2>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            getFollowScript.mainCameraTarget = cameraCoordinatesTransform;
            CameraFollowVer2.cameraLockVer2 = true;
            // m_Camera.transform.position = cameraCoordinates.transform.position;
            Debug.Log("CAMERA LOCKED!");
        }
    }

}
