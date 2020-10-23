using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockPOI : POI_Object
{
    public AudioSource AS;
    public GameObject fireEstinPref;
    // Start is called before the first frame update
    protected override void Start()
    {
        AS = GetComponent<AudioSource>();
        base.Start();
        poiName = "Stock";
        isElectrical = true;
        EventDone = false;
        lastStatus = PoiStatus.UnActive;
        status = PoiStatus.UnActive;
        isInteractable = true;
    }

    // Update is called once per frame
    void Update()
    {
        switch (status)
        {
            case PoiStatus.UnActive:
                {
                    isInteractable = true;
                }
                break;
            case PoiStatus.OnInteraction:
                {
                    isInteractable = false;
                    MiniGameInteraction();
                }
                break;
            case PoiStatus.Disabled:
                {
                    isInteractable = false;
                }
                break;
            case PoiStatus.AfterEvent:
                {
                    ResetAfterEvent();
                }
                break;
        }
    }

    public override void MiniGameInteraction()
    {
        if (!EventDone) return;
        else { NewStatus(PoiStatus.AfterEvent); EventDone = false; }
    }

    public override void ResetAfterEvent()
    {
        isInteractable = true;
        AS.Play();
        NewStatus(PoiStatus.UnActive);
        GM.gm.PC.GetItem(fireEstinPref, 0);
        EventDone = false;
    }

    public override void Interacting()
    {
        if (isInteractable)
        {
            NewStatus(PoiStatus.OnInteraction);
            miniGame.Init();
        }
    }
}
