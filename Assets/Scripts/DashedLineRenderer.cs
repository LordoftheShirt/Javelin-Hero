using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashedLineRenderer : MonoBehaviour
{
    [SerializeField] private Transform anchorPosition;
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    private void FixedUpdate()
    {
        transform.position = RecordCursor.GetCursorPosition();
        lineRenderer.SetPosition(0, anchorPosition.position - transform.position);
    }
}
