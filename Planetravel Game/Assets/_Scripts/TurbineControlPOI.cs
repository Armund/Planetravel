using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurbineControlPOI : POI_Object
{
    public GameObject TurbineFlames;
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

                    PoiEvent();

                }
                break;
            case PoiStatus.OnInteraction:
                {
                    //временное
                    SetEventDone();
                    MiniGameInteraction();
                }
                break;
            case PoiStatus.AfterEvent:
                {
                    ResetAfterEvent();
                }
                break;
        }

        Interacting();
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
        if (Input.GetKeyDown(KeyCode.T) && isInteractable)
        {
            NewStatus(PoiStatus.OnInteraction);
            //Вот тут вызов миниигры
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
}
