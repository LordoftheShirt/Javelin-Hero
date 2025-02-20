using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPickUp : MonoBehaviour
{
    [SerializeField] private HealthManager healthManager;
    [SerializeField] private int healAmount = 1;
    [SerializeField] private int pointsGained = 5;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            healthManager.InputHealing(healAmount);
            healthManager.InputPoints(pointsGained);
        }
    }
}
