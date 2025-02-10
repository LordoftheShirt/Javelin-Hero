using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taskmaster : MonoBehaviour
{
    private MousePosition mousePositionScript;
    private MouseOverVinny mouseOnVinnyTimerScript;
    private CheckOnVinnyMission vinnyMissionScript;
    [SerializeField] private GameObject truck, ladder, roofPane ,mousePositionObject,lowfuel, submitButton, weinerButton, mouseOnVinnyObject, checkOnVinnyMissionObject, checkOnVinny, putOnMusic, sitDown, selecton1, holdConversation, doNotCrash, buildPyramid, buildIgloo, buildTower, placeWeinerBröd, selection2, checkWarningSigns, finale, gasHeist, selection3;
    private Animation currentAnimation;
    private bool pyramidPhase = false, iglooPhase = false, towerPhase = false;
    void Start()
    {
        vinnyMissionScript = checkOnVinnyMissionObject.GetComponent<CheckOnVinnyMission>();
        Invoke("CheckVinny", 2f);
        mouseOnVinnyTimerScript = mouseOnVinnyObject.GetComponent<MouseOverVinny>();
        mousePositionScript = mousePositionObject.GetComponent<MousePosition>();
    }
    
    private void CheckVinny()
    {
        checkOnVinny.SetActive(true);
    }
    public void CheckVinnyCompleted()
    {
        currentAnimation = checkOnVinny.gameObject.GetComponent<Animation>();
        currentAnimation.Play("CompletedTaskTextAnimation");
        vinnyMissionScript.TaskCompleteSound();
        Invoke("StartRadio", 2f);
    }
    private void StartRadio()
    {
        putOnMusic.SetActive(true);
    }
    public void StartRadioCompleted()
    {
        currentAnimation = putOnMusic.gameObject.GetComponent<Animation>();
        currentAnimation.Play("CompletedTaskTextAnimation");
        Invoke("SitWithVinny", 2f);
        vinnyMissionScript.TaskCompleteSound();
    }
    private void SitWithVinny()
    {
        sitDown.SetActive(true);
        vinnyMissionScript.activateSecondDialogue = true;
    }

    public void SitWithVinnyCompleted()
    {
        currentAnimation = sitDown.gameObject.GetComponent<Animation>();
        currentAnimation.Play("CompletedTaskTextAnimation");
        Invoke("QueueSection2", 2f);
        vinnyMissionScript.TaskCompleteSound();
    }
    private void QueueSection2()
    {
        currentAnimation = selecton1.gameObject.GetComponent<Animation>();
        currentAnimation.Play("TaskSelectionWipe");
        Invoke("HoldConversation", 3f);
    }
    private void HoldConversation()
    {
        holdConversation.SetActive(true);
        Invoke("DoNotCrashTask", 15f); 
        Invoke("BuildPyramid", 4f);
        mouseOnVinnyTimerScript.countDownObject.SetActive(true);
        mouseOnVinnyTimerScript.timerActivate = true;
        vinnyMissionScript.talkToVinny = true;
    }
    private void DoNotCrashTask()
    {
        doNotCrash.SetActive(true);
    }
    private void BuildPyramid()
    {
        buildPyramid.SetActive(true);
        pyramidPhase = true;
    }

    private void BuildIglooMethod()
    {
        buildIgloo.SetActive(true);
        iglooPhase = true;
    }
    private void BuildTowerPhaseMethod()
    {
        buildTower.SetActive(true);
        towerPhase = true;
    }
    private void PlaceWeinerMethod()
    {
        placeWeinerBröd.SetActive(true);
        weinerButton.SetActive(true);
    }
    public void Submitted()
    {
        if (pyramidPhase) 
        {
            currentAnimation = buildPyramid.gameObject.GetComponent<Animation>();
            currentAnimation.Play("CompletedTaskTextAnimation");
            pyramidPhase = false;
            Debug.Log("PYRAMID PHASE OVER!");
            submitButton.SetActive(false);
            Invoke("BuildIglooMethod", 3f);
            Invoke("SubmitButtonReactivate", 3f);
            vinnyMissionScript.TaskCompleteSound();

        }

        if (iglooPhase)
        {
            currentAnimation = buildIgloo.gameObject.GetComponent<Animation>();
            currentAnimation.Play("CompletedTaskTextAnimation");
            iglooPhase = false;
            Invoke("BuildTowerPhaseMethod", 3f);
            Invoke("SubmitButtonReactivate", 3f);
            vinnyMissionScript.TaskCompleteSound();
        }

        if (towerPhase)
        {
            currentAnimation = buildTower.gameObject.GetComponent<Animation>();
            currentAnimation.Play("CompletedTaskTextAnimation");
            towerPhase = false;
            Invoke("PlaceWeinerMethod", 3f);
            Invoke("SubmitButtonReactivate", 3f);
            vinnyMissionScript.TaskCompleteSound();
        }
    }

    private void SubmitButtonReactivate()
    {
        submitButton.SetActive(true);
    }

    public void PlaceWeinerCompleted()
    {
        currentAnimation = placeWeinerBröd.gameObject.GetComponent<Animation>();
        currentAnimation.Play("CompletedTaskTextAnimation");
        currentAnimation = finale.gameObject.GetComponent<Animation>();
        currentAnimation.Play("CompletedTaskTextAnimation");
        mousePositionScript.TurnOffMusic();
        vinnyMissionScript.TaskCompleteSound();
        Invoke("Selection2Queue", 2f);
    }
    private void Selection2Queue()
    {
        currentAnimation = selection2.gameObject.GetComponent<Animation>();
        currentAnimation.Play("TaskSelectionWipe");
        vinnyMissionScript.Warning();
        Invoke("CheckWarning", 2f);
    }
    private void CheckWarning()
    {
        finale.SetActive(true); 
        checkWarningSigns.SetActive(true);
        lowfuel.SetActive(true);
        roofPane.SetActive(false);
    }

    public void GasHeist()
    {
        gasHeist.SetActive(true);
        ladder.SetActive(true);
        truck.SetActive(true);
    }

    public void lastTouch ()
    {
        vinnyMissionScript.BloxLightsOnDelay();
    }
}
