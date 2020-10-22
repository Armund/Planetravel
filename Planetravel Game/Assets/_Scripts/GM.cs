using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { Init, Tutorial, Start, Session, LoseState, WinState, End }
public class GM : MonoBehaviour
{
    public ImageController img;
    public PlayerControl PC;
    public bool Electricity;
    public GameState state = GameState.Init;
    public static GM gm = null;
    public List<POI_Object> PointsOfInterests = new List<POI_Object>();
    public List<TurbineControlPOI> Turbines = new List<TurbineControlPOI>();
    public MainComPOI MC;
    public EnergyShieldPOI ES;
    public GenPOI G;
    public FireMaker FM;
    public GameObject Meteorites;
    public PlanetShip PS;
    public int DistanceToPlanet
    {
        get { return (int)(PS.parsecToDestination - PS.parsecFromStartPoint); }
    }
    public int ActiveEvents;
    public int ActiveTurbines;
    public bool ChangeDifficult = true;
    // public List<POI_Object> POIwithActiveEvents;
    public int maxOfEvents;
    public int Difficult;

    // Start is called before the first frame update
    void Start()
    {
        ActiveEvents = 0;
        if (gm == null)
        {
            gm = this;
        }
        else if (gm == this)
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
                    if (DistanceToPlanet <= 5000 && DistanceToPlanet > 4000 && ChangeDifficult)
                    {
                        ChangeDifficult = false;
                        //Generator
                        G.InitSetter(30f, 35f, 15f);
                        //MainComputer (doesn't use third param)
                        MC.InitSetter(10000f, 10000f, 0f);
                        MC.timeBeforeNextEvent = Random.Range(MC.minTimeBeforeEvent, MC.maxTImeBeforeEvent);
                        //Turbine
                        foreach (TurbineControlPOI poi in Turbines)
                        {
                            poi.InitSetter(10f, 15f, 10f);
                        }
                        //EnergyShield
                        ES.InitSetter(20f, 25f, 15f);
                        //Fires
                        FM.InitSetter(90f, 1);


                    }
                    else if (DistanceToPlanet <= 4000 && DistanceToPlanet > 1500 && ChangeDifficult)
                    {
                        ChangeDifficult = false;
                        //Generator
                        G.InitSetter(20f, 25f, 15f);
                        //MainComputer (doesn't use third param)
                        MC.InitSetter(20f, 25f, 0f);
                        MC.timeBeforeNextEvent = Random.Range(MC.minTimeBeforeEvent, MC.maxTImeBeforeEvent);
                        //Turbine
                        foreach (TurbineControlPOI poi in Turbines)
                        {
                            poi.InitSetter(10f, 15f, 5f);
                        }
                        //EnergyShield
                        ES.InitSetter(15f, 20f, 8f);
                        //Fires
                        FM.InitSetter(45f, 1);

                    }
                    else if (DistanceToPlanet <= 1500 && DistanceToPlanet >= 0 && ChangeDifficult)
                    {
                        ChangeDifficult = false;
                        //Generator
                        G.InitSetter(15f, 20f, 5f);
                        //MainComputer (doesn't use third param)
                        MC.InitSetter(10f, 15f, 0f);
                        MC.timeBeforeNextEvent = Random.Range(MC.minTimeBeforeEvent, MC.maxTImeBeforeEvent);
                        //Turbine
                        foreach (TurbineControlPOI poi in Turbines)
                        {
                            poi.InitSetter(10f, 15f, 5f);
                        }
                        //EnergyShield
                        ES.InitSetter(10f, 15f, 5f);
                        //Fires
                        FM.InitSetter(20f, 2);
                        
                    }
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
        DifficultChanger();
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

    public void DifficultChanger()
    {
        if (Difficult == 1)
        {
            if (DistanceToPlanet <= 4000)
            {
                ChangeDifficult = true;
                Difficult = 2;
            }
            return;
        }
        else if (Difficult == 2)
        {
            if (DistanceToPlanet <= 1500)
            {
                ChangeDifficult = true;
                Difficult = 3;
            }
            else if (DistanceToPlanet > 4000)
            {
                ChangeDifficult = true;
                Difficult = 1;
            }
            return;

        }
        else if (Difficult == 3)
        {
            if (DistanceToPlanet > 1500) ChangeDifficult = true;
            return;
        }
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
            if (poi.poiName == "Gen") G = poi.GetComponent<GenPOI>();
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
        foreach (POI_Object poi in PointsOfInterests)
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
