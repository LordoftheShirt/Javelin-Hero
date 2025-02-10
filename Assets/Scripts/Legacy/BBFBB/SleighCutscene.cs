using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SleighCutscene : MonoBehaviour
{
    public GameObject Player;
    public Transform Sleigh;
     [SerializeField] private int levelChanger;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
          other.transform.parent = Sleigh;
         GetComponent<Animator>().SetTrigger("trigger");
           Invoke("LoadNextLevel", 3f);
        }
    }

     private void LoadNextLevel()
       {
        SceneManager.LoadScene(levelChanger);
       }
}
