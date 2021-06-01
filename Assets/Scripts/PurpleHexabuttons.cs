using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleHexabuttons{
	private ColorfulButtonSeries coloredHexabuttons;
	private KMAudio Audio;
	private int moduleId;
	private KMSelectable[] hexButtons;
	private MeshRenderer[] buttonMesh;
	private TextMesh[] buttonText;
	private Material[] ledColors;
	private MeshRenderer[] ledMesh;
	private Transform transform;
	private string[] voiceMessage;
	private int[] solution;
	private int numButtonPresses;
	private bool moduleSolved;
	private string[] positions = { "TL", "TR", "ML", "MR", "BL", "BR" };
	private int[] buttonIndex = { 0, 1, 2, 3, 4, 5 };
	public PurpleHexabuttons(ColorfulButtonSeries m, KMAudio aud, int MI, KMSelectable[] HB, MeshRenderer[] BM, TextMesh[] BT, Material[] LC, MeshRenderer[] LM, Transform T)
	{
		coloredHexabuttons = m;
		Audio = aud;
		moduleId = MI;
		hexButtons = HB;
		buttonMesh = BM;
		buttonText = BT;
		ledColors = LC;
		ledMesh = LM;
		transform = T;
	}
	public void run()
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
		for (int aa = 0; aa < num0; aa++)
		{
			pos0 = pos0 + "" + pos1[UnityEngine.Random.Range(0, pos1.Length)];
			bins[0][pos0[aa] - '0'] = 0;
			pos1 = pos1.Replace(pos0[aa] + "", "");
		}
		string pos2 = "012345";
		for (int aa = 0; aa < 2; aa++)
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
		for (int aa = 0; aa < num0; aa++)
		{
			int n = pos2[UnityEngine.Random.Range(0, pos2.Length)] - '0';
			bins[1][n] = 0;
			pos2 = pos2.Replace(n + "", "");
		}
		string[] opers = { "AND", "OR", "XOR", "NAND", "NOR", "XNOR", "->", "<-" };
		int oper = UnityEngine.Random.Range(0, 8);
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
				default://<-
					if (bins[0][aa] == 1 || bins[1][aa] == 0)
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
			if (numbers[aa] < 10)
			{
				voiceMessage[aa * 2] = "0";
				voiceMessage[(aa * 2) + 1] = numbers[aa] + "";
			}
			else
			{
				voiceMessage[aa * 2] = (numbers[aa] / 10) + "";
				voiceMessage[(aa * 2) + 1] = (numbers[aa] % 10) + "";
			}
		}
		Debug.LogFormat("[Colored Hexabuttons #{0}] Generated 6 digit number: {1}{2}{3}{4}{5}{6}", moduleId, voiceMessage[0], voiceMessage[1], voiceMessage[2], voiceMessage[3], voiceMessage[4], voiceMessage[5]);
		Debug.LogFormat("[Colored Hexabuttons #{0}] Generated left binary: {1}{2}{3}{4}{5}{6}", moduleId, bins[0][0], bins[0][1], bins[0][2], bins[0][3], bins[0][4], bins[0][5]);
		Debug.LogFormat("[Colored Hexabuttons #{0}] Generated right binary: {1}{2}{3}{4}{5}{6}", moduleId, bins[1][0], bins[1][1], bins[1][2], bins[1][3], bins[1][4], bins[1][5]);
		Debug.LogFormat("[Colored Hexabuttons #{0}] Resulting binary: {1}{2}{3}{4}{5}{6}", moduleId, bins[2][0], bins[2][1], bins[2][2], bins[2][3], bins[2][4], bins[2][5]);
		Debug.LogFormat("[Colored Hexabuttons #{0}] Logic operator used: {1}", moduleId, opers[oper]);
		hexButtons[6].OnInteract = delegate { pressedPurpleCenter(); return false; };
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
		Debug.LogFormat("[Colored Hexabuttons #{0}] Buttons to be read in this order: {1} {2} {3} {4} {5} {6}", moduleId, positions[solution[0]], positions[solution[1]], positions[solution[2]], positions[solution[3]], positions[solution[4]], positions[solution[5]]);
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
			buttonText[solution[i]].text = randomAlpha[n + (4 * UnityEngine.Random.Range(0, 9))] + "";
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
					solution[bb] = numButtonPresses + 0;
					numButtonPresses++;
					if (numButtonPresses == 6)
						break;
				}
			}
			if (numButtonPresses == 6)
				break;
		}
		Debug.LogFormat("[Colored Hexabuttons #{0}] Solution: {1} {2} {3} {4} {5} {6}", moduleId, positions[solution[0]], positions[solution[1]], positions[solution[2]], positions[solution[3]], positions[solution[4]], positions[solution[5]]);
		numButtonPresses = 0;
		foreach (int i in buttonIndex)
			hexButtons[i].OnInteract = delegate { pressedPurple(i); return false; };
	}
	void pressedPurple(int n)
	{
		if (!(moduleSolved))
		{
			Debug.LogFormat("[Colored Hexabuttons #{0}] User pressed {1}.", moduleId, positions[n]);
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
					coloredHexabuttons.Solve();
				}
			}
			else
			{
				Debug.LogFormat("[Colored Hexabuttons #{0}] Strike! I was expecting the {1} button!", moduleId, positions[solution[numButtonPresses]]);
				coloredHexabuttons.Strike();
				foreach (int i in buttonIndex)
				{
					Vector3 pos = buttonMesh[i].transform.localPosition;
					pos.y = 0.0169f;
					buttonMesh[i].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
					hexButtons[i].OnInteract = delegate { pressedPurple(i); return false; };
					ledMesh[i].material = ledColors[0];
				}
				numButtonPresses = 0;
			}
		}
	}
	void pressedPurpleCenter()
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
		hexButtons[6].OnInteract = delegate { pressedPurpleCenter(); return false; };
	}
}
