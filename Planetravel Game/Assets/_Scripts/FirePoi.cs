using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePoi : POI_Object
{
    public FireMaker moma;
    public int dmg;
    public float cooldown;
    public float cooldownSetter;
    // Start is called before the first frame update
    protected override void Start()
    {
        gameObject.tag = "POI";
        isElectrical = false;
        isInteractable = true;
        cooldown = cooldownSetter;
        ItemUser = true;
      
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown > 0) cooldown -= Time.deltaTime;
        else
        {
            GM.gm.ShipGetsDamage(dmg);
            cooldown = cooldownSetter;
        }
        
    }

    public override void Interacting()
    {
        if (GM.gm.isGotFireEST()) { GM.gm.PC.UseItem(); moma.DeleteFire();  gameObject.SetActive(false); }
    }
}
