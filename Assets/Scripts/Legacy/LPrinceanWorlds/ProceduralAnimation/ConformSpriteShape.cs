using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public class ConformSpriteShape : MonoBehaviour
{
    [SerializeField] private Transform jointCollection;
    private Transform []jointsArray;

    private SpriteShapeController sprite;
    private Spline spline;
    private int splineCount;
    private bool allSplinesGenerated = false;
    void Start()
    {
        sprite = GetComponent<SpriteShapeController>();
        spline = sprite.spline;
        splineCount = spline.GetPointCount();

        GenerateSplines();
    }

    private void Update()
    {
        GenerateSplines();
    }

    void FixedUpdate()
    {
        // Conform all splines to creature anchor points
        if (allSplinesGenerated == true) 
        {
            for (int i = 0; i < jointsArray.Length ; i++)
            {
                Joint(i);
            }        
        }
        //sprite.RefreshSpriteShape();

        // THIS WORKS! IT DOESN'T LOSE ITS SKIN! HOW INTENSIVE IS THIS?
        sprite.enabled = false;
        sprite.enabled = true;
    }

    private void Joint(int arrayNumber)
    {
        // ERROR FIX EXPLANATION: This for and these if's are all here to prevent the circumstance wherein a spriteShape spline has its position set too close upon another, thereby creating an error. These if's should block that from happening.
        for (int i = 0; i < jointsArray.Length; i++)
        {
            if (arrayNumber != i)
            {
                if (Math.Round(jointsArray[arrayNumber].position.x, 1) != Math.Round(jointsArray[i].position.x, 1) || Math.Round(jointsArray[arrayNumber].position.y, 1) != Math.Round(jointsArray[i].position.y, 1))
                {
                    // Teleports Spline to anchor location. Then changes scale to match the anchors size.
                    spline.SetPosition(arrayNumber, new Vector2(jointsArray[arrayNumber].position.x, jointsArray[arrayNumber].position.y));
                    spline.SetHeight(arrayNumber, jointsArray[arrayNumber].lossyScale.y * 2);
                }
                else 
                { 
                    Debug.Log("I, " + gameObject.name + " Am fucked. Number i: " + i + " arrayNumber: " + arrayNumber);
                    Debug.Log("X1: " + Math.Round(jointsArray[arrayNumber].position.x, 1) + " X2: " + Math.Round(jointsArray[i].position.x, 1));
                    Debug.Log("Y1: " + Math.Round(jointsArray[arrayNumber].position.y, 1) + " Y2: " + Math.Round(jointsArray[i].position.y, 1));
                }
            }
        }
    }

    private void GenerateSplines()
    {
        if (allSplinesGenerated == false)
        {
            // Counts "JointCollection"'s number of children and adds them by index number into the "jointsArray"
            jointsArray = new Transform[jointCollection.childCount];
            for (int i = 0; i < jointCollection.childCount; i++)
            {
                jointsArray[i] = jointCollection.transform.GetChild(i);

                //Debug.Log("Child " + i + " Retrieved Into Array");
            }

            // Spawns in splines. Matches "splineCount" to be equal to the arrays length.
            for (int i = splineCount; jointsArray.Length > i; i++)
            {
                spline.InsertPointAt(i, new Vector3(i, i));
                splineCount = i;

                //Debug.Log("Spline " + (i) + " Added.");
            }
            allSplinesGenerated = true;
        }

    }
}
