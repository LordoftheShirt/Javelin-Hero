using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZoneWithCheckpoint : MonoBehaviour
{
    [SerializeField] private Transform respawn;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.transform.position = respawn.position;
            BlackBoxManager.turnBlack = true;
        }
    }
}
