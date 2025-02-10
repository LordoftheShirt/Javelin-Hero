using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameCutscene : MonoBehaviour
{
    [SerializeField] private GameObject openDoor, closeDoor, creditsBackground, creditsCompleteBlack, textNames, titleScreenButton, santa, player;
    [SerializeField] private Rigidbody2D playerBody; 
    [SerializeField] AudioClip closeDoorSound, santaVoiceLine, ahFreeman;
    [SerializeField] SpriteRenderer santaSprite;


    private AudioSource sound;

    void Start()
    {
        santaSprite = santa.GetComponent<SpriteRenderer>();
        sound = GetComponent<AudioSource>();
        playerBody = player.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement.canMove = false;
            playerBody.bodyType = RigidbodyType2D.Static;
            Invoke("CloseDoor", 3);
        }
    }

    private void CloseDoor()
    {
        openDoor.SetActive(false);
        closeDoor.SetActive(true);
        sound.PlayOneShot(closeDoorSound);
        Invoke("AhFreeman", 1); 
    }

    private void AhFreeman()
    {
        sound.PlayOneShot(ahFreeman);
        Invoke("SantaFlip", 1);
    }

    private void SantaFlip()
    {
        santaSprite.flipX = false;
        Invoke("VoiceLine", 1);
    }

    private void VoiceLine()
    {
        sound.PlayOneShot(santaVoiceLine);
        Invoke("RollCredits", 1);
    }

    private void RollCredits()
    {
        creditsCompleteBlack.SetActive(true);
        creditsBackground.SetActive(true);
        Invoke("GameCreators", 1);
    }

    private void GameCreators()
    {
        textNames.SetActive(true);
        Invoke("ThanksForPlaying", 4);
    }

    private void ThanksForPlaying()
    {
        titleScreenButton.SetActive(true);
    }

}