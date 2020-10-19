using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public Meteorites M;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Meteor")
        {
            M.Kaboom();
        }
    }
}
