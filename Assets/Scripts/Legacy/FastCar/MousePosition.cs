using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class MousePosition : MonoBehaviour
{
    [SerializeField] Taskmaster taskmasterScript;
    public GameObject spawnCube, spawnWeinerBröd, casette, taskmasterObject, radioWithInstruments;
    private GameObject radio;
    private AudioSource radioAccess;
    public Vector3 screenPosition;
    public Vector3 worldPosition;
    [SerializeField] private Material findMaterial;
    Plane plane = new Plane(Vector3.down, 0);
    static public bool click, rightClick;
    public bool holdClick;
    [SerializeField] public Color opaque, originalColour;

    // 2: Thése variables pair with method 2. Not in use.
    public LayerMask layerToHit;
    public Ray ray;
    void Update()
    {
        screenPosition = Input.mousePosition;
        click = Input.GetMouseButtonDown(0);
        rightClick = Input.GetMouseButtonDown(1);
        holdClick = Input.GetMouseButton(0);
        CursorLocationRecord();

        MouseButtonBeingHeld();

    }
    private void CursorLocationRecord()
    {


        ray = Camera.main.ScreenPointToRay(screenPosition);

        // 2: Sends a ray from Camera's NearClipPlane out into its cone. Records value wherever this ray collides with an object. Layermasks help ignore unwanted collisions with object X.  
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 100, layerToHit))
        {
            worldPosition = hitInfo.point;

            if (hitInfo.collider.CompareTag("PickUp"))
            {
                GameObject objectPickUp = hitInfo.collider.gameObject;
                //Debug.Log("CAN pickup");
            }
            if (hitInfo.collider.CompareTag("Interact"))
            {
                GameObject interact = hitInfo.collider.gameObject;
                //Debug.Log("CAN interact!");
            }
            if (hitInfo.collider.CompareTag("RADIO"))
            {
                radio = hitInfo.collider.gameObject;
                //Debug.Log("InsertMusic");
                if (casette != null)
                {
                    if (holdClick && casette.gameObject.TryGetComponent<InteractableCasette>(out InteractableCasette script))
                    {
                        if (script.isPickedUp)
                        {
                            radioAccess = radio.GetComponent<AudioSource>();
                            radioAccess.Play();
                            Destroy(casette);
                            taskmasterScript = taskmasterObject.GetComponent<Taskmaster>();

                            radioAccess = radioWithInstruments.GetComponent<AudioSource>();
                            radioAccess.Play();

                            taskmasterScript.StartRadioCompleted();
                            //Debug.Log("Play song");
                        }
                    }
                }
            }
        }
        transform.position = worldPosition;
    }

    private void MouseButtonBeingHeld()
    {
        if (holdClick)
        {
            findMaterial.color = opaque;
        }
        else
        {
            findMaterial.color = originalColour;
        }
    }

    public void TurnOffMusic()
    {
        radioAccess.Stop();

        radioAccess = radio.GetComponent<AudioSource>();
        radioAccess.Stop();
    }
}

