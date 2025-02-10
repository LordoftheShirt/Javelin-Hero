using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam3D : MonoBehaviour
{
    public float sensX;
    public float sensY;

    static public bool cameraLock = false;

    public Transform orientation;

    float xRotation;
    float yRotation;

    private void Start()
    {
        // First one locks cursor, second one turns cursor invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (!cameraLock)
        {
            // Records input of mouse movement of X and Y axis.
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

            // The fuck?
            yRotation += mouseX;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }
}
