using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class IntroScene : MonoBehaviour
{
    [SerializeField] private GameObject babyPlayer1, babyPlayer2, babyPlayer3, coinTrap, birthParticle, prisonSuitCase, birdBabe, musicPlayer, playerStats, blackScreen, embarkButton;
    [SerializeField] private Transform babyLocation1, babyLocation2, babyLocation3;
    [SerializeField] private AudioClip coinSound, alienMusic, thinkingSound;
    [SerializeField] private Rigidbody2D babyBody1, babyBody2, babyBody3;


    private AudioSource sound;
    // Start is called before the first frame update
    void Start()
    {
        
        sound = GetComponent<AudioSource>();
        babyBody1 = babyPlayer1.GetComponent<Rigidbody2D>();
        babyBody2 = babyPlayer2.GetComponent<Rigidbody2D>();
        babyBody3 = babyPlayer3.GetComponent<Rigidbody2D>();
        Invoke("spawnBaby1", 5);
        Invoke("spawnBaby2", 10);
        Invoke("spawnBaby3", 15);
        Invoke("spawnTrap", 18);

        sound.Stop();
    }


    void spawnBaby1() { babyPlayer1.SetActive(true); Instantiate(birthParticle, babyPlayer1.transform.position, Quaternion.identity); }
    void spawnBaby2() { babyPlayer2.SetActive(true); Instantiate(birthParticle, babyPlayer2.transform.position, Quaternion.identity); }
    void spawnBaby3() { babyPlayer3.SetActive(true); Instantiate(birthParticle, babyPlayer3.transform.position, Quaternion.identity); }
    void spawnTrap() { coinTrap.SetActive(true); }

    public void setInMotion()
    {
        musicPlayer.SetActive(false);
        sound.PlayOneShot(thinkingSound);
        sound.PlayOneShot(coinSound, 0.5f);
        babyBody1.bodyType = RigidbodyType2D.Static;
        babyBody2.bodyType = RigidbodyType2D.Static;
        babyBody3.bodyType = RigidbodyType2D.Static;
        Invoke("babiesInPosition", 2f);
    }
    private void babiesInPosition()
    {
        sound.Play();
        //sound.PlayOneShot(alienMusic);

        babyPlayer1.gameObject.transform.position = babyLocation1.position;
        babyPlayer2.gameObject.transform.position = babyLocation2.position;
        babyPlayer3.gameObject.transform.position = babyLocation3.position;
        Invoke("spawnCage", 2);
    }

    private void spawnCage()
    {
        prisonSuitCase.SetActive(true);
        babyPlayer1.transform.SetParent(prisonSuitCase.transform);
        babyPlayer2.transform.SetParent(prisonSuitCase.transform);
        babyPlayer3.transform.SetParent(prisonSuitCase.transform);
        birdBabe.transform.SetParent(prisonSuitCase.transform);
        Invoke("activatePlayerHealth", 5f);
    }

    private void activatePlayerHealth()
    {
        playerStats.SetActive(true);
        Invoke("fadeToBlack", 1f);
        
    }

    private void fadeToBlack()
    {
        blackScreen.SetActive(true);
        Invoke("nextLevelButton", 1f);
    }

    private void nextLevelButton()
    {
        embarkButton.SetActive(true);
    }

    public void exitIntro()
    {
        sound.Stop();
        sound.PlayOneShot(coinSound);
        Invoke("loadScene", 2f);
    }

    private void loadScene()
    {
        SceneManager.LoadScene(3);
    }
}