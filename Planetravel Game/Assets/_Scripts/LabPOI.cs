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
    public AudioClip FillingUpBubble;
    public AudioClip GetFuelBulk;
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
        occupScale = new Vector3(0, ((0.248f - 0.001f) / (containerСapacity / occupancy)), 0);
        startScale = container.transform.localScale;
        isElectrical = true;
        EventDone = false;
        AS.clip = FillingUpBubble;
        AS.loop = true;
        lastStatus = PoiStatus.UnActive;
        status = PoiStatus.UnActive;
        isInteractable = true;
        AS = GetComponent<AudioSource>();

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
            case PoiStatus.Active:
                {
                    if (!AS.isPlaying)
                    {
                        AS.clip = FillingUpBubble; AS.Play();
                    }
                    fillingUP();
                }
                break;
            case PoiStatus.Disabled:
                {
                    if (AS.isPlaying) AS.Stop();
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
            curCapacity += occupancy * Time.deltaTime;
            container.transform.localScale += occupScale * Time.deltaTime;
        }
        else
        {
            isInteractable = true;
            fuelBattery.SetActive(true);
            container.transform.localScale = startScale;
            AS.Stop();
            curCapacity = 0;
            NewStatus(PoiStatus.Event);
        }
    }


    public override void MiniGameInteraction()
    {
        if (!EventDone) return;
        else
        {
            NewStatus(PoiStatus.Active);
            EventDone = false;
            AS.clip = FillingUpBubble;
            AS.loop = true;
            AS.Play();
            if (GM.gm.isTutorial)
            {
                GM.gm.NextState(10);
                GM.gm.PC.speedMove = 11;
            }
        }
    }

    public override void Interacting()
    {
        if (isInteractable)
        {
            

            if (status == PoiStatus.UnActive)
            {
                if (GM.gm.isTutorial)
                {
                    GM.gm.PC.speedMove = 0;
                    GM.gm.NextState(8);
                }
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
        AS.clip = GetFuelBulk;
        AS.loop = false;
        AS.Play();
        fuelBattery.SetActive(false);
        GM.gm.PC.GetItem(fuelBatteryPref, 1);
    }


}
