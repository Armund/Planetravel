using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public Button StartButton;
    public Button ExitButton;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartTheGame()
    {
        SceneManager.LoadScene("SessionScene", LoadSceneMode.Single);
    }

    public void ExitTheGame()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
