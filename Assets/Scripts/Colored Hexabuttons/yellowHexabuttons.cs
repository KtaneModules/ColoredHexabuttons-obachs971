using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class yellowHexabuttons : MonoBehaviour
{
    private static readonly string[][] yellowMaze =
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

    public KMBombModule module;
    public KMAudio Audio;
    private int moduleId;
    private static int moduleIdCounter = 1;
    public KMSelectable[] hexButtons;
    public MeshRenderer[] buttonMesh;
    public MeshFilter[] buttonMF;
    public MeshFilter[] highlightMF;
    public Transform[] highlightTF;
    public MeshFilter[] shapes;
    public TextMesh centerText;

    private bool deafMode = false;
    private string[] voiceMessage;
    private bool moduleSolved;
    private readonly string[] positions = { "TL", "TR", "ML", "MR", "BL", "BR", "C" };
    private readonly int[] buttonIndex = { 0, 1, 2, 3, 4, 5 };
    private readonly int[] order = new int[6];
    private readonly float[][] shapeSizes =
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
    private readonly float[] shapeHLSizes = { 1.045f, 1.08f, 1.06f, 1.06f, 1.04f, 1.05f, 1.06f, 1.12f, 1.05f, 1.05f };
    private readonly float[] shapeHLPositions = { -0.5f, -0.5f, -0.24f, -0.5f, -0.24f, -0.025f, -0.5f, -0.5f, -0.35f, -0.016f };
    private readonly string[] shapeNames = { "CIRCLE", "TRIANGLE", "SQUARE", "PENTAGON", "HEXAGON", "OCTAGON", "HEART", "STAR", "CRESCENT", "CROSS" };
    private readonly string[] dirNames = { "N", "NE", "SE", "S", "SW", "NW" };
    private int[] yellowRC;
    void Awake()
    {
        moduleSolved = false;
        moduleId = moduleIdCounter++;
        string yellowShapes = new string("0123456789".ToCharArray().Shuffle()).Substring(0, 7);
        for (int i = 0; i < 7; i++)
        {
            buttonMF[i].mesh = shapes[yellowShapes[i] - '0'].sharedMesh;
            highlightMF[i].mesh = shapes[yellowShapes[i] - '0'].sharedMesh;
            highlightTF[i].transform.localScale = new Vector3(shapeHLSizes[yellowShapes[i] - '0'], 0.01f, shapeHLSizes[yellowShapes[i] - '0']);
            highlightTF[i].transform.localPosition = new Vector3(0f, shapeHLPositions[yellowShapes[i] - '0'], 0f);
            hexButtons[i].transform.localScale = new Vector3(shapeSizes[yellowShapes[i] - '0'][0], shapeSizes[yellowShapes[i] - '0'][1], shapeSizes[yellowShapes[i] - '0'][2]);
            Debug.LogFormat("[Yellow Hexabuttons #{0}] {1} button is a {2}", moduleId, positions[i], shapeNames[yellowShapes[i] - '0']);
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
        Debug.LogFormat("[Yellow Hexabuttons #{0}] Button Order: {1}", moduleId, buttonPriority);
        foreach (int i in buttonIndex)
        {
            hexButtons[i].OnInteract = delegate { pressedButton(i, order[i]); return false; };
            hexButtons[i].OnInteractEnded = delegate { releaseButton(i); };
        }
        hexButtons[6].OnInteract = delegate { pressedCenter(); return false; };
        hexButtons[7].OnInteract = delegate { deafMode = !(deafMode); return false; };
        voiceMessage = new string[4];
        yellowRC = new int[2];
        string numberPos = UnityEngine.Random.Range(1, 92) + "";
        if (numberPos.Length < 2)
            numberPos = "0" + numberPos;
        voiceMessage[2] = numberPos[0] + "";
        voiceMessage[3] = numberPos[1] + "";
        Debug.LogFormat("[Yellow Hexabuttons #{0}] Goal Space: {1}{2}", moduleId, voiceMessage[2], voiceMessage[3]);
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
                possdir += "0";//N
            if (tempMaze[yellowRC[0] - 1][yellowRC[1] + 1].Equals("O"))
                possdir += "1";//NE
            if (tempMaze[yellowRC[0] + 1][yellowRC[1] + 1].Equals("O"))
                possdir += "2";//SE
            if (tempMaze[yellowRC[0] + 1][yellowRC[1]].Equals("O"))
                possdir += "3";//S
            if (tempMaze[yellowRC[0] + 1][yellowRC[1] - 1].Equals("O"))
                possdir += "4";//SW
            if (tempMaze[yellowRC[0] - 1][yellowRC[1] - 1].Equals("O"))
                possdir += "5";//NW
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
        Debug.LogFormat("[Yellow Hexabuttons #{0}] Current Space: {1}{2}", moduleId, voiceMessage[0], voiceMessage[1]);
    }
    void pressedButton(int n, int d)
    {
        Debug.LogFormat("[Yellow Hexabuttons #{0}] User pressed {1}, which is {2}.", moduleId, positions[n], dirNames[d]);
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        Vector3 pos = buttonMesh[n].transform.localPosition;
        pos.y = 0.0126f;
        buttonMesh[n].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
        switch (d)
        {
            case 0://N
                if (yellowMaze[yellowRC[0] - 1][yellowRC[1]].Equals("W"))
                {
                    Debug.LogFormat("[Yellow Hexabuttons #{0}] Strike! There is a wall to the N of {1}{2}", moduleId, voiceMessage[0], voiceMessage[1]);
                    module.HandleStrike();
                }
                else
                {
                    yellowRC[0] -= 4;
                    voiceMessage[0] = yellowMaze[yellowRC[0]][yellowRC[1]][0] + "";
                    voiceMessage[1] = yellowMaze[yellowRC[0]][yellowRC[1]][1] + "";
                    Debug.LogFormat("[Yellow Hexabuttons #{0}] Moving N, current space is now {1}{2}", moduleId, voiceMessage[0], voiceMessage[1]);
                }
                break;
            case 1://NE
                if (yellowMaze[yellowRC[0] - 1][yellowRC[1] + 1].Equals("W"))
                {
                    Debug.LogFormat("[Yellow Hexabuttons #{0}] Strike! There is a wall to the NE of {1}{2}", moduleId, voiceMessage[0], voiceMessage[1]);
                    module.HandleStrike();
                }
                else
                {
                    yellowRC[0] -= 2;
                    yellowRC[1] += 2;
                    voiceMessage[0] = yellowMaze[yellowRC[0]][yellowRC[1]][0] + "";
                    voiceMessage[1] = yellowMaze[yellowRC[0]][yellowRC[1]][1] + "";
                    Debug.LogFormat("[Yellow Hexabuttons #{0}] Moving NE, current space is now {1}{2}", moduleId, voiceMessage[0], voiceMessage[1]);
                }
                break;
            case 2://SE
                if (yellowMaze[yellowRC[0] + 1][yellowRC[1] + 1].Equals("W"))
                {
                    Debug.LogFormat("[Yellow Hexabuttons #{0}] Strike! There is a wall to the SE of {1}{2}", moduleId, voiceMessage[0], voiceMessage[1]);
                    module.HandleStrike();
                }
                else
                {
                    yellowRC[0] += 2;
                    yellowRC[1] += 2;
                    voiceMessage[0] = yellowMaze[yellowRC[0]][yellowRC[1]][0] + "";
                    voiceMessage[1] = yellowMaze[yellowRC[0]][yellowRC[1]][1] + "";
                    Debug.LogFormat("[Yellow Hexabuttons #{0}] Moving SE, current space is now {1}{2}", moduleId, voiceMessage[0], voiceMessage[1]);
                }
                break;
            case 3://S
                if (yellowMaze[yellowRC[0] + 1][yellowRC[1]].Equals("W"))
                {
                    Debug.LogFormat("[Yellow Hexabuttons #{0}] Strike! There is a wall to the S of {1}{2}", moduleId, voiceMessage[0], voiceMessage[1]);
                    module.HandleStrike();
                }
                else
                {
                    yellowRC[0] += 4;
                    voiceMessage[0] = yellowMaze[yellowRC[0]][yellowRC[1]][0] + "";
                    voiceMessage[1] = yellowMaze[yellowRC[0]][yellowRC[1]][1] + "";
                    Debug.LogFormat("[Yellow Hexabuttons #{0}] Moving S, current space is now {1}{2}", moduleId, voiceMessage[0], voiceMessage[1]);
                }
                break;
            case 4://SW
                if (yellowMaze[yellowRC[0] + 1][yellowRC[1] - 1].Equals("W"))
                {
                    Debug.LogFormat("[Yellow Hexabuttons #{0}] Strike! There is a wall to the SW of {1}{2}", moduleId, voiceMessage[0], voiceMessage[1]);
                    module.HandleStrike();
                }
                else
                {
                    yellowRC[0] += 2;
                    yellowRC[1] -= 2;
                    voiceMessage[0] = yellowMaze[yellowRC[0]][yellowRC[1]][0] + "";
                    voiceMessage[1] = yellowMaze[yellowRC[0]][yellowRC[1]][1] + "";
                    Debug.LogFormat("[Yellow Hexabuttons #{0}] Moving SW, current space is now {1}{2}", moduleId, voiceMessage[0], voiceMessage[1]);
                }
                break;
            default://NW
                if (yellowMaze[yellowRC[0] - 1][yellowRC[1] - 1].Equals("W"))
                {
                    Debug.LogFormat("[Yellow Hexabuttons #{0}] Strike! There is a wall to the NW of {1}{2}", moduleId, voiceMessage[0], voiceMessage[1]);
                    module.HandleStrike();
                }
                else
                {
                    yellowRC[0] -= 2;
                    yellowRC[1] -= 2;
                    voiceMessage[0] = yellowMaze[yellowRC[0]][yellowRC[1]][0] + "";
                    voiceMessage[1] = yellowMaze[yellowRC[0]][yellowRC[1]][1] + "";
                    Debug.LogFormat("[Yellow Hexabuttons #{0}] Moving NW, current space is now {1}{2}", moduleId, voiceMessage[0], voiceMessage[1]);
                }
                break;
        }
        if (voiceMessage[0] == voiceMessage[2] && voiceMessage[1] == voiceMessage[3])
        {
            moduleSolved = true;
            foreach (int index in buttonIndex)
            {
                hexButtons[index].OnInteract = null;
                hexButtons[index].OnInteractEnded = null;
            }
            hexButtons[6].OnInteract = null;
            hexButtons[7].OnInteract = null;
            module.HandlePass();
        }
    }
    void releaseButton(int n)
    {
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonRelease, transform);
        Vector3 pos = buttonMesh[n].transform.localPosition;
        pos.y = 0.0169f;
        buttonMesh[n].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
    }
    void pressedCenter()
    {
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        Vector3 pos = buttonMesh[6].transform.localPosition;
        pos.y = 0.0126f;
        buttonMesh[6].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
        StartCoroutine(playAudio());
    }
    IEnumerator playAudio()
    {
        hexButtons[6].OnInteract = null;
        yield return new WaitForSeconds(0.5f);
        for (int aa = 0; aa < voiceMessage.Length; aa++)
        {
            Audio.PlaySoundAtTransform(voiceMessage[aa], transform);
            if (deafMode)
                centerText.text = voiceMessage[aa] + "";
            yield return new WaitForSeconds(1.2f);
            centerText.text = "";
            yield return new WaitForSeconds(0.3f);
        }
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonRelease, transform);
        Vector3 pos = buttonMesh[6].transform.localPosition;
        pos.y = 0.0169f;
        buttonMesh[6].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
        if (!(moduleSolved))
            hexButtons[6].OnInteract = delegate { pressedCenter(); return false; };
    }
