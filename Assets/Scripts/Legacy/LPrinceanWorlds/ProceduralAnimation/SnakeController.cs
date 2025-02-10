using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    // All movement related variables
    Rigidbody2D rb;
    private float horizontalValue;
    private float verticalValue;
    private Transform orientation;
    Vector2 moveDirection, right = new Vector2(1, 0), up = new Vector2(0, 1);

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 100;
    [SerializeField] private float maxSpeed = 20;
    /*[SerializeField]*/ private Transform cursor, cameraSpot;

    private Vector2 screenPosition, lineInSpace;
    private Vector3 worldPosition;
    private Ray mouseRay, playerToCursorRay;
    private bool aiming = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        orientation = GetComponent<Transform>();
    }
    void Update()
    {
        MyInput();
        //RecordCursorLocation();
        //CameraMovement();
    }

    private void FixedUpdate()
    {
        //UpTowardsCursor();
        PlayerMovement();
    }

    private void MyInput()
    {
        horizontalValue = Input.GetAxisRaw("Horizontal");
        verticalValue = Input.GetAxisRaw("Vertical");
    }

    private void PlayerMovement()
    {
        moveDirection = right * horizontalValue + verticalValue * up;
        rb.AddForce(moveDirection * moveSpeed, ForceMode2D.Force);

        Vector2 flatVel = new Vector2(rb.velocity.x, rb.velocity.y);
        if (flatVel.magnitude > maxSpeed)
        {
            Vector2 limitedVel = flatVel.normalized * maxSpeed;
            rb.velocity = new Vector2(limitedVel.x, limitedVel.y);
        }
    }

    private void RecordCursorLocation()
    {
        // Gets input of mouse, Ray "mouseRay" records the Vector2 (x and y) of the mouse position on screen relative to resolution.
        screenPosition = Input.mousePosition;
        mouseRay = Camera.main.ScreenPointToRay(screenPosition);

        // Vector3 worldPosition translates the x and y to where that position is in the real world. Since it is Vector3 (has z), z must be set to 0. Otherwise it'll be on the same z as camera.
        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        worldPosition.z = 0;
    }

    private void UpTowardsCursor()
    {
        // moves "end" object to cursor location.
        cursor.position = worldPosition;

        // Creates vector between cursor and pivot point
        lineInSpace = new Vector2(cursor.position.x - transform.position.x, cursor.position.y - transform.position.y);

        //Rotates Player always in direction of vector
        transform.up = lineInSpace;
    }

    private void CameraMovement()
    {
        if (!aiming)
        {
            Vector3 zoomOut = new Vector3(transform.position.x, transform.position.y, -20);
            cameraSpot.position = zoomOut;
        }
        else
        {
            Vector3 halfMagnitude = new Vector3((cursor.position.x + transform.position.x) / 2, (cursor.position.y + transform.position.y) / 2, -20);
            cameraSpot.position = halfMagnitude;
        }
    }

}
