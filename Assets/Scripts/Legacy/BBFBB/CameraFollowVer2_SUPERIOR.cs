using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowVer2: MonoBehaviour
{
    [SerializeField] public Transform mainCameraTarget;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10f);
    [SerializeField] private float smoothing = 1.0f;
    [SerializeField] static public bool cameraLockVer2 = false;
    public Transform playerLocation;
    void LateUpdate()
    {
        if (cameraLockVer2 == false)
        {
            Vector3 newposition = Vector3.Lerp(transform.position, mainCameraTarget.position + offset, smoothing * Time.deltaTime);
            transform.position = newposition;
        }
        else
        {
            Vector3 newposition = Vector3.Lerp(transform.position, mainCameraTarget.position, smoothing * Time.deltaTime);
            transform.position = newposition;
        }        
    }
}
