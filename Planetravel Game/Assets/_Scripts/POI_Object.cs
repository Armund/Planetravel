using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PoiStatus { UnActive, Active, Disabled, Event, OnInteraction, AfterEvent }
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
    public GameObject Sparkles;
    public bool EventDone;
    public float timeForRepair;
    public float repairCounter = 0;

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
    public void TickToEvent()
    {
        if (timeBeforeNextEvent > 0) timeBeforeNextEvent -= Time.deltaTime;
        else
        {
            isInteractable = true;
            Sparkles.SetActive(true);
            repairCounter = 0;
            GM.gm.AddActiveEvent();
            NewStatus(PoiStatus.Event);
        }
    }

    //Функция отключения для электрических точек интереса
    public void SwitchElectricity(bool trig)
    {
        if (!isElectrical) return;

        if (trig && status == PoiStatus.Disabled) status = lastStatus;
        else if(!trig) NewStatus(PoiStatus.Disabled);
    }

    public void PoiEvent()
    {
        if (repairCounter < timeForRepair)
        {
            repairCounter += Time.deltaTime;
        }
        else
        {
            PoiEventEffect();
        }
    }

    public virtual void PoiEventEffect()
    {
        return;
    }

    public virtual void Interacting()
    {
        return;
    }

    public virtual void ResetAfterEvent()
    {
        return;
    }

    public virtual void MiniGameInteraction()
    {
        return;
    }

    //Функция которую должна вызывать миниигра при прохождении миниигры
    public void SetEventDone()
    {
        EventDone = true;
    }

    //при проигрыше миниигры/ивента точка интереса возвращается из статуса (OnInteraction) в прошлый
    public void EventLosing()
    {
        status = lastStatus;
    }

    public void NewStatus(PoiStatus st)
    {
        lastStatus = status;
        status = st;
    }
}
