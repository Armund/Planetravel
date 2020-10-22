using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LeversPresets
{
	public static int presetsNumber = 9;
	public static int[,] valuesTop = new int[,] {
		{3,3,5,1},
		{0,0,0,0},
		{0,0,0,0},
		{0,0,0,0},
		{0,0,0,0},
		{0,0,0,0},
		{0,0,0,0},
		{0,0,0,0},
		{0,0,0,0},
		{0,0,0,0},
	};
	public static int[,] valuesBot = new int[,] {
		{1,5,6,5},
		{0,0,0,0},
		{0,0,0,0},
		{0,0,0,0},
		{0,0,0,0},
		{0,0,0,0},
		{0,0,0,0},
		{0,0,0,0},
		{0,0,0,0},
		{0,0,0,0}
	};
	public static int[] goalsTop = new int[] {8,0,0,0,0,0,0,0,0,0};
	public static int[] goalsBot = new int[] {6,0,0,0,0,0,0,0,0,0};
}
