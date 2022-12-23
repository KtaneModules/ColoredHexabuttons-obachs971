using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;

public class purpleHexabuttons : MonoBehaviour
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
	public TextMesh centerText;
	private string[] voiceMessage;
	private int[] solution;
	private int numButtonPresses;
	private bool moduleSolved;
	private string[] positions = { "TL", "TR", "ML", "MR", "BL", "BR" };
	private int[] buttonIndex = { 0, 1, 2, 3, 4, 5 };
	private bool deafMode = false;
	void Awake()
	{
		moduleSolved = false;
		moduleId = moduleIdCounter++;
		int[][] bins =
		{
			new int[]{1, 1, 1, 1, 1, 1},
			new int[]{1, 1, 1, 1, 1, 1},
			new int[]{0, 0, 0, 0, 0, 0},
		};
		string pos1 = "012345";
		string pos0 = "";
		int num0 = Random.Range(2, 5);
		for (int aa = 0; aa < num0; aa++)
		{
			pos0 = pos0 + "" + pos1[Random.Range(0, pos1.Length)];
			bins[0][pos0[aa] - '0'] = 0;
			pos1 = pos1.Replace(pos0[aa] + "", "");
		}
		string pos2 = "012345";
		for (int aa = 0; aa < 2; aa++)
		{
			int n = pos0[Random.Range(0, pos0.Length)] - '0';
			bins[1][n] = aa;
			pos0 = pos0.Replace(n + "", "");
			pos2 = pos2.Replace(n + "", "");
		}
		for (int aa = 0; aa < 2; aa++)
		{
			int n = pos1[Random.Range(0, pos1.Length)] - '0';
			bins[1][n] = aa;
			pos1 = pos1.Replace(n + "", "");
			pos2 = pos2.Replace(n + "", "");
		}
		num0 = Random.Range(0, 3);
		for (int aa = 0; aa < num0; aa++)
		{
			int n = pos2[Random.Range(0, pos2.Length)] - '0';
			bins[1][n] = 0;
			pos2 = pos2.Replace(n + "", "");
		}
		string[] opers = { "AND", "OR", "XOR", "NAND", "NOR", "XNOR", "->", "<-", "!->", "<-!" };
		int oper = Random.Range(0, opers.Length);
		for (int aa = 0; aa < 6; aa++)
		{
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
				case 7://<-
					if (bins[0][aa] == 1 || bins[1][aa] == 0)
						bins[2][aa] = 1;
					break;
				case 8://!->
					if (bins[0][aa] == 1 && bins[1][aa] == 0)
						bins[2][aa] = 1;
					break;
				default://<-!
					if (bins[0][aa] == 0 && bins[1][aa] == 1)
						bins[2][aa] = 1;
					break;
			}
		}
		int[] numbers = { 0, 0, 0 };
		for (int aa = 0; aa < 6; aa++)
		{
			for (int bb = 0; bb < 3; bb++)
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
		voiceMessage = new string[6];
		for (int aa = 0; aa < 3; aa++)
		{
			voiceMessage[aa * 2] = (numbers[aa] / 10) + "";
			voiceMessage[(aa * 2) + 1] = (numbers[aa] % 10) + "";
		}
		Debug.LogFormat("[Purple Hexabuttons #{0}] Generated 6 digit number: {1}{2}{3}{4}{5}{6}", moduleId, voiceMessage[0], voiceMessage[1], voiceMessage[2], voiceMessage[3], voiceMessage[4], voiceMessage[5]);
		Debug.LogFormat("[Purple Hexabuttons #{0}] Generated left binary: {1}{2}{3}{4}{5}{6}", moduleId, bins[0][0], bins[0][1], bins[0][2], bins[0][3], bins[0][4], bins[0][5]);
		Debug.LogFormat("[Purple Hexabuttons #{0}] Generated right binary: {1}{2}{3}{4}{5}{6}", moduleId, bins[1][0], bins[1][1], bins[1][2], bins[1][3], bins[1][4], bins[1][5]);
		Debug.LogFormat("[Purple Hexabuttons #{0}] Resulting binary: {1}{2}{3}{4}{5}{6}", moduleId, bins[2][0], bins[2][1], bins[2][2], bins[2][3], bins[2][4], bins[2][5]);
		Debug.LogFormat("[Purple Hexabuttons #{0}] Logic operator used: {1}", moduleId, opers[oper]);
		hexButtons[6].OnInteract = delegate { pressedCenter(); return false; };
		hexButtons[7].OnInteract = delegate { deafMode = !(deafMode); return false; };
		solution = new int[6];
		numButtonPresses = 0;
		for (int aa = 0; aa < 10; aa++)
		{
			for (int bb = 0; bb < 6; bb++)
			{
				if (aa == voiceMessage[bb][0] - '0')
				{
					solution[bb] = numButtonPresses + 0;
					numButtonPresses++;
					if (numButtonPresses == 6)
						break;
				}
			}
			if (numButtonPresses == 6)
				break;
		}
		Debug.LogFormat("[Purple Hexabuttons #{0}] Buttons to be read in this order: {1} {2} {3} {4} {5} {6}", moduleId, positions[solution[0]], positions[solution[1]], positions[solution[2]], positions[solution[3]], positions[solution[4]], positions[solution[5]]);
		numbers[0] = 0;
		numbers[1] = 0;
		numbers[2] = 0;
		for (int aa = 0; aa < 6; aa++)
		{
			bins[0][aa] = Random.Range(0, 2);
			bins[1][aa] = Random.Range(0, 2);
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
				case 7://<-
					if (bins[0][aa] == 1 || bins[1][aa] == 0)
						bins[2][aa] = 1;
					break;
				case 8://!->
					if (bins[0][aa] == 1 && bins[1][aa] == 0)
						bins[2][aa] = 1;
					break;
				default://<-!
					if (bins[0][aa] == 0 && bins[1][aa] == 1)
							bins[2][aa] = 1;
					break;
			}
			for (int bb = 0; bb < 3; bb++)
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
			buttonText[solution[i]].text = randomAlpha[n + (4 * Random.Range(0, 9))] + "";
		}
		string numberDigits = "";
		for (int aa = 0; aa < 3; aa++)
		{
			if (numbers[aa] < 10)
				numberDigits = numberDigits + "0" + numbers[aa];
			else
				numberDigits = numberDigits + "" + numbers[aa];
		}
		Debug.LogFormat("[Purple Hexabuttons #{0}] Generated text on buttons in reading order: {1}{2}{3}{4}{5}{6}", moduleId, buttonText[0].text, buttonText[1].text, buttonText[2].text, buttonText[3].text, buttonText[4].text, buttonText[5].text);
		Debug.LogFormat("[Purple Hexabuttons #{0}] Generated left binary: {1}{2}{3}{4}{5}{6}", moduleId, bins[0][0], bins[0][1], bins[0][2], bins[0][3], bins[0][4], bins[0][5]);
		Debug.LogFormat("[Purple Hexabuttons #{0}] Generated right binary: {1}{2}{3}{4}{5}{6}", moduleId, bins[1][0], bins[1][1], bins[1][2], bins[1][3], bins[1][4], bins[1][5]);
		Debug.LogFormat("[Purple Hexabuttons #{0}] Resulting binary: {1}{2}{3}{4}{5}{6}", moduleId, bins[2][0], bins[2][1], bins[2][2], bins[2][3], bins[2][4], bins[2][5]);
		Debug.LogFormat("[Purple Hexabuttons #{0}] Generated 6 digit number: {1}", moduleId, numberDigits);
		numButtonPresses = 0;
		for (int aa = 0; aa < 10; aa++)
		{
			for (int bb = 0; bb < 6; bb++)
			{
				if (aa == numberDigits[bb] - '0')
				{
					solution[bb] = numButtonPresses + 0;
					numButtonPresses++;
					if (numButtonPresses == 6)
						break;
				}
			}
			if (numButtonPresses == 6)
				break;
		}
		Debug.LogFormat("[Purple Hexabuttons #{0}] Solution: {1} {2} {3} {4} {5} {6}", moduleId, positions[solution[0]], positions[solution[1]], positions[solution[2]], positions[solution[3]], positions[solution[4]], positions[solution[5]]);
		numButtonPresses = 0;
		foreach (int i in buttonIndex)
			hexButtons[i].OnInteract = delegate { pressedButton(i); return false; };
	}
	void pressedButton(int n)
	{
		Debug.LogFormat("[Purple Hexabuttons #{0}] User pressed {1}.", moduleId, positions[n]);
		Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
		if (solution[numButtonPresses] == n)
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
				hexButtons[7].OnInteract = null;
				module.HandlePass();
			}
		}
		else
		{
			Debug.LogFormat("[Purple Hexabuttons #{0}] Strike! I was expecting the {1} button!", moduleId, positions[solution[numButtonPresses]]);
			module.HandleStrike();
			foreach (int i in buttonIndex)
			{
				Vector3 pos = buttonMesh[i].transform.localPosition;
				pos.y = 0.0169f;
				buttonMesh[i].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
				hexButtons[i].OnInteract = delegate { pressedButton(i); return false; };
				ledMesh[i].material = ledColors[0];
			}
			numButtonPresses = 0;
		}
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
		centerText.text = "";
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
	IEnumerator TwitchHandleForcedSolve()
	{
		while (!moduleSolved)
		{
			hexButtons[solution[numButtonPresses]].OnInteract();
			yield return new WaitForSeconds(0.2f);
		}
	}
}
