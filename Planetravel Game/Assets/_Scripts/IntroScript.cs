using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour
{
	public Image img1, img2, img3;
	int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void ButtonPressed() {
		if (i == 0) {
			img1.gameObject.SetActive(false);
			img2.gameObject.SetActive(true);
			i++;
		} else if (i == 1) {
			img2.gameObject.SetActive(false);
			img3.gameObject.SetActive(true);
			i++;
		} else if (i == 2) {
			SceneManager.LoadScene("SessionScene");
		}
	}
}
