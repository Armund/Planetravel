using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Levers : MiniGame
{
	public Canvas canvas;
	public Sprite buttonUpSprite;
	public Sprite buttonDownSprite;

	//состояния
	bool isStarted;

	
	public Text[] numbersTop = new Text[6];
	public Text[] numbersBot = new Text[6];
	int[] valuesTop = new int[6];
	int[] valuesBot = new int[6];
	public Text goalTopText;
	public Text goalBotText;
	int goalTop;
	int goalBot;
	bool[] levers = new bool[6];

	public GameObject[] buttons = new GameObject[6];

	//без повторений
	int gameNumber;

	void Start()
    {
		isStarted = false;
		gameNumber = 0;
	}

	override public void Init() {
		if (!isStarted) {
			canvas.gameObject.SetActive(true);

			//int rand = Random.Range(0, 10);
			if (gameNumber > LeversPresets.presetsNumber) {
				gameNumber = 0;
			} else {
				gameNumber++;
			}
			int rand = gameNumber;
			rand = 0; //пока не сделаны пресеты

			for (int i = 0; i < 6; i++) {
				valuesTop[i] = LeversPresets.valuesTop[rand, i];
				valuesBot[i] = LeversPresets.valuesBot[rand, i];
			}
			goalTop = LeversPresets.goalsTop[rand];
			goalBot = LeversPresets.goalsBot[rand];

			for (int i = 0; i < 6; i++) {
				levers[i] = true;
				buttons[i].GetComponent<Image>().sprite = buttonUpSprite;
				numbersTop[i].text = valuesTop[i].ToString();
				numbersBot[i].text = valuesBot[i].ToString();
			}

			goalTopText.text = goalTop.ToString();
			goalBotText.text = goalBot.ToString();
			IsGameOver(); // 

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

	public void SwitchLever(int leverNumber) {
		levers[leverNumber] = !levers[leverNumber];
		if (levers[leverNumber]) {
			buttons[leverNumber].GetComponent<Image>().sprite = buttonUpSprite;
		} else {
			buttons[leverNumber].GetComponent<Image>().sprite = buttonDownSprite;
		}

		if (IsGameOver()) {
			goalTopText.text = "WIN";
			poi.SetEventDone();
		}
		//Debug.Log("Button " + leverNumber + " pressed");
	}

	private bool IsGameOver () {
		int resultTop = 0;
		int resultBot = 0;

		for (int i = 0; i < 6; i++) {
			if (levers[i]) {
				resultTop += valuesTop[i];
			} else {
				resultBot += valuesBot[i];
			}
		}

		if (resultTop == goalTop) {
			goalTopText.color = Color.green;
		} else {
			goalTopText.color = Color.red;
		}
		if (resultBot == goalBot) {
			goalBotText.color = Color.green;
		} else {
			goalBotText.color = Color.red;
		}

		return resultTop == goalTop && resultBot == goalBot;
	}
}
