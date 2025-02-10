using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titlescreen : MonoBehaviour
{

    public void GoToScene()
    {   
            SceneManager.LoadScene("StartMenu");
    }
}

