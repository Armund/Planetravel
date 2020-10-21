using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IsMatch : MiniGame
{
	public Canvas canvas;
	public Text progressText;
	int goal;
	int currentScore;
	public Image[] pictures = new Image[5];
    
	public GameObject place1;
	public GameObject place2;
	public GameObject place3;

	public Text result;

	Image pictureToDelete;
	Image pictureToDelete2;

    //состояния
    bool isWin;
    //bool isStarted;

    int[] imageValues = new int[2];

	// Start is called before the first frame update
	void Start()
    {
		//Init();
	}

    // Update is called once per frame
    void Update()
    {
		if (isStarted) {
			if (pictureToDelete != null) {
				pictureToDelete.gameObject.transform.position = 
					Vector3.MoveTowards(pictureToDelete.gameObject.transform.position, place2.transform.position, Time.deltaTime*10_000);
			}
			if (pictureToDelete2 != null) {
				pictureToDelete2.gameObject.transform.position = 
					Vector3.MoveTowards(pictureToDelete2.gameObject.transform.position, place1.transform.position, Time.deltaTime*10_000);
			}
		}

		/*
        if (Input.GetKeyDown(KeyCode.Q)) {
			Answer(false);
		}
		if (Input.GetKeyDown(KeyCode.E)) {
			Answer(true);
		}
		*/
	}

	override public void Init() {
		if (!isStarted) {
			canvas.gameObject.SetActive(true);
			progressText.color = Color.yellow;
			goal = 20;
			currentScore = 0;
			imageValues[0] = 0;
			imageValues[1] = 0;
			isStarted = true;
			RefreshProgressText();
			StartCoroutine(FirstPicture());
		}
	}

	IEnumerator FirstPicture() {
		int rand = Random.Range(0,4);
		imageValues[0] = rand;
		pictureToDelete = PlaceImage(pictures[rand], place3);

		yield return new WaitForSeconds(1);

		pictureToDelete2 = pictureToDelete;
		//pictureToDelete2 = PlaceImage(pictures[4], place1);
		rand = Random.Range(0, 4);
		imageValues[1] = rand;
		//Destroy(pictureToDelete.gameObject);
		pictureToDelete = PlaceImage(pictures[rand], place3);
	}

	override public void Close() {
        canvas.gameObject.SetActive(false);
        imageValues[0] = 0;
        imageValues[1] = 0;
        if (pictureToDelete != null)
        {
            Destroy(pictureToDelete.gameObject);
        }
        if (pictureToDelete2 != null)
        {
            Destroy(pictureToDelete2.gameObject);
        }
        if (isStarted) {

            if (!isWin) poi.EventLosing();
            isWin = false;
			isStarted = false;
		}
		//StartCoroutine(FirstPicture());
	}

	public void Answer(bool answer) {
		if (answer == (imageValues[0] == imageValues[1])) {
			currentScore += 1;
			//Debug.Log("GOOD");
			result.text = "RIGHT";
			result.color = Color.green;
		} else {
			currentScore -= 2;
			if (currentScore < 0) {
				currentScore = 0;
			}
			//Debug.Log("BAD");
			result.text = "WRONG";
			result.color = Color.red;
		}
		if (IsGameOver()) {
			progressText.color = Color.green;
			poi.SetEventDone();
            isWin = true;
		}
		RefreshProgressText();
		NewPictures();
	}

	private void NewPictures() {
		Destroy(pictureToDelete2.gameObject);
		imageValues[0] = imageValues[1];

		int rand = Random.Range(0, 4);
		imageValues[1] = rand;
		pictureToDelete2 = pictureToDelete;
		pictureToDelete = PlaceImage(pictures[rand], place3);
	}

	void RefreshProgressText() {
		progressText.text = "" + currentScore + " / " + goal;
	}

	bool IsGameOver() {
		return currentScore >= goal;
	}

	Image PlaceImage(Image image, GameObject place) {
		return Instantiate(image, place.transform.position, Quaternion.identity, canvas.transform);
	}
}
