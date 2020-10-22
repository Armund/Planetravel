using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class LabyrinthPresets {
	public static int presetsNumber = 9;
												   //0	1	2	3	4	5	6	7	8	9
	public static int[] startCells = new int[10] { 0,	0,	4,	4,	20, 24, 24, 20, 5,	2 };
	public static int[] finishCells = new int[10] { 22, 24, 15, 22, 3,	4,	0,	21, 23, 14 };
	public static List<int>[,] availableCells = new List<int>[,] {
		//0
		{ new List<int>() {1}, new List<int>() {6}, new List<int>() {3,7}, new List<int>() {8}, new List<int>() {}
		, new List<int>() {}, new List<int>() {1,7,11}, new List<int>() {6,12,2}, new List<int>() {3,9}, new List<int>() {8,14}
		, new List<int>() {}, new List<int>() {6,16}, new List<int>() {7,13}, new List<int>() {12,18}, new List<int>() {9,19}
		, new List<int>() {16,20}, new List<int>() {15,11}, new List<int>() {18,22}, new List<int>() {13,17}, new List<int>() {24,14}
		, new List<int>() {21,15}, new List<int>() {20}, new List<int>() {17}, new List<int>() {24}, new List<int>() {23,19}},
		//1
		{ new List<int>() {1}, new List<int>() {6}, new List<int>() {3,7}, new List<int>() {4}, new List<int>() {3}
		, new List<int>() {6,10}, new List<int>() {5,7}, new List<int>() {12,8,2}, new List<int>() {9,13,7}, new List<int>() {8,14}
		, new List<int>() {5,15}, new List<int>() {12}, new List<int>() {7,11}, new List<int>() {8,14,18}, new List<int>() {9,13,19}
		, new List<int>() {10,16}, new List<int>() {15}, new List<int>() {18,22}, new List<int>() {13,17}, new List<int>() {14}
		, new List<int>() {21}, new List<int>() {20,22}, new List<int>() {17,21,23}, new List<int>() {22,24}, new List<int>() {23}},
		//2
		{ new List<int>() {1,5}, new List<int>() {6}, new List<int>() {3,7}, new List<int>() {2,4}, new List<int>() {3,9}
		, new List<int>() {10,0}, new List<int>() {1,7,11}, new List<int>() {2,6,12}, new List<int>() {9}, new List<int>() {4,8}
		, new List<int>() {5}, new List<int>() {6,12}, new List<int>() {7,11,13}, new List<int>() {12,14,18}, new List<int>() {13,19}
		, new List<int>() {20}, new List<int>() {17}, new List<int>() {16,22}, new List<int>() {13,19,23}, new List<int>() {14,18}
		, new List<int>() {15,21}, new List<int>() {20,22}, new List<int>() {17,21,23}, new List<int>() {18,22,24}, new List<int>() {23}},
		//3
		{ new List<int>() {1,5}, new List<int>() {0,6}, new List<int>() {3}, new List<int>() {2,8}, new List<int>() {9}
		, new List<int>() {0,10}, new List<int>() {1,7}, new List<int>() {6,8,12}, new List<int>() {3,7,9}, new List<int>() {4,8}
		, new List<int>() {5,15}, new List<int>() {12,16}, new List<int>() {11,17}, new List<int>() {14}, new List<int>() {13,19}
		, new List<int>() {10,20}, new List<int>() {11}, new List<int>() {12,18}, new List<int>() {17}, new List<int>() {14,24}
		, new List<int>() {15,21}, new List<int>() {20,22}, new List<int>() {21,23}, new List<int>() {22,24}, new List<int>() {19,23}},
		//4
		{ new List<int>() {1,5}, new List<int>() {0,2}, new List<int>() {1,3}, new List<int>() {2,4,8}, new List<int>() {3,9}
		, new List<int>() {0,6}, new List<int>() {5,7}, new List<int>() {6,12}, new List<int>() {3,9}, new List<int>() {4,8}
		, new List<int>() {11,15}, new List<int>() {10,16}, new List<int>() {7,17}, new List<int>() {14,18}, new List<int>() {13,19}
		, new List<int>() {10}, new List<int>() {11,17}, new List<int>() {12,16,18}, new List<int>() {13,17}, new List<int>() {14,24}
		, new List<int>() {21}, new List<int>() {20,22}, new List<int>() {21,23}, new List<int>() {22,24}, new List<int>() {19,23}},
		//5
		{ new List<int>() {1,5}, new List<int>() {0,2}, new List<int>() {1,3}, new List<int>() {2,4}, new List<int>() {3}
		, new List<int>() {0,6,10}, new List<int>() {5,7}, new List<int>() {6,12}, new List<int>() {9}, new List<int>() {8,14}
		, new List<int>() {5}, new List<int>() {16}, new List<int>() {7,13,17}, new List<int>() {12,18}, new List<int>() {9,19}
		, new List<int>() {20}, new List<int>() {11,21}, new List<int>() {12}, new List<int>() {13,23}, new List<int>() {14,24}
		, new List<int>() {15,21}, new List<int>() {16,20,22}, new List<int>() {21,23}, new List<int>() {18,22,24}, new List<int>() {19,23}},
		//6
		{ new List<int>() {1}, new List<int>() {0,2}, new List<int>() {1,3,7}, new List<int>() {2,4}, new List<int>() {3,9}
		, new List<int>() {6,10}, new List<int>() {5,11}, new List<int>() {2,8,12}, new List<int>() {7,13}, new List<int>() {4}
		, new List<int>() {5,15}, new List<int>() {6,12,16}, new List<int>() {7,11,13}, new List<int>() {8,12,14,18}, new List<int>() {13}
		, new List<int>() {10,20}, new List<int>() {11,21}, new List<int>() {22}, new List<int>() {13,23}, new List<int>() {24}
		, new List<int>() {15}, new List<int>() {16,22}, new List<int>() {17,21,23}, new List<int>() {18,22,24}, new List<int>() {23}},
		//7
		{ new List<int>() {1,5}, new List<int>() {0,2}, new List<int>() {1,7}, new List<int>() {4,8}, new List<int>() {3}
		, new List<int>() {0,6}, new List<int>() {5,11}, new List<int>() {2,8,12}, new List<int>() {3,7}, new List<int>() {14}
		, new List<int>() {11,15}, new List<int>() {6,10}, new List<int>() {7,17}, new List<int>() {14,18}, new List<int>() {9,13,19}
		, new List<int>() {10,20}, new List<int>() {21}, new List<int>() {12,18}, new List<int>() {13,17,23}, new List<int>() {14}
		, new List<int>() {15}, new List<int>() {16,22}, new List<int>() {21,23}, new List<int>() {18,22,24}, new List<int>() {23}},
		//8
		{ new List<int>() {1,5}, new List<int>() {0,2,6}, new List<int>() {1,3}, new List<int>() {2,4}, new List<int>() {3,9}
		, new List<int>() {0}, new List<int>() {1,7}, new List<int>() {6,8,12}, new List<int>() {7,13}, new List<int>() {4,14}
		, new List<int>() {11,15}, new List<int>() {10,12}, new List<int>() {7,11,17}, new List<int>() {8}, new List<int>() {9}
		, new List<int>() {10,20}, new List<int>() {17}, new List<int>() {12,16,18}, new List<int>() {17,19,23}, new List<int>() {18,24}
		, new List<int>() {15,21}, new List<int>() {20,22}, new List<int>() {21}, new List<int>() {18}, new List<int>() {19}},
		//9
		{ new List<int>() {1,5}, new List<int>() {0}, new List<int>() {7}, new List<int>() {4}, new List<int>() {3,9}
		, new List<int>() {0,10}, new List<int>() {7,11}, new List<int>() {6,8}, new List<int>() {7,9}, new List<int>() {4,8}
		, new List<int>() {5,15}, new List<int>() {6,16}, new List<int>() {17}, new List<int>() {14,18}, new List<int>() {13,19}
		, new List<int>() {10,16,20}, new List<int>() {15}, new List<int>() {18,22}, new List<int>() {13,17}, new List<int>() {14}
		, new List<int>() {15,21}, new List<int>() {20,22}, new List<int>() {17,21}, new List<int>() {18,24}, new List<int>() {23}},
	};
}