using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Graph : MiniGame {
	public Canvas canvas;
	public Text winText;

	Node[] nodes = new Node[9];
	int[] values = new int[9];
	public GameObject[] buttons = new GameObject[9];

	public Sprite spriteYellow;

	Node currentCell;
	// Start is called before the first frame update
	void Start() {
		values[1] = 5;
		values[2] = 3;
		values[3] = 2;
		values[4] = 6;
		values[5] = 2;
		values[6] = 3;
		values[7] = 8;
		for (int i = 0; i < nodes.Length; i++) {
			nodes[i] = new Node {
				cellNumber = i
			};
			//nodes[i].availableCells = LabyrinthPresets.availableCells[rand, i];
			//buttons[i].GetComponent<Image>().sprite = spriteYellow;
			buttons[i].GetComponentInChildren<Text>().text = values[i].ToString();
		}
		nodes[0].availableCells = new List<int>() { 1, 2 };
		nodes[1].availableCells = new List<int>() { 4 };
		nodes[2].availableCells = new List<int>() { 3, 4, 5 };
		nodes[3].availableCells = new List<int>() { 6 };
		nodes[4].availableCells = new List<int>() { 8 };
		nodes[5].availableCells = new List<int>() { 7 };
		nodes[6].availableCells = new List<int>() { 8 };
		nodes[7].availableCells = new List<int>() { 8 };
		nodes[8].availableCells = new List<int>() { };
	}

	override public void Init() {
	}

	override public void Close() {
		if (isStarted) {
			canvas.gameObject.SetActive(false);
			poi.EventLosing();
			isStarted = false;
		}
	}

	public void ButtonPressed(int but) {

	}
}
