using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    [SerializeField] private bool checkVariables = false;

    [Header("Objects")]
    private static GameObject insertColliderFolder;
    [SerializeField] private GameObject insertSpecialObjectsFolder;
    [SerializeField] private GameObject insertForExtraColorFolder;

    public ColorTheme[] colorThemes;

    public static SpriteRenderer[] walls;
    public static SpriteRenderer background;
    public static SpriteRenderer player;

    public static SpriteManager instance;

    void Awake()
    { 
        insertColliderFolder = GameObject.FindWithTag("Colliders");
        background = GameObject.FindWithTag("Background").GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("PlayerSprite").GetComponent<SpriteRenderer>();

        //insertSpecialObjectsFolder = GameObject.FindWithTag("SpecialObjectsFolder");
        //insertForExtraColorFolder = GameObject.FindWithTag("ExtraColorFolder");

        walls = new SpriteRenderer[insertColliderFolder.transform.childCount];

        for (int i = 0; walls.Length > i; i++)
        {
            walls[i] = insertColliderFolder.transform.GetChild(i).GetComponent<SpriteRenderer>();
        }
    }

    private void Update()
    {

        if (checkVariables) 
        {
            print("Walls index 0: " + walls[0]);
            print("background: " + background);
            print("player: " + player);
            print("me, spriteManager: " + instance);
            print("");
            print("colorThemes index 0: " + colorThemes[0]);
            checkVariables = false;
        }
    }

}

