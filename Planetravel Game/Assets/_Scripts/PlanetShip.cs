using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetShip : MonoBehaviour
{
    public Scrollbar HPbar;
    public Scrollbar FuelBar;
    public Text PtoD;
    public ImageController img;
    public float maxHP;
    public float HP;
    public float maxFuel;
    public float fuel;
    public float fuelWastingSpeed;
    public float parsecToDestination;
    public float parsecFromStartPoint;
    public float speed;
    public float speedMod
    {
        get { return GM.gm.GetSpeedMod(); }
    }
    public float speedOfOneTurbine;
    public int workingTurbines
    {
        get { return GM.gm.ActiveTurbines; }
    }
    // Start is called before the first frame update
    void Start()
    {
        parsecFromStartPoint = 0;
        HP = maxHP;
        fuel = maxFuel;
        
    }

    // Update is called once per frame
    void Update()
    {
        SetSpeed();
        Travel();
        SetUI();
        imgCon();
    }

    public void SetSpeed()
    {
        speed = speedMod * workingTurbines * speedOfOneTurbine;
    }

    public void imgCon()
    {
        img.currentDistance = parsecToDestination - parsecFromStartPoint;
    }

    public void Travel()
    {
        parsecFromStartPoint += speed * Time.deltaTime;
        fuel -= workingTurbines * fuelWastingSpeed * Time.deltaTime;
        if (fuel <= 0.0001f) fuel = 0f;
    }

    public void SetUI()
    {
        HPbar.size = HP / maxHP;
        FuelBar.size = fuel / maxFuel;
        PtoD.text = "Km left to the destination:\n" + ((int)(parsecToDestination - parsecFromStartPoint)).ToString();
    }

    public bool WinCondition()
    {
        return (parsecToDestination - parsecFromStartPoint) <= 0;
    }

    public bool LoseCondition()
    {
        return (HP <= 0);
    }
}
