using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleVectorCube : MonoBehaviour
{
    private float hypotenusan;
    [SerializeField] float lineThickness = 0.25f;
    [SerializeField] private Transform end, start, halfMagnitudePivot;
    private Vector2 lineInSpace, halfMagnitude;
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        lineInSpace = new Vector2(end.position.x - start.position.x, end.position.y - start.position.y);

        // hittar medelvärdet mellan de två punkter.
        halfMagnitude = new Vector2((end.position.x + start.position.x)/2, (end.position.y + start.position.y)/2);

        hypotenusan = lineInSpace.magnitude;

        transform.up = lineInSpace;
        transform.position = halfMagnitude;
        transform.localScale = new Vector2(lineThickness, hypotenusan);
        if (halfMagnitudePivot != null) { halfMagnitudePivot.position = transform.position; }
    }
}
