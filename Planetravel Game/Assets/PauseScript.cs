using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
	[SerializeField]
	public static bool pause = false;

	public Canvas pauseMenu;
	public Canvas HowToPlayWindow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
			PauseGame();
		}
    }

	public void PauseGame() {
		if (!pause) {
			Time.timeScale = 0;
			pause = true;
			pauseMenu.gameObject.SetActive(true);
		} else {
			Time.timeScale = 1;
			pause = false;
			pauseMenu.gameObject.SetActive(false);
			HowToPlayWindow.gameObject.SetActive(false);
		}
	}

	public void HowToPlay() {
		HowToPlayWindow.gameObject.SetActive(true);
		pauseMenu.gameObject.SetActive(false);
	}

	public void Back() {
		HowToPlayWindow.gameObject.SetActive(false);
		pauseMenu.gameObject.SetActive(true);
	}
}
