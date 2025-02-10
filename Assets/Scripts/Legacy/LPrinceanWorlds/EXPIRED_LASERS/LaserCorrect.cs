using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LaserCorrect : MonoBehaviour
{
    // Record Cursor Location Variables
    public Vector2 screenPosition;
    public Vector3 worldPosition;
    private Ray mouseRay;

    // Must haves
    [SerializeField] private Transform cursor, end, start;
    private LineRenderer lineRenderer;

    // Laser Construction work
    RaycastHit2D hit, secondHit;
    Vector2 lineInSpace, afterFirstBounce;
    [SerializeField] LayerMask layerMask;
    [SerializeField] private float defaultLength = 50;
    // [SerializeField] private float numOfReflects = 10;
    [SerializeField] private float offset = 1;


    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 4;

    }
    void Update()
    {
        RecordCursorLocation();
        FinalLaser();
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

    private void FinalLaser()
    {
        // creates a vector between cursor and the transform "start" position.
        lineInSpace = new Vector2(cursor.position.x - start.position.x, cursor.position.y - start.position.y);

        // Sets the first point of the lineRenderer to follow thus vector but at defaultLength.
        lineRenderer.SetPosition(0, lineInSpace.normalized * defaultLength);
        lineRenderer.positionCount = 2;

        float remainLength = defaultLength;

        // Creates a raycast which should align perfectly with the lineRenderer above.
        hit = Physics2D.Raycast(lineInSpace.normalized, cursor.position, remainLength, layerMask);
            if (hit)
            {
                end.position = hit.point;
                lineRenderer.SetPosition(0, end.position - start.position);
                //remainLength -= Vector2.Distance(end.position, start.position);

                afterFirstBounce = Vector2.Reflect(lineInSpace, hit.normal);
                secondHit = Physics2D.Raycast(hit.point + hit.normal.normalized * offset, afterFirstBounce, remainLength, layerMask);
                if (secondHit) 
                {
                lineRenderer.SetPosition(1, afterFirstBounce.normalized * remainLength);
                Debug.Log("SECOND HIT!");
                }
            }
    }
}
