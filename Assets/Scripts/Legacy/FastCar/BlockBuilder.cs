using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBuilder : MonoBehaviour
{
    private Taskmaster taskmasterScript;
    [SerializeField] private GameObject player, mainCamera, cameraLockCoordinates, buildingBlockButton, ExitButton, taskmasterObject, submitButton;
    private PlayerMovement3D moveScript;
    private PlayerCam3D camScript;
    private bool triggerOn = false;
    private bool modeActive = false;
    private bool firstSitting = true;

    private void Start()
    {
        camScript = mainCamera.GetComponent<PlayerCam3D>();
        moveScript = player.GetComponent<PlayerMovement3D>();
        taskmasterScript = taskmasterObject.GetComponent<Taskmaster>();
    }

    private void Update()
    {
        if (triggerOn) 
        {
            if (Input.GetKey(moveScript.changeMode) && modeActive == false)
            {
                Debug.Log("Set active FALSE");
                player.SetActive(false);
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                PlayerCam3D.cameraLock = true;
                modeActive = true;
                buildingBlockButton.SetActive(true);
                ExitButton.SetActive(true);
                submitButton.SetActive(true);
                if (firstSitting)
                {
                    taskmasterScript.SitWithVinnyCompleted();
                    firstSitting=false;
                    taskmasterScript.lastTouch();
                }
            }
            if(modeActive)
            {
                mainCamera.transform.position = cameraLockCoordinates.transform.position;
                mainCamera.transform.rotation = cameraLockCoordinates.transform.rotation;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    { 
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Tag Compared");
            triggerOn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Tag Compared");
            triggerOn = false;
        }
    }

    public void ExitBlockBuilding()
    {
        modeActive = false;
        triggerOn = false;
        player.SetActive(true);
        PlayerCam3D.cameraLock = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        buildingBlockButton.SetActive(false);
        ExitButton.SetActive(false);
        submitButton.SetActive(false);
    }
}
