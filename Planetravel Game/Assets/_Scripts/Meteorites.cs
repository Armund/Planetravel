using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorites : MonoBehaviour
{
    public Transform[] Spawner = new Transform[2];
    public Transform[] Getter = new Transform[2];
    public GameObject meteoritePrefab;
    public GameObject curMeteorite;
    public AudioSource curAS;
    public bool Act;
    public float cooldownBetweenMeteorites;
    public float cooldownSetter;
    public int meteoriteDMG = 500;
    public int randomic
    {
        get { return Random.Range(0, 2); }
    }
    public int r;
    public float changer = 0;
    public ParticleSystem EXP;
    public ParticleSystem curEXP;
    public float timeForParticle;
    public bool isParticleCreated;
    // Start is called before the first frame update
    void Start()
    {
        cooldownBetweenMeteorites = cooldownSetter;
    }

    // Update is called once per frame
    void Update()
    {
        if(Act)
        {
            NextMeteorite();
            FallingOnShip();
            DeleteParticle();
        }
    }

    public void NextMeteorite()
    {
        if (cooldownBetweenMeteorites > 0) cooldownBetweenMeteorites -= Time.deltaTime;
        else
        {
            r = randomic;
            curMeteorite = Instantiate(meteoritePrefab, Spawner[r].position, Quaternion.identity);
            cooldownBetweenMeteorites = cooldownSetter;
        }
    }

    public void FallingOnShip()
    {
        if(curMeteorite != null)
        {
            curMeteorite.transform.position = Vector3.Lerp(Spawner[r].position, Getter[r].position, changer);
            changer += 0.12f * Time.deltaTime;
            
            if (changer >= 1)
            {
                
                GM.gm.ShipGetsDamage(meteoriteDMG);
                Kaboom();
                
            }
        }
    }

    
    public void Kaboom()
    {
        changer = 0;
        curEXP = Instantiate(EXP, curMeteorite.transform.position, Quaternion.identity);
        curEXP.Play();
        curAS = curEXP.GetComponent<AudioSource>();
        curAS.Play();
        isParticleCreated = true;
        timeForParticle = 1;
        Destroy(curMeteorite);
    }

    public void DeleteParticle()
    {
        if(isParticleCreated)
        {
            if (timeForParticle > 0) timeForParticle -= Time.deltaTime;
            else
            {
                Destroy(curEXP);
            }
        }
    }
}
