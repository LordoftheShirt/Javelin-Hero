using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class BowLineRenderer : MonoBehaviour
{
    // all vector laser variables
    private LineRenderer lineRenderer;
    [SerializeField] private float defaultLength = 50f;
    [SerializeField] private Transform end, start, cursor, bounceTransform;
    [SerializeField] private LayerMask bowLaserDetect;
    public Vector2 lineInSpace, bounceVector1;
    private RaycastHit2D secondHit, hit;
    private Ray2D bouncingRay;
    [SerializeField] private float numOfReflections = 10;

    // all cursor detection variables.
    public Vector2 screenPosition;
    public Vector3 worldPosition;
    private Ray mouseRay;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    private void Update()
    {
        RecordCursorLocation();
        VisualiseVectorAvatarMouse();
        //ReflectLaser();
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
        lineRenderer.SetPosition(1, lineInSpace.normalized * defaultLength);

        // Creates a raycast using the vector above. Infinte length, looking for objects within the "bowLaserDetect" layermask. Teleports a laser pointer circle to the position of collision.
          RaycastHit2D hit = Physics2D.Raycast(start.position, lineInSpace, defaultLength, bowLaserDetect);
          if (hit.collider != null)
        {
            end.position = hit.point;
            lineRenderer.SetPosition(1, (end.position - transform.position));
            bounceVector1 = Vector2.Reflect(lineInSpace, hit.normal);

              secondHit = Physics2D.Raycast(hit.point, bounceVector1, Mathf.Infinity, bowLaserDetect);
              if (secondHit.collider != null)
              {
                  bounceTransform.position = secondHit.point;
              }
          }
    }
    // burn this fucking shit below. fuck you
    private void ReflectLaser()
    {
        lineInSpace = new Vector2(cursor.position.x - start.position.x, cursor.position.y - start.position.y);
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);
        
        for (int i = 0; i < numOfReflections -1; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(start.position, lineInSpace, defaultLength, bowLaserDetect);
            if (hit)
            {
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);

                bouncingRay = new Ray2D(hit.point, Vector2.Reflect(bouncingRay.direction, hit.normal));
            }
            else
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, bouncingRay.origin + (bouncingRay.direction * defaultLength));
            }
        }
    }

}



