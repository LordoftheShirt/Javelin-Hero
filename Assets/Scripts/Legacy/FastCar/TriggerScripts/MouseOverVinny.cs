using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MouseOverVinny : MonoBehaviour
{
    [SerializeField] public GameObject countDownObject, checkVinnyTriggerObject;
    private CheckOnVinnyMission vinnyAudioScript;
    private TextMeshProUGUI textMeshProChangeNumber;
    private int countDown = 21;
    private int secondCreator = 0;
    public bool timerActivate = false;
    private bool voicelineCooldown = false;
    private void Start()
    {
        textMeshProChangeNumber = countDownObject.GetComponent<TextMeshProUGUI>();
        vinnyAudioScript = checkVinnyTriggerObject.GetComponent<CheckOnVinnyMission>();
    }
    private void FixedUpdate()
    {
        if (timerActivate)
        {
            secondCreator++;
            if (secondCreator == 50)
            {
                secondCreator = 0;
                countDown = countDown - 1;
                textMeshProChangeNumber.text = "" + countDown;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MousePointer"))
        {
            countDown = 20;
            textMeshProChangeNumber.text = "" + countDown;
            if (voicelineCooldown == false)
            {
                vinnyAudioScript.RandomVinny();
                voicelineCooldown = true;
                Invoke("VoicelineTimer", 1);
            }
        }
    }
    private void VoicelineTimer()
    {
        voicelineCooldown = false;
    }

}

