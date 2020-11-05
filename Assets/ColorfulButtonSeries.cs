using System;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class ColorfulButtonSeries : MonoBehaviour
{
	private static int moduleIdCounter;
	private int moduleId;

	private bool moduleSolved;
	public KMBombModule module;
	public KMAudio Audio;
	public KMBombInfo bomb;

	public Material[] buttonColors;
	public MeshRenderer[] buttonMesh;
	public KMSelectable[] hexButtons;
	public TextMesh[] buttonText;
	public MeshFilter[] buttonMF;
	public MeshFilter[] highlightMF;
	public Transform[] highlightTF;
	public Material[] ledColors;
	public MeshRenderer[] ledMesh;
	public Light[] ledLights;
	public Light[] flashLights;
	public AudioClip[] alphabet;
	public AudioClip[] numbers;
	public AudioClip[] ciphers;
	public MeshFilter[] shapes;
	public AudioClip[] notes;
	private string TPOrder;
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
	private float[] shapeHLSizes =
	{
		1.045f,
		1.08f,
		1.06f,
		1.06f,
		1.04f,
		1.05f,
		1.06f,
		1.12f,
		1.05f,
		1.05f
	};
	private float[] shapeHLPositions =
	{
		-0.5f,
		-0.5f,
		-0.24f,
		-0.5f,
		-0.24f,
		-0.025f,
		-0.5f,
		-0.5f,
		-0.35f,
		-0.016f
	};
	private int colorIndex;
	private int[] buttonIndex = { 0, 1, 2, 3, 4, 5 };
	private string[] positions = { "TL", "TR", "ML", "MR", "BL", "BR", "C"};
	private string[] shapeNames = {"CIRCLE", "TRIANGLE", "SQUARE", "PENTAGON", "HEXAGON", "OCTAGON", "HEART", "STAR", "CRESCENT", "CROSS"};
	private string[] dirNames = {"N", "NE", "SE", "S", "SW", "NW"};
	private int[] redSolution;
	private string orangeSolution;
	private string orangeScramble;
	private string yellowShapes;
	private int[] yellowRC;
	private string greenSolution;
	private string greenNotes;
	private bool greenSwitch;
	private int[] blueSolution;
	private int[] blueRotations;
	private int[] blueButtonValues;
	private string blueButtonText;
	private int[] purpleSolution;
	private string whiteBHC;
	private int[] whiteFlashes;
	private string whiteSolution;
	private int numButtonPresses;
	private string[] buttonCenterText;
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
	void Awake()
    {
		TPOrder = "0123456";
		moduleId = moduleIdCounter++;
		moduleSolved = false;
		colorIndex = UnityEngine.Random.Range(0, 7);
		for(int aa = 0; aa < 7; aa++)
		{
			buttonMesh[aa].material = buttonColors[colorIndex];
			ledLights[aa].enabled = false;
			if (aa < 6)
				flashLights[aa].enabled = false;
		}
		switch (colorIndex)
		{
			case 0:
				red();
				break;
			case 1:
				orange();
				break;
			case 2:
				yellow();
				break;
			case 3:
				green();
				break;
			case 4:
				blue();
				break;
			case 5:
				purple();
				break;
			default:
				white();
				break;
		}
	}
	void Start()
	{

	}
	void red()
	{
		Debug.LogFormat("[Colored Hexabuttons #{0}] Color Generated: Red", moduleId);
		foreach (int i in buttonIndex)
			hexButtons[i].OnInteract = delegate { pressedRed(buttonIndex[i]); return false; };
		hexButtons[6].OnInteract = delegate { pressedRedCenter(); return false; };
		hexButtons[6].OnInteractEnded = delegate { pressedRedCenterRelease(); };
		int[][] chart =
		{
			new int[6]{2, 4, 5, 1, 0, 3},
			new int[6]{1, 5, 3, 0, 2, 4},
			new int[6]{5, 4, 3, 1, 2, 0},
			new int[6]{1, 0, 2, 3, 4, 5},
			new int[6]{3, 0, 1, 2, 5, 4},
			new int[6]{5, 0, 1, 2, 4, 3},
			new int[6]{4, 2, 3, 5, 0, 1},
			new int[6]{0, 4, 5, 1, 3, 2},
			new int[6]{0, 5, 3, 2, 1, 4},
			new int[6]{4, 3, 5, 1, 2, 0},
			new int[6]{2, 0, 3, 4, 5, 1},
			new int[6]{2, 5, 4, 3, 1, 0},
			new int[6]{3, 1, 0, 4, 2, 5},
			new int[6]{1, 4, 5, 2, 3, 0},
			new int[6]{3, 2, 4, 5, 0, 1},
			new int[6]{5, 2, 4, 3, 0, 1},
			new int[6]{5, 3, 2, 0, 1, 4},
			new int[6]{5, 1, 0, 4, 3, 2},
			new int[6]{4, 0, 1, 2, 3, 5},
			new int[6]{1, 3, 4, 5, 0, 2},
			new int[6]{3, 5, 2, 1, 4, 0},
			new int[6]{0, 1, 2, 3, 4, 5},
			new int[6]{0, 2, 1, 4, 5, 3},
			new int[6]{2, 1, 0, 5, 3, 4},
			new int[6]{4, 1, 0, 3, 5, 2},
			new int[6]{0, 3, 4, 5, 2, 1}
		};
		int pos1 = UnityEngine.Random.Range(0, chart.Length);
		int pos2 = UnityEngine.Random.Range(0, 6);
		buttonText[6].color = Color.white;
		buttonCenterText = new string[2] {"ABCDEFGHIJKLMNOPQRSTUVWXYZ"[pos1] + "", (pos2 + 1) + ""};
		Debug.LogFormat("[Colored Hexabuttons #{0}] Generated Message: {1}{2}", moduleId, buttonCenterText[0], buttonCenterText[1]);
		redSolution = new int[6];
		string output = "";
		for (int aa = 0; aa < 6; aa++)
		{
			redSolution[aa] = chart[pos1][(pos2 + aa) % 6];
			output = output + "" + positions[redSolution[aa]] + " ";
		}
		Debug.LogFormat("[Colored Hexabuttons #{0}] Solution: {1}", moduleId, output);
		numButtonPresses = 0;
	}
	void pressedRed(int n)
	{
		if (!(moduleSolved))
		{
			Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
			Debug.LogFormat("[Colored Hexabuttons #{0}] User pressed {1}", moduleId, positions[n]);
			if (redSolution[numButtonPresses] == n)
			{
				Vector3 pos = buttonMesh[n].transform.localPosition;
				pos.y = 0.0126f;
				buttonMesh[n].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
				hexButtons[n].OnInteract = null;
				ledMesh[n].material = ledColors[1];
				ledLights[n].enabled = true;
				if (numButtonPresses == 5)
				{
					moduleSolved = true;
					module.HandlePass();
				}
				else
					numButtonPresses++;
			}
			else
			{
				Debug.LogFormat("[Colored Hexabuttons #{0}] Strike! I was expecting {1}!", moduleId, positions[redSolution[numButtonPresses]]);
				module.HandleStrike();
				Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.Strike, transform);
				for (int aa = 0; aa < 6; aa++)
				{
					Vector3 pos = buttonMesh[aa].transform.localPosition;
					pos.y = 0.0169f;
					buttonMesh[aa].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
					ledMesh[aa].material = ledColors[0];
					ledLights[aa].enabled = false;
				}
				foreach (int i in buttonIndex)
					hexButtons[i].OnInteract = delegate { pressedRed(buttonIndex[i]); return false; };
				numButtonPresses = 0;
			}
		}
	}
	void pressedRedCenter()
	{
		if (!(moduleSolved))
		{
			Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
			Vector3 pos = buttonMesh[6].transform.localPosition;
			pos.y = 0.0126f;
			buttonMesh[6].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
		}
	}
	void pressedRedCenterRelease()
	{
		if (!(moduleSolved))
		{
			Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonRelease, transform);
			Vector3 pos = buttonMesh[6].transform.localPosition;
			pos.y = 0.0169f;
			buttonMesh[6].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
			StartCoroutine(showCenter());
		}
	}
	IEnumerator showCenter()
	{
		yield return new WaitForSeconds(0.5f);
		for(int aa = 0; aa < buttonCenterText.Length; aa++)
		{
			Audio.PlaySoundAtTransform(buttonCenterText[aa], transform);
			yield return new WaitForSeconds(1.5f);
		}
	}
	void orange()
	{
		Debug.LogFormat("[Colored Hexabuttons #{0}] Color Generated: Orange", moduleId);
		string[] wordList =
			{
					"ABACUS", "ACTION", "ADVICE", "AFFECT", "AGENCY", "ALMOND", "AMOUNT", "ANARCH", "APPEAR", "ARRIVE",
					"BALLAD", "BAKERY", "BEACON", "BINARY", "BLEACH", "BRONZE", "BOXING", "BREEZE", "BELIEF", "BITTER",
					"CACTUS", "CEREAL", "CHERRY", "CITRUS", "CLOSET", "COFFEE", "CRISIS", "CURSOR", "CONVEX", "CELLAR",
					"DANGER", "DEBRIS", "DINNER", "DOODLE", "DRIVER", "DUSTER", "DEFEAT", "DIRECT", "DOMINO", "DRAWER",
					"EASTER", "EDITOR", "EFFECT", "EGGNOG", "EMBLEM", "ENROLL", "EQUALS", "ERASER", "ESCAPE", "EXPERT",
					"FABRIC", "FELINE", "FILTER", "FLAVOR", "FOREST", "FREEZE", "FUTURE", "FACADE", "FOLLOW", "FINISH",
					"GALLON", "GEYSER", "GALAXY", "GLANCE", "GROWTH", "GUTTER", "GAMBLE", "GERBIL", "GINGER", "GIVING",
					"HAMMER", "HEIGHT", "HIDING", "HOLLOW", "HUNTER", "HYBRID", "HANDLE", "HELMET", "HAZARD", "HURDLE",
					"ICICLE", "IMPORT", "INSERT", "ITALIC", "IMPAIR", "INCOME", "IMPACT", "INSULT", "INSECT", "INTENT",
					"JESTER", "JINGLE", "JOGGER", "JUNGLE", "JERSEY", "JOCKEY", "JUGGLE", "JUMBLE", "JUNIOR", "JAILER",
					"KETTLE", "KIDNEY", "KNIGHT", "KENNEL", "KINGLY", "KITTEN", "KRAKEN", "KINDLY", "KERNEL", "KEEPER",
					"LAGOON", "LEADER", "LIMBER", "LOCKET", "LUXURY", "LYCHEE", "LADDER", "LEGACY", "LIQUID", "LOTION",
					"MAGNET", "MEADOW", "MIDDLE", "MOMENT", "MUSEUM", "MYSTIC", "MATRIX", "MELODY", "MIRROR", "MUFFIN",
					"NAPKIN", "NEEDLE", "NICKEL", "NOBODY", "NUTMEG", "NATION", "NECTAR", "NINETY", "NOTICE", "NARROW",
					"OBJECT", "OCELOT", "OFFICE", "OPTION", "ORANGE", "OUTPUT", "OXYGEN", "OYSTER", "OFFSET", "OUTFIT",
					"PALACE", "PEBBLE", "PICNIC", "PLAQUE", "POCKET", "PROFIT", "PUDDLE", "PENCIL", "PIGEON", "POETRY",
					"QUARTZ", "QUIVER", "QUARRY", "QUEASY", "RABBIT", "REFLEX", "RHYTHM", "RIBBON", "ROCKET", "RAFFLE",
					"RECIPE", "RUBBER", "RADIUS", "RECORD", "SAILOR", "SCHEME", "SEARCH", "SHADOW", "SIGNAL", "SLEIGH",
					"SMUDGE", "SNEEZE", "SOCIAL", "SQUEAK", "TAILOR", "TEACUP", "THIRST", "TICKET", "TOGGLE", "TRAVEL",
					"TUNNEL", "TWITCH", "TEMPLE", "THEORY", "UNISON", "UPWARD", "UTMOST", "UTOPIA", "UNIQUE", "UNREST",
					"UNSEEN", "UNWRAP", "UNVIEL", "UPHOLD", "VACUUM", "VECTOR", "VIEWER", "VORTEX", "VALLEY", "VERBAL",
					"VICTIM", "VOLUME", "VANISH", "VERMIN", "WAFFLE", "WEALTH", "WHEEZE", "WIDGET", "WOLVES", "WRENCH",
					"WALNUT", "WEIGHT", "WISDOM", "WONDER", "YEARLY", "YELLOW", "YONDER", "ZEALOT", "ZEBRAS", "ZODIAC"
			};
		string temp = wordList[UnityEngine.Random.Range(0, wordList.Length)].ToUpper();
		orangeSolution = temp.ToUpper();
		Debug.LogFormat("[Colored Hexabuttons #{0}] Generated Word: {1}", moduleId, orangeSolution);
		orangeScramble = "";
		for(int aa = 0; aa < 6; aa++)
		{
			int pos = UnityEngine.Random.Range(0, temp.Length);
			orangeScramble = orangeScramble + "" + temp[pos];
			temp = temp.Remove(pos, 1);
		}
		Debug.LogFormat("[Colored Hexabuttons #{0}] Scrambled Word: {1}", moduleId, orangeScramble);
		foreach (int i in buttonIndex)
			hexButtons[i].OnInteract = delegate { pressedOrange(i); return false; };
		hexButtons[6].OnInteract = delegate { pressedRedCenter(); return false; };
		hexButtons[6].OnInteractEnded = delegate { pressedRedCenterRelease(); };
		string alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		switch(UnityEngine.Random.Range(0, 7))
		{
			case 0://Atbash

				buttonCenterText = new string[1];
				buttonCenterText[0] = "ATBASH";
				for (int aa = 0; aa < 6; aa++)
				{
					buttonText[aa].color = Color.black;
					buttonText[aa].text = alpha[25 - alpha.IndexOf(orangeScramble[aa])] + "";
				}
				Debug.LogFormat("[Colored Hexabuttons #{0}] Encryption using ATBASH: {1}{2}{3}{4}{5}{6}", moduleId, buttonText[0].text, buttonText[1].text, buttonText[2].text, buttonText[3].text, buttonText[4].text, buttonText[5].text);
				break;
			case 1://Caesar
				buttonCenterText = new string[3];
				buttonCenterText[0] = "CAESAR";
				int r1 = UnityEngine.Random.Range(1, 26);
				if(r1 < 10)
				{
					buttonCenterText[1] = "0";
					buttonCenterText[2] = r1 + "";
				}
				else
				{
					buttonCenterText[1] = (r1 / 10) + "";
					buttonCenterText[2] = (r1 % 10) + "";
				}
				for (int aa = 0; aa < 6; aa++)
				{
					buttonText[aa].color = Color.black;
					buttonText[aa].text = alpha[mod(alpha.IndexOf(orangeScramble[aa]) - r1, 26)] + "";
				}
				Debug.LogFormat("[Colored Hexabuttons #{0}] Encryption using CAESAR {1}{2}: {3}{4}{5}{6}{7}{8}", moduleId, buttonCenterText[1], buttonCenterText[2], buttonText[0].text, buttonText[1].text, buttonText[2].text, buttonText[3].text, buttonText[4].text, buttonText[5].text);
				break;
			case 2://Condi
				buttonCenterText = new string[3];
				buttonCenterText[0] = "CONDI";
				int r2 = UnityEngine.Random.Range(1, 26);
				if (r2 < 10)
				{
					buttonCenterText[1] = "0";
					buttonCenterText[2] = r2 + "";
				}
				else
				{
					buttonCenterText[1] = (r2 / 10) + "";
					buttonCenterText[2] = (r2 % 10) + "";
				}
				for (int aa = 0; aa < 6; aa++)
				{
					buttonText[aa].color = Color.black;
					buttonText[aa].text = alpha[mod(alpha.IndexOf(orangeScramble[aa]) + r2, 26)] + "";
					r2 = alpha.IndexOf(orangeScramble[aa]) + 1;
				}
				Debug.LogFormat("[Colored Hexabuttons #{0}] Encryption using CONDI {1}{2}: {3}{4}{5}{6}{7}{8}", moduleId, buttonCenterText[1], buttonCenterText[2], buttonText[0].text, buttonText[1].text, buttonText[2].text, buttonText[3].text, buttonText[4].text, buttonText[5].text);
				break;
			case 3://Monoalphabetic
				buttonCenterText = new string[7];
				buttonCenterText[0] = "MONOALPHABETIC";
				temp = alpha.ToUpper();
				for(int aa = 1; aa < 7; aa++)
				{
					buttonCenterText[aa] = temp[UnityEngine.Random.Range(0, temp.Length)] + "";
					temp = temp.Replace(buttonCenterText[aa], "");
				}
				for (int aa = 6; aa >= 1; aa--)
					temp = buttonCenterText[aa] + "" + temp;
				for (int aa = 0; aa < 6; aa++)
				{
					buttonText[aa].color = Color.black;
					buttonText[aa].text = temp[alpha.IndexOf(orangeScramble[aa])] + "";
				}
				Debug.LogFormat("[Colored Hexabuttons #{0}] Encryption using MONOALPHABETIC {1}{2}{3}{4}{5}{6}: {7}{8}{9}{10}{11}{12}", moduleId, buttonCenterText[1], buttonCenterText[2], buttonCenterText[3], buttonCenterText[4], buttonCenterText[5], buttonCenterText[6], buttonText[0].text, buttonText[1].text, buttonText[2].text, buttonText[3].text, buttonText[4].text, buttonText[5].text);
				break;
			case 4://Porta
				buttonCenterText = new string[7];
				buttonCenterText[0] = "PORTA";
				temp = alpha.ToUpper();
				for (int aa = 1; aa < 7; aa++)
				{
					buttonCenterText[aa] = temp[UnityEngine.Random.Range(0, temp.Length)] + "";
					temp = temp.Replace(buttonCenterText[aa], "");
				}
				string[] portaChart =
				{
					"NOPQRSTUVWXYZ",
					"OPQRSTUVWXYZN",
					"PQRSTUVWXYZNO",
					"QRSTUVWXYZNOP",
					"RSTUVWXYZNOPQ",
					"STUVWXYZNOPQR",
					"TUVWXYZNOPQRS",
					"UVWXYZNOPQRST",
					"VWXYZNOPQRSTU",
					"WXYZNOPQRSTUV",
					"XYZNOPQRSTUVW",
					"YZNOPQRSTUVWX",
					"ZNOPQRSTUVWXY"
				};
				for(int aa = 0; aa < 6; aa++)
				{
					buttonText[aa].color = Color.black;
					if (alpha.IndexOf(orangeScramble[aa]) < 13)
						buttonText[aa].text = portaChart[alpha.IndexOf(buttonCenterText[aa + 1]) / 2][alpha.IndexOf(orangeScramble[aa])] + "";
					else
						buttonText[aa].text = alpha[portaChart[alpha.IndexOf(buttonCenterText[aa + 1]) / 2].IndexOf(orangeScramble[aa])] + "";
				}
				Debug.LogFormat("[Colored Hexabuttons #{0}] Encryption using PORTA {1}{2}{3}{4}{5}{6}: {7}{8}{9}{10}{11}{12}", moduleId, buttonCenterText[1], buttonCenterText[2], buttonCenterText[3], buttonCenterText[4], buttonCenterText[5], buttonCenterText[6], buttonText[0].text, buttonText[1].text, buttonText[2].text, buttonText[3].text, buttonText[4].text, buttonText[5].text);
				break;
			case 5://Ragbaby
				buttonCenterText = new string[3];
				buttonCenterText[0] = "RAGBABY";
				int r3 = UnityEngine.Random.Range(0, 26);
				if (r3 < 10)
				{
					buttonCenterText[1] = "0";
					buttonCenterText[2] = r3 + "";
				}
				else
				{
					buttonCenterText[1] = (r3 / 10) + "";
					buttonCenterText[2] = (r3 % 10) + "";
				}
				for(int aa = 0; aa < 6; aa++)
				{
					buttonText[aa].color = Color.black;
					buttonText[aa].text = alpha[mod(alpha.IndexOf(orangeScramble[aa]) + r3, 26)] + "";
					r3++;
				}
				Debug.LogFormat("[Colored Hexabuttons #{0}] Encryption using RAGBABY {1}{2}: {3}{4}{5}{6}{7}{8}", moduleId, buttonCenterText[1], buttonCenterText[2], buttonText[0].text, buttonText[1].text, buttonText[2].text, buttonText[3].text, buttonText[4].text, buttonText[5].text);
				break;
			default://Vigenere
				buttonCenterText = new string[7];
				buttonCenterText[0] = "VIGENERE";
				temp = alpha.ToUpper();
				string alpha2 = "-ABCDEFGHIJKLMNOPQRSTUVWXYZ";
				for (int aa = 1; aa < 7; aa++)
				{
					buttonCenterText[aa] = temp[UnityEngine.Random.Range(0, temp.Length)] + "";
					temp = temp.Replace(buttonCenterText[aa], "");
					int r4 = alpha2.IndexOf(orangeScramble[aa - 1]) + alpha2.IndexOf(buttonCenterText[aa]);
					while (r4 > 26)
						r4 -= 26;
					buttonText[aa - 1].color = Color.black;
					buttonText[aa - 1].text = alpha2[r4] + "";
				}
				Debug.LogFormat("[Colored Hexabuttons #{0}] Encryption using VIGENERE {1}{2}{3}{4}{5}{6}: {7}{8}{9}{10}{11}{12}", moduleId, buttonCenterText[1], buttonCenterText[2], buttonCenterText[3], buttonCenterText[4], buttonCenterText[5], buttonCenterText[6], buttonText[0].text, buttonText[1].text, buttonText[2].text, buttonText[3].text, buttonText[4].text, buttonText[5].text);
				break;
		}
	}
	void pressedOrange(int n)
	{
		if (!(moduleSolved))
		{
			Debug.LogFormat("[Colored Hexabuttons #{0}] User pressed {1}. Which is the decrypted letter {2}.", moduleId, positions[n], orangeScramble[n]);
			Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
			if(orangeSolution[numButtonPresses] == orangeScramble[n])
			{
				Vector3 pos = buttonMesh[n].transform.localPosition;
				pos.y = 0.0126f;
				buttonMesh[n].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
				hexButtons[n].OnInteract = null;
				ledMesh[n].material = ledColors[1];
				ledLights[n].enabled = true;
				if (numButtonPresses == 5)
				{
					moduleSolved = true;
					module.HandlePass();
				}
				else
					numButtonPresses++;
			}
			else
			{
				Debug.LogFormat("[Colored Hexabuttons #{0}] Strike! I was expecting {1}!", moduleId, orangeSolution[numButtonPresses]);
				module.HandleStrike();
				Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.Strike, transform);
				for (int aa = 0; aa < 6; aa++)
				{
					Vector3 pos = buttonMesh[aa].transform.localPosition;
					pos.y = 0.0169f;
					buttonMesh[aa].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
					ledMesh[aa].material = ledColors[0];
					ledLights[aa].enabled = false;
				}
				foreach (int i in buttonIndex)
					hexButtons[i].OnInteract = delegate { pressedOrange(i); return false; };
				numButtonPresses = 0;
			}
		}
	}
	void yellow()
	{
		Debug.LogFormat("[Colored Hexabuttons #{0}] Color Generated: Yellow", moduleId);
		string possible = "0123456789";
		yellowShapes = "";
		for(int i = 0; i < 7; i++)
		{
			if(i < 6)
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
		for(int aa = 0; aa < 9; aa++)
		{
			if (yellowShapes.IndexOf(priorityList[yellowShapes[6] - '0'][aa]) >= 0)
			{
				int ind = yellowShapes.IndexOf(priorityList[yellowShapes[6] - '0'][aa]);
				order[ind] = accum + 0;
				buttonPriority = buttonPriority + "" + positions[ind] + " ";
				accum++;
			}
			if(accum == 6)
				break;
		}
		Debug.LogFormat("[Colored Hexabuttons #{0}] Button Order: {1}", moduleId, buttonPriority);
		foreach (int i in buttonIndex)
		{
			hexButtons[i].OnInteract = delegate { pressedYellow(i, order[i]); return false; };
			hexButtons[i].OnInteractEnded = delegate { pressedYellowRelease(i); };
		}
		hexButtons[6].OnInteract = delegate { pressedRedCenter(); return false; };
		hexButtons[6].OnInteractEnded = delegate { pressedRedCenterRelease();};
		buttonCenterText = new string[4];
		yellowRC = new int[2];
		string numberPos = UnityEngine.Random.Range(1, 92) + "";
		if (numberPos.Length < 2)
			numberPos = "0" + numberPos;
		buttonCenterText[2] = numberPos[0] + "";
		buttonCenterText[3] = numberPos[1] + "";
		Debug.LogFormat("[Colored Hexabuttons #{0}] Goal Space: {1}{2}", moduleId, buttonCenterText[2], buttonCenterText[3]);
		bool flag = false;
		for(int aa = 0; aa < yellowMaze.Length; aa++)
		{
			for(int bb = 0; bb < yellowMaze[aa].Length; bb++)
			{
				if(yellowMaze[aa][bb].Equals(numberPos))
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
		while(dir.Length < 6)
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
				switch(dir[dir.Length - 1])
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
		buttonCenterText[0] = yellowMaze[yellowRC[0]][yellowRC[1]][0] + "";
		buttonCenterText[1] = yellowMaze[yellowRC[0]][yellowRC[1]][1] + "";
		Debug.LogFormat("[Colored Hexabuttons #{0}] Current Space: {1}{2}", moduleId, buttonCenterText[0], buttonCenterText[1]);
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
						Debug.LogFormat("[Colored Hexabuttons #{0}] Strike! There is a wall to the N of {1}{2}", moduleId,  buttonCenterText[0], buttonCenterText[1]);
						module.HandleStrike();
						Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.Strike, transform);
					}
					else
					{
						yellowRC[0] -= 4;
						buttonCenterText[0] = yellowMaze[yellowRC[0]][yellowRC[1]][0] + "";
						buttonCenterText[1] = yellowMaze[yellowRC[0]][yellowRC[1]][1] + "";
						Debug.LogFormat("[Colored Hexabuttons #{0}] Moving N, current space is now {1}{2}", moduleId, buttonCenterText[0], buttonCenterText[1]);
					}
					break;
				case 1://NE
					if (yellowMaze[yellowRC[0] - 1][yellowRC[1] + 1].Equals("W"))
					{
						Debug.LogFormat("[Colored Hexabuttons #{0}] Strike! There is a wall to the NE of {1}{2}", moduleId, buttonCenterText[0], buttonCenterText[1]);
						module.HandleStrike();
						Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.Strike, transform);
					}
					else
					{
						yellowRC[0] -= 2;
						yellowRC[1] += 2;
						buttonCenterText[0] = yellowMaze[yellowRC[0]][yellowRC[1]][0] + "";
						buttonCenterText[1] = yellowMaze[yellowRC[0]][yellowRC[1]][1] + "";
						Debug.LogFormat("[Colored Hexabuttons #{0}] Moving NE, current space is now {1}{2}", moduleId, buttonCenterText[0], buttonCenterText[1]);
					}
					break;
				case 2://SE
					if (yellowMaze[yellowRC[0] + 1][yellowRC[1] + 1].Equals("W"))
					{
						Debug.LogFormat("[Colored Hexabuttons #{0}] Strike! There is a wall to the SE of {1}{2}", moduleId, buttonCenterText[0], buttonCenterText[1]);
						module.HandleStrike();
						Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.Strike, transform);
					}
					else
					{
						yellowRC[0] += 2;
						yellowRC[1] += 2;
						buttonCenterText[0] = yellowMaze[yellowRC[0]][yellowRC[1]][0] + "";
						buttonCenterText[1] = yellowMaze[yellowRC[0]][yellowRC[1]][1] + "";
						Debug.LogFormat("[Colored Hexabuttons #{0}] Moving SE, current space is now {1}{2}", moduleId, buttonCenterText[0], buttonCenterText[1]);
					}
					break;
				case 3://S
					if (yellowMaze[yellowRC[0] + 1][yellowRC[1]].Equals("W"))
					{
						Debug.LogFormat("[Colored Hexabuttons #{0}] Strike! There is a wall to the S of {1}{2}", moduleId, buttonCenterText[0], buttonCenterText[1]);
						module.HandleStrike();
						Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.Strike, transform);
					}
					else
					{
						yellowRC[0] += 4;
						buttonCenterText[0] = yellowMaze[yellowRC[0]][yellowRC[1]][0] + "";
						buttonCenterText[1] = yellowMaze[yellowRC[0]][yellowRC[1]][1] + "";
						Debug.LogFormat("[Colored Hexabuttons #{0}] Moving S, current space is now {1}{2}", moduleId, buttonCenterText[0], buttonCenterText[1]);
					}
					break;
				case 4://SW
					if (yellowMaze[yellowRC[0] + 1][yellowRC[1] - 1].Equals("W"))
					{
						Debug.LogFormat("[Colored Hexabuttons #{0}] Strike! There is a wall to the SW of {1}{2}", moduleId, buttonCenterText[0], buttonCenterText[1]);
						module.HandleStrike();
						Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.Strike, transform);
					}
					else
					{
						yellowRC[0] += 2;
						yellowRC[1] -= 2;
						buttonCenterText[0] = yellowMaze[yellowRC[0]][yellowRC[1]][0] + "";
						buttonCenterText[1] = yellowMaze[yellowRC[0]][yellowRC[1]][1] + "";
						Debug.LogFormat("[Colored Hexabuttons #{0}] Moving SW, current space is now {1}{2}", moduleId, buttonCenterText[0], buttonCenterText[1]);
					}
					break;
				default://NW
					if (yellowMaze[yellowRC[0] - 1][yellowRC[1] - 1].Equals("W"))
					{
						Debug.LogFormat("[Colored Hexabuttons #{0}] Strike! There is a wall to the NW of {1}{2}", moduleId, buttonCenterText[0], buttonCenterText[1]);
						module.HandleStrike();
						Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.Strike, transform);
					}
					else
					{
						yellowRC[0] -= 2;
						yellowRC[1] -= 2;
						buttonCenterText[0] = yellowMaze[yellowRC[0]][yellowRC[1]][0] + "";
						buttonCenterText[1] = yellowMaze[yellowRC[0]][yellowRC[1]][1] + "";
						Debug.LogFormat("[Colored Hexabuttons #{0}] Moving NW, current space is now {1}{2}", moduleId, buttonCenterText[0], buttonCenterText[1]);
					}
					break;
			}
			if (buttonCenterText[0] == buttonCenterText[2] && buttonCenterText[1] == buttonCenterText[3])
			{
				moduleSolved = true;
				module.HandlePass();
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
	void green()
	{
		Debug.LogFormat("[Colored Hexabuttons #{0}] Color Generated: Green", moduleId);
		string[] sheetMusic =
		{
			"9A5230",
			"983472",
			"62018A",
			"162830",
			"A62857",
			"937A81",
			"064892",
			"93150A",
			"725936",
			"4230A7",
			"7A4820",
			"18A760",
			"3491A5",
			"A68239",
			"75491A",
			"674318",
			"605827",
			"4208A9",
			"0145A2",
			"980341",
			"056A14",
			"02386A",
			"2A8136",
			"A56439",
			"04371A",
			"957031",
			"5086A1",
			"359801",
			"369185",
			"2850A3",
			"362581",
			"718395",
			"1A7405",
			"712980",
			"78A362",
			"062591",
			"5A7902",
			"091A86",
			"305761",
			"97A042",
			"84A712",
			"047685",
			"780936",
			"928435",
			"36A597",
			"61702A",
			"913756",
			"0A6397",
			"A38902",
			"250971",
			"790132",
			"254107",
			"21306A",
			"843591",
			"413A02",
			"476089",
			"38752A",
			"1A4086",
			"504A93",
			"941076",
			"9738A4",
			"698415",
			"9705A3",
			"13A854",
			"42380A",
			"9216A5",
			"795082",
			"619387",
			"67859A",
			"37A580",
			"46A507",
			"896421",
			"738062",
			"601235",
			"92517A",
			"472A50",
			"406531",
			"235A71",
			"768519",
			"129534",
			"731925",
			"09A431",
			"462905",
			"396510",
			"A13640",
			"186A24",
			"640923",
			"746359",
			"395240",
			"027853",
			"07163A",
			"4A6780",
			"612435",
			"42A915",
			"054276",
			"903215",
			"562410",
			"75A180",
			"3A1824",
			"762358",
			"179265",
			"8140A5",
			"15032A",
			"A36105",
			"645802",
			"49A017",
			"047629",
			"27634A",
			"632781",
			"240A17",
			"264870",
			"496178",
			"2A1437",
			"083761",
			"427018",
			"016284",
			"094A52",
			"836470",
			"358210",
			"53A942",
			"053816",
			"02A178",
			"810463",
			"473A29",
			"A51986",
			"0A6751",
			"A67014",
			"382154",
			"326405",
			"863941",
			"75A980",
			"981A52",
			"7A3652",
			"A51428",
			"543876",
			"09A685",
			"470261",
			"847015",
			"148605",
			"697810",
			"246A15",
			"3814A6",
			"04285A",
			"942573",
			"412A79",
			"85102A",
			"9058A4",
			"0A4128",
			"342186",
			"570649",
			"210549",
			"142576",
			"536724",
			"21A037",
			"926487",
			"087951",
			"73A650",
			"578932",
			"490258",
			"6714A2",
			"504173",
			"02A598",
			"842516",
			"23817A",
			"102375",
			"625380",
			"5A8427",
			"715A20",
			"5679A2",
			"2A3597",
			"4A2931",
			"91A356",
			"710456",
			"815062",
			"710843",
			"257A89",
			"8097A3",
			"2A3854",
			"072A69",
			"768593",
			"36091A",
			"08A972",
			"169720",
			"634528",
			"65A172",
			"6A0951",
			"582019",
			"38A129",
			"A49601",
			"056397",
			"962754",
			"403216",
			"42715A",
			"4981A0",
			"5963A8",
			"4A2863",
			"62A590",
			"75403A",
			"85126A",
			"8102A9",
			"0A6429",
			"048927",
			"453A78",
			"79354A",
			"23A069",
			"0A6412",
			"61A392",
			"4A3967",
			"A51839",
			"482159",
			"743689",
			"384260",
			"625089",
			"869345",
			"296A47",
			"80A369",
			"963840",
			"392A85",
			"84A536",
			"453201",
			"287043",
			"9713A4",
			"498073",
			"752184",
			"346591",
			"952380",
			"3042A5",
			"892A43",
			"836921",
			"031967",
			"4869A3",
			"960578",
			"426A05",
			"57468A",
			"380192",
			"809342",
			"0468A9",
			"329876",
			"51A867",
			"92601A",
			"34A850",
			"371692",
			"148723",
			"1A4563",
			"271034",
			"81A497",
			"502A86",
			"A81034",
			"317249",
			"580247",
			"3A7861",
			"89A246",
			"7A5694",
			"183907",
			"234706",
			"904281",
			"7063A2",
			"516072",
			"245308",
			"709A68",
			"32748A",
			"631A57",
			"820739",
			"A19465",
			"269134",
			"93841A",
			"601983",
			"03647A",
			"A90214",
			"931647",
			"871062",
			"A14829",
			"A46753",
			"03A861",
			"201367",
			"A65403",
			"A27640",
			"753081",
			"8653A1",
			"5A0263",
			"057281",
			"421679",
			"145836",
			"20A468",
			"10853A",
			"4A3209",
			"812690",
			"076529",
			"29346A",
			"043A62",
			"905734",
			"47892A",
			"984350",
			"1937A5",
			"475091",
			"635908",
			"28096A",
			"643A90",
			"A12038",
			"97A328",
			"45A097",
			"A58937",
			"319526",
			"803795",
			"A54978",
			"302574",
			"2A4136",
			"376025",
			"104792",
			"701659",
			"432A57",
			"390274",
			"42856A",
			"60A479",
			"671325",
			"846157",
			"509784",
			"942615",
			"19706A",
			"592063",
			"039627",
			"0A8934",
			"172346",
			"935406",
			"713892",
			"756083",
			"918762",
			"508634",
			"87A521",
			"581437",
			"617850",
			"89256A",
			"876A02",
			"9156A7",
			"A65418",
			"642973",
			"905341",
			"409615",
			"81A523",
			"32685A",
			"742985",
			"489506",
			"875234",
			"5A7301",
			"481795",
			"A90465",
			"A86507",
			"73A049",
			"2954A6",
			"62A705",
			"316490",
			"03695A",
			"49527A",
			"43A176",
			"4619A2",
			"69A218",
			"46A352",
			"8A3072",
			"187264",
			"875946",
			"324867",
			"051894",
			"8439A5",
			"971543",
			"562879",
			"397485",
			"710849",
			"1689A4",
			"39A021",
			"062319",
			"9517A8",
			"1A4735",
			"A85036",
			"871625",
			"3A7196",
			"34A708",
			"293564",
			"19327A",
			"704538",
			"70458A",
			"416807",
			"3785A1",
			"745213",
			"9A4078",
			"85A429",
			"A91708",
			"295703",
			"785291",
			"823140",
			"548A60",
			"76192A",
			"A06378",
			"093647",
			"206A51",
			"3A6819",
			"289165",
			"192430",
			"214580",
			"978620",
			"7A6982",
			"7612A8",
			"834A06",
			"238107",
			"246875",
			"32591A",
			"4A7581",
			"A79130",
			"890641",
			"8A1746",
			"352819",
			"53214A",
			"A21590",
			"51476A",
			"419A85",
			"4091A5",
			"7523A0",
			"7132A6",
			"7A9164",
			"27A396",
			"865713",
			"05938A",
			"457163",
			"A72019",
			"A05827",
			"81950A",
			"063475",
			"568190",
			"794512",
			"749301",
			"982413",
			"1074A8",
			"2A3907",
			"257183",
			"679514",
			"1A9436",
			"430671",
			"056A97",
			"805134",
			"74A138",
			"75A910",
			"76452A",
			"238569",
			"A29718",
			"A75368",
			"6A8459",
			"314A50",
			"6A9871",
			"298360",
			"8371A0",
			"A84697",
			"32596A",
			"486952",
			"1309A8",
			"574902",
			"892463",
			"A39867",
			"046921",
			"87A436",
			"A65213",
			"241798",
			"2A4867",
			"973418"
		};
		int measure = UnityEngine.Random.Range(0, sheetMusic.Length);
		Debug.LogFormat("[Colored Hexabuttons #{0}] Generated Measure: {1}", moduleId, measure);
		buttonText[6].text = (measure + 1) + "";
		greenSolution = sheetMusic[measure].ToUpper();
		string temp = greenSolution.ToUpper();
		Debug.LogFormat("[Colored Hexabuttons #{0}] Notes in measure: {1} {2} {3} {4} {5} {6}", moduleId, "-0123456789A".IndexOf(temp[0]), "-0123456789A".IndexOf(temp[1]), "-0123456789A".IndexOf(temp[2]), "-0123456789A".IndexOf(temp[3]), "-0123456789A".IndexOf(temp[4]), "-0123456789A".IndexOf(temp[5]));
		greenNotes = "";
		foreach(int i in buttonIndex)
		{
			greenNotes = greenNotes + "" + temp[UnityEngine.Random.Range(0, temp.Length)];
			hexButtons[i].OnInteract = delegate { pressedGreen(i); return false; };
			hexButtons[i].OnInteractEnded = delegate { pressedGreenRelease(i);};
			temp = temp.Replace(greenNotes[i] + "", "");
		}
		Debug.LogFormat("[Colored Hexabuttons #{0}] Notes on buttons in reading order: {1} {2} {3} {4} {5} {6}", moduleId, "-0123456789A".IndexOf(greenNotes[0]), "-0123456789A".IndexOf(greenNotes[1]), "-0123456789A".IndexOf(greenNotes[2]), "-0123456789A".IndexOf(greenNotes[3]), "-0123456789A".IndexOf(greenNotes[4]), "-0123456789A".IndexOf(greenNotes[5]));
		hexButtons[6].OnInteract = delegate { pressedGreenCenter(); return false; };
		hexButtons[6].OnInteractEnded = delegate { pressedGreenCenterRelease();};
		greenSwitch = false;
	}
	void pressedGreen(int n)
	{
		if (!(moduleSolved))
		{
			Audio.PlaySoundAtTransform(notes["0123456789A".IndexOf(greenNotes[n])].name, transform);
			Vector3 pos = buttonMesh[n].transform.localPosition;
			pos.y = 0.0126f;
			buttonMesh[n].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
		}
	}
	void pressedGreenRelease(int n)
	{
		if (!(moduleSolved))
		{
			Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonRelease, transform);
			Vector3 pos = buttonMesh[n].transform.localPosition;
			pos.y = 0.0169f;
			buttonMesh[n].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
		}
	}
	void pressedGreenSubmit(int n)
	{
		if (!(moduleSolved))
		{
			Debug.LogFormat("[Colored Hexabuttons #{0}] User submitted {1}, which plays the note {2}.", moduleId, positions[n], "-0123456789A".IndexOf(greenNotes[n]));
			Audio.PlaySoundAtTransform(notes["0123456789A".IndexOf(greenNotes[n])].name, transform);
			if(greenSolution[numButtonPresses] == greenNotes[n])
			{
				Vector3 pos = buttonMesh[n].transform.localPosition;
				pos.y = 0.0126f;
				buttonMesh[n].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
				numButtonPresses++;
				hexButtons[n].OnInteract = null;
				ledLights[n].enabled = true;
				ledMesh[n].material = ledColors[1];
				if(numButtonPresses == 6)
				{
					moduleSolved = true;
					greenSwitch = false;
					module.HandlePass();
				}
			}
			else
			{
				Debug.LogFormat("[Colored Hexabuttons #{0}] Strike! I was expecting note {1} which is on the {2} button!", moduleId, "-0123456789A".IndexOf(greenSolution[n]), positions[greenNotes.IndexOf(greenSolution[numButtonPresses])]);
				module.HandleStrike();
				Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.Strike, transform);
				foreach (int i in buttonIndex)
				{
					Vector3 pos = buttonMesh[i].transform.localPosition;
					pos.y = 0.0169f;
					buttonMesh[i].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
					hexButtons[i].OnInteract = delegate { pressedGreen(i); return false; };
					hexButtons[i].OnInteractEnded = delegate { pressedGreenRelease(i);};
					ledLights[i].enabled = false;
					ledMesh[i].material = ledColors[0];
				}
				greenSwitch = false;
			}
		}
	}
	void pressedGreenCenter()
	{
		if(!(moduleSolved))
		{
			Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
			Vector3 pos = buttonMesh[6].transform.localPosition;
			pos.y = 0.0126f;
			buttonMesh[6].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
			if (greenSwitch)
			{
				greenSwitch = false;
				foreach (int i in buttonIndex)
				{
					Vector3 pos2 = buttonMesh[i].transform.localPosition;
					pos2.y = 0.0169f;
					buttonMesh[i].transform.localPosition = new Vector3(pos2.x, pos2.y, pos2.z);
					hexButtons[i].OnInteract = delegate { pressedGreen(i); return false; };
					hexButtons[i].OnInteractEnded = delegate { pressedGreenRelease(i); };
					ledLights[i].enabled = false;
					ledMesh[i].material = ledColors[0];
				}
			}
			else
			{
				greenSwitch = true;
				numButtonPresses = 0;
				foreach (int i in buttonIndex)
				{
					hexButtons[i].OnInteract = delegate { pressedGreenSubmit(i); return false; };
					hexButtons[i].OnInteractEnded = null;
				}
				StartCoroutine(greenFlasher());
			}
		}
	}
	void pressedGreenCenterRelease()
	{
		if(!(moduleSolved))
		{
			Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonRelease, transform);
			Vector3 pos = buttonMesh[6].transform.localPosition;
			pos.y = 0.0169f;
			buttonMesh[6].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
		}
	}
	IEnumerator greenFlasher()
	{
		yield return new WaitForSeconds(1.0f);
		while(greenSwitch)
		{
			ledLights[6].enabled = true;
			yield return new WaitForSeconds(1.0f);
			ledLights[6].enabled = false;
			yield return new WaitForSeconds(1.0f);
		}
		ledLights[6].enabled = false;
	}
	void blue()
	{
		Debug.LogFormat("[Colored Hexabuttons #{0}] Color Generated: Blue", moduleId);
		numButtonPresses = 0;
		greenSwitch = true;
		string order = "ζ☮υΞτβΓσΛΣ♥ΩγνλψιωηρδΨακξεΔθφποΠμχς∞";
		List<int> num = new List<int>();
		for (int aa = 0; aa < 28; aa++)
			num.Add(aa);
		blueRotations = new int[6];
		blueSolution = new int[6];
		blueButtonValues = new int[6];
		for(int aa = 0; aa < 6; aa++)
		{
			blueRotations[aa] = num.PickRandom();
			blueSolution[aa] = blueRotations[aa] + 0;
			num.Remove(blueRotations[aa]);
		}
		Array.Sort(blueSolution);
		string temp = "012345";
		blueButtonText = "";
		for(int aa = 0; aa < 6; aa++)
		{
			int n = temp[UnityEngine.Random.Range(0, temp.Length)] - '0';
			blueButtonText = blueButtonText + "" + order[n + (UnityEngine.Random.Range(0, 6) * 6)];
			buttonText[aa].text = blueButtonText[aa] + "";
			blueButtonValues[aa] = blueRotations[n] + 0;
			temp = temp.Replace(n + "", "");
		}
		hexButtons[6].OnInteract = delegate { pressedBlueCenter(); return false; };
		Debug.LogFormat("[Colored Hexabuttons #{0}] Generated text on buttons in reading order: {1} {2} {3} {4} {5} {6}", moduleId, buttonText[0].text, buttonText[1].text, buttonText[2].text, buttonText[3].text, buttonText[4].text, buttonText[5].text);
		Debug.LogFormat("[Colored Hexabuttons #{0}] Buttons linked to which rotation in reading order: {1} {2} {3} {4} {5} {6}", moduleId, (order.IndexOf(buttonText[0].text) % 6) + 1, (order.IndexOf(buttonText[1].text) % 6) + 1, (order.IndexOf(buttonText[2].text) % 6) + 1, (order.IndexOf(buttonText[3].text) % 6) + 1, (order.IndexOf(buttonText[4].text) % 6) + 1, (order.IndexOf(buttonText[5].text) % 6) + 1);
		Debug.LogFormat("[Colored Hexabuttons #{0}] Rotation Values: {1} {2} {3} {4} {5} {6}", moduleId, blueRotations[0], blueRotations[1], blueRotations[2], blueRotations[3], blueRotations[4], blueRotations[5]);

		hexButtons[6].OnInteractEnded = delegate { pressedBlueCenterRelease();};
	}
	void pressedBlueCenter()
	{
		if(!(moduleSolved))
		{
			Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
			Vector3 pos = buttonMesh[6].transform.localPosition;
			pos.y = 0.0126f;
			buttonMesh[6].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
		}
	}
	void pressedBlueCenterRelease()
	{
		if(!(moduleSolved))
		{
			Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonRelease, transform);
			Vector3 pos = buttonMesh[6].transform.localPosition;
			pos.y = 0.0169f;
			buttonMesh[6].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
			if(greenSwitch)
			{
				greenSwitch = false;
				StartCoroutine(movements(blueRotations));
			}
			else
			{
				greenSwitch = true;
				hexButtons[0].transform.localPosition = new Vector3(-0.025f, 0.0169f, 0.034f);
				hexButtons[1].transform.localPosition = new Vector3(0.025f, 0.0169f, 0.034f);
				hexButtons[2].transform.localPosition = new Vector3(-0.05f, 0.0169f, -0.008f);
				hexButtons[3].transform.localPosition = new Vector3(0.05f, 0.0169f, -0.008f);
				hexButtons[4].transform.localPosition = new Vector3(-0.025f, 0.0169f, -0.05f);
				hexButtons[5].transform.localPosition = new Vector3(0.025f, 0.0169f, -0.05f);
				foreach(int i in buttonIndex)
				{
					buttonText[i].color = Color.white;
					buttonText[i].text = blueButtonText[i] + "";
					hexButtons[i].OnInteract = null;
					ledLights[i].enabled = false;
					ledMesh[i].material = ledColors[0];
				}
				numButtonPresses = 0;
			}
		}
	}
	void pressedBlue(int n, int p)
	{
		if(!(moduleSolved))
		{
			Debug.LogFormat("[Colored Hexabuttons #{0}] User pressed {1}, which has a value of {2}.", moduleId, positions[n], p);
			Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
			if (blueSolution[numButtonPresses] == p)
			{
				Vector3 pos = buttonMesh[n].transform.localPosition;
				pos.y = 0.0126f;
				buttonMesh[n].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
				hexButtons[n].OnInteract = null;
				ledMesh[n].material = ledColors[1];
				ledLights[n].enabled = true;
				numButtonPresses++;
				if(numButtonPresses == 6)
				{
					moduleSolved = true;
					module.HandlePass();
				}
			}
			else
			{
				Debug.LogFormat("[Colored Hexabuttons #{0}] Strike! I was expecting a value of {1}!", moduleId, blueSolution[numButtonPresses]);
				module.HandleStrike();
				Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.Strike, transform);
				foreach (int i in buttonIndex)
				{
					hexButtons[i].OnInteract = delegate { pressedBlue(i, blueButtonValues[i]); return false; };
					Vector3 pos = buttonMesh[i].transform.localPosition;
					pos.y = 0.0169f;
					buttonMesh[i].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
					ledMesh[i].material = ledColors[0];
					ledLights[i].enabled = false;
				}
				numButtonPresses = 0;
			}
		}
	}
	
	IEnumerator movements(int[] nums)
	{
		hexButtons[6].OnInteract = null;
		hexButtons[6].OnInteractEnded = null;
		for (int aa = 0; aa < 100; aa++)
		{
			Vector3 pos = hexButtons[6].transform.localPosition;
			pos.y -= 0.0001f;
			hexButtons[6].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
			for(int bb = 0; bb < 6; bb++)
			{
				Color color = buttonText[bb].color;
				color.r -= 0.01f;
				color.g -= 0.01f;
				buttonText[bb].color = color;
			}
			yield return new WaitForSeconds(0.02f);
		}
		for (int bb = 0; bb < 6; bb++)
			buttonText[bb].text = "";
		yield return new WaitForSeconds(1.0f);
		TPOrder = "012345";
		for(int i = 0; i < nums.Length; i++)
		{
			string[] swaps = new string[0];
			int numcw = 0;
			switch (nums[i])
			{
				case 3:
					swaps = new string[1];
					swaps[0] = TPOrder[0] + "" + TPOrder[1];
					TPOrder = TPOrder.Replace(swaps[0][0], '*');
					TPOrder = TPOrder.Replace(swaps[0][1], swaps[0][0]);
					TPOrder = TPOrder.Replace('*', swaps[0][1]);
					break;
				case 17:
					swaps = new string[1];
					swaps[0] = TPOrder[0] + "" + TPOrder[2];
					TPOrder = TPOrder.Replace(swaps[0][0], '*');
					TPOrder = TPOrder.Replace(swaps[0][1], swaps[0][0]);
					TPOrder = TPOrder.Replace('*', swaps[0][1]);
					break;
				case 0:
					swaps = new string[1];
					swaps[0] = TPOrder[0] + "" + TPOrder[3];
					TPOrder = TPOrder.Replace(swaps[0][0], '*');
					TPOrder = TPOrder.Replace(swaps[0][1], swaps[0][0]);
					TPOrder = TPOrder.Replace('*', swaps[0][1]);
					break;
				case 25:
					swaps = new string[1];
					swaps[0] = TPOrder[0] + "" + TPOrder[4];
					TPOrder = TPOrder.Replace(swaps[0][0], '*');
					TPOrder = TPOrder.Replace(swaps[0][1], swaps[0][0]);
					TPOrder = TPOrder.Replace('*', swaps[0][1]);
					break;
				case 6:
					swaps = new string[1];
					swaps[0] = TPOrder[0] + "" + TPOrder[5];
					TPOrder = TPOrder.Replace(swaps[0][0], '*');
					TPOrder = TPOrder.Replace(swaps[0][1], swaps[0][0]);
					TPOrder = TPOrder.Replace('*', swaps[0][1]);
					break;
				case 22:
					swaps = new string[1];
					swaps[0] = TPOrder[1] + "" + TPOrder[2];
					TPOrder = TPOrder.Replace(swaps[0][0], '*');
					TPOrder = TPOrder.Replace(swaps[0][1], swaps[0][0]);
					TPOrder = TPOrder.Replace('*', swaps[0][1]);
					break;
				case 4:
					swaps = new string[1];
					swaps[0] = TPOrder[1] + "" + TPOrder[3];
					TPOrder = TPOrder.Replace(swaps[0][0], '*');
					TPOrder = TPOrder.Replace(swaps[0][1], swaps[0][0]);
					TPOrder = TPOrder.Replace('*', swaps[0][1]);
					break;
				case 23:
					swaps = new string[1];
					swaps[0] = TPOrder[1] + "" + TPOrder[4];
					TPOrder = TPOrder.Replace(swaps[0][0], '*');
					TPOrder = TPOrder.Replace(swaps[0][1], swaps[0][0]);
					TPOrder = TPOrder.Replace('*', swaps[0][1]);
					break;
				case 11:
					swaps = new string[1];
					swaps[0] = TPOrder[1] + "" + TPOrder[5];
					TPOrder = TPOrder.Replace(swaps[0][0], '*');
					TPOrder = TPOrder.Replace(swaps[0][1], swaps[0][0]);
					TPOrder = TPOrder.Replace('*', swaps[0][1]);
					break;
				case 5:
					swaps = new string[1];
					swaps[0] = TPOrder[2] + "" + TPOrder[3];
					TPOrder = TPOrder.Replace(swaps[0][0], '*');
					TPOrder = TPOrder.Replace(swaps[0][1], swaps[0][0]);
					TPOrder = TPOrder.Replace('*', swaps[0][1]);
					break;
				case 16:
					swaps = new string[1];
					swaps[0] = TPOrder[2] + "" + TPOrder[4];
					TPOrder = TPOrder.Replace(swaps[0][0], '*');
					TPOrder = TPOrder.Replace(swaps[0][1], swaps[0][0]);
					TPOrder = TPOrder.Replace('*', swaps[0][1]);
					break;
				case 9:
					swaps = new string[1];
					swaps[0] = TPOrder[2] + "" + TPOrder[5];
					TPOrder = TPOrder.Replace(swaps[0][0], '*');
					TPOrder = TPOrder.Replace(swaps[0][1], swaps[0][0]);
					TPOrder = TPOrder.Replace('*', swaps[0][1]);
					break;
				case 27:
					swaps = new string[1];
					swaps[0] = TPOrder[3] + "" + TPOrder[4];
					TPOrder = TPOrder.Replace(swaps[0][0], '*');
					TPOrder = TPOrder.Replace(swaps[0][1], swaps[0][0]);
					TPOrder = TPOrder.Replace('*', swaps[0][1]);
					break;
				case 20:
					swaps = new string[1];
					swaps[0] = TPOrder[3] + "" + TPOrder[5];
					TPOrder = TPOrder.Replace(swaps[0][0], '*');
					TPOrder = TPOrder.Replace(swaps[0][1], swaps[0][0]);
					TPOrder = TPOrder.Replace('*', swaps[0][1]);
					break;
				case 24:
					swaps = new string[1];
					swaps[0] = TPOrder[4] + "" + TPOrder[5];
					TPOrder = TPOrder.Replace(swaps[0][0], '*');
					TPOrder = TPOrder.Replace(swaps[0][1], swaps[0][0]);
					TPOrder = TPOrder.Replace('*', swaps[0][1]);
					break;
				case 2://M|
					swaps = new string[3];
					swaps[0] = TPOrder[0] + "" + TPOrder[1];
					swaps[1] = TPOrder[2] + "" + TPOrder[3];
					swaps[2] = TPOrder[4] + "" + TPOrder[5];
					for (int aa = 0; aa < swaps.Length; aa++)
					{
						TPOrder = TPOrder.Replace(swaps[aa][0], '*');
						TPOrder = TPOrder.Replace(swaps[aa][1], swaps[aa][0]);
						TPOrder = TPOrder.Replace('*', swaps[aa][1]);
					}
					break;
				case 14://M/
					swaps = new string[3];
					swaps[0] = TPOrder[0] + "" + TPOrder[5];
					swaps[1] = TPOrder[1] + "" + TPOrder[3];
					swaps[2] = TPOrder[2] + "" + TPOrder[4];
					for (int aa = 0; aa < swaps.Length; aa++)
					{
						TPOrder = TPOrder.Replace(swaps[aa][0], '*');
						TPOrder = TPOrder.Replace(swaps[aa][1], swaps[aa][0]);
						TPOrder = TPOrder.Replace('*', swaps[aa][1]);
					}
					break;
				case 15://M\
					swaps = new string[3];
					swaps[0] = TPOrder[0] + "" + TPOrder[2];
					swaps[1] = TPOrder[1] + "" + TPOrder[4];
					swaps[2] = TPOrder[3] + "" + TPOrder[5];
					for (int aa = 0; aa < swaps.Length; aa++)
					{
						TPOrder = TPOrder.Replace(swaps[aa][0], '*');
						TPOrder = TPOrder.Replace(swaps[aa][1], swaps[aa][0]);
						TPOrder = TPOrder.Replace('*', swaps[aa][1]);
					}
					break;
				case 26:
					numcw = 1;
					break;
				case 8:
					numcw = 2;
					break;
				case 10:
					numcw = 3;
					break;
				case 1:
					numcw = 4;
					break;
				case 19:
					numcw = 5;
					break;
				case 13:
					numcw = -1;
					break;
				case 21:
					numcw = -2;
					break;
				case 18:
					numcw = -3;
					break;
				case 7:
					numcw = -4;
					break;
				case 12:
					numcw = -5;
					break;
			}
			if(swaps.Length > 0)
			{
				float[][] diff = new float[swaps.Length][];
				for (int aa = 0; aa < diff.Length; aa++)
				{
					diff[aa] = new float[4];
					diff[aa][0] = (buttonMesh[swaps[aa][1] - '0'].transform.localPosition.x - buttonMesh[swaps[aa][0] - '0'].transform.localPosition.x) / 100f;
					diff[aa][1] = (buttonMesh[swaps[aa][1] - '0'].transform.localPosition.z - buttonMesh[swaps[aa][0] - '0'].transform.localPosition.z) / 100f;
					diff[aa][2] = (buttonMesh[swaps[aa][0] - '0'].transform.localPosition.x - buttonMesh[swaps[aa][1] - '0'].transform.localPosition.x) / 100f;
					diff[aa][3] = (buttonMesh[swaps[aa][0] - '0'].transform.localPosition.z - buttonMesh[swaps[aa][1] - '0'].transform.localPosition.z) / 100f;
				}
				for (int zz = 0; zz < 100; zz++)
				{
					for (int aa = 0; aa < swaps.Length; aa++)
					{
						Vector3 pos1 = buttonMesh[swaps[aa][0] - '0'].transform.localPosition;
						pos1.x += diff[aa][0];
						pos1.z += diff[aa][1];
						buttonMesh[swaps[aa][0] - '0'].transform.localPosition = new Vector3(pos1.x, pos1.y, pos1.z);
						Vector3 pos2 = buttonMesh[swaps[aa][1] - '0'].transform.localPosition;
						pos2.x += diff[aa][2];
						pos2.z += diff[aa][3];
						buttonMesh[swaps[aa][1] - '0'].transform.localPosition = new Vector3(pos2.x, pos2.y, pos2.z);
					}
					yield return new WaitForSeconds(0.01f);
				}
			}
			else if(numcw > 0)
			{
				float[][] diff = new float[6][];
				int[] cwpos = {0, 1, 3, 5, 4, 2};
				for(int bb = 0; bb < numcw; bb++)
				{
					for (int aa = 0; aa < 6; aa++)
					{
						diff[aa] = new float[2];
						diff[aa][0] = (buttonMesh[TPOrder[cwpos[(aa + 1) % 6]] - '0'].transform.localPosition.x - buttonMesh[TPOrder[cwpos[aa]] - '0'].transform.localPosition.x) / 100f;
						diff[aa][1] = (buttonMesh[TPOrder[cwpos[(aa + 1) % 6]] - '0'].transform.localPosition.z - buttonMesh[TPOrder[cwpos[aa]] - '0'].transform.localPosition.z) / 100f;
					}
					for (int zz = 0; zz < 100; zz++)
					{
						for (int aa = 0; aa < diff.Length; aa++)
						{
							Vector3 pos1 = buttonMesh[TPOrder[cwpos[aa]] - '0'].transform.localPosition;
							pos1.x += diff[aa][0];
							pos1.z += diff[aa][1];
							buttonMesh[TPOrder[cwpos[aa]] - '0'].transform.localPosition = new Vector3(pos1.x, pos1.y, pos1.z);
						}
						yield return new WaitForSeconds(0.01f);
					}
					TPOrder = TPOrder[2] + "" + TPOrder[0] + "" + TPOrder[4] + "" + TPOrder[1] + "" + TPOrder[5] + "" + TPOrder[3];
				}
				
			}
			else
			{
				float[][] diff = new float[6][];
				int[] cwpos = {0, 2, 4, 5, 3, 1};
				numcw *= -1;
				for (int bb = 0; bb < numcw; bb++)
				{
					for (int aa = 0; aa < 6; aa++)
					{
						diff[aa] = new float[2];
						diff[aa][0] = (buttonMesh[TPOrder[cwpos[(aa + 1) % 6]] - '0'].transform.localPosition.x - buttonMesh[TPOrder[cwpos[aa]] - '0'].transform.localPosition.x) / 100f;
						diff[aa][1] = (buttonMesh[TPOrder[cwpos[(aa + 1) % 6]] - '0'].transform.localPosition.z - buttonMesh[TPOrder[cwpos[aa]] - '0'].transform.localPosition.z) / 100f;
					}
					for (int zz = 0; zz < 100; zz++)
					{
						for (int aa = 0; aa < diff.Length; aa++)
						{
							Vector3 pos1 = buttonMesh[TPOrder[cwpos[aa]] - '0'].transform.localPosition;
							pos1.x += diff[aa][0];
							pos1.z += diff[aa][1];
							buttonMesh[TPOrder[cwpos[aa]] - '0'].transform.localPosition = new Vector3(pos1.x, pos1.y, pos1.z);
						}
						yield return new WaitForSeconds(0.01f);
					}
					TPOrder = TPOrder[1] + "" + TPOrder[3] + "" + TPOrder[0] + "" + TPOrder[5] + "" + TPOrder[2] + "" + TPOrder[4];
				}
			}
			yield return new WaitForSeconds(1.0f);
		}
		foreach (int i in buttonIndex)
			hexButtons[i].OnInteract = delegate { pressedBlue(i, blueButtonValues[i]); return false;};
		for(int aa = 0; aa < 100; aa++)
		{
			Vector3 pos = hexButtons[6].transform.localPosition;
			pos.y += 0.0001f;
			hexButtons[6].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
			yield return new WaitForSeconds(0.02f);
		}
		hexButtons[6].OnInteract = delegate { pressedBlueCenter(); return false; };
		hexButtons[6].OnInteractEnded = delegate { pressedBlueCenterRelease(); };
		TPOrder = TPOrder + "6";
	}
	void purple()
	{
		Debug.LogFormat("[Colored Hexabuttons #{0}] Generated Color: Purple", moduleId);
		int[][] bins =
		{ 
			new int[]{1, 1, 1, 1, 1, 1},
			new int[]{1, 1, 1, 1, 1, 1},
			new int[]{0, 0, 0, 0, 0, 0},
		};
		string pos1 = "012345";
		string pos0 = "";
		int num0 = UnityEngine.Random.Range(2, 5);
		for(int aa = 0; aa < num0; aa++)
		{
			pos0 = pos0 + "" + pos1[UnityEngine.Random.Range(0, pos1.Length)];
			bins[0][pos0[aa] - '0'] = 0;
			pos1 = pos1.Replace(pos0[aa] + "", "");
		}
		string pos2 = "012345";
		for(int aa = 0; aa < 2; aa++)
		{
			int n = pos0[UnityEngine.Random.Range(0, pos0.Length)] - '0';
			bins[1][n] = aa;
			pos0 = pos0.Replace(n + "", "");
			pos2 = pos2.Replace(n + "", "");
		}
		for (int aa = 0; aa < 2; aa++)
		{
			int n = pos1[UnityEngine.Random.Range(0, pos1.Length)] - '0';
			bins[1][n] = aa;
			pos1 = pos1.Replace(n + "", "");
			pos2 = pos2.Replace(n + "", "");
		}
		num0 = UnityEngine.Random.Range(0, 3);
		for(int aa = 0; aa < num0; aa++)
		{
			int n = pos2[UnityEngine.Random.Range(0, pos2.Length)] - '0';
			bins[1][n] = 0;
			pos2 = pos2.Replace(n + "", "");
		}
		string[] opers = {"AND", "OR", "XOR", "NAND", "NOR", "XNOR", "->", "<-"};
		int oper = UnityEngine.Random.Range(0, 8);
		for(int aa = 0; aa < 6; aa++)
		{
			switch(oper)
			{
				case 0://AND
					if(bins[0][aa] == 1 && bins[1][aa] == 1)
						bins[2][aa] = 1;
					break;
				case 1://OR
					if (bins[0][aa] == 1 || bins[1][aa] == 1)
						bins[2][aa] = 1;
					break;
				case 2://XOR
					if (bins[0][aa] != bins[1][aa])
						bins[2][aa] = 1;
					break;
				case 3://NAND
					if (!(bins[0][aa] == 1 && bins[1][aa] == 1))
						bins[2][aa] = 1;
					break;
				case 4://NOR
					if (!(bins[0][aa] == 1 || bins[1][aa] == 1))
						bins[2][aa] = 1;
					break;
				case 5://XNOR
					if (bins[0][aa] == bins[1][aa])
						bins[2][aa] = 1;
					break;
				case 6://->
					if (bins[1][aa] == 1 || bins[0][aa] == 0)
						bins[2][aa] = 1;
					break;
				default://<-
					if (bins[0][aa] == 1 || bins[1][aa] == 0)
						bins[2][aa] = 1;
					break;
			}
		}
		int[] numbers = {0, 0, 0};
		for(int aa = 0; aa < 6; aa++)
		{
			for(int bb = 0; bb < 3; bb++)
			{
				if(bins[bb][aa] == 1)
				{
					switch(aa)
					{
						case 0:
							numbers[bb] += 32;
							break;
						case 1:
							numbers[bb] += 16;
							break;
						case 2:
							numbers[bb] += 8;
							break;
						case 3:
							numbers[bb] += 4;
							break;
						case 4:
							numbers[bb] += 2;
							break;
						default:
							numbers[bb] += 1;
							break;
					}
				}
			}
		}
		buttonCenterText = new string[6];
		for(int aa = 0; aa < 3; aa++)
		{
			if(numbers[aa] < 10)
			{
				buttonCenterText[aa * 2] = "0";
				buttonCenterText[(aa * 2) + 1] = numbers[aa] + "";
			}
			else
			{
				buttonCenterText[aa * 2] = (numbers[aa] / 10) + "";
				buttonCenterText[(aa * 2) + 1] = (numbers[aa] % 10) + "";
			}
		}
		Debug.LogFormat("[Colored Hexabuttons #{0}] Generated 6 digit number: {1}{2}{3}{4}{5}{6}", moduleId, buttonCenterText[0], buttonCenterText[1], buttonCenterText[2], buttonCenterText[3], buttonCenterText[4], buttonCenterText[5]);
		Debug.LogFormat("[Colored Hexabuttons #{0}] Generated left binary: {1}{2}{3}{4}{5}{6}", moduleId, bins[0][0], bins[0][1], bins[0][2], bins[0][3], bins[0][4], bins[0][5]);
		Debug.LogFormat("[Colored Hexabuttons #{0}] Generated right binary: {1}{2}{3}{4}{5}{6}", moduleId, bins[1][0], bins[1][1], bins[1][2], bins[1][3], bins[1][4], bins[1][5]);
		Debug.LogFormat("[Colored Hexabuttons #{0}] Resulting binary: {1}{2}{3}{4}{5}{6}", moduleId, bins[2][0], bins[2][1], bins[2][2], bins[2][3], bins[2][4], bins[2][5]);
		Debug.LogFormat("[Colored Hexabuttons #{0}] Logic operator used: {1}", moduleId, opers[oper]);
		hexButtons[6].OnInteract = delegate{ pressedRedCenter(); return false;};
		hexButtons[6].OnInteractEnded = delegate { pressedRedCenterRelease();};
		purpleSolution = new int[6];
		numButtonPresses = 0;
		for(int aa = 0; aa < 10; aa++)
		{
			for(int bb = 0; bb < 6; bb++)
			{
				if(aa == buttonCenterText[bb][0] - '0')
				{
					purpleSolution[bb] = numButtonPresses + 0;
					numButtonPresses++;
					if (numButtonPresses == 6)
						break;
				}
			}
			if (numButtonPresses == 6)
				break;
		}
		Debug.LogFormat("[Colored Hexabuttons #{0}] Buttons to be read in this order: {1} {2} {3} {4} {5} {6}", moduleId, positions[purpleSolution[0]], positions[purpleSolution[1]], positions[purpleSolution[2]], positions[purpleSolution[3]], positions[purpleSolution[4]], positions[purpleSolution[5]]);
		numbers[0] = 0;
		numbers[1] = 0;
		numbers[2] = 0;
		for (int aa = 0; aa < 6; aa++)
		{
			bins[0][aa] = UnityEngine.Random.Range(0, 2);
			bins[1][aa] = UnityEngine.Random.Range(0, 2);
			bins[2][aa] = 0;
			switch (oper)
			{
				case 0://AND
					if (bins[0][aa] == 1 && bins[1][aa] == 1)
						bins[2][aa] = 1;
					break;
				case 1://OR
					if (bins[0][aa] == 1 || bins[1][aa] == 1)
						bins[2][aa] = 1;
					break;
				case 2://XOR
					if (bins[0][aa] != bins[1][aa])
						bins[2][aa] = 1;
					break;
				case 3://NAND
					if (!(bins[0][aa] == 1 && bins[1][aa] == 1))
						bins[2][aa] = 1;
					break;
				case 4://NOR
					if (!(bins[0][aa] == 1 || bins[1][aa] == 1))
						bins[2][aa] = 1;
					break;
				case 5://XNOR
					if (bins[0][aa] == bins[1][aa])
						bins[2][aa] = 1;
					break;
				case 6://->
					if (bins[1][aa] == 1 || bins[0][aa] == 0)
						bins[2][aa] = 1;
					break;
				default://<-
					if (bins[0][aa] == 1 || bins[1][aa] == 0)
						bins[2][aa] = 1;
					break;
			}
			for(int bb = 0; bb < 3; bb++)
			{
				if (bins[bb][aa] == 1)
				{
					switch (aa)
					{
						case 0:
							numbers[bb] += 32;
							break;
						case 1:
							numbers[bb] += 16;
							break;
						case 2:
							numbers[bb] += 8;
							break;
						case 3:
							numbers[bb] += 4;
							break;
						case 4:
							numbers[bb] += 2;
							break;
						default:
							numbers[bb] += 1;
							break;
					}
				}
			}
		}
		string randomAlpha = "VOT6C8GU9NJRAX3Q5DM12FPEBZKYW7SLIH40";
		foreach (int i in buttonIndex)
		{
			int n;
			switch (bins[0][i] + "" + bins[1][i])
			{
				case "00":
					n = 0;
					break;
				case "01":
					n = 1;
					break;
				case "10":
					n = 2;
					break;
				default:
					n = 3;
					break;
			}
			buttonText[purpleSolution[i]].text = randomAlpha[n + (4 * UnityEngine.Random.Range(0, 9))] + "";
		}
		string numberDigits = "";
		for (int aa = 0; aa < 3; aa++)
		{
			if (numbers[aa] < 10)
				numberDigits = numberDigits + "0" + numbers[aa];
			else
				numberDigits = numberDigits + "" + numbers[aa];
		}
		Debug.LogFormat("[Colored Hexabuttons #{0}] Generated text on buttons in reading order: {1}{2}{3}{4}{5}{6}", moduleId, buttonText[0].text, buttonText[1].text, buttonText[2].text, buttonText[3].text, buttonText[4].text, buttonText[5].text);
		Debug.LogFormat("[Colored Hexabuttons #{0}] Generated left binary: {1}{2}{3}{4}{5}{6}", moduleId, bins[0][0], bins[0][1], bins[0][2], bins[0][3], bins[0][4], bins[0][5]);
		Debug.LogFormat("[Colored Hexabuttons #{0}] Generated right binary: {1}{2}{3}{4}{5}{6}", moduleId, bins[1][0], bins[1][1], bins[1][2], bins[1][3], bins[1][4], bins[1][5]);
		Debug.LogFormat("[Colored Hexabuttons #{0}] Resulting binary: {1}{2}{3}{4}{5}{6}", moduleId, bins[2][0], bins[2][1], bins[2][2], bins[2][3], bins[2][4], bins[2][5]);
		Debug.LogFormat("[Colored Hexabuttons #{0}] Generated 6 digit number: {1}", moduleId, numberDigits);
		numButtonPresses = 0;
		for (int aa = 0; aa < 10; aa++)
		{
			for (int bb = 0; bb < 6; bb++)
			{
				if (aa == numberDigits[bb] - '0')
				{
					purpleSolution[bb] = numButtonPresses + 0;
					numButtonPresses++;
					if (numButtonPresses == 6)
						break;
				}
			}
			if (numButtonPresses == 6)
				break;
		}
		Debug.LogFormat("[Colored Hexabuttons #{0}] Solution: {1} {2} {3} {4} {5} {6}", moduleId, positions[purpleSolution[0]], positions[purpleSolution[1]], positions[purpleSolution[2]], positions[purpleSolution[3]], positions[purpleSolution[4]], positions[purpleSolution[5]]);
		numButtonPresses = 0;
		foreach(int i in buttonIndex)
			hexButtons[i].OnInteract = delegate { pressedPurple(i); return false; };
	}
	void pressedPurple(int n)
	{
		if(!(moduleSolved))
		{
			Debug.LogFormat("[Colored Hexabuttons #{0}] User pressed {1}.", moduleId, positions[n]);
			Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
			if(purpleSolution[numButtonPresses] == n)
			{
				Vector3 pos = buttonMesh[n].transform.localPosition;
				pos.y = 0.0126f;
				buttonMesh[n].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
				hexButtons[n].OnInteract = null;
				ledMesh[n].material = ledColors[1];
				ledLights[n].enabled = true;
				numButtonPresses++;
				if(numButtonPresses == 6)
				{
					moduleSolved = true;
					module.HandlePass();
				}
			}
			else
			{
				Debug.LogFormat("[Colored Hexabuttons #{0}] Strike! I was expecting the {1} button!", moduleId, positions[purpleSolution[numButtonPresses]]);
				module.HandleStrike();
				Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.Strike, transform);
				foreach (int i in buttonIndex)
				{
					Vector3 pos = buttonMesh[i].transform.localPosition;
					pos.y = 0.0169f;
					buttonMesh[i].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
					hexButtons[i].OnInteract = delegate { pressedPurple(i); return false;};
					ledMesh[i].material = ledColors[0];
					ledLights[i].enabled = false;
				}
				numButtonPresses = 0;
			}
		}
	}
	void white()
	{
		Debug.LogFormat("[Colored Hexabuttons #{0}] Generated Color: White", moduleId);
		string[][] chart =
		{
			new string[]{"Y*", "BP", "B*", "GB", "YG", "P*"},
			new string[]{"OP", "GP", "26", "RO", "16", "25"},
			new string[]{"46", "R*", "36", "15", "RG", "YP"},
			new string[]{"56", "RB", "RP", "45", "OY", "13"},
			new string[]{"OG", "24", "14", "G*", "OB", "23"},
			new string[]{"RY", "O*", "34", "35", "12", "YB"},
		};
		string colorChoices = "ROYGBP";
		whiteBHC = "";
		foreach(int i in buttonIndex)
		{
			whiteBHC = whiteBHC + "" + colorChoices[UnityEngine.Random.Range(0, colorChoices.Length)];
			hexButtons[i].OnHighlight = delegate { buttonMesh[i].material = buttonColors["ROYGBP".IndexOf(whiteBHC[i])];};
			hexButtons[i].OnHighlightEnded = delegate { buttonMesh[i].material = buttonColors[6];};
			hexButtons[i].OnInteract = delegate {pressedWhite(i); return false;};
			colorChoices = colorChoices.Replace(whiteBHC[i] + "", "");
		}
		whiteBHC = whiteBHC + "" + whiteBHC[UnityEngine.Random.Range(0, 6)];
		hexButtons[6].OnHighlight = delegate { buttonMesh[6].material = buttonColors["ROYGBP".IndexOf(whiteBHC[6])]; };
		hexButtons[6].OnHighlightEnded = delegate { buttonMesh[6].material = buttonColors[6];};
		Debug.LogFormat("[Colored Hexabuttons #{0}] Hovered Colors: {1}", moduleId, whiteBHC);
		whiteFlashes = new int[6];
		for (int aa = 0; aa < 6; aa++)
			whiteFlashes[aa] = UnityEngine.Random.Range(0, 6);
		Debug.LogFormat("[Colored Hexabuttons #{0}] Flashed Colors: {1}{2}{3}{4}{5}{6}", moduleId, whiteBHC[whiteFlashes[0]], whiteBHC[whiteFlashes[1]], whiteBHC[whiteFlashes[2]], whiteBHC[whiteFlashes[3]], whiteBHC[whiteFlashes[4]], whiteBHC[whiteFlashes[5]]);
		whiteSolution = whiteBHC[0] + "" + whiteBHC[1] + "" + whiteBHC[3] + "" + whiteBHC[5] + "" + whiteBHC[4] + "" + whiteBHC[2];
		whiteSolution = whiteSolution.Substring(whiteSolution.IndexOf(whiteBHC[6])) + "" + whiteSolution.Substring(0, whiteSolution.IndexOf(whiteBHC[6]));
		Debug.LogFormat("[Colored Hexabuttons #{0}] Initial color sequence: {1}", moduleId, whiteSolution);
		for (int aa = 0; aa < 6; aa++)
		{
			string instruct = chart["ROYGBP".IndexOf(whiteBHC[whiteFlashes[aa]])]["ROYGBP".IndexOf(whiteBHC[whiteFlashes[(aa + 1) % 6]])];
			if (instruct.Contains("*"))
			{
				if (instruct[0] == whiteSolution[0])
					whiteSolution = whiteSolution.Substring(1) + "" + whiteSolution[0];
				else
					whiteSolution = instruct[0] + "" + whiteSolution.Replace(instruct[0] + "", "");
			}
			else
			{
				if ("123456".IndexOf(instruct[0]) >= 0)
					instruct = whiteSolution[instruct[0] - '0' - 1] + "" + whiteSolution[instruct[1] - '0' - 1];
				whiteSolution = whiteSolution.Replace(instruct[0], '*');
				whiteSolution = whiteSolution.Replace(instruct[1], instruct[0]);
				whiteSolution = whiteSolution.Replace('*', instruct[1]);
			}
			Debug.LogFormat("[Colored Hexabuttons #{0}] Instruction {1}: {2}", moduleId, chart["ROYGBP".IndexOf(whiteBHC[whiteFlashes[aa]])]["ROYGBP".IndexOf(whiteBHC[whiteFlashes[(aa + 1) % 6]])], whiteSolution);
		}
		hexButtons[6].OnInteract = delegate { pressedWhiteCenter(); return false; };
		numButtonPresses = 0;
	}
	void pressedWhiteCenter()
	{
		if(!(moduleSolved))
		{
			foreach (int i in buttonIndex)
			{
				Vector3 pos2 = buttonMesh[i].transform.localPosition;
				pos2.y = 0.0169f;
				buttonMesh[i].transform.localPosition = new Vector3(pos2.x, pos2.y, pos2.z);
				buttonMesh[i].material = buttonColors[6];
				hexButtons[i].OnHighlight = null;
				hexButtons[i].OnHighlightEnded = null;
				hexButtons[i].OnInteract = null;
				ledMesh[i].material = ledColors[0];
				ledLights[i].enabled = false;
			}
			numButtonPresses = 0;
			hexButtons[6].OnHighlight = null;
			hexButtons[6].OnHighlightEnded = null;
			Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
			Vector3 pos = buttonMesh[6].transform.localPosition;
			pos.y = 0.0126f;
			buttonMesh[6].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
			hexButtons[6].OnInteract = null;
			StartCoroutine(whiteFlasher());
		}
	}
	void pressedWhite(int n)
	{
		if(!(moduleSolved))
		{
			Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
			if(whiteSolution[numButtonPresses] == whiteBHC[n])
			{
				Vector3 pos = buttonMesh[n].transform.localPosition;
				pos.y = 0.0126f;
				buttonMesh[n].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
				hexButtons[n].OnInteract = null;
				hexButtons[n].OnHighlight = null;
				hexButtons[n].OnHighlightEnded = null;
				ledMesh[n].material = ledColors[1];
				ledLights[n].enabled = true;
				numButtonPresses++;
				if (numButtonPresses == 6)
				{
					moduleSolved = true;
					module.HandlePass();
				}
			}
			else
			{
				module.HandleStrike();
				Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.Strike, transform);
				foreach (int i in buttonIndex)
				{
					Vector3 pos = buttonMesh[i].transform.localPosition;
					pos.y = 0.0169f;
					buttonMesh[i].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
					buttonMesh[i].material = buttonColors[6];
					hexButtons[i].OnInteract = delegate { pressedWhite(i); return false; };
					hexButtons[i].OnHighlight = delegate { buttonMesh[i].material = buttonColors["ROYGBP".IndexOf(whiteBHC[i])]; };
					hexButtons[i].OnHighlightEnded = delegate { buttonMesh[i].material = buttonColors[6]; };
					ledMesh[i].material = ledColors[0];
					ledLights[i].enabled = false;
				}
				numButtonPresses = 0;
			}
		}
	}
	IEnumerator whiteFlasher()
	{
		yield return new WaitForSeconds(1.0f);
		for(int aa = 0; aa < 6; aa++)
		{
			flashLights[whiteFlashes[aa]].enabled = true;
			yield return new WaitForSeconds(0.5f);
			flashLights[whiteFlashes[aa]].enabled = false;
			yield return new WaitForSeconds(0.5f);
		}
		Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonRelease, transform);
		Vector3 pos = buttonMesh[6].transform.localPosition;
		pos.y = 0.0169f;
		buttonMesh[6].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
		hexButtons[6].OnHighlight = delegate { buttonMesh[6].material = buttonColors["ROYGBP".IndexOf(whiteBHC[6])]; };
		hexButtons[6].OnHighlightEnded = delegate { buttonMesh[6].material = buttonColors[6]; };
		hexButtons[6].OnInteract = delegate { pressedWhiteCenter(); return false;};
		buttonMesh[6].material = buttonColors[6];
		foreach (int i in buttonIndex)
		{
			hexButtons[i].OnHighlight = delegate { buttonMesh[i].material = buttonColors["ROYGBP".IndexOf(whiteBHC[i])]; };
			hexButtons[i].OnHighlightEnded = delegate { buttonMesh[i].material = buttonColors[6]; };
			hexButtons[i].OnInteract = delegate { pressedWhite(i); return false; };
		}
	}
	int mod(int n, int m)
	{
		while (n < 0)
			n += m;
		return (n % m);
	}
