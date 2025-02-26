using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    [SerializeField] private bool checkVariables = false;

    [Header("Objects")]
    private static GameObject insertColliderFolder;
    private GameObject insertSpecialObjectsFolder;
    [SerializeField] private GameObject insertForExtraColorFolder;

    public ColorTheme[] colorThemes;

    public static SpriteRenderer[] walls;
    public static SpriteRenderer[] specialObjects;
    public static SpriteRenderer background;
    public static SpriteRenderer player;

    public static SpriteManager instance;
    private int numberOfColliderChildren = 0;

    void Awake()
    {
        insertSpecialObjectsFolder = GameObject.FindWithTag("SpecialObjects");
        insertColliderFolder = GameObject.FindWithTag("Colliders");
        background = GameObject.FindWithTag("Background").GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("PlayerSprite").GetComponent<SpriteRenderer>();

        //insertSpecialObjectsFolder = GameObject.FindWithTag("SpecialObjectsFolder");
        //insertForExtraColorFolder = GameObject.FindWithTag("ExtraColorFolder");

        walls = new SpriteRenderer[insertColliderFolder.transform.childCount];
        //print("Array length before for loop: " + walls.Length);

        // This for loop extends the length of the array by checking what number of objects have children with sprite renderers.
        for (int i = 0; walls.Length > i; i++)
        {
            if (insertColliderFolder.transform.GetChild(i).childCount > 0)
            {
                for (int y = 0; insertColliderFolder.transform.GetChild(i).childCount > y; y++)
                {
                    if (insertColliderFolder.transform.GetChild(i).GetChild(y).TryGetComponent<SpriteRenderer>(out SpriteRenderer checkAlpha))
                    {
                        if (checkAlpha.color.a > 1)
                        {
                            numberOfColliderChildren++;
                        }
                    }
                }


                //if there are children without spriteRenderers, this process will eventually create null spaces at the end of the array since it doesn't check (OUTDATED):
                //numberOfColliderChildren += insertColliderFolder.transform.GetChild(i).childCount;
            }

        }

        walls = new SpriteRenderer[walls.Length + numberOfColliderChildren];
        //print("Array length after for loop: " + walls.Length);


        // This for loop adds all objects that are children of the Collider folder. If an object doesn't have the spriteRender component, the for loop will then check if this object is a parent to other objects with spriteRenderers and an alpha above 0. In which case, it'll add those.
        for (int i = 0; insertColliderFolder.transform.childCount > i; i++)
        {
            if (insertColliderFolder.transform.GetChild(i).TryGetComponent<SpriteRenderer>( out SpriteRenderer spriteRenderer))
            {
                walls[i] = spriteRenderer;
                //print(i);
            }
            else if(insertColliderFolder.transform.GetChild(i).childCount > 0)
            {
                for (int y = 0; insertColliderFolder.transform.GetChild(i).childCount > y; y++)
                {
                    if (insertColliderFolder.transform.GetChild(i).GetChild(y).TryGetComponent<SpriteRenderer>(out SpriteRenderer checkAlpha))
                    {
                        if (checkAlpha.color.a > 0)
                        {
                            walls[i] = checkAlpha;
                            //print("child added to array spot: " + i);
                        }
                    }
                }
            }
        }

        specialObjects = new SpriteRenderer[insertSpecialObjectsFolder.transform.childCount];

        for (int i = 0; specialObjects.Length > i; i++)
        {
            specialObjects[i] = insertSpecialObjectsFolder.transform.GetChild(i).GetComponent<SpriteRenderer>();
        }
    }

    private void Update()
    {

        if (checkVariables) 
        {
            print("Walls index 0: " + walls[0]);
            print("Walls index Last: " + walls[walls.Length-1]);
            print("background: " + background);
            print("player: " + player);
            print("me, spriteManager: " + instance);
            print("");
            print("colorThemes index 0: " + colorThemes[0]);
            checkVariables = false;
        }
    }

}

