using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldColor : MonoBehaviour
{
    [SerializeField] private bool changeColor = false;
    [Header("Speed")]
    [SerializeField] private float speed = 2f;
    [Header("Palettes")]
    [SerializeField] private Color allWallsColor;
    [SerializeField] private Color backgroundColor;
    [SerializeField] private Color playerColor;

    [SerializeField] private int roomNumber;
    private static int roomPriority;

    void Start()
    {
    }

    void LateUpdate()
    {

        if (changeColor && roomPriority == roomNumber)
        {
            if (SpriteManager.walls[SpriteManager.walls.Length - 1].color == allWallsColor && SpriteManager.player.color == playerColor && SpriteManager.background.color == backgroundColor)
            {
                changeColor = false;
            }


            foreach (var sprite in SpriteManager.walls)
            {
                if (sprite != null)
                    sprite.color = Color.LerpUnclamped(sprite.color, allWallsColor, speed * Time.deltaTime);

            }

            SpriteManager.player.color = Color.LerpUnclamped(SpriteManager.player.color, playerColor, speed * Time.deltaTime);
            SpriteManager.background.color = Color.LerpUnclamped(SpriteManager.background.color, backgroundColor, speed * Time.deltaTime);
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

}
