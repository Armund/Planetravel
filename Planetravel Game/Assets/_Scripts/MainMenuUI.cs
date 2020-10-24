using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public Button StartButton;
    public Button ExitButton;
	public GameObject mainMenu;
	public GameObject TutorialQuestion;

	public static bool playTutorial = false;

	//public GameObject credits;
	public Image howToPlay;
	public Image credits;

	//Text HowToPlay;
	//bool active = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartTheGame()
    {
        SceneManager.LoadScene("IntroScene", LoadSceneMode.Single);
    }

	public void Play() {
		TutorialQuestion.SetActive(true);
	}

	public void ExitTheGame()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

	public void SetActiveCredits () {
		credits.gameObject.SetActive(true);
		mainMenu.SetActive(false);
	}

	public void SetActiveHowToPlay() {
		howToPlay.gameObject.SetActive(true);
		mainMenu.SetActive(false);
	}

	public void Back() {
		mainMenu.SetActive(true);
		credits.gameObject.SetActive(false);
		howToPlay.gameObject.SetActive(false);
	}

	public void PlayTutorial(bool value) {
		playTutorial = value;
		SceneManager.LoadScene("IntroScene", LoadSceneMode.Single);
	}

}
