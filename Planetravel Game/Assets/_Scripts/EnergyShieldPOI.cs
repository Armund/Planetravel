﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyShieldPOI : POI_Object
{
    public Material FuelMat;
    public Material genOffMat;
    public GameObject EnergySphere;
    public GameObject LeftShield;
    public GameObject RightShield;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        MR = EnergySphere.GetComponent<MeshRenderer>();
        MR.material = FuelMat;
        poiName = "EShield";
        isElectrical = true;
        LeftShield.SetActive(true);
        RightShield.SetActive(true);
        lastStatus = PoiStatus.Active;
        status = PoiStatus.Active;
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
                    ResetAfterEvent();
                }
                break;
        }
    }

    public override void PoiEventEffect()
    {
        repairCounter = 0;
        NewStatus(PoiStatus.Disabled);
        RightShield.SetActive(false);
        LeftShield.SetActive(false);
        MR.material = genOffMat;
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
        isInteractable = false;
        timeBeforeNextEvent = Random.Range(minTimeBeforeEvent, maxTImeBeforeEvent);
        MR.material = FuelMat;
        GM.gm.DeleteActiveEvent();
        LeftShield.SetActive(true);
        RightShield.SetActive(true);
        NewStatus(PoiStatus.Active);
        EventDone = false;
    }
}