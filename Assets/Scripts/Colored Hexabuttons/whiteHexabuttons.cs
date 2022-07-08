using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class whiteHexabuttons : MonoBehaviour
{
	public KMBombModule module;
	public KMAudio Audio;
	private int moduleId;
	private static int moduleIdCounter = 1;
	public KMSelectable[] hexButtons;
	public MeshRenderer[] buttonMesh;
	public Material[] buttonColors;
	public Light[] lights;
	public TextMesh[] buttonText;
	private string solution;
	private int numButtonPresses;
	private string whiteBHC;
	private int[] whiteFlashes;
	private string[] positions = { "TL", "TR", "ML", "MR", "BL", "BR" };
	private int[] buttonIndex = { 0, 1, 2, 3, 4, 5 };
	private bool colorBlind = false;
	void Awake()
	{
		moduleId = moduleIdCounter++;
		string[][] chart =
		{
			new string[]{"Y*", "BP", "B*", "GB", "YG", "P*"},
			new string[]{"OP", "GP", "26", "RO", "16", "25"},
			new string[]{"46", "R*", "36", "15", "RG", "YP"},
			new string[]{"56", "RB", "RP", "45", "OY", "13"},
			new string[]{"OG", "24", "14", "G*", "OB", "23"},
			new string[]{"RY", "O*", "34", "35", "12", "YB"},
		};
		whiteBHC = new string("ROYGBP".ToCharArray().Shuffle());
		foreach (int i in buttonIndex)
		{
			hexButtons[i].OnHighlight = delegate { buttonMesh[i].material = buttonColors["ROYGBP".IndexOf(whiteBHC[i])]; if (colorBlind) buttonText[i].text = whiteBHC[i] + ""; };
			hexButtons[i].OnHighlightEnded = delegate { buttonMesh[i].material = buttonColors[6]; buttonText[i].text = ""; };
			hexButtons[i].OnInteract = delegate { pressedButton(i); return false; };
			buttonText[i].color = "OY".Contains(whiteBHC[i] + "") ? Color.black : Color.white;
		}
		whiteBHC = whiteBHC + "" + whiteBHC[UnityEngine.Random.Range(0, 6)];
		hexButtons[6].OnHighlight = delegate { buttonMesh[6].material = buttonColors["ROYGBP".IndexOf(whiteBHC[6])]; if (colorBlind) buttonText[6].text = whiteBHC[6] + ""; };
		hexButtons[6].OnHighlightEnded = delegate { buttonMesh[6].material = buttonColors[6]; buttonText[6].text = ""; };
		buttonText[6].color = "OY".Contains(whiteBHC[6] + "") ? Color.black : Color.white;
		Debug.LogFormat("[White Hexabuttons #{0}] Hovered Colors: {1}", moduleId, whiteBHC);
		whiteFlashes = new int[6];
		for (int aa = 0; aa < 6; aa++)
			whiteFlashes[aa] = UnityEngine.Random.Range(0, 6);
		Debug.LogFormat("[White Hexabuttons #{0}] Flashed Colors: {1}{2}{3}{4}{5}{6}", moduleId, whiteBHC[whiteFlashes[0]], whiteBHC[whiteFlashes[1]], whiteBHC[whiteFlashes[2]], whiteBHC[whiteFlashes[3]], whiteBHC[whiteFlashes[4]], whiteBHC[whiteFlashes[5]]);
		solution = whiteBHC[0] + "" + whiteBHC[1] + "" + whiteBHC[3] + "" + whiteBHC[5] + "" + whiteBHC[4] + "" + whiteBHC[2];
		solution = solution.Substring(solution.IndexOf(whiteBHC[6])) + "" + solution.Substring(0, solution.IndexOf(whiteBHC[6]));
		Debug.LogFormat("[White Hexabuttons #{0}] Initial color sequence: {1}", moduleId, solution);
		for (int aa = 0; aa < 6; aa++)
		{
			string instruct = chart["ROYGBP".IndexOf(whiteBHC[whiteFlashes[aa]])]["ROYGBP".IndexOf(whiteBHC[whiteFlashes[(aa + 1) % 6]])];
			if (instruct.Contains("*"))
			{
				if (instruct[0] == solution[0])
					solution = solution.Substring(1) + "" + solution[0];
				else
					solution = instruct[0] + "" + solution.Replace(instruct[0] + "", "");
			}
			else
			{
				if ("123456".Contains(instruct[0] + ""))
					instruct = solution[instruct[0] - '0' - 1] + "" + solution[instruct[1] - '0' - 1];
				solution = solution.Replace(instruct[0], '*');
				solution = solution.Replace(instruct[1], instruct[0]);
				solution = solution.Replace('*', instruct[1]);
			}
			Debug.LogFormat("[White Hexabuttons #{0}] Instruction {1}: {2}", moduleId, chart["ROYGBP".IndexOf(whiteBHC[whiteFlashes[aa]])]["ROYGBP".IndexOf(whiteBHC[whiteFlashes[(aa + 1) % 6]])], solution);
		}
		hexButtons[6].OnInteract = delegate { pressedCenter(); return false; };
		hexButtons[7].OnInteract = delegate { colorBlind = !(colorBlind); return false; };
		numButtonPresses = 0;
	}
	void Start()
	{
		
		float scalar = transform.lossyScale.x;
		foreach (Light light in lights)
		{
			light.enabled = false;
			light.range *= scalar;
		}
		
	}
	void pressedCenter()
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
			buttonText[i].text = "";
		}
		numButtonPresses = 0;
		hexButtons[6].OnHighlight = null;
		hexButtons[6].OnHighlightEnded = null;
		buttonMesh[6].material = buttonColors["ROYGBP".IndexOf(whiteBHC[6])];
		Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
		Vector3 pos = buttonMesh[6].transform.localPosition;
		pos.y = 0.0126f;
		buttonMesh[6].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
		hexButtons[6].OnInteract = null;
		StartCoroutine(whiteFlasher());
	}
	void pressedButton(int n)
	{
		Debug.LogFormat("[White Hexabuttons #{0}] User pressed {1} which is the color {2}", moduleId, positions[n], whiteBHC[n]);
		Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
		if (solution[numButtonPresses] == whiteBHC[n])
		{
			Vector3 pos = buttonMesh[n].transform.localPosition;
			pos.y = 0.0126f;
			buttonMesh[n].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
			hexButtons[n].OnInteract = null;
			hexButtons[n].OnHighlight = null;
			hexButtons[n].OnHighlightEnded = null;
			buttonMesh[n].material = buttonColors["ROYGBP".IndexOf(whiteBHC[n])];
			numButtonPresses++;
			if (numButtonPresses == 6)
			{
				hexButtons[6].OnInteract = null;
				hexButtons[6].OnHighlight = null;
				hexButtons[6].OnHighlightEnded = null;
				hexButtons[7].OnInteract = null;
				module.HandlePass();
			}
		}
		else
		{
			Debug.LogFormat("[White Hexabuttons #{0}] Strike! I was expecting {1} which is the color {2}", moduleId, positions[whiteBHC.IndexOf(solution[numButtonPresses])], solution[numButtonPresses]);
			module.HandleStrike();
			foreach (int i in buttonIndex)
			{
				Vector3 pos = buttonMesh[i].transform.localPosition;
				pos.y = 0.0169f;
				buttonMesh[i].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
				buttonMesh[i].material = buttonColors[6];
				hexButtons[i].OnInteract = delegate { pressedButton(i); return false; };
				hexButtons[i].OnHighlight = delegate { buttonMesh[i].material = buttonColors["ROYGBP".IndexOf(whiteBHC[i])]; if (colorBlind) buttonText[i].text = whiteBHC[i] + ""; };
				hexButtons[i].OnHighlightEnded = delegate { buttonMesh[i].material = buttonColors[6]; buttonText[i].text = ""; };
				buttonText[i].text = "";
			}
			numButtonPresses = 0;
		}
	}
	IEnumerator whiteFlasher()
	{
		yield return new WaitForSeconds(1.0f);
		for (int aa = 0; aa < 6; aa++)
		{
			lights[whiteFlashes[aa]].enabled = true;
			yield return new WaitForSeconds(0.7f);
			lights[whiteFlashes[aa]].enabled = false;
			yield return new WaitForSeconds(0.3f);
		}
		Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonRelease, transform);
		Vector3 pos = buttonMesh[6].transform.localPosition;
		pos.y = 0.0169f;
		buttonMesh[6].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
		hexButtons[6].OnHighlight = delegate { buttonMesh[6].material = buttonColors["ROYGBP".IndexOf(whiteBHC[6])]; if (colorBlind) buttonText[6].text = whiteBHC[6] + ""; };
		hexButtons[6].OnHighlightEnded = delegate { buttonMesh[6].material = buttonColors[6]; buttonText[6].text = ""; };
		hexButtons[6].OnInteract = delegate { pressedCenter(); return false; };
		buttonMesh[6].material = buttonColors[6];
		buttonText[6].text = "";
		foreach (int i in buttonIndex)
		{
			hexButtons[i].OnHighlight = delegate { buttonMesh[i].material = buttonColors["ROYGBP".IndexOf(whiteBHC[i])]; if (colorBlind) buttonText[i].text = whiteBHC[i] + ""; };
			hexButtons[i].OnHighlightEnded = delegate { buttonMesh[i].material = buttonColors[6]; buttonText[i].text = ""; };
			hexButtons[i].OnInteract = delegate { pressedButton(i); return false; };
		}
	}
#pragma warning disable 414
	private readonly string TwitchHelpMessage = @"!{0} press|p tl/1 tr/2 ml/3 mr/4 bl/5 br/6 c/7 presses the top-left, top-right, middle-left, middle-right, bottom-left, bottom-right, and center buttons in that order. !{0} hover|h tl/1 tr/2 ml/3 mr/4 bl/5 br/6 c/7 will hover the buttons in the same fashion.";
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
					}
				}
			}
			else
				yield return "sendtochat An error occured because the user inputted something wrong.";
		}
		else if ((Regex.IsMatch(param[0], @"^\s*HOVER\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant) || Regex.IsMatch(param[0], @"^\s*H\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant)) && param.Length > 1)
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
					if (hexButtons[cursor].OnHighlight != null)
					{
						hexButtons[cursor].OnHighlight();
						yield return new WaitForSeconds(1.0f);
						hexButtons[cursor].OnHighlightEnded();
						yield return new WaitForSeconds(0.3f);
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
				case "SL":
					break;
				default:
					return false;
			}
		}
		return true;
	}
	private bool isPos2(string[] param)
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
}
