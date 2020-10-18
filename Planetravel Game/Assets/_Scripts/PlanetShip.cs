using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetShip : MonoBehaviour
{
    public float maxHP;
    public float HP;
    public float maxFuel;
    public float fuel;
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
    }

    public void SetSpeed()
    {
        speed = speedMod * workingTurbines * speedOfOneTurbine;
    }

    public void Travel()
    {
        parsecFromStartPoint += speed * Time.deltaTime;
    }
}
