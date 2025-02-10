using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablePlayerCompletely : MonoBehaviour
{
    private AudioSource endMusic;
    private PlayerCam3D playerCamScript;
    private PlayerMovement3D playerMovementScript;
    private MoveCamera moveCameraScript;
    [SerializeField] private GameObject player, playerCamera;

    private void Start()
    {
        playerMovementScript = player.GetComponent<PlayerMovement3D>();
        moveCameraScript = playerCamera.GetComponent<MoveCamera>();
        playerCamScript = playerCamera.GetComponent<PlayerCam3D>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerMovementScript.enabled = false;
            moveCameraScript.enabled = false;
            playerCamScript.enabled = false;
        }
    }
}
