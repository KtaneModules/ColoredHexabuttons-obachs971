using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class blueHexabuttons : MonoBehaviour
{
	public KMBombModule module;
	public KMAudio Audio;
	private int moduleId;
	private static int moduleIdCounter = 1;
	public KMSelectable[] hexButtons;
	public MeshRenderer[] buttonMesh;
	public TextMesh[] buttonText;
	public Material[] ledColors;
	public MeshRenderer[] ledMesh;
	private bool moduleSolved;
	private int[] solution;
	private int[] blueRotations;
	private int[] blueButtonValues;
	private string blueButtonText;
	private bool flag;
	private string TPOrder;
	private int numButtonPresses;
	private string[] positions = { "TL", "TR", "ML", "MR", "BL", "BR" };
	private int[] buttonIndex = { 0, 1, 2, 3, 4, 5 };
	void Awake()
	{
		moduleId = moduleIdCounter++;
		numButtonPresses = 0;
		flag = true;
		string order = "ζ¢υΞτβΓσΛΣ$Ωγνλ£ιωηρδΨακξεΔθφποΠμχς∞";
		List<int> num = new List<int>();
		for (int aa = 0; aa < 28; aa++)
			num.Add(aa);
		blueRotations = new int[6];
		solution = new int[6];
		blueButtonValues = new int[6];
		//int[] debugrot = {10, 8, 20, 16, 2, 23};
		for (int aa = 0; aa < 6; aa++)
		{
			blueRotations[aa] = num.PickRandom();
			//blueRotations[aa] = debugrot[aa];
			solution[aa] = blueRotations[aa] + 0;
			num.Remove(blueRotations[aa]);
		}
		Array.Sort(solution);
		string temp = "012345";
		blueButtonText = "";
		//string debugtext = "520431";
		for (int aa = 0; aa < 6; aa++)
		{
			int n = temp[UnityEngine.Random.Range(0, temp.Length)] - '0';
			//int n = debugtext[aa] - '0';
			blueButtonText = blueButtonText + "" + order[n + (UnityEngine.Random.Range(0, 6) * 6)];
			buttonText[aa].text = blueButtonText[aa] + "";
			blueButtonValues[aa] = blueRotations[n] + 0;
			temp = temp.Replace(n + "", "");
		}
		hexButtons[6].OnInteract = delegate { pressedCenter(); return false; };
		Debug.LogFormat("[Blue Hexabuttons #{0}] Generated text on buttons in reading order: {1} {2} {3} {4} {5} {6}", moduleId, buttonText[0].text, buttonText[1].text, buttonText[2].text, buttonText[3].text, buttonText[4].text, buttonText[5].text);
		Debug.LogFormat("[Blue Hexabuttons #{0}] Buttons linked to which rotation in reading order: {1} {2} {3} {4} {5} {6}", moduleId, (order.IndexOf(buttonText[0].text) % 6) + 1, (order.IndexOf(buttonText[1].text) % 6) + 1, (order.IndexOf(buttonText[2].text) % 6) + 1, (order.IndexOf(buttonText[3].text) % 6) + 1, (order.IndexOf(buttonText[4].text) % 6) + 1, (order.IndexOf(buttonText[5].text) % 6) + 1);
		Debug.LogFormat("[Blue Hexabuttons #{0}] Rotation Values: {1} {2} {3} {4} {5} {6}", moduleId, blueRotations[0], blueRotations[1], blueRotations[2], blueRotations[3], blueRotations[4], blueRotations[5]);

		hexButtons[6].OnInteractEnded = delegate { releasedCenter(); };
		TPOrder = "012345";
	}
	void pressedCenter()
	{
		Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
		Vector3 pos = buttonMesh[6].transform.localPosition;
		pos.y = 0.0126f;
		buttonMesh[6].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
	}
	void releasedCenter()
	{
		Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonRelease, transform);
		Vector3 pos = buttonMesh[6].transform.localPosition;
		pos.y = 0.0169f;
		buttonMesh[6].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
		if (flag)
		{
			flag = false;
			StartCoroutine(movements(blueRotations));
		}
		else
		{
			flag = true;
			hexButtons[0].transform.localPosition = new Vector3(-0.025f, 0.0169f, 0.034f);
			hexButtons[1].transform.localPosition = new Vector3(0.025f, 0.0169f, 0.034f);
			hexButtons[2].transform.localPosition = new Vector3(-0.05f, 0.0169f, -0.008f);
			hexButtons[3].transform.localPosition = new Vector3(0.05f, 0.0169f, -0.008f);
			hexButtons[4].transform.localPosition = new Vector3(-0.025f, 0.0169f, -0.05f);
			hexButtons[5].transform.localPosition = new Vector3(0.025f, 0.0169f, -0.05f);
			foreach (int i in buttonIndex)
			{
				buttonText[i].color = Color.white;
				buttonText[i].text = blueButtonText[i] + "";
				hexButtons[i].OnInteract = null;
				ledMesh[i].material = ledColors[0];
			}
			numButtonPresses = 0;
			TPOrder = "012345";
		}
	}
	void pressedButton(int n, int p)
	{
		Debug.LogFormat("[Blue Hexabuttons #{0}] User pressed {1}, which has a value of {2}.", moduleId, positions[TPOrder.IndexOf((n + ""))], p);
		Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
		if (solution[numButtonPresses] == p)
		{
			Vector3 pos = buttonMesh[n].transform.localPosition;
			pos.y = 0.0126f;
			buttonMesh[n].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
			hexButtons[n].OnInteract = null;
			ledMesh[n].material = ledColors[1];
			numButtonPresses++;
			if (numButtonPresses == 6)
			{
				moduleSolved = true;
				hexButtons[6].OnInteract = null;
				hexButtons[6].OnInteractEnded = null;
				module.HandlePass();
			}
		}
		else
		{
			Debug.LogFormat("[Blue Hexabuttons #{0}] Strike! I was expecting a value of {1}!", moduleId, solution[numButtonPresses]);
			module.HandleStrike();
			foreach (int i in buttonIndex)
			{
				hexButtons[i].OnInteract = delegate { pressedButton(i, blueButtonValues[i]); return false; };
				Vector3 pos = buttonMesh[i].transform.localPosition;
				pos.y = 0.0169f;
				buttonMesh[i].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
				ledMesh[i].material = ledColors[0];
			}
			numButtonPresses = 0;
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
			for (int bb = 0; bb < 6; bb++)
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
		for (int i = 0; i < nums.Length; i++)
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
			if (swaps.Length > 0)
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
			else if (numcw > 0)
			{
				float[][] diff = new float[6][];
				int[] cwpos = { 0, 1, 3, 5, 4, 2 };
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
					TPOrder = TPOrder[2] + "" + TPOrder[0] + "" + TPOrder[4] + "" + TPOrder[1] + "" + TPOrder[5] + "" + TPOrder[3];
				}

			}
			else
			{
				float[][] diff = new float[6][];
				int[] cwpos = { 0, 2, 4, 5, 3, 1 };
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
			hexButtons[i].OnInteract = delegate { pressedButton(i, blueButtonValues[i]); return false; };
		for (int aa = 0; aa < 100; aa++)
		{
			Vector3 pos = hexButtons[6].transform.localPosition;
			pos.y += 0.0001f;
			hexButtons[6].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
			yield return new WaitForSeconds(0.02f);
		}
		if (!(moduleSolved))
        {
			hexButtons[6].OnInteract = delegate { pressedCenter(); return false; };
			hexButtons[6].OnInteractEnded = delegate { releasedCenter(); };
		}
	}
#pragma warning disable 414
	private readonly string TwitchHelpMessage = @"!{0} press|p tl/1 tr/2 ml/3 mr/4 bl/5 br/6 c/7 presses the top-left, top-right, middle-left, middle-right, bottom-left, bottom-right, and center buttons in that order.";
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
					int cursor = -1;
					switch (param[i])
					{
						case "TL":
						case "1":
							cursor = TPOrder[0] - '0';
							break;
						case "TR":
						case "2":
							cursor = TPOrder[1] - '0';
							break;
						case "ML":
						case "3":
							cursor = TPOrder[2] - '0';
							break;
						case "MR":
						case "4":
							cursor = TPOrder[3] - '0';
							break;
						case "BL":
						case "5":
							cursor = TPOrder[4] - '0';
							break;
						case "BR":
						case "6":
							cursor = TPOrder[5] - '0';
							break;
						case "C":
						case "7":
							cursor = 6;
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
			else
				yield return "sendtochat An error occured because the user inputted something wrong.";
		}
		else
			yield return "sendtochat An error occured because the user inputted something wrong.";
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
					break;
				default:
					return false;
			}
		}
		return true;
	}
	IEnumerator TwitchHandleForcedSolve()
    {
		if (flag)
        {
			hexButtons[6].OnInteract();
			yield return new WaitForSeconds(0.2f);
			hexButtons[6].OnInteractEnded();
			yield return new WaitForSeconds(0.2f);
		}
		while (hexButtons.All(x => x.OnInteract == null)) yield return true;
		while (!moduleSolved)
        {
			hexButtons[Array.IndexOf(blueButtonValues, solution[numButtonPresses])].OnInteract();
			yield return new WaitForSeconds(0.2f);
		}
    }
}
