using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Клас Генератора Электричества
public class GenPOI : POI_Object
{
    public Material genOnMat;
    public Material genOffMat;
    public GameObject Sparkles;
    public float timeForRepair;
    public float repairCounter = 0;
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
                    if(GM.gm.isMoreEventAvailable()) Tick();
                    MR.material = genOnMat;
                }
                break;
            case PoiStatus.Disabled:
                {
                    Interacting();
                }
                break;
            case PoiStatus.Event:
                {

                    RepairEvent();
                    Interacting();
                }
                break;
        }
    }

    public override void Tick()
    {

        if (timeBeforeNextEvent > 0) timeBeforeNextEvent -= Time.deltaTime;
        else 
        {
            isInteractable = true;
            Sparkles.SetActive(true);
            NewStatus(PoiStatus.Event);
        }
    }

    public void RepairEvent()
    {
        if (repairCounter < timeForRepair)
        {
            repairCounter += Time.deltaTime;
        }
        else
        {
            EGenerator(false);
            MR.material = genOffMat;
            NewStatus(PoiStatus.Disabled);
            Sparkles.SetActive(false);
        }
    }

    public override void Interacting()
    {
        if (Input.GetKeyDown(KeyCode.G) && isInteractable)
        {
            Sparkles.SetActive(false);
            isInteractable = false;
            repairCounter = 0;
            timeBeforeNextEvent = Random.Range(minTimeBeforeEvent, maxTImeBeforeEvent);
            EGenerator(true);
            NewStatus(PoiStatus.Active);
        }       
    }

    public void EGenerator(bool trig)
    {
        GM.gm.Electricity = trig;
        GM.gm.OnElectricity();
    }

}
