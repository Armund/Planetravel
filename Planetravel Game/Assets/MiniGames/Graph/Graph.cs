using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Graph : MiniGame {
	public Canvas canvas;
	public Text winText;
	public Text stateText;

	public GameObject[] buttons = new GameObject[9];

	//игровые значения
	Node[] nodes = new Node[9];
	int[] values = new int[9];
	int currentResult;
	int goal;

	//int startPoint = 0;
	int finishPoint;

	public Sprite spriteYellow;
	public Sprite spriteGreen;
	public Sprite spriteRed;
	public Image pathImg;
	public GameObject pathImages;
	List<Image> pathDrawn;

	List<int> currentPath = new List<int>();

	Node currentCell;

	int gameNumber;

	// Start is called before the first frame update
	void Start() {
		//Init();
		pathDrawn = new List<Image>();
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
			if (gameNumber > GraphPresets.presetsNumber) {
				gameNumber = 0;
			} else {
				gameNumber++;
			}
			int rand = gameNumber;
			rand = 0; //пока не сделаны пресеты

			goal = GraphPresets.goals[rand];
			finishPoint = 8;

			for (int i = 0; i < nodes.Length; i++) {
				nodes[i] = new Node {
					cellNumber = i
				};
				nodes[i].availableCells = GraphPresets.availableCells[rand, i];

				values[i] = GraphPresets.valuesInside[rand, i];
				if (i != 0 && i != 8) {
					buttons[i].GetComponentInChildren<Text>().text = values[i].ToString();
				} else {
					buttons[i].GetComponentInChildren<Text>().text = "";
				}
			}

			currentCell = nodes[0];
			winText.text = "";
			
			currentPath.Clear();
			currentPath.Add(0);

			//pathDrawn = new List<Image>();
			DrawLine();

			currentResult = 0;
			IsGameOver();

			isStarted = true;
		}
	}

	override public void Close() {
		if (isStarted) {
			canvas.gameObject.SetActive(false);
			if (poi != null) {
				poi.EventLosing();
			}
			isStarted = false;
		}
	}

	public void ButtonPressed(int but) {
		if (currentPath.Contains(but)) {
			int del = currentPath.IndexOf(but) + 1;
			int num = currentPath.Count - del;
			if (del < currentPath.Count) {
				currentPath.RemoveRange(del, num);
			}
			currentCell = nodes[but];

			currentResult = 0;
			foreach (int val in currentPath) {
				currentResult += values[val];
			}

			DrawLine();
		} else if (currentCell.availableCells.Contains(but)) {
			currentCell = nodes[but];
			currentPath.Add(but);
			//buttons[but].GetComponent<Image>().sprite = spriteGreen;
			currentResult += values[but];

			DrawLine();
		}

		if (IsGameOver()) {
			winText.text = "WIN";
		} else if (currentCell.cellNumber == finishPoint) {
			buttons[8].GetComponent<Image>().sprite = spriteRed;
		}
	}

	private void DrawLine() {
		//удаляем картинки пути
		foreach (Image img in pathDrawn) {
			Destroy(img.gameObject);
		}
		pathDrawn.Clear();

		//меняем цвет узлов
		foreach (GameObject but in buttons) {
			but.GetComponent<Image>().sprite = spriteYellow;
		}
		buttons[0].GetComponent<Image>().sprite = spriteGreen;

		for (int i = 1; i < currentPath.Count; i++) {
			buttons[currentPath[i]].GetComponent<Image>().sprite = spriteGreen;

			Vector3 pos = buttons[currentPath[i]].transform.position - (buttons[currentPath[i]].transform.position - buttons[currentPath[i - 1]].transform.position) / 2;
			pathDrawn.Add(Instantiate(pathImg, pos, Quaternion.identity, pathImages.transform));
			//поворачиваем
			pathDrawn[pathDrawn.Count - 1].transform.up = (buttons[currentPath[i]].transform.position - buttons[currentPath[i - 1]].transform.position).normalized;
		}
	}

	private bool IsGameOver() {
		stateText.text = "" + currentResult + " < " + goal;
		if (currentResult < goal) {
			stateText.color = Color.green;
		} else {
			stateText.color = Color.red;
		}
		return currentCell.cellNumber == finishPoint && currentResult < goal;
	}

	IEnumerator StartAgain() {
		yield return new WaitForSeconds(2);

	}
}
