using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable2D : MonoBehaviour
{
    public bool isInRange;
    public KeyCode interactKey;
    public UnityEvent interactAction;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange) // If we're in range to interact
        {
            if (Input.GetKeyDown(interactKey)) // And player presses key
            {
                interactAction.Invoke(); // Fire Event
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        if(otherObject.gameObject.CompareTag("Player"))
        {
            isInRange = true;
            Debug.Log("Player now IN range");
        }
    }

    private void OnTriggerExit2D(Collider2D otherObject)
    {
        if (otherObject.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            Debug.Log("Player now OUT OF range");
        }
    }
}
