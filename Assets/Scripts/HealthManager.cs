using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class HealthManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_Object;
    [SerializeField] private GameObject[] healthIndicatorObjects;
    private int maxHealth;
    private int healthCounter;
    private int pointsCollected;

    void Start()
    {
        pointsCollected = 0;
        m_Object.text = "Points: \n " + pointsCollected;
        maxHealth = healthIndicatorObjects.Length;
        healthCounter = maxHealth;;
    }

    public void InputDamage(int damage)
    {
        // imagine 3 health as max.
        healthCounter -= damage;

        if (healthCounter < 0)
        {
            healthCounter = 0;
        }

        // starts from the highest value in the heart display array and counts down the number of hearts which should turn off.
        for (int i = maxHealth; healthCounter < i; i--)
        {
            healthIndicatorObjects[i-1].SetActive(false);
        }

        if (healthCounter == 0)
        {
            // death happens. Resets health:
            InputHealing(999);
        }
    }

    public void InputHealing(int healingAmount)
    {
        // dont put in negative values, dude. I'll deal with you later.
        healthCounter += healingAmount;
        if (healthCounter > maxHealth)
        {
            healthCounter = maxHealth;
        }
        // starts from the lowest value in the array, turns on the already on, then goes further.
        for (int i = 0; healthCounter != i; i++)
        {
            healthIndicatorObjects[i].SetActive(true);
        }
    }

    public void InputPoints(int points) 
    {
        pointsCollected += points;
        m_Object.text = "Points: \n " + pointsCollected;
    }

    public int GetPoints()
    {
        return pointsCollected;
    }
}
