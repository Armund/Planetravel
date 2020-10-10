using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Клас Генератора Электричества
public class GenPOI : POI_Object
{
    private MeshRenderer MR;
    public Material genOnMat;
    public Material genOffMat;
    
    // Start is called before the first frame update
    void Start()
    {
        MR = GetComponent<MeshRenderer>();
        MR.material = genOnMat;
        poiName = "Generator";
        isElectrical = true;
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
                    Tick();
                    MR.material = genOnMat;
                }
                break;
            case PoiStatus.Broken:
                {
                    Destroy(gameObject);
                }
                break;
            case PoiStatus.Disabled:
                {
                    GM.inst.Electricity = false;
                    MR.material = genOffMat;
                }
                break;
            case PoiStatus.Event:
                {
                    status = PoiStatus.Disabled;
                }
                break;
        }
    }




}
