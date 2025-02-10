using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MamaBear : MonoBehaviour
{
    [SerializeField] private GameObject dialoguebox, finishedText, contfinishedText, unfinishedText;
    [SerializeField] private int questGoal = 3;



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerMovement>().PorridgeCollected >= questGoal)
            {
                dialoguebox.SetActive(true);
                finishedText.SetActive(true);
               
            }
            else
            {
                dialoguebox.SetActive(true);
                unfinishedText.SetActive(true);
            }
        }
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dialoguebox.SetActive(false);
            finishedText.SetActive(false);
            unfinishedText.SetActive(false);
        }
    }

}
