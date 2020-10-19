using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class LabyrinthPresets
{
	public static int presetsNumber = 1;

	public static int[] startCells = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
	public static int[] finishCells = new int[10] { 22, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
	public static List<int>[,] availableCells = new List<int>[,] {
		{ new List<int>() {1}, new List<int>() {6}, new List<int>() {3,7}, new List<int>() {8}, new List<int>() {}
		, new List<int>() {}, new List<int>() {1,7,11}, new List<int>() {6,12,2}, new List<int>() {3,9}, new List<int>() {8,14}
		, new List<int>() {}, new List<int>() {6,16}, new List<int>() {7,13}, new List<int>() {12,18}, new List<int>() {9,19}
		, new List<int>() {16,20}, new List<int>() {15,11}, new List<int>() {18,22}, new List<int>() {13,17}, new List<int>() {24,14}
		, new List<int>() {21,15}, new List<int>() {20}, new List<int>() {17}, new List<int>() {24}, new List<int>() {23,19}}
	};
}
/*
		nodes[20].availableCells = new List<int> { 21, 15 };
		nodes[21].availableCells = new List<int> { 20 };
		nodes[22].availableCells = new List<int> { 17 };
		nodes[23].availableCells = new List<int> { 24 };
		nodes[24].availableCells = new List<int> { 23, 19 };
		*/