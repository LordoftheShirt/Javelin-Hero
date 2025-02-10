using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitIntro : MonoBehaviour
{
    public void exitIntro()
    {
        SceneManager.LoadScene(2);
    }
}
