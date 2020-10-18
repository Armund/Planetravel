using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Клас Генератора Электричества
public class GenPOI : POI_Object
{
    public Material genOnMat;
    public Material genOffMat;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        MR.material = genOnMat;
        poiName = "Generator";
        isElectrical = false;
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
                    if(GM.gm.isMoreEventAvailable()) TickToEvent();
                    
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
                    //SetEventDone();
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
        EGenerator(false);
        MR.material = genOffMat;
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
            //Вот тут вызов миниигры
        }       
    }

    //Взаимодействие с миниигрой работает через функцию SetEventDone
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
        EGenerator(true);
        MR.material = genOnMat;
        GM.gm.DeleteActiveEvent();
        NewStatus(PoiStatus.Active);
        EventDone = false;    
    }

    public void EGenerator(bool trig)
    {
        GM.gm.Electricity = trig;
        GM.gm.OnElectricity();
    }

}
