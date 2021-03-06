﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame : MonoBehaviour
{
    [SerializeField]
	protected POI_Object poi;
	public bool isStarted = false;

	public void SetPoi(POI_Object poiObj) {
		poi = poiObj;
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	virtual public void Init() { }
	virtual public void Close() { }
	//virtual public bool IsGameOver() { return true; }
	public IEnumerator CloseCoroutine() {
		yield return new WaitForSeconds(0.5f);
		Close();
	}
}
