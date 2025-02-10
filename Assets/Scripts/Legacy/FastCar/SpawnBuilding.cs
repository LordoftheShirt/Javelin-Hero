using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnBuilding : MonoBehaviour
{
    [SerializeField] private Vector3 offset = new Vector3(0, 1, 0);
    [SerializeField] private GameObject cursorObject, weinerButton, taskmasterObject;
    private MousePosition setBuildingOnCursor;
    private Taskmaster taskmasterScript;
    static public bool spawnCubeBeenPressed = false;
    private bool locationSelected = false;
    private bool rotateBuildingPhase = false;
    public bool WeinerPlaced = false;
    private GameObject selectedBuilding, placedBuilding;
    private float gatherXData;

    private AudioSource localSource;
    private void Start()
    {
        setBuildingOnCursor = cursorObject.GetComponent<MousePosition>();
        taskmasterScript = taskmasterObject.gameObject.GetComponent<Taskmaster>();
        localSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        cubePlacementInteraction();
    }
    public void SpawnCubeClick()
    {
        if (spawnCubeBeenPressed == false)
        {
            Debug.Log("Button press!");
            selectedBuilding = Instantiate(setBuildingOnCursor.spawnCube);
        }
        else
        {
            Debug.Log("A building is already selected!");
        }
        spawnCubeBeenPressed = true;
    }

    public void WeinerBreadSpawn()
    {
        selectedBuilding = Instantiate (setBuildingOnCursor.spawnWeinerBröd);
        spawnCubeBeenPressed = true;
        WeinerPlaced = true;
        weinerButton.SetActive(false);
    }

    public void cubePlacementInteraction()
    {
        if (spawnCubeBeenPressed == true)
        {
            if (locationSelected == false)
            {
                selectedBuilding.transform.position = cursorObject.transform.position + offset;

                selectedBuilding.transform.Rotate(0, 0.4f, 0);
                if (MousePosition.click)
                {
                    Debug.Log("Location Selected!");
                    locationSelected = true;
                }
            }
            if (locationSelected == true)
            {
                gatherXData = setBuildingOnCursor.screenPosition.x;
                selectedBuilding.transform.rotation = Quaternion.Euler(0, -gatherXData, 0);
                if (rotateBuildingPhase == true)
                {
                    if (MousePosition.click)
                    {
                        Debug.Log("Building placed!");
                        placedBuilding = Instantiate(selectedBuilding);
                        placedBuilding.layer = 0;
                        Destroy(selectedBuilding);
                        spawnCubeBeenPressed = false;
                        locationSelected = false;
                        rotateBuildingPhase = false;
                        localSource.Play();
                        if (WeinerPlaced)
                        {
                            WeinerPlaced = false;
                            taskmasterScript.PlaceWeinerCompleted();
                        }
                    }
                }
                else
                {
                    Debug.Log("ROTATE BUILDING NOW");
                    rotateBuildingPhase = true;
                }
            }
        }
    }
}