#pragma warning disable 414
    private readonly string TwitchHelpMessage = @"!{0} press|p tl/1 tr/2 ml/3 mr/4 bl/5 br/6 c/7 sl presses the top-left, top-right, middle-left, middle-right, bottom-left, bottom-right, center, and the status light in that order.";
#pragma warning restore 414
    IEnumerator ProcessTwitchCommand(string command)
    {
        string[] param = command.ToUpper().Split(' ');
        if ((Regex.IsMatch(param[0], @"^\s*PRESS\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant) || Regex.IsMatch(param[0], @"^\s*P\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant)) && param.Length > 1)
        {
            if (isPos(param))
            {
                yield return null;
                for (int i = 1; i < param.Length; i++)
                {
                    int cursor;
                    switch (param[i])
                    {
                        case "TL":
                        case "1":
                            cursor = 0;
                            break;
                        case "TR":
                        case "2":
                            cursor = 1;
                            break;
                        case "ML":
                        case "3":
                            cursor = 2;
                            break;
                        case "MR":
                        case "4":
                            cursor = 3;
                            break;
                        case "BL":
                        case "5":
                            cursor = 4;
                            break;
                        case "BR":
                        case "6":
                            cursor = 5;
                            break;
                        case "C":
                        case "7":
                            cursor = 6;
                            break;
                        default:
                            cursor = 7;
                            break;
                    }
                    if (hexButtons[cursor].OnInteract != null)
                    {
                        hexButtons[cursor].OnInteract();
                        yield return new WaitForSeconds(0.2f);
                        if (hexButtons[cursor].OnInteractEnded != null)
                        {
                            hexButtons[cursor].OnInteractEnded();
                            yield return new WaitForSeconds(0.2f);
                        }
                    }
                }
            }
        }
    }
    private bool isPos(string[] param)
    {
        for (int aa = 1; aa < param.Length; aa++)
        {
            switch (param[aa])
            {
                case "TL":
                case "TR":
                case "ML":
                case "MR":
                case "BL":
                case "BR":
                case "C":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "SL":
                    break;
                default:
                    return false;
            }
        }
        return true;
    }
    IEnumerator TwitchHandleForcedSolve()
    {
        var q = new Queue<int[]>();
        var allMoves = new List<Movement>();
        var startPoint = new int[] { yellowRC[0], yellowRC[1] };
        var target = new int[2];
        q.Enqueue(startPoint);
        while (q.Count > 0)
        {
            var next = q.Dequeue();
            if (yellowMaze[next[0]][next[1]] == voiceMessage[2] + voiceMessage[3])
            {
                target[0] = next[0];
                target[1] = next[1];
                goto readyToSubmit;
            }
            var offsets = new int[,] { { -1, 0 }, { -1, 1 }, { 1, 1 }, { 1, 0 }, { 1, -1 }, { -1, -1 } };
            var offsets2 = new int[,] { { -4, 0 }, { -2, 2 }, { 2, 2 }, { 4, 0 }, { 2, -2 }, { -2, -2 } };
            for (int i = 0; i < 6; i++)
            {
                var check = new int[] { next[0] + offsets[order[i], 0], next[1] + offsets[order[i], 1] };
                var check2 = new int[] { next[0] + offsets2[order[i], 0], next[1] + offsets2[order[i], 1] };
                if (!yellowMaze[check[0]][check[1]].Equals("W") && !allMoves.Any(x => x.start[0] == check2[0] && x.start[1] == check2[1]))
                {
                    q.Enqueue(check2);
                    allMoves.Add(new Movement { start = next, end = check2, direction = i });
                }
            }
        }
        throw new InvalidOperationException("There is a bug in Yellow Hexabutton's TP autosolver.");
        readyToSubmit:
        KMSelectable[] hexBtns = new KMSelectable[] { hexButtons[0], hexButtons[1], hexButtons[2], hexButtons[3], hexButtons[4], hexButtons[5] };
        var lastMove = allMoves.First(x => x.end[0] == target[0] && x.end[1] == target[1]);
        var relevantMoves = new List<Movement> { lastMove };
        while (lastMove.start != startPoint)
        {
            lastMove = allMoves.First(x => x.end[0] == lastMove.start[0] && x.end[1] == lastMove.start[1]);
            relevantMoves.Add(lastMove);
        }
        for (int i = 0; i < relevantMoves.Count; i++)
        {
            hexBtns[relevantMoves[relevantMoves.Count - 1 - i].direction].OnInteract();
            yield return new WaitForSeconds(.2f);
            if (hexBtns[relevantMoves[relevantMoves.Count - 1 - i].direction].OnInteractEnded != null)
            {
                hexBtns[relevantMoves[relevantMoves.Count - 1 - i].direction].OnInteractEnded();
                yield return new WaitForSeconds(.2f);
            }
        }
    }
    class Movement
    {
        public int[] start;
        public int[] end;
        public int direction;
    }
}
