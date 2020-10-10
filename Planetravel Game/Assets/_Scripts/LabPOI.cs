using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabPOI : POI_Object
{
    public float containerСapacity = 500;
    public float curCapacity = 0;
    private float limitCap = 100;
    public float occupancy = 2;
    public Vector3 occupScale;
    public GameObject container;
   
    // Start is called before the first frame update
    void Start()
    {
        poiName = "Lab";
        isElectrical = true;
        status = PoiStatus.Active;
        container = transform.Find("ConPivot").gameObject;
        occupScale = new Vector3(0,((1 - 0.01f) / (containerСapacity / occupancy)),0);
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
                    Destroy(container);
                }
                break;
            case PoiStatus.Disabled:
                {

                }
                break;
            case PoiStatus.Event:
                {

                }
                break;
        }

    }

    //Заполнения контейнера с топливом. Будучи заполненым, контейнер какое-то время еще будет заполнятся, пока не сломается.
    public void fillingUP()
    {

        if (curCapacity > containerСapacity + limitCap)
        {
            curCapacity = 0;
            status = PoiStatus.Broken;
        }else curCapacity += occupancy * Time.deltaTime;

        if (curCapacity < containerСapacity)
        {           
            container.transform.localScale += occupScale *Time.deltaTime;
        }
    }

  
}
