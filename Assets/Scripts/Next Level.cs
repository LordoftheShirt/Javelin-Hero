using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField] private int goToLevel = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(goToLevel);
    }
}
