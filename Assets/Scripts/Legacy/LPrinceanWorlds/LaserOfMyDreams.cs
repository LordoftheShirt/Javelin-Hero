using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserOfMyDreams : MonoBehaviour
{
    // all vector laser variables
    private LineRenderer lineRenderer;

    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float defaultLength = 50;
    [SerializeField] private float numOfReflections = 10;
    [SerializeField] private float offset = 0.1f;

    private RaycastHit2D hit;
    private Ray2D ray;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    private void Update()
    {
        ReflectLaser();
    }
    void FixedUpdate()
    {
    }
    private void ReflectLaser()
    {
        ray = new Ray2D(transform.position, transform.up);

        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);

        float remainLength = defaultLength;

        for (int i = 0; i < numOfReflections; i++)
        {
            hit = Physics2D.Raycast(ray.origin, ray.direction, remainLength, layerMask);
            if (hit)
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point + hit.normal.normalized * offset);

                remainLength -= Vector2.Distance(ray.origin, hit.point);

                ray = new Ray2D(hit.point + hit.normal.normalized * offset, Vector2.Reflect(ray.direction, hit.normal));
            }
            else
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, ray.origin + (ray.direction * remainLength));
            }
        }
    }
}