using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using TMPro;
using UnityEngine;

public class IntroQuestScript : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private HealthManager healthManager;
    [SerializeField] private TextMeshProUGUI m_Object;
    private float counter = 0;
    private bool beginCount = false, firstText = false, secondText = false, thirdText = false, doorOpened = false;
    private void Start()
    {
        m_Object.text = "";
    }
    private void FixedUpdate()
    {
        if (beginCount) 
        {
            counter += Time.fixedDeltaTime;
        }

        if (counter > 1 && !firstText)
        {
            firstText = true;
            m_Object.text = "Hello.";
        }

        if (counter > 4 && !secondText)
        {
            secondText = true;
            m_Object.text += "\nGet 5 points.";
        }

        if (counter > 20 && !thirdText)
        {
            thirdText = true;
            m_Object.text += "\nGET 5 POINTS NOW.";
        }

        if (healthManager.GetPoints() >= 5 && !doorOpened)
        {
            doorOpened = true;
            door.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        beginCount = true;
    }
}
