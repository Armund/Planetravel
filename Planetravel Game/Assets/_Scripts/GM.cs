using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    public bool Electricity;
    public static GM inst = null;
    public List<POI_Object> PointsOfInterests = new List<POI_Object>();
    // Start is called before the first frame update
    void Start()
    {
        if (inst == null)
        {
            inst = this;
        } else if (inst == this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        Initialization();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Electricity) OnElectricityOff();
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
    private void OnElectricityOff()
    {
        foreach(POI_Object poi in PointsOfInterests)
        {
            poi.SwitchElectricity(false);
        }
    }
}
