using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windSound : MonoBehaviour
{
    private AudioSource windroar;
    void Start()
    {
        windroar = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            windroar.Play();
        }
    }
}
