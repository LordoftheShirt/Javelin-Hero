using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorExperiment : MonoBehaviour
{
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

    private int[] colorPaletteIndexArray;
    // Start is called before the first frame update
    void Start()
    {
        // has to grow somehow depending on the number of color Palettes in existence. Serializable 2D arrays are not allowed :(
        colorPaletteIndexArray = new int[12];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
