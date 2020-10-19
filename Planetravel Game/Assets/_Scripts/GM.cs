using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { Init, Tutorial, Start, Session, LoseState, WinState, End }
public class GM : MonoBehaviour
{
    
    public bool Electricity;
    public GameState state = GameState.Init;
    public static GM gm = null;
    public List<POI_Object> PointsOfInterests = new List<POI_Object>();
    public List<TurbineControlPOI> Turbines = new List<TurbineControlPOI>();
    public MainComPOI MC;
    public int ActiveEvents;
    public int ActiveTurbines;
   // public List<POI_Object> POIwithActiveEvents;
    public int maxOfEvents;
    
    // Start is called before the first frame update
    void Start()
    {
        ActiveEvents = 0;
        if (gm == null)
        {
            gm = this;
        } else if (gm == this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case GameState.Init:
                {
                   Initialization();
                }
                break;
            case GameState.Start:
                {
                    
                }
                break;
            case GameState.Session:
                {
                    
                }
                break;
        
        }
        ActiveTurbine();
    }

    
    public void AddActiveEvent()
    {
        ActiveEvents++;
    }

    public void AddActiveTurbine()
    {
        ActiveTurbines++;
    }

    public void DeleteActiveEvent()
    {
        ActiveEvents--;
    }

    public void DeleteActiveTurbine()
    {
        ActiveTurbines--;
    }

   /* public void FindAllEvents()
    {
        foreach (POI_Object poi in PointsOfInterests)
        {
            if (poi.status == PoiStatus.Event) POIwithActiveEvents.Add(poi);
            else if (POIwithActiveEvents.Exists(p => p.poiName == poi.poiName)) POIwithActiveEvents.Remove(poi);
        }
    }*/
    private void Initialization()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("POI");
        foreach (GameObject poi in temp)
        {
            PointsOfInterests.Add(poi.GetComponent<POI_Object>()); 
            
        }
        foreach (POI_Object poi in PointsOfInterests)
        {
            if (poi.poiName == "Tur") Turbines.Add(poi.gameObject.GetComponent<TurbineControlPOI>());
            if (poi.poiName == "MC") MC = poi.GetComponent<MainComPOI>();
        }

        state = GameState.Start;
        
    }

    private void ActiveTurbine()
    {
        ActiveTurbines = 0;
        foreach (TurbineControlPOI tur in Turbines)
        {
            if (tur.status != PoiStatus.Disabled) ActiveTurbines++;
        }
    }

    public float GetSpeedMod()
    {
        return MC.speedMod;
    }

    //Отключение всех электро-ТИ
    public void OnElectricity()
    {
        foreach(POI_Object poi in PointsOfInterests)
        {
            poi.SwitchElectricity(Electricity);
        }
    }

    public bool isMoreEventAvailable()
    {
        return ActiveEvents < maxOfEvents;
    }
}
