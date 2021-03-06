﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Labyrinth : MiniGame {
	public Canvas canvas;
	public Text winText;
	public Image labyrinthImage;
	public Sprite[] labSprites = new Sprite[10];

	Node[] nodes = new Node[25];
	public GameObject[] buttons = new GameObject[25];
	Node currentCell;

	int startPoint;
	int finishPoint;

	public Sprite spriteBlue;
	bool isWin;
	List<int> currentPath = new List<int>();

	//LineRenderer lRend;
	public GameObject LineDrawn;
	public Image line;
	List<Image> lineDrawn;

	int gameNumber;

	void Start() {
		gameNumber = 0;
		//isStarted = false;
		gameNumber = 9;

		lineDrawn = new List<Image>();
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
			canvas.gameObject.SetActive(true);

			//int rand = Random.Range(0, 10);
			if (gameNumber == LeversPresets.presetsNumber) {
				gameNumber = 0;
			} else {
				gameNumber++;
			}
			int rand = gameNumber;
			//rand = 8; //пока не сделаны пресеты

			startPoint = LabyrinthPresets.startCells[rand];
			finishPoint = LabyrinthPresets.finishCells[rand];
			for (int i = 0; i < nodes.Length; i++) {
				nodes[i] = new Node {
					cellNumber = i
				};
				nodes[i].availableCells = LabyrinthPresets.availableCells[rand, i];
				buttons[i].GetComponent<Image>().sprite = spriteBlue;
			}
			labyrinthImage.sprite = labSprites[rand];

			currentCell = nodes[startPoint];
			currentPath.Clear();
			currentPath.Add(startPoint);

			//lineDrawn = new List<Image>();

			DrawLine();
			winText.text = "";

			isStarted = true;
		}
	}

	override public void Close() {
		canvas.gameObject.SetActive(false);
		if (isStarted) {
			if (!isWin && poi != null) {
				poi.EventLosing();
			}
			isStarted = false;
			isWin = false;
		}
	}

	public void ButtonPressed(int but) {
		if (currentPath.Contains(but)) {
			int del = currentPath.IndexOf(but) + 1;
			//del++;
			int num = currentPath.Count - del;
			if (del < currentPath.Count) {
				currentPath.RemoveRange(del, num);
			}
			currentCell = nodes[but];
			DrawLine();
		} else if (currentCell.availableCells.Contains(but)) {
			currentCell = nodes[but];
			currentPath.Add(but);
			DrawLine();
			//buttons[but]
			//buttons[but].GetComponent<Image>().sprite = spriteBlue;
		}

		if (IsGameOver()) {
			winText.text = "FIXED";
			if (poi != null) {
				poi.SetEventDone();
			}
			StartCoroutine(CloseCoroutine());
			isWin = true;
		}
	}

	private bool IsGameOver() {
		return currentCell.cellNumber == finishPoint;
	}

	private void DrawLine() {
		//lRend.positionCount = 0;
		//lRend.positionCount = currentPath.Count;
		foreach (Image img in lineDrawn) {
			Destroy(img.gameObject);
		}
		lineDrawn.Clear();

		for (int i = 1; i < currentPath.Count; i++) {
			//Instantiate(line, buttons[currentPath[i]].transform.position, Quaternion.FromToRotation());
			//lRend.SetPosition(i, buttons[currentPath[i]].transform.position);
			lineDrawn.Add(Instantiate(line, buttons[currentPath[i - 1]].transform.position, Quaternion.identity, LineDrawn.transform));
			int dif = currentPath[i] - currentPath[i - 1];
			if (dif == 5) {
				lineDrawn[lineDrawn.Count - 1].transform.Rotate(new Vector3(0, 0, 180));
			} else if (dif == 1) {
				lineDrawn[lineDrawn.Count - 1].transform.Rotate(new Vector3(0, 0, -90));
			} else if (dif == -1) {
				lineDrawn[lineDrawn.Count - 1].transform.Rotate(new Vector3(0, 0, 90));
			}
		}
	}

}
