using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WorldColor : MonoBehaviour
{

    [SerializeField] private float colorChangeSpeed = 2f;

    [SerializeField] private int roomNumber;
    [SerializeField] string desiredColorTheme;

    private bool changeColor = false;

    private Color allWallsColor;
    private Color backgroundColor;
    private Color playerColor;
    private Color specialObject;

    private static int roomPriority;

    private static SpriteManager spriteManager;
    private ColorTheme myTheme;



    void Start()
    {
        spriteManager = GameObject.FindGameObjectWithTag("SpriteManager").GetComponent<SpriteManager>();
        myTheme = Array.Find(spriteManager.colorThemes, findTheme => findTheme.name == desiredColorTheme);

        if (myTheme == null)
        {
            Debug.LogWarning("Sound: " + desiredColorTheme + " not found!");
            return;
        }
        AssignColors();
    }

    void LateUpdate()
    {
        if (changeColor && roomPriority == roomNumber)
        {
            if (SpriteManager.walls[SpriteManager.walls.Length - 1].color == allWallsColor && SpriteManager.player.color == playerColor && SpriteManager.background.color == backgroundColor)
            {
                changeColor = false;
            }

            foreach (var sprite in SpriteManager.specialObjects)
            {
                if (sprite != null)
                    sprite.color = Color.LerpUnclamped(sprite.color, specialObject, colorChangeSpeed * Time.deltaTime);
            }

            foreach (var sprite in SpriteManager.walls)
            {
                if (sprite != null)
                    sprite.color = Color.LerpUnclamped(sprite.color, allWallsColor, colorChangeSpeed * Time.deltaTime);

            }

            SpriteManager.player.color = Color.LerpUnclamped(SpriteManager.player.color, playerColor, colorChangeSpeed * Time.deltaTime);
            SpriteManager.background.color = Color.LerpUnclamped(SpriteManager.background.color, backgroundColor, colorChangeSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            AssignColors();
            print("GO");
            changeColor = true;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            roomPriority = roomNumber;
            changeColor = true;
        }
    }

    private void AssignColors()
    {
        allWallsColor = myTheme.walls;
        backgroundColor = myTheme.background;
        playerColor = myTheme.player;
        specialObject = myTheme.specialObjects;
    }

}
