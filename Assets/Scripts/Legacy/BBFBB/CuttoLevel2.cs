using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class CuttoLevel2 : MonoBehaviour
{
     [SerializeField] private int levelChanger;
      [SerializeField] private AudioClip coinSound;
       private AudioSource sound;

      private void Start()
    {
           Invoke("LoadNextLevel", 6f);

    }

    private void LoadNextLevel()
       {
        SceneManager.LoadScene(levelChanger);
       }
}

