﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Levers : MiniGame
{
    public Canvas can;
	public Sprite buttonUpSprite;
	public Sprite buttonDownSprite;

	//состояния
	//bool isStarted;
	bool isWin;

	public static int leversNumber = 4;
	
	public Text[] numbersTop = new Text[leversNumber];
	public Text[] numbersBot = new Text[leversNumber];
	int[] valuesTop = new int[leversNumber];
	int[] valuesBot = new int[leversNumber];
	public Text goalTopText;
	public Text goalBotText;
	int goalTop;
	int goalBot;
	bool[] levers = new bool[leversNumber];

	public GameObject[] buttons = new GameObject[leversNumber];

	//без повторений
	int gameNumber;

	void Start()
    {
		isStarted = false;
        isWin = false;
		gameNumber = 0;
		//Init();
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.J)) {
			//Init();
		}
		if (Input.GetKeyDown(KeyCode.K)) {
			//Close();
		}
	}

	override public void Init() {
		if (!isStarted) {
			can.gameObject.SetActive(true);

			//int rand = Random.Range(0, 10);
			if (gameNumber == LeversPresets.presetsNumber) {
				gameNumber = 0;
			} else {
				gameNumber++;
			}
			int rand = gameNumber;
			//rand = 0; //пока не сделаны пресеты

			for (int i = 0; i < leversNumber; i++) {
				valuesTop[i] = LeversPresets.valuesTop[rand, i];
				valuesBot[i] = LeversPresets.valuesBot[rand, i];
			}
			goalTop = LeversPresets.goalsTop[rand];
			goalBot = LeversPresets.goalsBot[rand];

			for (int i = 0; i < leversNumber; i++) {
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
        can.gameObject.SetActive(false);
        if (isStarted) {		
			if(!isWin) poi.EventLosing();
            isWin = false;
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
			goalTopText.text = "FIXED";
			goalBotText.text = "FIXED";
			if (poi != null) {
				poi.SetEventDone();
			}
			StartCoroutine(CloseCoroutine());
            isWin = true;
		}
		//Debug.Log("Button " + leverNumber + " pressed");
	}

	private bool IsGameOver () {
		int resultTop = 0;
		int resultBot = 0;

		for (int i = 0; i < leversNumber; i++) {
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
