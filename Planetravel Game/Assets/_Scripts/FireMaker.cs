using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMaker : MonoBehaviour
{
    public FirePoi[] fires = new FirePoi[5];
    public float cooldownBetweenFire;
    public float cooldownSetter;
    public int randomic
    {
        get { return Random.Range(0, 5); }
    }
    public int r;
    public int maxFires;
    public int curFires;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GrogMakeFire();
    }

    public void GrogMakeFire()
    {
        if (cooldownBetweenFire > 0 && curFires < maxFires) cooldownBetweenFire -= Time.deltaTime;
        else if(curFires < maxFires)
        {
            do
            {
                r = randomic;
            } while (fires[r].gameObject.activeSelf);

            fires[r].gameObject.SetActive(true);
            curFires++;
            cooldownBetweenFire = cooldownSetter;
        }
        
    }

    public void DeleteFire()
    {
        curFires--;
    }
}
