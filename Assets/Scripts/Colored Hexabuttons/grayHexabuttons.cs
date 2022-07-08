using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class grayHexabuttons : MonoBehaviour
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
	public Light centerLight;

	private int numButtonPresses;
	private bool flag;
	private int[] values;
	private int[] results;
	private string[] positions = { "TL", "TR", "ML", "MR", "BL", "BR" };
	private int[] buttonIndex = { 0, 1, 2, 3, 4, 5 };
	void Awake()
	{
		moduleId = moduleIdCounter++;
		values = new int[6];
		results = new int[6];
		string[] choices = { "123456", "ABCDEF" };
		string funct = "";
		int con = UnityEngine.Random.Range(0, 6) + 1;
		//int con = 2;
		//int[] valueDebug = {4, 1, 5, 3, 2, 6};
		//string functDebug = "CFDBEA";
		Debug.LogFormat("[Gray Hexabuttons #{0}] Constant: {1}", moduleId, con);
		foreach (int aa in buttonIndex)
		{
			values[aa] = (choices[0][UnityEngine.Random.Range(0, choices[0].Length)] - '0');
			//values[aa] = valueDebug[aa];
			choices[0] = choices[0].Replace((values[aa] + ""), "");
			funct = funct + "" + choices[1][UnityEngine.Random.Range(0, choices[1].Length)];
			//funct = funct + "" + functDebug[aa];
			choices[1] = choices[1].Replace(funct[aa] + "", "");
			switch (funct[aa])
			{
				case 'A':
					results[aa] = (values[aa] + con);
					break;
				case 'B':
					results[aa] = ((2 * values[aa]) + con);
					break;
				case 'C':
					results[aa] = (values[aa] + (2 * con));
					break;
				case 'D':
					results[aa] = (values[aa] - con);
					break;
				case 'E':
					results[aa] = ((2 * values[aa]) - con);
					break;
				case 'F':
					results[aa] = (values[aa] - (2 * con));
					break;
			}

			Debug.LogFormat("[Gray Hexabuttons #{0}] {1} button's value: {2}", moduleId, positions[aa], values[aa]);
			Debug.LogFormat("[Gray Hexabuttons #{0}] {1} button's function: {2}", moduleId, positions[aa], funct[aa]);
			Debug.LogFormat("[Gray Hexabuttons #{0}] {1} button's result: {2}", moduleId, positions[aa], results[aa]);
			hexButtons[aa].OnInteract = delegate { pressedGray(aa); return false; };
		}
		hexButtons[6].OnInteract = delegate { pressedGrayCenter(); return false; };
		hexButtons[6].OnInteractEnded = delegate { pressedGrayCenterRelease(); };
		//Get a list of duplicate values
		ArrayList repeats = new ArrayList();
		ArrayList prev = new ArrayList();
		for (int aa = 0; aa < 6; aa++)
		{
			if (!(prev.Contains(results[aa])))
			{
				string temp = getRepeats(results, aa);
				if (temp.Length > 1)
					repeats.Add(temp);
				prev.Add(results[aa]);
			}
		}
		//Take each one randomly and assign it to its function letter
		string functCur = "";
		int valueCur;
		for (int aa = 0; aa < repeats.Count; aa++)
		{
			string repeat = (string)repeats[aa];
			//Debug.LogFormat(repeat);
			while (repeat.Length > 1)
			{
				functCur = functCur + "" + repeat[UnityEngine.Random.Range(0, repeat.Length)];
				repeat = repeat.Replace(functCur[functCur.Length - 1] + "", "");
			}
		}
		//Finally choose one of those letters and replace it with a number instead.
		if (functCur.Length == 0)
		{
			valueCur = UnityEngine.Random.Range(0, 6);
			string functTemp = "012345".Replace((valueCur + ""), "");
			functCur = functTemp[UnityEngine.Random.Range(0, functTemp.Length)] + "";
		}
		else if (functCur.Length == 1)
		{
			if (UnityEngine.Random.Range(0, 2) == 0)
			{
				valueCur = (functCur[0] - '0');
				functCur = "012345".Replace(functCur, "");
				functCur = functCur[UnityEngine.Random.Range(0, functCur.Length)] + "";
			}
			else
			{
				string possCur = "012345".Replace(functCur, "");
				valueCur = (possCur[UnityEngine.Random.Range(0, possCur.Length)] - '0');
			}
		}
		else
		{
			valueCur = (functCur[UnityEngine.Random.Range(0, functCur.Length)] - '0');
			functCur = functCur.Replace((valueCur + ""), "");
		}
		buttonText[valueCur].text = values[valueCur] + "";
		foreach (char f in functCur)
			buttonText[f - '0'].text = funct[f - '0'] + "";
		flag = false;
		centerLight.color = Color.white;
		centerLight.enabled = false;
	}
	int[] getResults(int[] x, int con, string funct)
	{
		int[] r = new int[6];
		for (int aa = 0; aa < x.Length; aa++)
		{
			switch (funct[aa])
			{
				case 'A':
					r[aa] = (x[aa] + con);
					break;
				case 'B':
					r[aa] = ((2 * x[aa]) + con);
					break;
				case 'C':
					r[aa] = (x[aa] + (2 * con));
					break;
				case 'D':
					r[aa] = (x[aa] - con);
					break;
				case 'E':
					r[aa] = ((2 * x[aa]) - con);
					break;
				case 'F':
					r[aa] = (x[aa] - (2 * con));
					break;
			}
		}
		return r;
	}
	string getRepeats(int[] r, int cur)
	{
		string repeats = cur + "";
		for (int aa = cur + 1; aa < 6; aa++)
		{
			if (r[cur] == r[aa])
				repeats = repeats + "" + aa;
		}
		return repeats;
	}
	void Start()
	{
		float scalar = transform.lossyScale.x;
		centerLight.range *= scalar;
	}
	void pressedGray(int n)
	{
		foreach (int aa in buttonIndex)
		{
			Vector3 pos = buttonMesh[aa].transform.localPosition;
			pos.y = 0.0169f;
			buttonMesh[aa].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
			ledMesh[aa].material = ledColors[0];
			hexButtons[aa].OnInteract = delegate { pressedGray(buttonIndex[aa]); return false; };
		}
		Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
		Vector3 pos2 = buttonMesh[n].transform.localPosition;
		pos2.y = 0.0126f;
		buttonMesh[n].transform.localPosition = new Vector3(pos2.x, pos2.y, pos2.z);
		hexButtons[n].OnInteract = null;
		ledMesh[n].material = ledColors[1];
		buttonText[6].text = (results[n] + "");
	}
	void pressedGraySubmit(int n)
	{
		Debug.LogFormat("[Gray Hexabuttons #{0}] User submitted {1} which has the value of {2}.", moduleId, positions[n], values[n]);
		Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
		if (values[n] == (numButtonPresses + 1))
		{
			Vector3 pos = buttonMesh[n].transform.localPosition;
			pos.y = 0.0126f;
			buttonMesh[n].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
			numButtonPresses++;
			hexButtons[n].OnInteract = null;
			ledMesh[n].material = ledColors[1];
			if (numButtonPresses == 6)
			{
				flag = false;
				hexButtons[6].OnInteract = null;
				hexButtons[6].OnInteractEnded = null;
				module.HandlePass();
			}
		}
		else
		{
			Debug.LogFormat("[Gray Hexabuttons #{0}] Strike! I was expecting a value of {1}!", moduleId, (numButtonPresses + 1));
			module.HandleStrike();
			foreach (int i in buttonIndex)
			{
				Vector3 pos = buttonMesh[i].transform.localPosition;
				pos.y = 0.0169f;
				buttonMesh[i].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
				hexButtons[i].OnInteract = delegate { pressedGray(i); return false; };
				ledMesh[i].material = ledColors[0];
			}
			flag = false;
		}
	}
	void pressedGrayCenter()
	{
		Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
		Vector3 pos = buttonMesh[6].transform.localPosition;
		pos.y = 0.0126f;
		buttonMesh[6].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
		buttonText[6].text = "";
		if (flag)
		{
			flag = false;
			foreach (int i in buttonIndex)
			{
				Vector3 pos2 = buttonMesh[i].transform.localPosition;
				pos2.y = 0.0169f;
				buttonMesh[i].transform.localPosition = new Vector3(pos2.x, pos2.y, pos2.z);
				hexButtons[i].OnInteract = delegate { pressedGray(i); return false; };
				ledMesh[i].material = ledColors[0];
			}
		}
		else
		{
			flag = true;
			numButtonPresses = 0;
			foreach (int i in buttonIndex)
			{
				Vector3 pos2 = buttonMesh[i].transform.localPosition;
				pos2.y = 0.0169f;
				buttonMesh[i].transform.localPosition = new Vector3(pos2.x, pos2.y, pos2.z);
				ledMesh[i].material = ledColors[0];
				hexButtons[i].OnInteract = delegate { pressedGraySubmit(i); return false; };
				hexButtons[i].OnInteractEnded = null;
			}
			StartCoroutine(grayFlasher());
		}
	}
	void pressedGrayCenterRelease()
	{
		Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonRelease, transform);
		Vector3 pos = buttonMesh[6].transform.localPosition;
		pos.y = 0.0169f;
		buttonMesh[6].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
	}
	IEnumerator grayFlasher()
	{
		yield return new WaitForSeconds(1.0f);
		while (flag)
		{
			centerLight.enabled = true;
			yield return new WaitForSeconds(1.0f);
			centerLight.enabled = false;
			yield return new WaitForSeconds(1.0f);
		}
		centerLight.enabled = false;
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
}
