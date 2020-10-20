﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurbineControlPOI : POI_Object
{
    public GameObject TurbineFlames;
    public bool isLostAllFuel;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        poiName = "Tur";
        isElectrical = false;
        TurbineFlames.SetActive(true);
        lastStatus = PoiStatus.Active;
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

                }
                break;
            case PoiStatus.Event:
                {
                    isInteractable = true;
                    PoiEvent();

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
                    isInteractable = true;
                    ResetAfterEvent();
                }
                break;
        }

        NoFuel();
        GotFuelAgain();
    }

    public override void PoiEventEffect()
    {
        TurbineFlames.SetActive(false);
        repairCounter = 0;
        NewStatus(PoiStatus.Disabled);
        Sparkles.SetActive(false);
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
        else { NewStatus(PoiStatus.AfterEvent); EventDone = false; }
    }

    public override void ResetAfterEvent()
    {
        Sparkles.SetActive(false);
        TurbineFlames.SetActive(true);
        isInteractable = false;
        timeBeforeNextEvent = Random.Range(minTimeBeforeEvent, maxTImeBeforeEvent);
        GM.gm.DeleteActiveEvent();
        NewStatus(PoiStatus.Active);
        EventDone = false;
    }

    public void NoFuel()
    {
        if (GM.gm.PS.fuel <= 0 && !isLostAllFuel)
        {
            if (status == PoiStatus.Event || status == PoiStatus.Disabled) GM.gm.DeleteActiveEvent();
            NewStatus(PoiStatus.Disabled);
            TurbineFlames.SetActive(false);
            Sparkles.SetActive(false);
            isInteractable = false;
            isLostAllFuel = true;
            miniGame.isStarted = false;
            miniGame.Close();
        }
    }

    public void GotFuelAgain()
    {
        if(isLostAllFuel && GM.gm.PS.fuel >0)
        {
            TurbineFlames.SetActive(true);
            if (lastStatus == PoiStatus.Disabled)
            {
                isInteractable = true;
                TurbineFlames.SetActive(false);
            }
            else if(lastStatus == PoiStatus.OnInteraction)
            {
                isInteractable = true;
                Sparkles.SetActive(true);
                TurbineFlames.SetActive(false);
                GM.gm.AddActiveEvent();
                NewStatus(PoiStatus.Event);
                isLostAllFuel = false;
                return;
            }
            status = lastStatus;
            isLostAllFuel = false;
        }
    }
}
