using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckOnVinnyMission : MonoBehaviour
{
    [SerializeField] private GameObject lightsOn, vinny, taskmasterObject;
    public AudioClip  lightsOnSound,taskCompleteBoop,warningSign, theyCallMeX, secretAgent, talkToAboutCars, canGoWithThat, talkToMeBaby;
    [Header("VINNY RANDOM ARRAY")]
    public AudioClip[] randomVinny;
    [SerializeField] private float volume = 1f;

    bool doNotRepeat = false;
    public bool activateSecondDialogue = false;
    public bool talkToVinny = false;
    private Taskmaster taskmasterScript;
    AudioSource vinnyAudioSource;
    private void Start()
    {
        vinnyAudioSource = vinny.GetComponent<AudioSource>();
        taskmasterScript = taskmasterObject.GetComponent<Taskmaster>();
        TaskCompleteSound();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!doNotRepeat)
            {
                vinnyAudioSource.PlayOneShot(theyCallMeX, volume);
                doNotRepeat = true;
                Invoke("SecondVoiceLine", 1.7f);
            }
            if (activateSecondDialogue)
            {
                Invoke("FourthVoiceLine", 0f);
            }
        }
    }
    private void SecondVoiceLine()
    {
        vinnyAudioSource.PlayOneShot(secretAgent, volume);
        Invoke("ThirdVoiceLine", 1.7f);
    }
    private void ThirdVoiceLine()
    {
        vinnyAudioSource.PlayOneShot(talkToAboutCars, volume);
        taskmasterScript.CheckVinnyCompleted();
    }
    private void FourthVoiceLine()
    {
        activateSecondDialogue = false;
        vinnyAudioSource.PlayOneShot(canGoWithThat, volume);
        Invoke("FifthVoiceLine", 2f);
    }
    private void FifthVoiceLine()
    {
        vinnyAudioSource.PlayOneShot(talkToMeBaby, volume);
    }

    public void RandomVinny()
    {
        if (talkToVinny)
        {
            int randomValue = Random.Range(0, randomVinny.Length);
            vinnyAudioSource.PlayOneShot(randomVinny[randomValue], volume);
        }
    }

    public void Warning()
    {
        vinnyAudioSource.PlayOneShot(warningSign, volume);
    }

    public void TaskCompleteSound()
    {
        vinnyAudioSource.PlayOneShot(taskCompleteBoop, volume);
    }

    public void BloxLightsOnDelay()
    {
        Invoke("BloxLightsOn", 1.5f);
    }

    private void BloxLightsOn()
    {
        vinnyAudioSource.PlayOneShot(lightsOnSound, volume);
        lightsOn.SetActive(true);
    }
}
