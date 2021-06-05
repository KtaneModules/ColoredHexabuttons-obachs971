using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrayHexabuttons {

	private ColorfulButtonSeries coloredHexabuttons;
	private KMAudio Audio;
	private int moduleId;
	private KMSelectable[] hexButtons;
	private MeshRenderer[] buttonMesh;
	private TextMesh[] buttonText;
	private Material[] ledColors;
	private MeshRenderer[] ledMesh;
	private Light[] lights;
	private Transform transform;
	private int numButtonPresses;
	private bool moduleSolved;
	private bool flag;
	private int[] values;
	private int[] results;
	private string[] positions = { "TL", "TR", "ML", "MR", "BL", "BR" };
	private int[] buttonIndex = { 0, 1, 2, 3, 4, 5 };
	public GrayHexabuttons(ColorfulButtonSeries m, KMAudio aud, int MI, KMSelectable[] HB, MeshRenderer[] BM, TextMesh[] BT, Material[] LC, MeshRenderer[] LM, Light[] L, Transform T)
	{
		coloredHexabuttons = m;
		Audio = aud;
		moduleId = MI;
		hexButtons = HB;
		buttonMesh = BM;
		buttonText = BT;
		ledColors = LC;
		ledMesh = LM;
		lights = L;
		transform = T;
	}
	public void run()
	{
		Debug.LogFormat("[Colored Hexabuttons #{0}] Color Generated: Gray", moduleId);
		values = new int[6];
		results = new int[6];
		string[] choices = {"123456", "ABCDEF"};
		string funct = "";
		int con = UnityEngine.Random.Range(0, 6) + 1;
		//int con = 2;
		//int[] valueDebug = {4, 1, 5, 3, 2, 6};
		//string functDebug = "CFDBEA";
		Debug.LogFormat("[Colored Hexabuttons #{0}] Constant: {1}", moduleId, con);
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

			Debug.LogFormat("[Colored Hexabuttons #{0}] {1} button's value: {2}", moduleId, positions[aa], values[aa]);
			Debug.LogFormat("[Colored Hexabuttons #{0}] {1} button's function: {2}", moduleId, positions[aa], funct[aa]);
			Debug.LogFormat("[Colored Hexabuttons #{0}] {1} button's result: {2}", moduleId, positions[aa], results[aa]);
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
		for(int aa = 0; aa < repeats.Count; aa++)
		{
			string repeat = (string)repeats[aa];
			//Debug.LogFormat(repeat);
			while(repeat.Length > 1)
			{
				functCur = functCur + "" + repeat[UnityEngine.Random.Range(0, repeat.Length)];
				repeat = repeat.Replace(functCur[functCur.Length - 1] + "", "");
			}
		}
		//Finally choose one of those letters and replace it with a number instead.
		if(functCur.Length == 0)
		{
			valueCur = UnityEngine.Random.Range(0, 6);
			string functTemp = "012345".Replace((valueCur + ""), "");
			functCur = functTemp[UnityEngine.Random.Range(0, functTemp.Length)] + "";
		}
		else if(functCur.Length == 1)
		{
			if(UnityEngine.Random.Range(0, 2) == 0)
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
		lights[6].color = Color.white;
	}
	int[] getResults(int[] x, int con, string funct)
	{
		int[] r = new int[6];
		for(int aa = 0; aa < x.Length; aa++)
		{
			switch(funct[aa])
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
		for(int aa = cur + 1; aa < 6; aa++)
		{
			if (r[cur] == r[aa])
				repeats = repeats + "" + aa;
		}
		return repeats;
	}
	void pressedGray(int n)
	{
		if(!(moduleSolved))
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
	}
	void pressedGraySubmit(int n)
	{
		if (!(moduleSolved))
		{
			Debug.LogFormat("[Colored Hexabuttons #{0}] User submitted {1} which has the value of {2}.", moduleId, positions[n], values[n]);
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
					moduleSolved = true;
					coloredHexabuttons.Solve();
				}
			}
			else
			{
				Debug.LogFormat("[Colored Hexabuttons #{0}] Strike! I was expecting a value of {1}!", moduleId, (numButtonPresses + 1));
				coloredHexabuttons.Strike();
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
	}
	void pressedGrayCenter()
	{
		if (!(moduleSolved))
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
				coloredHexabuttons.StartCoroutine(grayFlasher());
			}
		}
	}
	void pressedGrayCenterRelease()
	{
		if (!(moduleSolved))
		{
			Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonRelease, transform);
			Vector3 pos = buttonMesh[6].transform.localPosition;
			pos.y = 0.0169f;
			buttonMesh[6].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
		}
	}
	IEnumerator grayFlasher()
	{
		yield return new WaitForSeconds(1.0f);
		while (flag)
		{
			lights[6].enabled = true;
			yield return new WaitForSeconds(1.0f);
			lights[6].enabled = false;
			yield return new WaitForSeconds(1.0f);
		}
		lights[6].enabled = false;
	}
}

