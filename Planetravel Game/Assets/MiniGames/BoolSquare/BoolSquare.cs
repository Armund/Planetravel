using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoolSquare : MiniGame
{
	public Canvas canvas;
	public Text winText;

	public Image[] lightImages = new Image[6];
	public Sprite redLight;
	public Sprite greenLight;

	//игровые значения
	bool[] lightValues = new bool[6];

	// Start is called before the first frame update
	void Start()
    {
		Init();
    }

	public override void Init() {
		if (!isStarted) {
			canvas.gameObject.SetActive(true);

			winText.text = "";

			for (int i = 0; i < 6; i++) {
				lightValues[i] = true;
				lightImages[i].sprite = greenLight;
			}
			ButtonPressed(0);

			//генерируем сетап
			while (lightValues[3] & lightValues[4] & lightValues[5]) {
				for (int i = 1; i < 4; i++) {
					int rand = Random.Range(0, 3);
					if (rand > 0) {
						ButtonPressed(i);
					}
				}
			}

			isStarted = true;
		}
	}

	override public void Close() {
		if (isStarted) {
			canvas.gameObject.SetActive(false);
			poi.EventLosing();
			isStarted = false;
		}
	}

	public void ButtonPressed(int but) {
		switch (but) {
			case 0:
				SwitchLight(0);
				SwitchLight(1);
				SwitchLight(2);
				break;
			case 1:
				SwitchLight(0);
				SwitchLight(3);
				break;
			case 2:
				SwitchLight(1);
				SwitchLight(4);
				break;
			case 3:
				SwitchLight(2);
				SwitchLight(5);
				break;
		}
		if (IsGameOver()) {
			winText.text = "WIN";
			winText.color = Color.green;
		}
	}

	void SwitchLight(int val) {
		lightValues[val] = !lightValues[val];
		if (lightValues[val]) {
			lightImages[val].sprite = greenLight;
		} else {
			lightImages[val].sprite = redLight;
		}
	}

	bool IsGameOver() {
		foreach (bool val in lightValues) {
			if (!val) {
				return false;
			}
		}
		return true;
	}
}
