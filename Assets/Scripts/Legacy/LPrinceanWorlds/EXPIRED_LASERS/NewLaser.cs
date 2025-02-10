using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLaser : MonoBehaviour
{
    // all vector laser variables
    private LineRenderer lineRenderer;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform cursor;
    [SerializeField] private float defaultLength = 50;
    [SerializeField] private float numOfReflections = 10;

    // all cursor detection variables.
    public Vector2 screenPosition;
    public Vector3 worldPosition;
    private Ray mouseRay;
    private RaycastHit2D hit;
    private Ray2D ray;
    [SerializeField] private float offset = 1;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    private void Update()
    {
        //RecordCursorLocation();
        //NormalLaser();
        //ReflectLaser();
    }
    void FixedUpdate()
    {
        ReflectLaser();
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

    private void ReflectLaser()
    {
        ray = new Ray2D(transform.position, transform.up);

        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);

        float remainLength = defaultLength;

        for (int i = 0; i < numOfReflections; i++) {
            hit = Physics2D.Raycast(ray.origin, ray.direction, remainLength, layerMask);
            if (hit)
            {
                Debug.Log("Hit NewLaser!");
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point + hit.normal.normalized * offset);

                remainLength -= Vector2.Distance(ray.origin, hit.point);

                ray = new Ray2D(hit.point + hit.normal.normalized * offset, Vector2.Reflect(ray.direction, hit.normal));
            }
            else 
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount -1, ray.origin + (ray.direction * remainLength)); 
            }
        }
    }

    private void NormalLaser()
    {
        lineRenderer.SetPosition(0, transform.position);

        hit = Physics2D.Raycast(transform.position, transform.up);
        if (hit)
        {
            Debug.Log("Hit NewLaser!");
            lineRenderer.SetPosition(1, hit.point);
        }
        else { lineRenderer.SetPosition(1, transform.position + (cursor.position * defaultLength)); }
    }
}
