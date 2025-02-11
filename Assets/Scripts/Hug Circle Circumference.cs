using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class HugCircleCircumference : MonoBehaviour
{
    [SerializeField] private TridentBehaviour trident;
    [SerializeField, Range(0f, 90f)] private float lookUpSnapMargin = 40;
    [SerializeField, Range(0f, 100f)] private float angleRailMargin = 75;
    [SerializeField, Range(0f, 5f)] private float anchorHeightOffset = 0.25f;
    [SerializeField, Range(0, 10)] private float radius = 1;
    [SerializeField] private Transform anchor;
    private Vector3 cursorPosition;
    private Vector3 cursorToTridentDelta;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cursorPosition = RecordCursor.GetCursorPosition();
    }

    private void FixedUpdate()
    {
        UpTowardsCursor();
    }

    private void UpTowardsCursor()
    {
        // moves "cursor" object to cursor location. I think this dude doesn't actually have to exist. I just like him.
        cursorPosition = RecordCursor.GetCursorPosition();

        cursorToTridentDelta = cursorPosition - transform.position;

        anchor.up = cursorToTridentDelta;
        if (anchor.eulerAngles.z >= 180 - angleRailMargin && anchor.eulerAngles.z <= 180 + angleRailMargin)
        {
            anchor.position = transform.position + new Vector3(0, anchorHeightOffset) + -cursorToTridentDelta.normalized * radius;
        }
        else
        {
            //print(-cursorToTridentDelta.normalized);
        }

        if (anchor.eulerAngles.z > 360 - lookUpSnapMargin || anchor.eulerAngles.z < lookUpSnapMargin)
        {
            //print(anchor.position);
            // if up left, else, up right
            if (anchor.eulerAngles.z > 360 - lookUpSnapMargin)
            {
                anchor.position = transform.position + new Vector3(0, anchorHeightOffset) + new Vector3(-1,0) * radius;
            }
            else 
            {
                anchor.position = transform.position + new Vector3(0, anchorHeightOffset) + new Vector3(1, 0) * radius;
            }
        }
    }
}
