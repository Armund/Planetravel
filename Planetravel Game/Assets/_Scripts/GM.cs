using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { Init, Tutorial, Start, Session, LoseState, WinState, End }
public class GM : MonoBehaviour
{
    public PlayerControl PC;
    public bool Electricity;
    public GameState state = GameState.Init;
    public static GM gm = null;
    public List<POI_Object> PointsOfInterests = new List<POI_Object>();
    public List<TurbineControlPOI> Turbines = new List<TurbineControlPOI>();
    public MainComPOI MC;
    public EnergyShieldPOI ES;
    public GameObject Meteorites;
    public PlanetShip PS;
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

      //  DontDestroyOnLoad(gameObject);
        
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
            case GameState.Session:
                {
                    if (PS.WinCondition()) state = GameState.WinState;
                    else if (PS.LoseCondition()) state = GameState.LoseState;
                }
                break;
            case GameState.WinState:
                {
                    SceneManager.LoadScene("WinScene", LoadSceneMode.Single);
                }
                break;
            case GameState.LoseState:
                {
                    SceneManager.LoadScene("LoseScene", LoadSceneMode.Single);
                }
                break;

        }
        ActiveTurbine();
    }

    public void ShipGetsDamage(int damage)
    {
        PS.HP -= damage;
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
            if (poi.poiName == "ES") ES = poi.GetComponent<EnergyShieldPOI>();
        }

        state = GameState.Session;
        
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

    public bool isGotFuel()
    {
        return PC.hasItem && PC.itemCode == 1;
    }

    public bool isGotFireEST()
    {
        return PC.hasItem && PC.itemCode == 0;
    }

    public void ReplenishFuel()
    {
        PS.fuel = PS.maxFuel;
    }

    public bool isMoreEventAvailable()
    {
        return ActiveEvents < maxOfEvents;
    }
}
