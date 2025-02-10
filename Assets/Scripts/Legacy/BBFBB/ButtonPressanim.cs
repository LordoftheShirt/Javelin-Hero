using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonPressanim : MonoBehaviour
{
public Button Text;
public Animator ani;
public Canvas yourcanvas;
 [SerializeField] private int levelChanger;
  [SerializeField] private GameObject dialoguebox, finishedText, contfinishedText, unfinishedText;
 
 
 
void Start () 
{
    Text = Text.GetComponent<Button> ();
    ani.enabled = false;
    yourcanvas.enabled = true;
}

 
public void Press() 
     
{
    Text.enabled = true;
    ani.enabled = true;
    dialoguebox.SetActive(false);
    finishedText.SetActive(false);
    unfinishedText.SetActive(false);
    Invoke("LoadNextLevel", 4f);


}

private void LoadNextLevel()
    {
        SceneManager.LoadScene(levelChanger);
    }
}