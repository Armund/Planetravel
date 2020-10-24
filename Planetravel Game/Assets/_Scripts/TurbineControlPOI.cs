using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurbineControlPOI : POI_Object
{
    public GameObject TurbineFlames;
    public AudioSource TAS;
    public AudioClip turbin;
    public AudioClip turbinEND;
    public Sprite WarningFuelSign;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        poiName = "Tur";
        TAS.loop = true;
        TAS.volume = 0.2f;
        TAS.Play();
        WarningSignCanvas.sprite = WarningAttentionSign;
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
                    
                        WarningSignCanvas.gameObject.SetActive(true);
                    
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
        TAS.clip = turbinEND;
        TAS.loop = false;
        TAS.volume = 0.6f;
        TAS.Play();
        
        NewStatus(PoiStatus.Disabled);
        WarningSignCanvas.sprite = WarningBrokenSign;
    }

    public override void Interacting()
    {
        if (isInteractable)
        {
            if(GM.gm.isTutorial)
            {
                GM.gm.NextState(5);
            }
            NewStatus(PoiStatus.OnInteraction);
            miniGame.Init();
        }
    }

    public override void MiniGameInteraction()
    {
        if (!EventDone) return;
        else { NewStatus(PoiStatus.AfterEvent); EventDone = false;  }
    }

    public override void ResetAfterEvent()
    {
        if (GM.gm.isTutorial && GM.gm.ActiveEvents == 1)
        {
            GM.gm.NextState(7);
        }
        else if(GM.gm.isTutorial && GM.gm.ActiveEvents == 2)
        {
            GM.gm.PC.speedMove = 11;
        }
        TAS.clip = turbin;
        TAS.loop = true;
        TAS.volume = 0.2f;
        TAS.Play();
        WarningSignCanvas.sprite = WarningAttentionSign;
        WarningSignCanvas.gameObject.SetActive(false);
        TurbineFlames.SetActive(true);
        isInteractable = false;
        timeBeforeNextEvent = Random.Range(minTimeBeforeEvent, maxTImeBeforeEvent);
        NewStatus(PoiStatus.Active);
        EventDone = false;
    }

    public void NoFuel()
    {
        if (GM.gm.PS.fuel <= 0 && !isLostAllFuel)
        {

            NewStatus(PoiStatus.Disabled);
            TurbineFlames.SetActive(false);
            TAS.clip = turbinEND;
            TAS.loop = false;
            TAS.volume = 0.6f;
            TAS.Play();
            WarningSignCanvas.sprite = WarningFuelSign;
            WarningSignCanvas.gameObject.SetActive(true);
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
            ResetAfterEvent();
            isLostAllFuel = false;
        }
    }
}
