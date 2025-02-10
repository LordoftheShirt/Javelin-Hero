using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowLaser : MonoBehaviour
{
    private float hypotenusan;
    [SerializeField] float lineThickness = 0.25f;
    [SerializeField] private Transform end, start, cursor;
    public Vector2 lineInSpace, halfMagnitude;
    public Vector2 screenPosition;
    public Vector3 worldPosition;
    private Ray mouseRay, playerToCursorRay;
    [SerializeField] private LayerMask bowLaserDetect;
    void Start()
    {
    }
    private void Update()
    {
        RecordCursorLocation();
    }
    void FixedUpdate()
    {
        VisualiseVectorAvatarMouse();
    }

    private void RecordCursorLocation()
    {
        // Gets input of mouse, Ray "mouseRay" records the Vector2 (x and y) of the mouse position on screen relative to resolution.
        screenPosition = Input.mousePosition;
        mouseRay = Camera.main.ScreenPointToRay(screenPosition);

        // Vector3 worldPosition translates the x and y to where that position is in the real world. Since it is Vector3 (has z), z must be set to 0. Otherwise it'll be on the same z as camera.
        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        worldPosition.z = 0;

        // moves "end" object to cursor location.
        end.position = worldPosition;
    }

    private void VisualiseVectorAvatarMouse()
    {
        // Calculates the delta line between the two points of interest.
        lineInSpace = new Vector2(end.position.x - start.position.x, end.position.y - start.position.y);

        // creates an average of both values to prepare create a visible vector.
        halfMagnitude = new Vector2((end.position.x + start.position.x) / 2, (end.position.y + start.position.y) / 2);

        // makes the visual box object always rotate in the same angle of the invisible vector lineInSpace.
        hypotenusan = lineInSpace.magnitude;
        transform.up = lineInSpace;

        // teleports the box object to the middle distance between both points, then stretches it to be the same length as the hypotenusa.
        transform.position = halfMagnitude;
        transform.localScale = new Vector2(lineThickness, hypotenusan);


         RaycastHit2D hit = Physics2D.Raycast(start.position, lineInSpace, Mathf.Infinity, bowLaserDetect);
        if (hit.collider != null)
        {
            cursor.position = hit.point;
        }
    }
}
