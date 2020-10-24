using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { Init, Tutorial, Start, Session, LoseState, WinState, End }

public enum TutorialState
{
    Init, Tutorial, Start, Intro,
    TurbineStart, TurbineUI, TurbineEvent,
    LabStart, LabUI, LabEvent,
    EnShieldStart, EnShieldUI, EnShieldEvent,
    FuelStart, FuelEvent,
    MainCompStart, MainCompUI, MainCompEvent,
    FlameStockStart, FlameStockEvent,
    GenStart, GenUI, GenEvent,
    End
}
public class GM : MonoBehaviour
{
    public ImageController img;
    public PlayerControl PC;
    public bool Electricity;
    public List<GameObject> LightSources;
    public GameState state = GameState.Init;
    public bool isTutorial;
    public TutorialState Tstate;
    public static GM gm = null;
    public List<POI_Object> PointsOfInterests = new List<POI_Object>();
    public GameObject IntroUI;
    public List<TurbineControlPOI> Turbines = new List<TurbineControlPOI>();
    public GameObject TurbineUI;
    public LabPOI Lab;
    public GameObject FuelUI;
    public GameObject LabUI;
    public GameObject LabMiniUI;
    public GameObject mainTurbineFlame;
    public MainComPOI MC;
    public GameObject McUI;
    public GameObject McMiniUI;
    public EnergyShieldPOI ES;
    public GameObject EsUI;
    public GameObject EsMiniUI;
    public GenPOI G;
    public GameObject GenUI;
    public GameObject GenMiniUI;
    public GameObject FireStockUI;
    public GameObject EndUI;
    public FireMaker FM;
    public Meteorites meteorites;
    public PlanetShip PS;
    public Canvas mainCan;
    public int DistanceToPlanet
    {
        get { return (int)(PS.parsecToDestination - PS.parsecFromStartPoint); }
    }
    public int ActiveEvents;
    public int ActiveTurbinesAmount;
    public bool ChangeDifficult = true;
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
                        MC.InitSetter(25f, 30f, 0f);
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
            case GameState.Tutorial:
                {
                    switch (Tstate)
                    {
                        case TutorialState.Init:
                            {
                                ChangeState = true;
                                foreach (POI_Object poi in PointsOfInterests)
                                {
                                    poi.status = PoiStatus.UnActive;
                                    poi.lastStatus = PoiStatus.UnActive;
                                    poi.isInteractable = false;
                                }
                                PS.speedOfOneTurbine = 1f;
                                waitForChangeSetter = 0.1f;
                                meteorites.meteoriteDMG = 50;
                                FM.InitSetter(100000f, 1);
                                waitForChange = waitForChangeSetter;
                                Tstate = TutorialState.Intro;
                            }
                            break;
                        case TutorialState.Intro:
                            {

                                if (ChangeState && waitForChange < 0)
                                {
                                    Time.timeScale = 0;
                                    IntroUI.SetActive(true);
                                    ChangeState = false;
                                }
                                else if (ChangeState)
                                {
                                    waitForChange -= Time.deltaTime;
                                }
                            }
                            break;

                        case TutorialState.TurbineStart:
                            {
                                Time.timeScale = 1;
                                if (ChangeState && waitForChange < 0)
                                {

                                    IntroUI.SetActive(false);
                                    foreach (TurbineControlPOI tur in Turbines)
                                    {
                                        tur.timeBeforeNextEvent = 1f;
                                        tur.timeForRepair = 1000000f;
                                        tur.status = PoiStatus.Active;
                                    }
                                    ChangeState = false;
                                }
                                else if (ChangeState)
                                {
                                    waitForChange -= Time.deltaTime;
                                }
                            }
                            break;
                        case TutorialState.TurbineUI:
                            {

                                if (ChangeState && waitForChange < 0)
                                {
                                    mainCan.sortingOrder = 1;
                                    Time.timeScale = 0;
                                    TurbineUI.SetActive(true);
                                    ChangeState = false;
                                }
                                else if (ChangeState)
                                {
                                    waitForChange -= Time.deltaTime;
                                }
                            }
                            break;
                        case TutorialState.TurbineEvent:
                            {
                                Time.timeScale = 1;
                                if (ChangeState && waitForChange < 0)
                                {
                                    mainCan.sortingOrder = 0;
                                    TurbineUI.SetActive(false);
                                    PC.speedMove = 0;
                                    ChangeState = false;
                                }
                                else if (ChangeState)
                                {
                                    waitForChange -= Time.deltaTime;
                                }
                            }
                            break;
                        case TutorialState.LabStart:
                            {

                                if (ChangeState && waitForChange < 0)
                                {
                                    Time.timeScale = 0;
                                    mainCan.sortingOrder = 1;
                                    foreach (TurbineControlPOI tur in Turbines)
                                    {
                                        tur.status = PoiStatus.UnActive;
                                        tur.isInteractable = false;
                                    }
                                    LabUI.SetActive(true);
                                    PC.speedMove = 11;
                                    ChangeState = false;
                                }
                                else if (ChangeState)
                                {
                                    waitForChange -= Time.deltaTime;
                                }
                            }
                            break;
                        case TutorialState.LabEvent:
                            {
                                Time.timeScale = 1;
                                if (ChangeState && waitForChange < 0)
                                {
                                    mainCan.sortingOrder = 0;
                                    LabUI.SetActive(false);
                                    LabMiniUI.SetActive(false);
                                    ChangeState = false;
                                }
                                else if (ChangeState)
                                {
                                    waitForChange -= Time.deltaTime;
                                }
                            }
                            break;
                        case TutorialState.LabUI:
                            {
                                if (ChangeState && waitForChange < 0)
                                {
                                    mainCan.sortingOrder = 1;
                                    Time.timeScale = 0;
                                    LabMiniUI.SetActive(true);
                                    ChangeState = false;
                                }
                                else if (ChangeState)
                                {
                                    waitForChange -= Time.deltaTime;
                                }
                            }
                            break;
                        case TutorialState.EnShieldStart:
                            {
                                if (ChangeState && waitForChange < 0)
                                {
                                    Time.timeScale = 0;
                                    mainCan.sortingOrder = 1;
                                    LabMiniUI.SetActive(false);
                                    EsUI.SetActive(true);
                                    PC.speedMove = 11;
                                    ES.status = PoiStatus.Active;
                                    ES.timeBeforeNextEvent = 0f;
                                    ES.timeForRepair = 0;
                                    ChangeState = false;
                                }
                                else if (ChangeState)
                                {
                                    waitForChange -= Time.deltaTime;
                                }
                            }
                            break;
                        case TutorialState.EnShieldEvent:
                            {
                                Time.timeScale = 1;
                                if (ChangeState && waitForChange < 0)
                                {
                                    mainCan.sortingOrder = 0;
                                    EsUI.SetActive(false);
                                    EsMiniUI.SetActive(false);
                                    ChangeState = false;
                                }
                                else if (ChangeState)
                                {
                                    waitForChange -= Time.deltaTime;
                                }
                            }
                            break;
                        case TutorialState.EnShieldUI:
                            {
                                if (ChangeState && waitForChange < 0)
                                {
                                    mainCan.sortingOrder = 1;
                                    Time.timeScale = 0;
                                    EsMiniUI.SetActive(true);
                                    ChangeState = false;
                                }
                                else if (ChangeState)
                                {
                                    waitForChange -= Time.deltaTime;
                                }
                            }
                            break;
                        case TutorialState.FuelStart:
                            {
                                if (ChangeState && waitForChange < 0)
                                {
                                    Time.timeScale = 0;
                                    mainCan.sortingOrder = 1;
                                    FuelUI.SetActive(true);
                                    PC.speedMove = 11;
                                    ES.status = PoiStatus.UnActive;
                                    ES.timeBeforeNextEvent = 11110f;
                                    ES.timeForRepair = 11110;
                                    ChangeState = false;
                                }
                                else if (ChangeState)
                                {
                                    waitForChange -= Time.deltaTime;
                                }
                            }
                            break;
                        case TutorialState.FuelEvent:
                            {
                                Time.timeScale = 1;
                                if (ChangeState && waitForChange < 0)
                                {
                                    mainCan.sortingOrder = 0;
                                    FuelUI.SetActive(false);
                                    ChangeState = false;
                                }
                                else if (ChangeState)
                                {
                                    waitForChange -= Time.deltaTime;
                                }
                            }
                            break;
                        case TutorialState.MainCompStart:
                            {
                                if (ChangeState && waitForChange < 0)
                                {
                                    Time.timeScale = 0;
                                    mainCan.sortingOrder = 1;
                                    McUI.SetActive(true);
                                    PC.speedMove = 11;
                                    MC.status = PoiStatus.Active;
                                    MC.timeBeforeNextEvent = 0f;
                                    ChangeState = false;
                                }
                                else if (ChangeState)
                                {
                                    waitForChange -= Time.deltaTime;
                                }
                            }
                            break;
                        case TutorialState.MainCompEvent:
                            {

                                Time.timeScale = 1;
                                if (ChangeState && waitForChange < 0)
                                {
                                    mainCan.sortingOrder = 0;
                                    McUI.SetActive(false);
                                    McMiniUI.SetActive(false);
                                    ChangeState = false;
                                }
                                else if (ChangeState)
                                {
                                    waitForChange -= Time.deltaTime;
                                }
                            }
                            break;
                        case TutorialState.MainCompUI:
                            {

                                if (ChangeState && waitForChange < 0)
                                {
                                    Time.timeScale = 0;
                                    mainCan.sortingOrder = 1;
                                    McMiniUI.SetActive(true);
                                    ChangeState = false;
                                }
                                else if (ChangeState)
                                {
                                    waitForChange -= Time.deltaTime;
                                }
                            }
                            break;
                        case TutorialState.FlameStockStart:
                            {
                                if (ChangeState && waitForChange < 0)
                                {
                                    Time.timeScale = 0;
                                    mainCan.sortingOrder = 1;
                                    FireStockUI.SetActive(true);
                                    PC.speedMove = 11;
                                    MC.status = PoiStatus.UnActive;
                                    MC.timeBeforeNextEvent = 1000f;
                                    FM.cooldownBetweenFire = 0f;
                                    ChangeState = false;
                                }
                                else if (ChangeState)
                                {
                                    waitForChange -= Time.deltaTime;
                                }
                            }
                            break;
                        case TutorialState.FlameStockEvent:
                            {
                                Time.timeScale = 1;
                                if (ChangeState && waitForChange < 0)
                                {
                                    mainCan.sortingOrder = 0;
                                    FireStockUI.SetActive(false);
                                    ChangeState = false;
                                }
                                else if (ChangeState)
                                {
                                    waitForChange -= Time.deltaTime;
                                }
                            }
                            break;
                        case TutorialState.GenStart:
                            {
                                if (ChangeState && waitForChange < 0)
                                {
                                    Time.timeScale = 0;
                                    mainCan.sortingOrder = 1;
                                    GenUI.SetActive(true);
                                    PC.speedMove = 11;
                                    G.status = PoiStatus.Active;
                                    G.timeBeforeNextEvent = 0f;
                                    G.timeForRepair = 0f;
                                    ChangeState = false;
                                }
                                else if (ChangeState)
                                {
                                    waitForChange -= Time.deltaTime;
                                }
                            }
                            break;
                        case TutorialState.GenEvent:
                            {
                                Time.timeScale = 1;
                                if (ChangeState && waitForChange < 0)
                                {
                                    mainCan.sortingOrder = 0;
                                    GenUI.SetActive(false);
                                    GenMiniUI.SetActive(false);
                                    ChangeState = false;
                                }
                                else if (ChangeState)
                                {
                                    waitForChange -= Time.deltaTime;
                                }
                            }
                            break;
                        case TutorialState.GenUI:
                            {
                                if (ChangeState && waitForChange < 0)
                                {
                                    Time.timeScale = 0;
                                    mainCan.sortingOrder = 1;
                                    GenMiniUI.SetActive(true);
                                    ChangeState = false;
                                }
                                else if (ChangeState)
                                {
                                    waitForChange -= Time.deltaTime;
                                }
                            }
                            break;
                        case TutorialState.End:
                            {
                                if(ChangeState && waitForChange < 0)
                                {
                                    Time.timeScale = 0;
                                    mainCan.sortingOrder = 1;
                                    EndUI.SetActive(true);
                                    PC.speedMove = 11;
                                    G.status = PoiStatus.UnActive;
                                    G.timeBeforeNextEvent = 11110f;
                                    G.timeForRepair = 11110f;
                                    ChangeState = false;
                                }
                                else if (ChangeState)
                                {
                                    waitForChange -= Time.deltaTime;
                                }
                            }
                            break;
                    }
                }
                break;


        }
        ActiveTurbine();
        ActiveEventsAmount();
        DifficultChanger();
    }

    public void NextState(int st)
    {
        Tstate = (TutorialState)st;
        ChangeState = true;
        waitForChange = waitForChangeSetter;
    }

    public void EndTutorial()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SessionScene");
    }

    public void ShipGetsDamage(float damage)
    {
        PS.HP -= damage;
    }


    public void AddActiveTurbine()
    {
        ActiveTurbinesAmount++;
    }


    public void DeleteActiveTurbine()
    {
        ActiveTurbinesAmount--;
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

        if (isTutorial) state = GameState.Tutorial;
        else state = GameState.Session;

    }

    private void ActiveEventsAmount()
    {
        ActiveEvents = 0;
        foreach (POI_Object poi in PointsOfInterests)
        {
            if ((((poi.status == PoiStatus.Disabled && (poi.lastStatus == PoiStatus.Event || poi.lastStatus == PoiStatus.OnInteraction))||(poi.status == PoiStatus.OnInteraction)||(poi.status == PoiStatus.Event))&& poi.poiName != "Lab")) ActiveEvents++;
        }
    }
    private void ActiveTurbine()
    {
        ActiveTurbinesAmount = 0;
        foreach (TurbineControlPOI tur in Turbines)
        {
            if (tur.status != PoiStatus.Disabled && !(tur.status == PoiStatus.OnInteraction && tur.lastStatus == PoiStatus.Disabled)) ActiveTurbinesAmount++;
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
