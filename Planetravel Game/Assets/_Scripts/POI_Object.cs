using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PoiStatus { Active, Disabled, Broken, Event, OnInteraction }
public class POI_Object : MonoBehaviour
{
    protected MeshRenderer MR;
    public string poiName;
    public PoiStatus status;
    public PoiStatus lastStatus;
    protected bool isElectrical;
    [SerializeField]
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
    public virtual void Tick()
    {
        if (timeBeforeNextEvent > 0) timeBeforeNextEvent -= Time.deltaTime;
        else NewStatus(PoiStatus.Event);
    }

    //Функция отключения для электрических точек интереса
    public void SwitchElectricity(bool trig)
    {
        if (!isElectrical || status == PoiStatus.Broken) return;

        if (trig && status == PoiStatus.Disabled) status = lastStatus;
        else if(!trig) NewStatus(PoiStatus.Disabled);
    }

    public virtual void Interacting()
    {
        return;
    }

    public void NewStatus(PoiStatus st)
    {
        lastStatus = status;
        status = st;
    }
}
