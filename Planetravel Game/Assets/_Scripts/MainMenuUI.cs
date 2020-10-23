using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public Button StartButton;
    public Button ExitButton;

	//public GameObject credits;
	public Image howToPlay;
	public Text credits;
	public Image omg;

	//Text HowToPlay;
	bool active = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartTheGame()
    {
        SceneManager.LoadScene("IntroScene", LoadSceneMode.Single);
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
		if (!active) {
			credits.gameObject.SetActive(true);
			howToPlay.gameObject.SetActive(true);
			omg.gameObject.SetActive(true);
			active = !active;
		} else {
			credits.gameObject.SetActive(false);
			howToPlay.gameObject.SetActive(false);
			omg.gameObject.SetActive(false);
			active = !active;
		}
	}
}
