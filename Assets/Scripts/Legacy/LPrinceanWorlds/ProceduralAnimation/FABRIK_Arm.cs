using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderData;

public class FABRIK_Arm : MonoBehaviour
{
    [SerializeField] private float constrainedDistanceLength = 3, howFarUntilStep = 5;
    [SerializeField] private Transform[] joints;
    [SerializeField] private Transform teleportingHandTarget, handBullsEye, shoulderTarget;


    private Vector2 outOfBoundsCheck, constrainedDistanceVector, recordDistanceTravelled, shoulderPastLocation;
    void Start()
    {
        // prepares conditions for the "if" within handTeleporter() 
        shoulderPastLocation = shoulderTarget.position;
        teleportingHandTarget.position = handBullsEye.position;
    }

    void FixedUpdate()
    {
        HandTeleporter();

        joints[0].position = teleportingHandTarget.position;
        // Towards Hand
        TugInForwardDirection();

        joints[joints.Length - 1].position = shoulderTarget.position;
        // Towards Shoulder
        TugInBackwardDirection();
    }
    private void TugInBackwardDirection()
    {
        //scans through the array from BOTTOM to TOP and moves each anchor in accordance.
        for (int i = joints.Length - 1; i != 0; i--)
        {
                // creates a vector between the two anchor points and normalizes it (magnitude a maximum of 1 with vector start point at 0)
                outOfBoundsCheck = new Vector2(joints[i - 1].position.x - joints[i].position.x, joints[i - 1].position.y - joints[i].position.y);
                outOfBoundsCheck.Normalize();

                // Sets the distance to the desired amount and makes it so that the position of the adjacentAnchor is relative to the primary.
                constrainedDistanceVector = new Vector2(outOfBoundsCheck.x * constrainedDistanceLength + joints[i].position.x, outOfBoundsCheck.y * constrainedDistanceLength + joints[i].position.y);
                joints[i - 1].position = constrainedDistanceVector;
        }

    }
    private void TugInForwardDirection()
    {
        //scans through the array from TOP to BOTTOM and moves each anchor in accordance.
        for (int i = 0; i < joints.Length - 1; i++)
        {
            // creates a vector between the two anchor points and normalizes it (magnitude a maximum of 1 with vector start point at 0)
            outOfBoundsCheck = new Vector2(joints[i + 1].position.x - joints[i].position.x, joints[i + 1].position.y - joints[i].position.y);
            outOfBoundsCheck.Normalize();

            // Sets the distance to the desired amount and makes it so that the position of the adjacentAnchor is relative to the primary.
            constrainedDistanceVector = new Vector2(outOfBoundsCheck.x * constrainedDistanceLength + joints[i].position.x, outOfBoundsCheck.y * constrainedDistanceLength + joints[i].position.y);
            joints[i + 1].position = constrainedDistanceVector;
        }
    }

    private void HandTeleporter()
    {
        // records distance travelled. When an "HowFarUntilStep" amount has been reached, the handBullsEye (where the hand wants to go) gets teleported to move to hand again.
        recordDistanceTravelled = new Vector2(shoulderPastLocation.x - shoulderTarget.position.x, shoulderPastLocation.y - shoulderTarget.position.y);
        if (recordDistanceTravelled.magnitude >= howFarUntilStep)
        {
            shoulderPastLocation = shoulderTarget.position;
            teleportingHandTarget.position = handBullsEye.position;
        }
    }
}
