using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private PlayerMovement3D movementScript;
    private MousePosition mouseScript;
    [SerializeField] GameObject mouse, pickUpSlot, player;
    Rigidbody rb;
    [SerializeField] private float throwForce = 1;
    private Vector3 throwDirection;
    private SphereCollider sphereCollider;
    private bool isPickedUp = false;
    [SerializeField] public Color glow, originalColour;
    [SerializeField] public Material findMaterial;

    private void Start()
    {
        movementScript = player.GetComponent<PlayerMovement3D>();
        mouseScript = mouse.GetComponent<MousePosition>();
        rb = GetComponent<Rigidbody>();
        sphereCollider = rb.GetComponent<SphereCollider>();
    }

    private void Update()
    {
        if (isPickedUp)
        {
            transform.position = pickUpSlot.transform.position;
            throwDirection = movementScript.moveDirection;
            if (MousePosition.rightClick == true)
            {
                isPickedUp=false;
                sphereCollider.enabled=true;
                //gameObject.transform.SetParent(null);
                rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);
                Debug.Log("YEET!");
            }
        }

    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("MousePointer"))
        {
            findMaterial.color = glow;
            Debug.Log("MousePOINTERTAG");
            if (mouseScript.holdClick == true)    
            {
                Debug.Log("INTERACT CLICK");
                sphereCollider.enabled = false;
                transform.position = pickUpSlot.transform.position;
                // gameObject.transform.SetParent(pickUpSlot.transform);
                isPickedUp = true;
                findMaterial.color= originalColour;
            }
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("MousePointer")) findMaterial.color = originalColour;
    }
}
