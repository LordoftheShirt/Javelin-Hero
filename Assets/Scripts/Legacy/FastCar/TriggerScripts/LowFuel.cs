using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowFuel : MonoBehaviour
{
    private Taskmaster taskmasterScript;
    [SerializeField] private GameObject taskMasterObject;
    private bool triggereredOnce = false;
    void FixedUpdate()
    {
        transform.Rotate(0, 2, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!triggereredOnce)
            {
                taskmasterScript = taskMasterObject.GetComponent<Taskmaster>();
                triggereredOnce = true;
                taskmasterScript.GasHeist();
            }
        }
    }
}
