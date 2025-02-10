using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerAnchorProcedural : MonoBehaviour
{
    [SerializeField] private float constrainedDistanceLength = 3;
    private Vector2 outOfBoundsCheck, constrainedDistanceVector;
    private Transform jointCollection, nextAnchorInLine;
    private bool nextAnchorFound = false;
    void Start()
    {
        jointCollection = transform.parent;

        // Conducts a search for next anchor of whom this transform shall pull upon.
        for (int i = 0; i < jointCollection.childCount; i++)
        {
            if (jointCollection.GetChild(i) == transform)
            {
                //Debug.Log("I found myself! " + i);

                if (i + 1 < jointCollection.childCount)
                {
                    nextAnchorInLine = jointCollection.GetChild(i + 1);
                    nextAnchorFound = true;

                    //Debug.Log("I found my sibling! " + (i + 1));
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (nextAnchorFound)
        {
            // creates a vector between the two anchor points and normalizes it (magnitude a maximum of 1 with vector start point at 0)
            outOfBoundsCheck = new Vector2(nextAnchorInLine.position.x - transform.position.x, nextAnchorInLine.position.y - transform.position.y);
            outOfBoundsCheck.Normalize();

            // Sets the distance to the desired amount and makes it so that the position of the adjacentAnchor is relative to the primary.
            constrainedDistanceVector = new Vector2(outOfBoundsCheck.x * constrainedDistanceLength + transform.position.x, outOfBoundsCheck.y * constrainedDistanceLength + transform.position.y);
            nextAnchorInLine.position = constrainedDistanceVector;

            // makes the joint rotate after the anchor of which it follows.
            nextAnchorInLine.right = outOfBoundsCheck;
        }
        
    }


}
