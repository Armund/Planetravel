﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void EventHandle(int value) {
		if (value == 0) {
			SceneManager.LoadScene("SessionScene", LoadSceneMode.Single);
		}
		if (value == 1) {
			SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);
		}
	}
}