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
    public List<POI_Object> POIwithActiveEvents;
    public int maxOfEvents;
    public int curAmountOfEvents;
    // Start is called before the first frame update
    void Start()
    {
        POIwithActiveEvents = new List<POI_Object>();
        if (gm == null)
        {
            gm = this;
        } else if (gm == this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        if(state == GameState.Init) Initialization();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case GameState.Init:
                {
                    
                }
                break;
            case GameState.Start:
                {
                    
                }
                break;
        
        }

    }

    
    public void AddActiveEvent(POI_Object poi)
    {
        POIwithActiveEvents.Add(poi);
    }

    public void DeleteActiveEvent(POI_Object poi)
    {
        if(POIwithActiveEvents.Exists(p => p.name == poi.name)) POIwithActiveEvents.Remove(poi);
    }

    public void FindAllEvents()
    {
        foreach (POI_Object poi in PointsOfInterests)
        {
            
        }
    }
    private void Initialization()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("POI");
        foreach (GameObject poi in temp)
        {
            PointsOfInterests.Add(poi.GetComponent<POI_Object>());        
        }
        
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
        return POIwithActiveEvents.Count < maxOfEvents;
    }
}
