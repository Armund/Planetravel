﻿using System.Collections;
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
        directionMod -= changingDirStep * Time.deltaTime;
    }


    public override void Interacting()
    {
        if (isInteractable)
        {
            NewStatus(PoiStatus.OnInteraction);
            miniGame.Init();
        }
    }

    public override void MiniGameInteraction()
    {
        if (!EventDone) return;
        else { NewStatus(PoiStatus.AfterEvent); EventDone = false; WarningSign.SetActive(false); }
    }

    public override void ResetAfterEvent()
    {
        isInteractable = false;
        timeBeforeNextEvent = Random.Range(minTimeBeforeEvent, maxTImeBeforeEvent);
        GM.gm.DeleteActiveEvent();
        WarningSign.SetActive(false);
        speedMod = 1;
        NewStatus(PoiStatus.Active);
        GM.gm.img.isDirectionToPlanet = true;
        EventDone = false;
    }
}
