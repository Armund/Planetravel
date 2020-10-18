using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
	public int cellNumber;
	public List<int> availableCells;
	//public int[] availableCells = new int[4] { -1, -1, -1, -1 };

		/*
	public int[] getAvailableCells() {
		int numberOfAvailable = 0;
		for (int i = 0; i < availableCells.Length; i++) {
			if (availableCells[i] >= 0) {
				numberOfAvailable++;
			}
		}
		int[] result = new int[numberOfAvailable];
		for (int i = 0; i < availableCells.Length; i++) {
			if (availableCells[i] >= 0) {
				numberOfAvailable++;
			}
		}
		return result;
	}
	*/

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
