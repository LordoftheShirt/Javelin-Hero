using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchSceneSimple : MonoBehaviour
{
    [SerializeField] private int waitTime, whichScene;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Invoke("SwitchScene", waitTime);
    }

    void SwitchScene()
    {
        SceneManager.LoadScene(whichScene);
    }
}
