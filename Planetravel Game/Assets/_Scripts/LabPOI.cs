using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class LabPOI : POI_Object
{
    public float containerСapacity = 500;
    public float curCapacity = 0;
    private float limitCap = 100;
    public float occupancy = 2;
    public Vector3 occupScale;
    public Vector3 startScale;
    public GameObject container;
    public GameObject fuelBattery;
    public Animator anim;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        poiName = "Lab";
        isElectrical = true;
        status = PoiStatus.Active;
        container = transform.Find("ConPivot").gameObject;
        occupScale = new Vector3(0,((1 - 0.01f) / (containerСapacity / occupancy)),0);
        startScale = container.transform.localScale;
        limitCap = containerСapacity / 5;
    }

    // Update is called once per frame
    void Update()
    {

        switch(status)
        {
            case PoiStatus.Active:
                {
                    fillingUP();
                }
                break;
            case PoiStatus.Broken:
                {
                    
                }
                break;
            case PoiStatus.Disabled:
                {
                    anim.SetBool("Event", false);
                    isInteractable = false;
                }
                break;
            case PoiStatus.Event:
                {
                    Interacting();
                    DangerEvent();                  
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
            status = PoiStatus.Event;
        }
    }

    public void DangerEvent()
    {
        if(curCapacity < containerСapacity + limitCap)
        {
            anim.SetBool("Event",true);
            curCapacity += occupancy * Time.deltaTime;
        } 
        else
        {
            status = PoiStatus.Broken;
            Destroy(container);
            isInteractable = false;
        }
    }

    public override void Interacting()
    {
        if(Input.GetKeyDown(KeyCode.L) && isInteractable)
        {
            status = PoiStatus.Active;
            isInteractable = false;
            anim.SetBool("Event", false);
            container.transform.localScale = startScale;
            fuelBattery.SetActive(true);
            curCapacity = 0;
            return;
        }
    }


}
