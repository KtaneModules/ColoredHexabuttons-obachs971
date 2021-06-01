using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueHexabuttons {

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
	private int[] blueRotations;
	private int[] blueButtonValues;
	private string blueButtonText;
	private bool flag;
	private string TPOrder;
	private int numButtonPresses;
	private bool moduleSolved;
	private string[] positions = { "TL", "TR", "ML", "MR", "BL", "BR" };
	private int[] buttonIndex = { 0, 1, 2, 3, 4, 5 };
	public BlueHexabuttons(ColorfulButtonSeries m, KMAudio aud, int MI, KMSelectable[] HB, MeshRenderer[] BM, TextMesh[] BT, Material[] LC, MeshRenderer[] LM, Transform T)
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
		Debug.LogFormat("[Colored Hexabuttons #{0}] Color Generated: Blue", moduleId);
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
		hexButtons[6].OnInteract = delegate { pressedBlueCenter(); return false; };
		Debug.LogFormat("[Colored Hexabuttons #{0}] Generated text on buttons in reading order: {1} {2} {3} {4} {5} {6}", moduleId, buttonText[0].text, buttonText[1].text, buttonText[2].text, buttonText[3].text, buttonText[4].text, buttonText[5].text);
		Debug.LogFormat("[Colored Hexabuttons #{0}] Buttons linked to which rotation in reading order: {1} {2} {3} {4} {5} {6}", moduleId, (order.IndexOf(buttonText[0].text) % 6) + 1, (order.IndexOf(buttonText[1].text) % 6) + 1, (order.IndexOf(buttonText[2].text) % 6) + 1, (order.IndexOf(buttonText[3].text) % 6) + 1, (order.IndexOf(buttonText[4].text) % 6) + 1, (order.IndexOf(buttonText[5].text) % 6) + 1);
		Debug.LogFormat("[Colored Hexabuttons #{0}] Rotation Values: {1} {2} {3} {4} {5} {6}", moduleId, blueRotations[0], blueRotations[1], blueRotations[2], blueRotations[3], blueRotations[4], blueRotations[5]);

		hexButtons[6].OnInteractEnded = delegate { pressedBlueCenterRelease(); };
	}
	void pressedBlueCenter()
	{
		if (!(moduleSolved))
		{
			Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
			Vector3 pos = buttonMesh[6].transform.localPosition;
			pos.y = 0.0126f;
			buttonMesh[6].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
		}
	}
	void pressedBlueCenterRelease()
	{
		if (!(moduleSolved))
		{
			Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonRelease, transform);
			Vector3 pos = buttonMesh[6].transform.localPosition;
			pos.y = 0.0169f;
			buttonMesh[6].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
			if (flag)
			{
				flag = false;
				coloredHexabuttons.StartCoroutine(movements(blueRotations));
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
				coloredHexabuttons.setOrder("0123456");
			}
		}
	}
	void pressedBlue(int n, int p)
	{
		if (!(moduleSolved))
		{
			Debug.LogFormat("[Colored Hexabuttons #{0}] User pressed {1}, which has a value of {2}.", moduleId, positions[TPOrder.IndexOf((n + ""))], p);
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
					coloredHexabuttons.Solve();
				}
			}
			else
			{
				Debug.LogFormat("[Colored Hexabuttons #{0}] Strike! I was expecting a value of {1}!", moduleId, solution[numButtonPresses]);
				coloredHexabuttons.Strike();
				foreach (int i in buttonIndex)
				{
					hexButtons[i].OnInteract = delegate { pressedBlue(i, blueButtonValues[i]); return false; };
					Vector3 pos = buttonMesh[i].transform.localPosition;
					pos.y = 0.0169f;
					buttonMesh[i].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
					ledMesh[i].material = ledColors[0];
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
		TPOrder = "012345";
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
			hexButtons[i].OnInteract = delegate { pressedBlue(i, blueButtonValues[i]); return false; };
		for (int aa = 0; aa < 100; aa++)
		{
			Vector3 pos = hexButtons[6].transform.localPosition;
			pos.y += 0.0001f;
			hexButtons[6].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
			yield return new WaitForSeconds(0.02f);
		}
		hexButtons[6].OnInteract = delegate { pressedBlueCenter(); return false; };
		hexButtons[6].OnInteractEnded = delegate { pressedBlueCenterRelease(); };
		TPOrder = TPOrder + "6";
		coloredHexabuttons.setOrder(TPOrder);
	}
}
