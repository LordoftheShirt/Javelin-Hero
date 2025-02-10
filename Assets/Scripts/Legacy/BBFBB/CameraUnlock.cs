using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUnlock : MonoBehaviour
{
    private CameraFollowVer2 getFollowScript;

    private void Start()
    {
        getFollowScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollowVer2>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Invoke("ReturnToPlayer", 0);
        }
    }

    public void ReturnToPlayer()
    {
        getFollowScript.mainCameraTarget = getFollowScript.playerLocation;
        CameraFollowVer2.cameraLockVer2 = false;
        Debug.Log("CameraUNLOCKED!");
    }



}
