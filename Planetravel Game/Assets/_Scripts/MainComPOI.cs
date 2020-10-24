using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainComPOI : POI_Object
{

    public float speedMod;
    private float directionMod;
    public float timeForBackwardDir = 120;
    public float changingDirStep;
    // Start is called before the first frame update
    protected override void Start()
    {

        base.Start();
        poiName = "MC";
        isElectrical = true;
        isElecNow = true;
        lastStatus = PoiStatus.Active;
        directionMod = 1; 
        speedMod = 1;
        changingDirStep = 2f / timeForBackwardDir;
        status = PoiStatus.Active;
        isInteractable = false;
        timeBeforeNextEvent = Random.Range(minTimeBeforeEvent, maxTImeBeforeEvent);
    }

    // Update is called once per frame
    void Update()
    {
        switch (status)
        {
            case PoiStatus.Active:
                {
                    if (GM.gm.isMoreEventAvailable()) TickToEvent();

                }
                break;
            case PoiStatus.Disabled:
                {
                    isInteractable = (isElecNow) ? true : false;
                    if (isElecNow)
                    {
                        WarningSignCanvas.gameObject.SetActive(true);
                    }
                    else
                    {
                        WarningSignCanvas.gameObject.SetActive(false);
                    }
                }
                break;
            case PoiStatus.Event:
                {
                    GM.gm.img.isDirectionToPlanet = false;
                    isInteractable = true;
                    ChangingWay();

                }
                break;
            case PoiStatus.OnInteraction:
                {
                    isInteractable = false;
                    MiniGameInteraction();
                }
                break;
            case PoiStatus.AfterEvent:
                {
                    ResetAfterEvent();
                }
                break;
        }

       
    }

    public void ChangingWay()
    {
        speedMod = Mathf.Lerp(-1, 1, directionMod);
        if (speedMod < 0) WarningSignCanvas.sprite = WarningBrokenSign;
        directionMod -= changingDirStep * Time.deltaTime;
    }


    public override void Interacting()
    {
        if (isInteractable)
        {

            if (GM.gm.isTutorial)
            {
                GM.gm.NextState(16);
                GM.gm.PC.speedMove = 0;
            }
            NewStatus(PoiStatus.OnInteraction);
            miniGame.Init();
        }
    }

    public override void MiniGameInteraction()
    {
        if (!EventDone) return;
        else
        {
            NewStatus(PoiStatus.AfterEvent);
            EventDone = false;
        }
    }

    public override void ResetAfterEvent()
    {
        if (GM.gm.isTutorial)
        {
            GM.gm.NextState(18);
            GM.gm.PC.speedMove = 11;
        }
        isInteractable = false;
        timeBeforeNextEvent = Random.Range(minTimeBeforeEvent, maxTImeBeforeEvent);
        GM.gm.DeleteActiveEvent();
        WarningSignCanvas.gameObject.SetActive(false);
        WarningSignCanvas.sprite = WarningAttentionSign;
        speedMod = 1;
        NewStatus(PoiStatus.Active);
        GM.gm.img.isDirectionToPlanet = true;
        EventDone = false;
    }
}
