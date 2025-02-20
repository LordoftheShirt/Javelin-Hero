using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableObject : MonoBehaviour
{
    [SerializeField] bool enable = false;
    [SerializeField] private GameObject[] changeStatus;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!enable)
            {
                foreach (GameObject disappear in changeStatus)
                {
                    disappear.SetActive(false);
                }
            }
            else
            {
                foreach (GameObject disappear in changeStatus)
                {
                    disappear.SetActive(true);
                }
            }
        }
    }
}
