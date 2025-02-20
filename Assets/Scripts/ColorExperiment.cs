using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class ColorExperiment : MonoBehaviour
{
    [SerializeField] private bool changeColor = false;
    [Header("Speed")]
    [SerializeField] private float speed = 2f;
    [Header("Objects")]
    [SerializeField] private SpriteRenderer[] colorShiftObjects;
    [Header("Palettes")]
    [SerializeField] private Color[] colorPalette0;
    [SerializeField] private Color[] colorPalette1;
    [SerializeField] private Color[] colorPalette2;
    [SerializeField] private Color[] colorPalette3;
    [SerializeField] private Color[] colorPalette4;
    [SerializeField] private Color[] colorPalette5;
    [SerializeField] private Color[] colorPalette6;
    [SerializeField] private Color[] colorPalette7;
    [SerializeField] private Color[] colorPalette8;
    [SerializeField] private Color[] colorPalette9;
    [SerializeField] private Color[] colorPalette10;
    [SerializeField] private Color[] colorPalette11;


    /*private float myR, myG, myB;
    private Vector3 myRGB;
    private Vector3 colorChange;
    private Vector3 velocity = Vector3.zero;
    private Vector3 paletteIntoVector;

    private int[] colorPaletteIndexArray; */
    // Start is called before the first frame update
    void Start()
    {
        // has to grow somehow depending on the number of color Palettes in existence. Serializable 2D arrays are not allowed :(
        /*
        colorPaletteIndexArray = new int[12];

        myR = colorShiftObjects[0].color.r;
        myG = colorShiftObjects[0].color.g;
        myB = colorShiftObjects[0].color.b;
        myRGB = new Vector3(myR, myG, myB);
        print(colorShiftObjects[0].color);
        paletteIntoVector = new Vector3(colorPalette0[0].r, colorPalette0[0].g, colorPalette0[0].b);
        print(colorPalette0[0]);
        print(paletteIntoVector);
        colorChange = myRGB;

        */


        //SIMPLEST METHOD:
        //colorShiftObjects[0].color = colorPalette0[0];
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // TRANSITION ATTEMPT (SMOOTHDAMP):
        //colorChange = Vector3.SmoothDamp(myRGB, paletteIntoVector, ref velocity, speed * Time.deltaTime);

        // MOVE TOWARDS:
        /*
       colorChange = Vector3.MoveTowards(colorChange, paletteIntoVector, speed * Time.deltaTime);
       colorShiftObjects[0].color = new Color(colorChange.x, colorChange.y, colorChange.z);
       print(colorShiftObjects[0].color);
        */
        //colorChange = Vector3.L
        if (changeColor)
        {
            foreach (var sprite in colorShiftObjects)
            {
                if (sprite != null)
                    sprite.color = Color.LerpUnclamped(sprite.color, colorPalette0[0], speed * Time.deltaTime);

                if (colorShiftObjects[colorShiftObjects.Length - 1].color == colorPalette0[0])
                {
                    print("STOP");
                    changeColor = false;
                }
            }
        }
        //colorShiftObjects[0].color = Color.LerpUnclamped(colorShiftObjects[0].color, colorPalette0[0], speed * Time.deltaTime);
    }


}
