using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class LabPOI : POI_Object
{
    public float containerСapacity = 500;
    public float curCapacity = 0;
    public float occupancy = 2;
    public AudioSource AS;
    public Vector3 occupScale;
    public Vector3 startScale;

    public GameObject container;
    public GameObject fuelBattery;
    public GameObject fuelBatteryPref;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        poiName = "Lab";
        isElectrical = true;
        EventDone = false;
        lastStatus = PoiStatus.UnActive;
        status = PoiStatus.UnActive;
        isInteractable = true;
        AS = GetComponent<AudioSource>();
        container = transform.Find("ConPivot").gameObject;
        occupScale = new Vector3(0,((1 - 0.01f) / (containerСapacity / occupancy)),0);
        startScale = container.transform.localScale;

    }

    // Update is called once per frame
    void Update()
    {

        switch(status)
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
            case PoiStatus.Active:
                {
                    fillingUP();
                }
                break;      
            case PoiStatus.Disabled:
                {
                    isInteractable = (lastStatus == PoiStatus.Event) ? true : false;
                }
                break;
            case PoiStatus.Event:
                {
                    isInteractable = true;              
                }
                break;
        }

       
    }

    //Заполнения контейнера с топливом. Будучи заполненым, контейнер какое-то время еще будет заполнятся, пока не сломается.
    public void fillingUP()
    {

        if (curCapacity < containerСapacity)
        {
            container.transform.localScale += occupScale * Time.deltaTime;
            curCapacity += occupancy * Time.deltaTime;
        }
        else
        {
            isInteractable = true;
            fuelBattery.SetActive(true);
            AS.Stop();
            container.transform.localScale = startScale;
            curCapacity = 0;
            NewStatus(PoiStatus.Event);
        }
    }


    public override void MiniGameInteraction()
    {
        if (!EventDone) return;
        else { NewStatus(PoiStatus.Active); EventDone = false; AS.Play(); }
    }

    public override void Interacting()
    {
        if(isInteractable)
        {
            if (status == PoiStatus.UnActive)
            {
                NewStatus(PoiStatus.OnInteraction);
                miniGame.Init();
                return;
                
            }
            else if (status == PoiStatus.Event)
            {
                GetFuel();
                isInteractable = true;
                NewStatus(PoiStatus.UnActive);
            }
            else if (status == PoiStatus.Disabled && lastStatus == PoiStatus.Event)
            {
                GetFuel();
                isInteractable = false;
                NewStatus(PoiStatus.UnActive);
                NewStatus(PoiStatus.Disabled);
            }
        }
    }

    public void GetFuel()
    {
        fuelBattery.SetActive(false);
        GM.gm.PC.GetItem(fuelBatteryPref, 1);
    }


}
