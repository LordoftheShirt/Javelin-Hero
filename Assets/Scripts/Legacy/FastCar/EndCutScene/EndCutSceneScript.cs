using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCutSceneScript : MonoBehaviour
{
    private AudioSource brotherAudio;
    [SerializeField] private GameObject car, blackScreen, textEnd;
    [SerializeField] private Transform driveDestination, cameraTeleport1, cameraTeleport2, cameraTeleport3, cameraTeleport4, cameraTeleport5, cameraTeleport6, cameraTeleport7, mCamera;
    private bool driving = false;
    [SerializeField] private int moveSpeed = 1;
    private int oncePerSec;
    [SerializeField] private int accelerationFrequency = 60;
    private int whichCameraJump = 1;
    void Start()
    {
        brotherAudio = GetComponent<AudioSource>();
        Invoke("CarMoveDelay", 5);
        Invoke("CameraJump1", 7.4f);
        Invoke("EndScene", 38f);
    }

    void Update()
    {
        if (driving)
        {
            transform.position = Vector3.MoveTowards(transform.position, driveDestination.position, moveSpeed * Time.fixedDeltaTime);
        }
    }
    private void FixedUpdate()
    {
        if (driving)
        {
                oncePerSec++;
                if (oncePerSec == accelerationFrequency)
                {
                    moveSpeed++;
                    oncePerSec = 0;
                }
                if (moveSpeed == 5)
            {
           //   driving = false;
            }
        }

        if (whichCameraJump == 2) 
        {
            mCamera.rotation = cameraTeleport2.rotation;
            mCamera.position = cameraTeleport2.position;
        }

        if (whichCameraJump == 3) 
        {
            mCamera.rotation = cameraTeleport3.rotation;
            mCamera.position = cameraTeleport3.position;
        }
        if (whichCameraJump == 4)
        {
            mCamera.rotation = cameraTeleport4.rotation;
            mCamera.position = cameraTeleport4.position;
        }

        if (whichCameraJump == 5)
        {
            mCamera.rotation = cameraTeleport5.rotation;
            mCamera.position = cameraTeleport5.position;
            moveSpeed = 0;
        }
        if (whichCameraJump == 6)
        {
            mCamera.rotation = cameraTeleport6.rotation;
            mCamera.position = cameraTeleport6.position;

        }
        if (whichCameraJump == 7)
        {
            mCamera.rotation = cameraTeleport7.rotation;
            mCamera.position = cameraTeleport7.position;

        }

    }

    private void CarMoveDelay()
    {
        driving = true;
    }

    private void CameraJump1()
    {
        mCamera.rotation = cameraTeleport1.rotation;
        mCamera.position = cameraTeleport1.position;
        Invoke("CameraJump2", 3);
        Debug.Log("JUMP ONE!");
    }
    private void CameraJump2()
    {
        whichCameraJump = 2;
        Invoke("CameraJump3", 5);
    }
    private void CameraJump3()
    {
        whichCameraJump = 3;
        Invoke("CameraJump4", 3);
    }

    private void CameraJump4()
    {
        whichCameraJump = 4;
        Invoke("CameraJump5", 3);
    }
    private void CameraJump5()
    {
        whichCameraJump = 5;
        Invoke("CameraJump6", 4.2f);
    }
    private void CameraJump6()
    {
        whichCameraJump = 6;
        Invoke("CameraJump7", 3f);
        brotherAudio.Play();
    }
    private void CameraJump7()
    {
        whichCameraJump = 7;
        Invoke("Black", 5);
    }
    private void Black()
    {
        blackScreen.SetActive(true);
        textEnd.SetActive(true);
        whichCameraJump = 3;
    }
    private void EndScene()
    {
        SceneManager.LoadScene(0);
    }
}
