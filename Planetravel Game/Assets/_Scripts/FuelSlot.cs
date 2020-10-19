using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelSlot : POI_Object
{

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        poiName = "FuelSlot";
        isElectrical = false;
        lastStatus = PoiStatus.UnActive;
        status = PoiStatus.UnActive;
    }

    

    public override void Interacting()
    {
        if (GM.gm.isGotFuel()) { GM.gm.ReplenishFuel(); GM.gm.PC.UseItem(); }
    }
}
