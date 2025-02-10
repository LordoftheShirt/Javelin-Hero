using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject creditsPanel, weinerButton, creditsText;
    private Animation creditsPanelAnimation;
    private AudioSource localAudio;
    public AudioClip auStart, auCredits, auCredits2, auWeinerbröd, auExitgame;
    private bool creditsPanelMode = false;
    void Start()
    {
        localAudio = GetComponent<AudioSource>();
        creditsPanelAnimation = creditsPanel.GetComponent<Animation>();
    }
    public void PlayGame()
    {
        localAudio.PlayOneShot(auStart);
        Invoke("PlayDelay", 3);
    }
    private void PlayDelay()
    {
        SceneManager.LoadScene(1);
    }

    public void CreditsEnter()
    {
        if (!creditsPanelMode)
        {
            localAudio.PlayOneShot(auCredits);
            creditsPanel.SetActive(true);
            creditsPanelAnimation.Play("PanelComeForth");
            creditsPanelMode = true;
            Invoke("CreditsTextActivate", 2);
        }
        else
        {
            localAudio.PlayOneShot(auCredits2);
            creditsPanelAnimation.Play("PanelBegone");
            creditsPanelMode = false;
            Invoke("CreditsTextActivate", 2);
        }
    }
    private void CreditsTextActivate()
    {
        if (creditsPanelMode)
        {
            creditsText.SetActive(true);
        }
        else
        {
            creditsText.SetActive(false);
        }
    }
    public void Weinerbröd()
    {
        localAudio.PlayOneShot(auWeinerbröd);
        weinerButton.SetActive(false);
        Invoke("WienerbrodReturn", 5f);
    }
    private void WienerbrodReturn()
    {
        weinerButton.SetActive(true);
    }

    public void ExitGame()
    {
        localAudio.PlayOneShot(auExitgame);
        Invoke("ExitDelay", 3);
    }
    private void ExitDelay()
    {
        Application.Quit();
    }
}
