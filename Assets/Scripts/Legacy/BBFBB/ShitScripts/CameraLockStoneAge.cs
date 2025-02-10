using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraLockStoneAge : MonoBehaviour
{
    [SerializeField] private GameObject m_Camera, cameraCoordinates;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CameraFollowVer2.cameraLockVer2 = true;
            m_Camera.transform.position = cameraCoordinates.transform.position;
        }
    }

}
