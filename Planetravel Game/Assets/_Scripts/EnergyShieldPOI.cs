﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyShieldPOI : POI_Object
{
    public Material FuelMat;
    public Material genOffMat;
    public GameObject EnergySphere;
    public GameObject LeftShield;
    public AudioSource LAS;
    public GameObject RightShield;
    public AudioSource RAS;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        MR = EnergySphere.GetComponent<MeshRenderer>();
        MR.material = FuelMat;
        isElecNow = true;
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
                    LeftShield.SetActive(true);
                    RightShield.SetActive(true);
                    MR.material = FuelMat;
                }
                break;
            case PoiStatus.Disabled:
                {
                    isInteractable = (isElecNow) ? true : false;
                    RightShield.SetActive(false);
                    LeftShield.SetActive(false);
                    MR.material = genOffMat;
                    if (isElecNow)
                    {
                        WarningSignCanvas.gameObject.SetActive(true);
                    }else
                    {
                        WarningSignCanvas.gameObject.SetActive(false);
                    }
                }
                break;
            case PoiStatus.Event:
                {
                    WarningSignCanvas.gameObject.SetActive(true);
                    LeftShield.SetActive(true);
                    RightShield.SetActive(true);
                    MR.material = FuelMat;
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
        LAS.Play();
        RAS.Play();
        WarningSignCanvas.sprite = WarningBrokenSign;
        WarningSignCanvas.gameObject.SetActive(true);
        MR.material = genOffMat;

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

        WarningSignCanvas.gameObject.SetActive(false);
        WarningSignCanvas.sprite = WarningAttentionSign;
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
