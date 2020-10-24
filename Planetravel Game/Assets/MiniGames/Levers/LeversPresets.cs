using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LeversPresets
{
	public static int presetsNumber = 9;
	public static int[,] valuesTop = new int[,] {
		{3,3,5,1},
		{6,3,2,4},
		{1,3,2,1},
		{4,2,1,2},
		{6,3,1,3},
		{5,2,4,3},
		{2,6,4,6},
		{1,3,5,1},
		{4,2,7,5},
		{2,6,7,5},
	};
	public static int[,] valuesBot = new int[,] {
		{1,5,6,5},
		{4,5,1,2},
		{6,4,3,3},
		{3,1,3,5},
		{5,2,2,6},
		{6,7,1,2},
		{6,4,1,2},
		{1,3,6,3},
		{1,6,1,9},
		{2,6,4,1}
	};
	public static int[] goalsTop = new int[] {8, 10, 3, 4, 9, 5, 8, 5, 9,  14};
	public static int[] goalsBot = new int[] {6, 6,  7, 6, 4, 7, 3, 7, 10, 6};
}
