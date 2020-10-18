using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Labyrinth : MiniGame
{
	public Canvas canvas;

	Node[] nodes = new Node[25];
	public GameObject[] buttons = new GameObject[25];
	Node currentCell;

	int startPoint;
	int finishPoint;

	public Sprite spriteBlue;

	List<int> currentPath = new List<int>();

	//LineRenderer lRend;
	public GameObject LineDrawn;
	public Image line;
	List<Image> lineDrawn;

    // Start is called before the first frame update
    void Start()
    {
		startPoint = 0;
		finishPoint = 22;
		lineDrawn = new List<Image>();
		//lRend = GetComponent<LineRenderer>();
		for (int i = 0; i < nodes.Length; i++) {
			nodes[i] = new Node {
				cellNumber = i
			};
		}
		nodes[0].availableCells = new List<int> { 1 };
		nodes[1].availableCells = new List<int> { 6 };
		nodes[2].availableCells = new List<int> { 3, 7 };
		nodes[3].availableCells = new List<int> { 8 };
		nodes[4].availableCells = new List<int> { };
		nodes[5].availableCells = new List<int> { };
		nodes[6].availableCells = new List<int> { 1, 7, 11 };
		nodes[7].availableCells = new List<int> { 6, 12, 2 };
		nodes[8].availableCells = new List<int> { 3, 9 };
		nodes[9].availableCells = new List<int> { 8, 14 };
		nodes[10].availableCells = new List<int> { };
		nodes[11].availableCells = new List<int> { 6, 16 };
		nodes[12].availableCells = new List<int> { 7, 13 };
		nodes[13].availableCells = new List<int> { 12, 18 };
		nodes[14].availableCells = new List<int> { 9, 19 };
		nodes[15].availableCells = new List<int> { 16, 20 };
		nodes[16].availableCells = new List<int> { 15, 11 };
		nodes[17].availableCells = new List<int> { 18, 22 };
		nodes[18].availableCells = new List<int> { 13, 17 };
		nodes[19].availableCells = new List<int> { 24, 14 };
		nodes[20].availableCells = new List<int> { 21, 15 };
		nodes[21].availableCells = new List<int> { 20 };
		nodes[22].availableCells = new List<int> { 17 };
		nodes[23].availableCells = new List<int> { 24 };
		nodes[24].availableCells = new List<int> { 23, 19 };
		currentCell = nodes[startPoint];
		currentPath.Add(startPoint);
		buttons[startPoint].GetComponent<Image>().sprite = spriteBlue;
		//Instantiate(line, buttons[startPoint].transform.position, Quaternion.identity, canvas.transform).transform.Rotate(new Vector3(0, 0, -90));
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public void ButtonPressed(int but) {
		if (currentPath.Contains(but)) {
			int del = currentPath.IndexOf(but);
			int num = currentPath.Count - del;
			del++;
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
			buttons[but].GetComponent<Image>().sprite = spriteBlue;
		}
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
			lineDrawn.Add(Instantiate(line, buttons[currentPath[i-1]].transform.position, Quaternion.identity, LineDrawn.transform));
			int dif = currentPath[i] - currentPath[i - 1];
			if (dif > 1) {
				lineDrawn[lineDrawn.Count - 1].transform.Rotate(new Vector3(0, 0, 180));
			} else if (dif > 0) {
				lineDrawn[lineDrawn.Count - 1].transform.Rotate(new Vector3(0, 0, -90));
			}			
		}		
	}

}
