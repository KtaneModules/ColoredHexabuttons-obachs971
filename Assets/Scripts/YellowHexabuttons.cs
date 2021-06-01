using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowHexabuttons{
	private string[][] yellowMaze =
		{
		new string[] {"W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W"},
		new string[] {"W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "64", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W"},
		new string[] {"W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "O", "W", "O", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W"},
		new string[] {"W", "W", "W", "W", "W", "W", "W", "W", "W", "09", "W", "W", "W", "43", "W", "W", "W", "W", "W", "W", "W", "W", "W"},
		new string[] {"W", "W", "W", "W", "W", "W", "W", "W", "O", "W", "W", "W", "W", "O", "W", "W", "W", "W", "W", "W", "W", "W", "W"},
		new string[] {"W", "W", "W", "W", "W", "W", "W", "06", "W", "W", "W", "76", "W", "O", "W", "33", "W", "W", "W", "W", "W", "W", "W"},
		new string[] {"W", "W", "W", "W", "W", "W", "O", "W", "W", "W", "W", "W", "O", "O", "W", "W", "O", "W", "W", "W", "W", "W", "W"},
		new string[] {"W", "W", "W", "W", "W", "71", "W", "W", "W", "56", "W", "W", "W", "46", "W", "W", "W", "61", "W", "W", "W", "W", "W"},
		new string[] {"W", "W", "W", "W", "W", "W", "O", "W", "O", "W", "O", "W", "W", "W", "O", "W", "O", "O", "W", "W", "W", "W", "W"},
		new string[] {"W", "W", "W", "78", "W", "W", "W", "86", "W", "W", "W", "75", "W", "W", "W", "12", "W", "O", "W", "29", "W", "W", "W"},
		new string[] {"W", "W", "O", "O", "W", "W", "O", "W", "W", "W", "O", "W", "O", "W", "W", "W", "W", "O", "W", "W", "O", "W", "W"},
		new string[] {"W", "31", "W", "O", "W", "07", "W", "W", "W", "21", "W", "W", "W", "30", "W", "W", "W", "08", "W", "W", "W", "19", "W"},
		new string[] {"W", "W", "W", "O", "O", "O", "W", "W", "W", "O", "W", "W", "W", "O", "W", "W", "W", "W", "W", "W", "O", "W", "W"},
		new string[] {"W", "W", "W", "59", "W", "O", "W", "91", "W", "O", "W", "73", "W", "O", "W", "20", "W", "W", "W", "11", "W", "W", "W"},
		new string[] {"W", "W", "O", "W", "W", "O", "W", "O", "W", "O", "W", "O", "W", "O", "W", "O", "W", "W", "W", "O", "W", "W", "W"},
		new string[] {"W", "35", "W", "W", "W", "69", "W", "O", "W", "36", "W", "O", "W", "15", "W", "O", "W", "32", "W", "O", "W", "63", "W"},
		new string[] {"W", "W", "O", "W", "W", "W", "O", "O", "W", "O", "O", "O", "W", "W", "O", "O", "W", "O", "W", "O", "O", "O", "W"},
		new string[] {"W", "W", "W", "44", "W", "W", "W", "42", "W", "O", "W", "62", "W", "W", "W", "02", "W", "O", "W", "81", "W", "O", "W"},
		new string[] {"W", "W", "W", "W", "W", "W", "W", "W", "W", "O", "W", "W", "O", "W", "W", "W", "O", "O", "W", "W", "W", "O", "W"},
		new string[] {"W", "26", "W", "W", "W", "74", "W", "W", "W", "37", "W", "W", "W", "66", "W", "W", "W", "88", "W", "W", "W", "49", "W"},
		new string[] {"W", "O", "O", "W", "W", "W", "O", "W", "W", "O", "W", "W", "W", "O", "W", "W", "W", "O", "O", "W", "W", "O", "W"},
		new string[] {"W", "O", "W", "03", "W", "W", "W", "55", "W", "O", "W", "27", "W", "O", "W", "25", "W", "O", "W", "16", "W", "O", "W"},
		new string[] {"W", "O", "W", "O", "W", "W", "O", "O", "W", "O", "O", "W", "W", "O", "W", "W", "O", "O", "W", "W", "O", "O", "W"},
		new string[] {"W", "01", "W", "O", "W", "57", "W", "O", "W", "70", "W", "W", "W", "53", "W", "W", "W", "58", "W", "W", "W", "52", "W"},
		new string[] {"W", "W", "W", "O", "O", "W", "W", "O", "W", "W", "W", "W", "O", "W", "W", "W", "W", "W", "W", "W", "O", "W", "W"},
		new string[] {"W", "W", "W", "48", "W", "W", "W", "79", "W", "W", "W", "04", "W", "W", "W", "54", "W", "W", "W", "90", "W", "W", "W"},
		new string[] {"W", "W", "W", "W", "O", "W", "W", "W", "O", "W", "W", "O", "W", "W", "W", "W", "O", "W", "W", "W", "W", "W", "W"},
		new string[] {"W", "47", "W", "W", "W", "28", "W", "W", "W", "89", "W", "O", "W", "50", "W", "W", "W", "85", "W", "W", "W", "51", "W"},
		new string[] {"W", "O", "O", "W", "W", "W", "O", "W", "W", "W", "O", "O", "O", "W", "O", "W", "O", "W", "W", "W", "W", "O", "W"},
		new string[] {"W", "O", "W", "45", "W", "W", "W", "83", "W", "W", "W", "18", "W", "W", "W", "17", "W", "W", "W", "40", "W", "O", "W"},
		new string[] {"W", "O", "W", "W", "W", "W", "W", "W", "O", "W", "W", "W", "W", "W", "W", "W", "O", "W", "W", "O", "O", "O", "W"},
		new string[] {"W", "77", "W", "W", "W", "84", "W", "W", "W", "24", "W", "W", "W", "60", "W", "W", "W", "10", "W", "O", "W", "87", "W"},
		new string[] {"W", "W", "O", "W", "O", "W", "O", "W", "W", "W", "O", "W", "O", "W", "O", "W", "W", "W", "O", "O", "W", "W", "W"},
		new string[] {"W", "W", "W", "67", "W", "W", "W", "34", "W", "W", "W", "38", "W", "W", "W", "41", "W", "W", "W", "68", "W", "W", "W"},
		new string[] {"W", "W", "W", "W", "O", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "O", "W", "W", "W", "W", "W", "W"},
		new string[] {"W", "W", "W", "W", "W", "13", "W", "W", "W", "39", "W", "W", "W", "23", "W", "W", "W", "14", "W", "W", "W", "W", "W"},
		new string[] {"W", "W", "W", "W", "W", "W", "O", "W", "W", "W", "O", "W", "W", "O", "O", "W", "O", "W", "W", "W", "W", "W", "W"},
		new string[] {"W", "W", "W", "W", "W", "W", "W", "65", "W", "W", "W", "82", "W", "O", "W", "72", "W", "W", "W", "W", "W", "W", "W"},
		new string[] {"W", "W", "W", "W", "W", "W", "W", "W", "O", "W", "O", "O", "W", "O", "W", "W", "W", "W", "W", "W", "W", "W", "W"},
		new string[] {"W", "W", "W", "W", "W", "W", "W", "W", "W", "05", "W", "O", "W", "80", "W", "W", "W", "W", "W", "W", "W", "W", "W"},
		new string[] {"W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "O", "O", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W"},
		new string[] {"W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "22", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W"},
		new string[] {"W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W"},
		};
	private ColorfulButtonSeries coloredHexabuttons;
	private KMAudio Audio;
	private int moduleId;
	private KMSelectable[] hexButtons;
	private MeshRenderer[] buttonMesh;
	private MeshFilter[] buttonMF;
	private MeshFilter[] highlightMF;
	private Transform[] highlightTF;
	private MeshRenderer[] ledMesh;
	private Transform transform;
	private MeshFilter[] shapes;
	private string[] voiceMessage;
	private bool moduleSolved;
	private string[] positions = { "TL", "TR", "ML", "MR", "BL", "BR", "C"};
	private int[] buttonIndex = { 0, 1, 2, 3, 4, 5 };
	private float[][] shapeSizes =
	{
		new float[]{0.046f, 0.00323f, 0.046f},//Circle
		new float[]{0.0173f, 0.0034f, 0.0173f},//Triangle
		new float[]{0.0341f, 0.0065f, 0.0341f},//Square
		new float[]{0.0198f, 0.0036f, 0.0198f},//Pentagon
		new float[]{0.017f, 0.007f, 0.015f},//Hexagon
		new float[]{0.217f, 0.063f, 0.217f},//Octagon
		new float[]{0.0196f, 0.003f, 0.0196f},//Heart
		new float[]{0.0197f, 0.0034f, 0.0197f},//Star
		new float[]{0.0204f, 0.0045f, 0.0204f},//Crescent
		new float[]{0.369f, 0.107f, 0.369f}//Cross
	};
	private float[] shapeHLSizes = {1.045f, 1.08f, 1.06f, 1.06f, 1.04f, 1.05f, 1.06f, 1.12f, 1.05f, 1.05f};
	private float[] shapeHLPositions = {-0.5f, -0.5f, -0.24f, -0.5f, -0.24f, -0.025f, -0.5f, -0.5f, -0.35f, -0.016f};
	private string[] shapeNames = {"CIRCLE", "TRIANGLE", "SQUARE", "PENTAGON", "HEXAGON", "OCTAGON", "HEART", "STAR", "CRESCENT", "CROSS"};
	private string[] dirNames = {"N", "NE", "SE", "S", "SW", "NW"};
	private string yellowShapes;
	private int[] yellowRC;
	public YellowHexabuttons(ColorfulButtonSeries m, KMAudio aud, int MI, KMSelectable[] HB, MeshRenderer[] BM, MeshFilter[] BMF, MeshFilter[] HMF, Transform[] HTF, MeshRenderer[] LM, Transform T, MeshFilter[] S)
	{
		coloredHexabuttons = m;
		Audio = aud;
		moduleId = MI;
		hexButtons = HB;
		buttonMesh = BM;
		buttonMF = BMF;
		highlightMF = HMF;
		highlightTF = HTF;
		ledMesh = LM;
		transform = T;
		shapes = S;
	}
	public void run()
	{
		Debug.LogFormat("[Colored Hexabuttons #{0}] Color Generated: Yellow", moduleId);
		string possible = "0123456789";
		yellowShapes = "";
		for (int i = 0; i < 7; i++)
		{
			if (i < 6)
				ledMesh[i].transform.localScale = new Vector3(0f, 0f, 0f);
			yellowShapes = yellowShapes + "" + possible[UnityEngine.Random.Range(0, possible.Length)];
			buttonMF[i].mesh = shapes[yellowShapes[i] - '0'].sharedMesh;
			highlightMF[i].mesh = shapes[yellowShapes[i] - '0'].sharedMesh;
			highlightTF[i].transform.localScale = new Vector3(shapeHLSizes[yellowShapes[i] - '0'], 0.01f, shapeHLSizes[yellowShapes[i] - '0']);
			highlightTF[i].transform.localPosition = new Vector3(0f, shapeHLPositions[yellowShapes[i] - '0'], 0f);
			hexButtons[i].transform.localScale = new Vector3(shapeSizes[yellowShapes[i] - '0'][0], shapeSizes[yellowShapes[i] - '0'][1], shapeSizes[yellowShapes[i] - '0'][2]);
			possible = possible.Replace(yellowShapes[i] + "", "");
			Debug.LogFormat("[Colored Hexabuttons #{0}] {1} button is a {2}", moduleId, positions[i], shapeNames[yellowShapes[i] - '0']);
		}
		//Index starts at 0: Circle, Triangle, Square, Pentagon, Hexagon, Octagon, Heart, Star, Crescent, Cross
		string[] priorityList =
		{
			"187625943",
			"708534692",
			"354701869",
			"296850471",
			"571269308",
			"912487036",
			"439172580",
			"063948125",
			"625093714",
			"840316257 "
		};
		int accum = 0;
		int[] order = new int[6];
		string buttonPriority = "";
		for (int aa = 0; aa < 9; aa++)
		{
			if (yellowShapes.IndexOf(priorityList[yellowShapes[6] - '0'][aa]) >= 0)
			{
				int ind = yellowShapes.IndexOf(priorityList[yellowShapes[6] - '0'][aa]);
				order[ind] = accum + 0;
				buttonPriority = buttonPriority + "" + positions[ind] + " ";
				accum++;
			}
			if (accum == 6)
				break;
		}
		Debug.LogFormat("[Colored Hexabuttons #{0}] Button Order: {1}", moduleId, buttonPriority);
		foreach (int i in buttonIndex)
		{
			hexButtons[i].OnInteract = delegate { pressedYellow(i, order[i]); return false; };
			hexButtons[i].OnInteractEnded = delegate { pressedYellowRelease(i); };
		}
		hexButtons[6].OnInteract = delegate { pressedYellowCenter(); return false; };
		voiceMessage = new string[4];
		yellowRC = new int[2];
		string numberPos = UnityEngine.Random.Range(1, 92) + "";
		if (numberPos.Length < 2)
			numberPos = "0" + numberPos;
		voiceMessage[2] = numberPos[0] + "";
		voiceMessage[3] = numberPos[1] + "";
		Debug.LogFormat("[Colored Hexabuttons #{0}] Goal Space: {1}{2}", moduleId, voiceMessage[2], voiceMessage[3]);
		bool flag = false;
		for (int aa = 0; aa < yellowMaze.Length; aa++)
		{
			for (int bb = 0; bb < yellowMaze[aa].Length; bb++)
			{
				if (yellowMaze[aa][bb].Equals(numberPos))
				{
					yellowRC[0] = aa;
					yellowRC[1] = bb;
					flag = true;
					break;
				}
			}
			if (flag)
				break;
		}
		string[][] tempMaze =
		{
		new string[] {"W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W"},
		new string[] {"W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "64", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W"},
		new string[] {"W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "O", "W", "O", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W"},
		new string[] {"W", "W", "W", "W", "W", "W", "W", "W", "W", "09", "W", "W", "W", "43", "W", "W", "W", "W", "W", "W", "W", "W", "W"},
		new string[] {"W", "W", "W", "W", "W", "W", "W", "W", "O", "W", "W", "W", "W", "O", "W", "W", "W", "W", "W", "W", "W", "W", "W"},
		new string[] {"W", "W", "W", "W", "W", "W", "W", "06", "W", "W", "W", "76", "W", "O", "W", "33", "W", "W", "W", "W", "W", "W", "W"},
		new string[] {"W", "W", "W", "W", "W", "W", "O", "W", "W", "W", "W", "W", "O", "O", "W", "W", "O", "W", "W", "W", "W", "W", "W"},
		new string[] {"W", "W", "W", "W", "W", "71", "W", "W", "W", "56", "W", "W", "W", "46", "W", "W", "W", "61", "W", "W", "W", "W", "W"},
		new string[] {"W", "W", "W", "W", "W", "W", "O", "W", "O", "W", "O", "W", "W", "W", "O", "W", "O", "O", "W", "W", "W", "W", "W"},
		new string[] {"W", "W", "W", "78", "W", "W", "W", "86", "W", "W", "W", "75", "W", "W", "W", "12", "W", "O", "W", "29", "W", "W", "W"},
		new string[] {"W", "W", "O", "O", "W", "W", "O", "W", "W", "W", "O", "W", "O", "W", "W", "W", "W", "O", "W", "W", "O", "W", "W"},
		new string[] {"W", "31", "W", "O", "W", "07", "W", "W", "W", "21", "W", "W", "W", "30", "W", "W", "W", "08", "W", "W", "W", "19", "W"},
		new string[] {"W", "W", "W", "O", "O", "O", "W", "W", "W", "O", "W", "W", "W", "O", "W", "W", "W", "W", "W", "W", "O", "W", "W"},
		new string[] {"W", "W", "W", "59", "W", "O", "W", "91", "W", "O", "W", "73", "W", "O", "W", "20", "W", "W", "W", "11", "W", "W", "W"},
		new string[] {"W", "W", "O", "W", "W", "O", "W", "O", "W", "O", "W", "O", "W", "O", "W", "O", "W", "W", "W", "O", "W", "W", "W"},
		new string[] {"W", "35", "W", "W", "W", "69", "W", "O", "W", "36", "W", "O", "W", "15", "W", "O", "W", "32", "W", "O", "W", "63", "W"},
		new string[] {"W", "W", "O", "W", "W", "W", "O", "O", "W", "O", "O", "O", "W", "W", "O", "O", "W", "O", "W", "O", "O", "O", "W"},
		new string[] {"W", "W", "W", "44", "W", "W", "W", "42", "W", "O", "W", "62", "W", "W", "W", "02", "W", "O", "W", "81", "W", "O", "W"},
		new string[] {"W", "W", "W", "W", "W", "W", "W", "W", "W", "O", "W", "W", "O", "W", "W", "W", "O", "O", "W", "W", "W", "O", "W"},
		new string[] {"W", "26", "W", "W", "W", "74", "W", "W", "W", "37", "W", "W", "W", "66", "W", "W", "W", "88", "W", "W", "W", "49", "W"},
		new string[] {"W", "O", "O", "W", "W", "W", "O", "W", "W", "O", "W", "W", "W", "O", "W", "W", "W", "O", "O", "W", "W", "O", "W"},
		new string[] {"W", "O", "W", "03", "W", "W", "W", "55", "W", "O", "W", "27", "W", "O", "W", "25", "W", "O", "W", "16", "W", "O", "W"},
		new string[] {"W", "O", "W", "O", "W", "W", "O", "O", "W", "O", "O", "W", "W", "O", "W", "W", "O", "O", "W", "W", "O", "O", "W"},
		new string[] {"W", "01", "W", "O", "W", "57", "W", "O", "W", "70", "W", "W", "W", "53", "W", "W", "W", "58", "W", "W", "W", "52", "W"},
		new string[] {"W", "W", "W", "O", "O", "W", "W", "O", "W", "W", "W", "W", "O", "W", "W", "W", "W", "W", "W", "W", "O", "W", "W"},
		new string[] {"W", "W", "W", "48", "W", "W", "W", "79", "W", "W", "W", "04", "W", "W", "W", "54", "W", "W", "W", "90", "W", "W", "W"},
		new string[] {"W", "W", "W", "W", "O", "W", "W", "W", "O", "W", "W", "O", "W", "W", "W", "W", "O", "W", "W", "W", "W", "W", "W"},
		new string[] {"W", "47", "W", "W", "W", "28", "W", "W", "W", "89", "W", "O", "W", "50", "W", "W", "W", "85", "W", "W", "W", "51", "W"},
		new string[] {"W", "O", "O", "W", "W", "W", "O", "W", "W", "W", "O", "O", "O", "W", "O", "W", "O", "W", "W", "W", "W", "O", "W"},
		new string[] {"W", "O", "W", "45", "W", "W", "W", "83", "W", "W", "W", "18", "W", "W", "W", "17", "W", "W", "W", "40", "W", "O", "W"},
		new string[] {"W", "O", "W", "W", "W", "W", "W", "W", "O", "W", "W", "W", "W", "W", "W", "W", "O", "W", "W", "O", "O", "O", "W"},
		new string[] {"W", "77", "W", "W", "W", "84", "W", "W", "W", "24", "W", "W", "W", "60", "W", "W", "W", "10", "W", "O", "W", "87", "W"},
		new string[] {"W", "W", "O", "W", "O", "W", "O", "W", "W", "W", "O", "W", "O", "W", "O", "W", "W", "W", "O", "O", "W", "W", "W"},
		new string[] {"W", "W", "W", "67", "W", "W", "W", "34", "W", "W", "W", "38", "W", "W", "W", "41", "W", "W", "W", "68", "W", "W", "W"},
		new string[] {"W", "W", "W", "W", "O", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "O", "W", "W", "W", "W", "W", "W"},
		new string[] {"W", "W", "W", "W", "W", "13", "W", "W", "W", "39", "W", "W", "W", "23", "W", "W", "W", "14", "W", "W", "W", "W", "W"},
		new string[] {"W", "W", "W", "W", "W", "W", "O", "W", "W", "W", "O", "W", "W", "O", "O", "W", "O", "W", "W", "W", "W", "W", "W"},
		new string[] {"W", "W", "W", "W", "W", "W", "W", "65", "W", "W", "W", "82", "W", "O", "W", "72", "W", "W", "W", "W", "W", "W", "W"},
		new string[] {"W", "W", "W", "W", "W", "W", "W", "W", "O", "W", "O", "O", "W", "O", "W", "W", "W", "W", "W", "W", "W", "W", "W"},
		new string[] {"W", "W", "W", "W", "W", "W", "W", "W", "W", "05", "W", "O", "W", "80", "W", "W", "W", "W", "W", "W", "W", "W", "W"},
		new string[] {"W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "O", "O", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W"},
		new string[] {"W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "22", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W"},
		new string[] {"W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W"},
		};
		string dir = "";
		while (dir.Length < 6)
		{
			string possdir = "";
			if (tempMaze[yellowRC[0] - 1][yellowRC[1]].Equals("O"))
				possdir = possdir + "0";//N
			if (tempMaze[yellowRC[0] - 1][yellowRC[1] + 1].Equals("O"))
				possdir = possdir + "1";//NE
			if (tempMaze[yellowRC[0] + 1][yellowRC[1] + 1].Equals("O"))
				possdir = possdir + "2";//SE
			if (tempMaze[yellowRC[0] + 1][yellowRC[1]].Equals("O"))
				possdir = possdir + "3";//S
			if (tempMaze[yellowRC[0] + 1][yellowRC[1] - 1].Equals("O"))
				possdir = possdir + "4";//SW
			if (tempMaze[yellowRC[0] - 1][yellowRC[1] - 1].Equals("O"))
				possdir = possdir + "5";//NW
			if (possdir.Length == 0)
			{
				switch (dir[dir.Length - 1])
				{
					case '0':
						yellowRC[0] += 4;
						break;
					case '1':
						yellowRC[0] += 2;
						yellowRC[1] -= 2;
						break;
					case '2':
						yellowRC[0] -= 2;
						yellowRC[1] -= 2;
						break;
					case '3':
						yellowRC[0] -= 4;
						break;
					case '4':
						yellowRC[0] -= 2;
						yellowRC[1] += 2;
						break;
					case '5':
						yellowRC[0] += 2;
						yellowRC[1] += 2;
						break;
				}
				dir = dir.Substring(0, dir.Length - 1);
			}
			else
			{
				dir = dir + "" + possdir[UnityEngine.Random.Range(0, possdir.Length)];
				switch (dir[dir.Length - 1])
				{
					case '0':
						tempMaze[yellowRC[0] - 1][yellowRC[1]] = "W";
						tempMaze[yellowRC[0] - 2][yellowRC[1]] = "W";
						tempMaze[yellowRC[0] - 3][yellowRC[1]] = "W";
						yellowRC[0] -= 4;
						break;
					case '1':
						tempMaze[yellowRC[0] - 1][yellowRC[1] + 1] = "W";
						yellowRC[0] -= 2;
						yellowRC[1] += 2;
						break;
					case '2':
						tempMaze[yellowRC[0] + 1][yellowRC[1] + 1] = "W";
						yellowRC[0] += 2;
						yellowRC[1] += 2;
						break;
					case '3':
						tempMaze[yellowRC[0] + 1][yellowRC[1]] = "W";
						tempMaze[yellowRC[0] + 2][yellowRC[1]] = "W";
						tempMaze[yellowRC[0] + 3][yellowRC[1]] = "W";
						yellowRC[0] += 4;
						break;
					case '4':
						tempMaze[yellowRC[0] + 1][yellowRC[1] - 1] = "W";
						yellowRC[0] += 2;
						yellowRC[1] -= 2;
						break;
					case '5':
						tempMaze[yellowRC[0] - 1][yellowRC[1] - 1] = "W";
						yellowRC[0] -= 2;
						yellowRC[1] -= 2;
						break;
				}
			}
		}
		voiceMessage[0] = yellowMaze[yellowRC[0]][yellowRC[1]][0] + "";
		voiceMessage[1] = yellowMaze[yellowRC[0]][yellowRC[1]][1] + "";
		Debug.LogFormat("[Colored Hexabuttons #{0}] Current Space: {1}{2}", moduleId, voiceMessage[0], voiceMessage[1]);
	}
	void pressedYellowCenter()
	{
		if (!(moduleSolved))
		{
			Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
			Vector3 pos = buttonMesh[6].transform.localPosition;
			pos.y = 0.0126f;
			buttonMesh[6].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
			coloredHexabuttons.StartCoroutine(playAudio());
		}
	}
	IEnumerator playAudio()
	{
		hexButtons[6].OnInteract = null;
		yield return new WaitForSeconds(0.5f);
		for (int aa = 0; aa < voiceMessage.Length; aa++)
		{
			Audio.PlaySoundAtTransform(voiceMessage[aa], transform);
			yield return new WaitForSeconds(1.5f);
		}
		Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonRelease, transform);
		Vector3 pos = buttonMesh[6].transform.localPosition;
		pos.y = 0.0169f;
		buttonMesh[6].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
		hexButtons[6].OnInteract = delegate { pressedYellowCenter(); return false; };
	}
	void pressedYellow(int n, int d)
	{
		if (!(moduleSolved))
		{
			Debug.LogFormat("[Colored Hexabuttons #{0}] User pressed {1}, which is {2}.", moduleId, positions[n], dirNames[d]);
			Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
			Vector3 pos = buttonMesh[n].transform.localPosition;
			pos.y = 0.0126f;
			buttonMesh[n].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
			switch (d)
			{
				case 0://N
					if (yellowMaze[yellowRC[0] - 1][yellowRC[1]].Equals("W"))
					{
						Debug.LogFormat("[Colored Hexabuttons #{0}] Strike! There is a wall to the N of {1}{2}", moduleId, voiceMessage[0], voiceMessage[1]);
						coloredHexabuttons.Strike();
					}
					else
					{
						yellowRC[0] -= 4;
						voiceMessage[0] = yellowMaze[yellowRC[0]][yellowRC[1]][0] + "";
						voiceMessage[1] = yellowMaze[yellowRC[0]][yellowRC[1]][1] + "";
						Debug.LogFormat("[Colored Hexabuttons #{0}] Moving N, current space is now {1}{2}", moduleId, voiceMessage[0], voiceMessage[1]);
					}
					break;
				case 1://NE
					if (yellowMaze[yellowRC[0] - 1][yellowRC[1] + 1].Equals("W"))
					{
						Debug.LogFormat("[Colored Hexabuttons #{0}] Strike! There is a wall to the NE of {1}{2}", moduleId, voiceMessage[0], voiceMessage[1]);
						coloredHexabuttons.Strike();
					}
					else
					{
						yellowRC[0] -= 2;
						yellowRC[1] += 2;
						voiceMessage[0] = yellowMaze[yellowRC[0]][yellowRC[1]][0] + "";
						voiceMessage[1] = yellowMaze[yellowRC[0]][yellowRC[1]][1] + "";
						Debug.LogFormat("[Colored Hexabuttons #{0}] Moving NE, current space is now {1}{2}", moduleId, voiceMessage[0], voiceMessage[1]);
					}
					break;
				case 2://SE
					if (yellowMaze[yellowRC[0] + 1][yellowRC[1] + 1].Equals("W"))
					{
						Debug.LogFormat("[Colored Hexabuttons #{0}] Strike! There is a wall to the SE of {1}{2}", moduleId, voiceMessage[0], voiceMessage[1]);
						coloredHexabuttons.Strike();
					}
					else
					{
						yellowRC[0] += 2;
						yellowRC[1] += 2;
						voiceMessage[0] = yellowMaze[yellowRC[0]][yellowRC[1]][0] + "";
						voiceMessage[1] = yellowMaze[yellowRC[0]][yellowRC[1]][1] + "";
						Debug.LogFormat("[Colored Hexabuttons #{0}] Moving SE, current space is now {1}{2}", moduleId, voiceMessage[0], voiceMessage[1]);
					}
					break;
				case 3://S
					if (yellowMaze[yellowRC[0] + 1][yellowRC[1]].Equals("W"))
					{
						Debug.LogFormat("[Colored Hexabuttons #{0}] Strike! There is a wall to the S of {1}{2}", moduleId, voiceMessage[0], voiceMessage[1]);
						coloredHexabuttons.Strike();
					}
					else
					{
						yellowRC[0] += 4;
						voiceMessage[0] = yellowMaze[yellowRC[0]][yellowRC[1]][0] + "";
						voiceMessage[1] = yellowMaze[yellowRC[0]][yellowRC[1]][1] + "";
						Debug.LogFormat("[Colored Hexabuttons #{0}] Moving S, current space is now {1}{2}", moduleId, voiceMessage[0], voiceMessage[1]);
					}
					break;
				case 4://SW
					if (yellowMaze[yellowRC[0] + 1][yellowRC[1] - 1].Equals("W"))
					{
						Debug.LogFormat("[Colored Hexabuttons #{0}] Strike! There is a wall to the SW of {1}{2}", moduleId, voiceMessage[0], voiceMessage[1]);
						coloredHexabuttons.Strike();
					}
					else
					{
						yellowRC[0] += 2;
						yellowRC[1] -= 2;
						voiceMessage[0] = yellowMaze[yellowRC[0]][yellowRC[1]][0] + "";
						voiceMessage[1] = yellowMaze[yellowRC[0]][yellowRC[1]][1] + "";
						Debug.LogFormat("[Colored Hexabuttons #{0}] Moving SW, current space is now {1}{2}", moduleId, voiceMessage[0], voiceMessage[1]);
					}
					break;
				default://NW
					if (yellowMaze[yellowRC[0] - 1][yellowRC[1] - 1].Equals("W"))
					{
						Debug.LogFormat("[Colored Hexabuttons #{0}] Strike! There is a wall to the NW of {1}{2}", moduleId, voiceMessage[0], voiceMessage[1]);
						coloredHexabuttons.Strike();
					}
					else
					{
						yellowRC[0] -= 2;
						yellowRC[1] -= 2;
						voiceMessage[0] = yellowMaze[yellowRC[0]][yellowRC[1]][0] + "";
						voiceMessage[1] = yellowMaze[yellowRC[0]][yellowRC[1]][1] + "";
						Debug.LogFormat("[Colored Hexabuttons #{0}] Moving NW, current space is now {1}{2}", moduleId, voiceMessage[0], voiceMessage[1]);
					}
					break;
			}
			if (voiceMessage[0] == voiceMessage[2] && voiceMessage[1] == voiceMessage[3])
			{
				moduleSolved = true;
				coloredHexabuttons.Solve();
			}
		}
	}
	void pressedYellowRelease(int n)
	{
		if (!(moduleSolved))
		{
			Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonRelease, transform);
			Vector3 pos = buttonMesh[n].transform.localPosition;
			pos.y = 0.0169f;
			buttonMesh[n].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
		}
	}
}
