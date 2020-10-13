using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PoiStatus { Active, Disabled, Broken, Event, OnInteraction }
public class POI_Object : MonoBehaviour
{
    protected MeshRenderer MR;
    public string poiName;
    public PoiStatus status;
    protected bool isElectrical;
    protected bool isInteractable;
    public float timeBeforeNextEvent;
    public float minTimeBeforeEvent;
    public float maxTImeBeforeEvent;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        MR = GetComponent<MeshRenderer>();
        gameObject.tag = "POI";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Отсчет времени до следующего ивента
    public void Tick()
    {
        if (timeBeforeNextEvent > 0) timeBeforeNextEvent -= Time.deltaTime;
        else status = PoiStatus.Event;
    }

    //Функция отключения для электрических точек интереса
    public void SwitchElectricity(bool trig)
    {
        if (!isElectrical || status == PoiStatus.Broken) return;

        if (trig) status = PoiStatus.Active;
        else status = PoiStatus.Disabled;
    }

    public virtual void Interacting()
    {
        return;
    }
}
