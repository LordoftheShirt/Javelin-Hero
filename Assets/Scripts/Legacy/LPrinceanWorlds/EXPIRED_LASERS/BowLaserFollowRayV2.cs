using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowLaserFollowRayV2: MonoBehaviour
{
    // all vector laser variables
    [SerializeField] float lineThickness = 0.25f;
    [SerializeField] private Transform end, start, cursor, bounceTransform;
    [SerializeField] GameObject firstRicochet;
    [SerializeField] private LayerMask bowLaserDetect;
    public Vector2 lineInSpace, halfMagnitude, bounceVector1;
    private RaycastHit2D secondHit;

    // all cursor detection variables.
    public Vector2 screenPosition;
    public Vector3 worldPosition;
    private Ray mouseRay;
    void Start()
    {
    }
    private void Update()
    {
        RecordCursorLocation();
        VisualiseVectorAvatarMouse();
    }
    void FixedUpdate()
    {

    }

    private void RecordCursorLocation()
    {
        // Gets input of mouse, Ray "mouseRay" records the Vector2 (x and y) of the mouse position on screen relative to resolution.
        screenPosition = Input.mousePosition;
        mouseRay = Camera.main.ScreenPointToRay(screenPosition);

        // Vector3 worldPosition translates the x and y to where that position is in the real world. Since it is Vector3 (has z), z must be set to 0. Otherwise it'll be on the same z as camera.
        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        worldPosition.z = 0;

        // moves "cursor" object to cursor location.
        cursor.position = worldPosition;
    }

    private void VisualiseVectorAvatarMouse()
    {
        // Calculates the delta line between the two points of interest.
        lineInSpace = new Vector2(cursor.position.x - start.position.x, cursor.position.y - start.position.y);
        
        // This was not at all even neccesary it appears. Apparently they can calculate reflections for you... the fuck?
        //Vector2 bounceVector1 = new Vector2(-lineInSpace.x, lineInSpace.y);

        // Creates a raycast using the vector above. Infinte length, looking for objects within the "bowLaserDetect" layermask. Teleports a laser pointer circle to the position of collision.
        RaycastHit2D hit = Physics2D.Raycast(start.position, lineInSpace, Mathf.Infinity, bowLaserDetect);
        if (hit.collider != null)
        {
            end.position = hit.point;
            bounceVector1 = Vector2.Reflect(lineInSpace, hit.normal);

            secondHit = Physics2D.Raycast(hit.point, bounceVector1, Mathf.Infinity, bowLaserDetect);
            if (secondHit.collider != null)
            {
                bounceTransform.position = secondHit.point;
            }
        }
        // The construction of all of the visual components within player to collider laser:
        {

            // creates an average of the end and start values. Teleports the object onto that middlepoint between them.
            halfMagnitude = new Vector2((end.position.x + start.position.x) / 2, (end.position.y + start.position.y) / 2);
            transform.position = halfMagnitude;

            // makes the object always rotate to have its head in the same angle as the invisible vector lineInSpace.
            transform.up = lineInSpace;

            // Stretches the object to be the same length as vector.
            transform.localScale = new Vector2(lineThickness, hit.distance);
        }

        // The construction of all of the visual components within first richochet laser:
        {

            // creates an average of the end and start values. Teleports the object onto that middlepoint between them.
            Vector2 secondHalfMagnitude = new Vector2((bounceTransform.position.x + end.position.x) / 2, (bounceTransform.position.y + end.position.y) / 2);
            firstRicochet.transform.position = secondHalfMagnitude;

            // makes the object always rotate to have its head in the same angle as the invisible vector lineInSpace.
            firstRicochet.transform.up = bounceVector1;

            // Stretches the object to be the same length as vector.
            firstRicochet.transform.localScale = new Vector2(lineThickness, secondHit.distance);
        }
    }
}
