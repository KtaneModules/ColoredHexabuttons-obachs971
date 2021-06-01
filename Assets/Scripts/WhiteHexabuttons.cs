using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteHexabuttons {
	private ColorfulButtonSeries coloredHexabuttons;
	private KMAudio Audio;
	private int moduleId;
	private KMSelectable[] hexButtons;
	private MeshRenderer[] buttonMesh;
	private Material[] buttonColors;
	private Material[] ledColors;
	private MeshRenderer[] ledMesh;
	private Light[] lights;
	private Transform transform;
	private string solution;
	private int numButtonPresses;
	private bool moduleSolved;
	private string whiteBHC;
	private int[] whiteFlashes;
	private string[] positions = { "TL", "TR", "ML", "MR", "BL", "BR" };
	private int[] buttonIndex = { 0, 1, 2, 3, 4, 5 };
	public WhiteHexabuttons(ColorfulButtonSeries m, KMAudio aud, int MI, KMSelectable[] HB, MeshRenderer[] BM, Material[] BC, Material[] LC, MeshRenderer[] LM, Light[] L, Transform T)
	{
		coloredHexabuttons = m;
		Audio = aud;
		moduleId = MI;
		hexButtons = HB;
		buttonMesh = BM;
		buttonColors = BC;
		ledColors = LC;
		ledMesh = LM;
		lights = L;
		transform = T;
	}
	public void run()
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
		foreach (int i in buttonIndex)
		{
			whiteBHC = whiteBHC + "" + colorChoices[UnityEngine.Random.Range(0, colorChoices.Length)];
			hexButtons[i].OnHighlight = delegate { buttonMesh[i].material = buttonColors["ROYGBP".IndexOf(whiteBHC[i])]; };
			hexButtons[i].OnHighlightEnded = delegate { buttonMesh[i].material = buttonColors[6]; };
			hexButtons[i].OnInteract = delegate { pressedWhite(i); return false; };
			colorChoices = colorChoices.Replace(whiteBHC[i] + "", "");
		}
		whiteBHC = whiteBHC + "" + whiteBHC[UnityEngine.Random.Range(0, 6)];
		hexButtons[6].OnHighlight = delegate { buttonMesh[6].material = buttonColors["ROYGBP".IndexOf(whiteBHC[6])]; };
		hexButtons[6].OnHighlightEnded = delegate { buttonMesh[6].material = buttonColors[6]; };
		Debug.LogFormat("[Colored Hexabuttons #{0}] Hovered Colors: {1}", moduleId, whiteBHC);
		whiteFlashes = new int[6];
		for (int aa = 0; aa < 6; aa++)
			whiteFlashes[aa] = UnityEngine.Random.Range(0, 6);
		Debug.LogFormat("[Colored Hexabuttons #{0}] Flashed Colors: {1}{2}{3}{4}{5}{6}", moduleId, whiteBHC[whiteFlashes[0]], whiteBHC[whiteFlashes[1]], whiteBHC[whiteFlashes[2]], whiteBHC[whiteFlashes[3]], whiteBHC[whiteFlashes[4]], whiteBHC[whiteFlashes[5]]);
		solution = whiteBHC[0] + "" + whiteBHC[1] + "" + whiteBHC[3] + "" + whiteBHC[5] + "" + whiteBHC[4] + "" + whiteBHC[2];
		solution = solution.Substring(solution.IndexOf(whiteBHC[6])) + "" + solution.Substring(0, solution.IndexOf(whiteBHC[6]));
		Debug.LogFormat("[Colored Hexabuttons #{0}] Initial color sequence: {1}", moduleId, solution);
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
				if ("123456".IndexOf(instruct[0]) >= 0)
					instruct = solution[instruct[0] - '0' - 1] + "" + solution[instruct[1] - '0' - 1];
				solution = solution.Replace(instruct[0], '*');
				solution = solution.Replace(instruct[1], instruct[0]);
				solution = solution.Replace('*', instruct[1]);
			}
			Debug.LogFormat("[Colored Hexabuttons #{0}] Instruction {1}: {2}", moduleId, chart["ROYGBP".IndexOf(whiteBHC[whiteFlashes[aa]])]["ROYGBP".IndexOf(whiteBHC[whiteFlashes[(aa + 1) % 6]])], solution);
		}
		hexButtons[6].OnInteract = delegate { pressedWhiteCenter(); return false; };
		numButtonPresses = 0;
	}
	void pressedWhiteCenter()
	{
		if (!(moduleSolved))
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
			coloredHexabuttons.StartCoroutine(whiteFlasher());
		}
	}
	void pressedWhite(int n)
	{
		if (!(moduleSolved))
		{
			Debug.LogFormat("[Colored Hexabuttons #{0}] User pressed {1} which is the color {2}", moduleId, positions[n], whiteBHC[n]);
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
				Debug.LogFormat("[Colored Hexabuttons #{0}] Strike! I was expecting {1} which is the color {2}", moduleId, positions[whiteBHC.IndexOf(solution[numButtonPresses])], solution[numButtonPresses]);
				coloredHexabuttons.Strike();
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
				}
				numButtonPresses = 0;
			}
		}
	}
	IEnumerator whiteFlasher()
	{
		yield return new WaitForSeconds(1.0f);
		for (int aa = 0; aa < 6; aa++)
		{
			lights[whiteFlashes[aa]].enabled = true;
			yield return new WaitForSeconds(0.5f);
			lights[whiteFlashes[aa]].enabled = false;
			yield return new WaitForSeconds(0.5f);
		}
		Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonRelease, transform);
		Vector3 pos = buttonMesh[6].transform.localPosition;
		pos.y = 0.0169f;
		buttonMesh[6].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
		hexButtons[6].OnHighlight = delegate { buttonMesh[6].material = buttonColors["ROYGBP".IndexOf(whiteBHC[6])]; };
		hexButtons[6].OnHighlightEnded = delegate { buttonMesh[6].material = buttonColors[6]; };
		hexButtons[6].OnInteract = delegate { pressedWhiteCenter(); return false; };
		buttonMesh[6].material = buttonColors[6];
		foreach (int i in buttonIndex)
		{
			hexButtons[i].OnHighlight = delegate { buttonMesh[i].material = buttonColors["ROYGBP".IndexOf(whiteBHC[i])]; };
			hexButtons[i].OnHighlightEnded = delegate { buttonMesh[i].material = buttonColors[6]; };
			hexButtons[i].OnInteract = delegate { pressedWhite(i); return false; };
		}
	}
}
