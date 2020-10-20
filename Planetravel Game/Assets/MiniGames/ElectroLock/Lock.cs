using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lock : MiniGame
{
	public Canvas canvas;
	public Text codeText;
	public Text winText;
	public Text currentCodeText;
    public bool isWin;
	List<int> goal = new List<int>();
	List<int> currentCode = new List<int>();

	// Start is called before the first frame update
	void Start()
    {
		//Init();
    }

	public override void Init() {
		if (!isStarted) {
			canvas.gameObject.SetActive(true);

			codeText.text = "";
			winText.text = "LOCKED";
			winText.color = Color.red;
			currentCodeText.text = "";
			for (int i = 0; i < 4; i++) {
				goal.Add(Random.Range(0, 10));
				codeText.text += goal[i].ToString();
			}

			isStarted = true;
		}
	}

	override public void Close() {
        canvas.gameObject.SetActive(false);
        if (isStarted) {
			
			if(!isWin) poi.EventLosing();
			isStarted = false;
            isWin = false;
		}
	}

	public void ButtonPressed(int but) {
		currentCode.Add(but);
		currentCodeText.text += but.ToString();
		if (IsGameOver()) {
			winText.text = "UNLOCKED";
            poi.SetEventDone();
            isWin = true;
            winText.color = Color.green;
		} else if (currentCode.Count == 4) {
			currentCodeText.text = "";
			currentCode.Clear();
		}
	}

	public bool IsGameOver() {
		for (int i = 0; i < 4; i++) {
			if (currentCode.Count < 4 || goal[i] != currentCode[i]) {
				return false;
			}
		}
		return true;
	}
}
