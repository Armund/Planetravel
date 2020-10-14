using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IsMatch : MiniGame
{
	public Canvas canvas;
	public Image[] pictures = new Image[5];

	public GameObject place1;
	public GameObject place2;

	public Text result;

	Image pictureToDelete;

	int[] imageValues = new int[2];

	// Start is called before the first frame update
	void Start()
    {
		//StartCoroutine(FirstPicture());
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
			Answer(false);
		}
		if (Input.GetKeyDown(KeyCode.E)) {
			Answer(true);
		}
	}
	
	new public void Init() {
		canvas.gameObject.SetActive(true);
		imageValues[0] = 0;
		imageValues[1] = 0;
		StartCoroutine(FirstPicture());
	}

	public void Answer(bool answer) {
		if (answer == (imageValues[0] == imageValues[1])) {
			Debug.Log("GOOD");
			result.text = "RIGHT";
			result.color = Color.green;
		} else {
			Debug.Log("BAD");
			result.text = "WRONG";
			result.color = Color.red;
		}
		NewPictures();
	}

	private void NewPictures() {
		Destroy(pictureToDelete.gameObject);
		imageValues[0] = imageValues[1];

		int rand = Random.Range(0, 4);
		imageValues[1] = rand;
		pictureToDelete = PlaceImage(pictures[rand], place2);
	}

	IEnumerator FirstPicture() {
		int rand = Random.Range(0,4);
		imageValues[0] = rand;
		pictureToDelete = PlaceImage(pictures[rand], place2);

		yield return new WaitForSeconds(1);

		PlaceImage(pictures[4], place1);
		rand = Random.Range(0, 4);
		imageValues[1] = rand;
		Destroy(pictureToDelete.gameObject);
		pictureToDelete = PlaceImage(pictures[rand], place2);
	}

	Image PlaceImage(Image image, GameObject place) {
		return Instantiate(image, place.transform.position, Quaternion.identity, canvas.transform);
	}
}
