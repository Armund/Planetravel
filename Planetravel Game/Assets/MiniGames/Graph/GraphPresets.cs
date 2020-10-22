using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GraphPresets
{
	public static int presetsNumber = 10;
	public static int[] goals = new int[] { 9, 5, 8, 3, 6, 7, 9, 11, 15, 10};
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
		{0,4,1,1,4,6,5,7,0},
		{0,5,6,4,2,1,1,3,0},
		{0,2,1,3,1,1,1,2,0},
		{0,2,1,4,3,5,2,5,0},
		{0,1,4,5,6,3,5,2,0},
		{0,4,6,1,7,3,4,3,0},
		{0,5,4,3,6,6,1,2,0},
		{0,5,7,5,8,7,5,3,0},
		{0,2,3,4,9,7,7,6,0}
	};
}
