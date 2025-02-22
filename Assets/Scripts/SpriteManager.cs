using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private GameObject insertColliderFolder;
    [SerializeField] private GameObject insertSpecialObjectsFolder;
    [SerializeField] private SpriteRenderer insertBackground;
    [SerializeField] private SpriteRenderer insertPlayer;
    [SerializeField] private GameObject insertForExtraColorFolder;

    public ColorTheme[] colorThemes;

    public static SpriteRenderer[] walls;
    public static SpriteRenderer background;
    public static SpriteRenderer player;

    public static SpriteManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        walls = new SpriteRenderer[insertColliderFolder.transform.childCount];

        for (int i = 0; walls.Length > i; i++)
        {
            walls[i] = insertColliderFolder.transform.GetChild(i).GetComponent<SpriteRenderer>();
        }
        background = insertBackground;
        player = insertPlayer;
    }
}