#pragma warning disable 414
	private readonly string TwitchHelpMessage = @"!{0} press|p tl/1 tr/2 ml/3 mr/4 bl/5 br/6 c/7 presses the top-left, top-right, middle-left, middle-right, bottom-left, bottom-right and center buttons in that order. !{0} hover|cycle|h|c will hover over the buttons in tl, tr, ml, mr, bl, br, c order.";
#pragma warning restore 414
	IEnumerator ProcessTwitchCommand(string command)
	{
		
		string[] param = command.ToUpper().Split(' ');
		if ((Regex.IsMatch(param[0], @"^\s*HOVER\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant) || Regex.IsMatch(param[0], @"^\s*H\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant)) || Regex.IsMatch(param[0], @"^\s*CYCLE\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant) || Regex.IsMatch(param[0], @"^\s*C\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
		{
			for(int aa = 0; aa < 7; aa++)
			{
				hexButtons[aa].OnHighlight();
				yield return new WaitForSeconds(1.5f);
				hexButtons[aa].OnHighlightEnded();
			}
		}
		else if ((Regex.IsMatch(param[0], @"^\s*PRESS\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant) || Regex.IsMatch(param[0], @"^\s*P\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant)) && param.Length > 1)
		{
			bool flag = true;
			for (int i = 1; i < param.Length; i++)
			{
				switch (param[i])
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
						break;
					default:
						flag = false;
						break;
				}
			}
			if(flag)
			{
				yield return new WaitForSeconds(0f);
				for (int i = 1; i < param.Length; i++)
				{
					int cursor = -1;
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
					}
					if(hexButtons[TPOrder[cursor] - '0'].OnInteract != null)
					{
						hexButtons[TPOrder[cursor] - '0'].OnInteract();
						yield return new WaitForSeconds(0.5f);
						if (hexButtons[TPOrder[cursor] - '0'].OnInteractEnded != null)
						{
							hexButtons[TPOrder[cursor] - '0'].OnInteractEnded();
							yield return new WaitForSeconds(0.5f);
						}
					}
				}
			}
			else
				yield return "sendtochat An error occured because the user inputted something wrong.";
		}
		else
			yield return "sendtochat An error occured because the user inputted something wrong.";
	}
}
