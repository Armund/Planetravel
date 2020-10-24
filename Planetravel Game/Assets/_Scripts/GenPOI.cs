using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Клас Генератора Электричества
public class GenPOI : POI_Object
{
    public Material genOnMat;
    public Material genOffMat;
    public AudioSource AS;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        poiName = "Gen";
        AS = GetComponent<AudioSource>();
        isElectrical = false;
        WarningSignCanvas.sprite = WarningAttentionSign;
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
        EGenerator(false);
        repairCounter = 0;
        NewStatus(PoiStatus.Disabled);
        WarningSignCanvas.sprite = WarningBrokenSign;
    }

    public override void Interacting()
    {
        if (isInteractable)
        {
            if (GM.gm.isTutorial)
            {
                GM.gm.NextState(21);
                GM.gm.PC.speedMove = 0;
            }
            NewStatus(PoiStatus.OnInteraction);
			miniGame.Init();
            //Вот тут вызов миниигры
        }       
    }

    //Взаимодействие с миниигрой работает через функцию SetEventDone
    public override void MiniGameInteraction()
    {
        if (!EventDone) return;
        else { NewStatus(PoiStatus.AfterEvent); EventDone = false;  }
    }

    public override void ResetAfterEvent()
    {
        if (GM.gm.isTutorial)
        {
            GM.gm.NextState(23);
            GM.gm.PC.speedMove = 11;
        }
        WarningSignCanvas.sprite = WarningAttentionSign;
        WarningSignCanvas.gameObject.SetActive(false);
        isInteractable = false;
        timeBeforeNextEvent = Random.Range(minTimeBeforeEvent, maxTImeBeforeEvent);
        if (lastStatus == PoiStatus.Disabled)
        {
            AS.Play();
        }
        EGenerator(true);
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
