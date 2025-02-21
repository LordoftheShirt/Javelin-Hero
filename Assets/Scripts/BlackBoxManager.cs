using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlackBoxManager : MonoBehaviour
{
    [SerializeField] private float fadeSpeed = 1f;
    [SerializeField] private GameObject blackBox;

    private Camera mainCamera;
    private float cameraSizeTracker;
    private SpriteRenderer boxRenderer;

    public static bool turnBlack;

    void Start()
    {
        turnBlack = true;

        mainCamera = GetComponent<Camera>();
        boxRenderer = blackBox.GetComponent<SpriteRenderer>();

        cameraSizeTracker = mainCamera.orthographicSize;
    }
    void Update()
    {
        if (cameraSizeTracker != mainCamera.orthographicSize)
        {
            blackBox.transform.localScale *= mainCamera.orthographicSize/cameraSizeTracker;
            cameraSizeTracker = mainCamera.orthographicSize;
        }
    }

    private void LateUpdate()
    {
        if (turnBlack) 
        {
            boxRenderer.color = new Color(boxRenderer.color.r, boxRenderer.color.g, boxRenderer.color.b, 1);
            turnBlack = false;
        }

        if (boxRenderer.color.a != 0)
        {
            boxRenderer.color = new Color (boxRenderer.color.r, boxRenderer.color.g, boxRenderer.color.b, Mathf.MoveTowards(boxRenderer.color.a, 0, fadeSpeed * Time.deltaTime));
        }

            
         
    }
}
