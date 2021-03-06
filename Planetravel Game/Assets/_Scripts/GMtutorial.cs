﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GMtutorial : MonoBehaviour
{
    public ImageController img;
    public PlayerControl PC;
    public bool Electricity;
    public List<GameObject> LightSources;
    public TutorialState state = TutorialState.Init;
    public static GMtutorial gm = null;
    public List<POI_Object> PointsOfInterests = new List<POI_Object>();
    public List<TurbineControlPOI> Turbines = new List<TurbineControlPOI>();
    public GameObject TurbineUI;
    public GameObject mainTurbineFlame;
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
    public int ActiveTurbinesAmount;
    public bool ChangeState = true;
    public float waitForChange;
    public float waitForChangeSetter;
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
            case TutorialState.Init:
                {
                    Initialization();
                }
                break;
            case TutorialState.Intro:
                {
                    if (ChangeState && waitForChange < 0)
                    {
                        Time.timeScale = 0;
                        TurbineUI.SetActive(true);
                        ChangeState = false;
                    }
                    else if(ChangeState)
                    {
                        waitForChange -= Time.deltaTime;
                    }
                   /* if (PS.WinCondition()) state = GameState.WinState;
                    else if (PS.LoseCondition()) state = GameState.LoseState;
                    if (DistanceToPlanet <= 5000 && DistanceToPlanet > 4000 && ChangeDifficult)
                    {
                        
                        //Generator
                        G.InitSetter(40f, 50f, 25f);
                        //MainComputer (doesn't use third param)
                        MC.InitSetter(10000f, 10000f, 0f);
                        MC.timeBeforeNextEvent = Random.Range(MC.minTimeBeforeEvent, MC.maxTImeBeforeEvent);
                        //Turbine
                        foreach (TurbineControlPOI poi in Turbines)
                        {
                            poi.InitSetter(10f, 30f, 15f);
                        }
                        //EnergyShield
                        ES.InitSetter(30f, 45f, 20f);
                        //Fires
                        FM.InitSetter(90f, 1);


                    }
                    else if (DistanceToPlanet <= 4000 && DistanceToPlanet > 1500 && ChangeDifficult)
                    {
                        ChangeDifficult = false;
                        //Generator
                        G.InitSetter(30f, 40f, 20f);
                        //MainComputer (doesn't use third param)
                        MC.InitSetter(30f, 40f, 0f);
                        MC.timeBeforeNextEvent = Random.Range(MC.minTimeBeforeEvent, MC.maxTImeBeforeEvent);
                        //Turbine
                        foreach (TurbineControlPOI poi in Turbines)
                        {
                            poi.InitSetter(10f, 20f, 10f);
                        }
                        //EnergyShield
                        ES.InitSetter(20f, 30f, 15f);
                        //Fires
                        FM.InitSetter(60f, 1);

                    }
                    else if (DistanceToPlanet <= 1500 && DistanceToPlanet >= 0 && ChangeDifficult)
                    {
                        ChangeDifficult = false;
                        //Generator
                        G.InitSetter(20f, 30f, 15f);
                        //MainComputer (doesn't use third param)
                        MC.InitSetter(20f, 25f, 0f);
                        MC.timeBeforeNextEvent = Random.Range(MC.minTimeBeforeEvent, MC.maxTImeBeforeEvent);
                        //Turbine
                        foreach (TurbineControlPOI poi in Turbines)
                        {
                            poi.InitSetter(10f, 15f, 7f);
                        }
                        //EnergyShield
                        ES.InitSetter(15f, 20f, 10f);
                        //Fires
                        FM.InitSetter(40f, 2);

                    }*/
                }
                break;
            case TutorialState.TurbineStart:
                {

                }
                break;
          

        }
        ActiveTurbine();
        DifficultChanger();
    }

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
            poi.status = PoiStatus.UnActive;
            poi.lastStatus = PoiStatus.UnActive;
            poi.isInteractable = false;
        }
        ChangeState = true;
        waitForChangeSetter = 1f;
        waitForChange = waitForChangeSetter;
        state = TutorialState.Intro;

    }

    public void NextState(int st)
    {
        state = (TutorialState)st;       
    }

    public void ShipGetsDamage(float damage)
    {
        PS.HP -= damage;
    }

    public void AddActiveEvent()
    {
        ActiveEvents++;
    }

    public void AddActiveTurbine()
    {
        ActiveTurbinesAmount++;
    }

    public void DeleteActiveEvent()
    {
        ActiveEvents--;
    }

    public void DeleteActiveTurbine()
    {
        ActiveTurbinesAmount--;
    }

    public void DifficultChanger()
    {
        /*if (Difficult == 1)
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
        }*/
    }

    /* public void FindAllEvents()
     {
         foreach (POI_Object poi in PointsOfInterests)
         {
             if (poi.status == PoiStatus.Event) POIwithActiveEvents.Add(poi);
             else if (POIwithActiveEvents.Exists(p => p.poiName == poi.poiName)) POIwithActiveEvents.Remove(poi);
         }
     }*/
    

    private void ActiveTurbine()
    {
        ActiveTurbinesAmount = 0;
        foreach (TurbineControlPOI tur in Turbines)
        {
            if (tur.status != PoiStatus.Disabled) ActiveTurbinesAmount++;
        }
        if (ActiveTurbinesAmount > 0)
        {
            mainTurbineFlame.SetActive(true);
        }
        else
        {
            mainTurbineFlame.SetActive(false);
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
        foreach (GameObject temp in LightSources)
        {
            temp.SetActive(Electricity);
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
