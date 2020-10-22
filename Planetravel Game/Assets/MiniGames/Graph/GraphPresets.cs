using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GraphPresets
{
	public static int presetsNumber = 9;
	public static int[] goals = new int[] { 9, 0, 0, 0, 0, 0, 0, 0, 0, 0};
	public static List<int>[,] availableCells = new List<int>[,] {
		//0
		{ new List<int>() { 1, 2},
			new List<int>() {3,4,5}, new List<int>() {3,4,5},
			new List<int>() {6,7}, new List<int>() {6,7}, new List<int>() {6,7},
			new List<int>() {8}, new List<int>() {8},
			new List<int>() {}},
		//1
		{ new List<int>() {},
			new List<int>() {}, new List<int>() {},
			new List<int>() {}, new List<int>() {}, new List<int>() {},
			new List<int>() {}, new List<int>() {},
			new List<int>() {}},
	};

	public static int[,] valuesInside = new int[,] {
		{0,5,3,2,6,2,3,8,0},
		{0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0}
	};
}
