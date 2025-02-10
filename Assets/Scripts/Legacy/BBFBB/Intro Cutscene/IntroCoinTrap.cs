using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class IntroCoinTrap : MonoBehaviour
{
    [SerializeField] private IntroScene neededScript;
    [SerializeField] private GameObject coinEffect, coinItself;
    private DestroyAfterLifetime destroyMe;
    private void Start()
    {
        destroyMe = GetComponent<DestroyAfterLifetime>();
        neededScript = GameObject.FindGameObjectWithTag("cutScene").GetComponent<IntroScene>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("COIN COLLIDED");
        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(coinEffect, other.transform.position, Quaternion.identity);
            destroyMe.enabled = true;
            Debug.Log("WITH PLAYER");
            PlayerMovement.canMove = false;
            neededScript.setInMotion();
        }
    }

}
