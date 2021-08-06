using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownHexabuttons {

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
	private string[] solution;
	private int numButtonPresses;
	private bool moduleSolved;
	private int[] present;
	private int[] absent;
	private string[] positions = { "TL", "TR", "ML", "MR", "BL", "BR" };
	private int[] buttonIndex = { 0, 1, 2, 3, 4, 5 };
	private string[] chemicals = {"B+B+B+", "B-B-B-", "B+S+S-", "B-S-S+", "S-B+S+", "S+B-S-", "S+S-B+", "S-S+B-"};
	private int flip = -1;
	private string[][] potionTable =
	{
		new string[]{"R-", "R+", "G+", "B-", "G-", "N"},
		new string[]{"G-", "B+", "R-", "N", "R+", "B-"},
		new string[]{"G+", "R-", "N", "R+", "B-", "B+"},
		new string[]{"B+", "N", "G-", "G+", "R-", "R+"},
		new string[]{"R+", "G+", "B-", "B+", "N", "G-"},
		new string[]{"N", "B-", "B+", "G-", "G+", "R-"},
		new string[]{"B-", "G-", "R+", "R-", "B+", "G+"}
	};
	public BrownHexabuttons(ColorfulButtonSeries m, KMAudio aud, int MI, KMSelectable[] HB, MeshRenderer[] BM, TextMesh[] BT, Material[] LC, MeshRenderer[] LM, Transform T)
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
		Debug.LogFormat("[Colored Hexabuttons #{0}] Color Generated: Brown", moduleId);
		string alpha = "0123456789ABCDEFGHIJKLMNOPQRSTUV";
		string choices = "01234567";
		string[] chemicalNames = {
		"Big Red Positive, Big Green Positive, Big Blue Positive",
		"Big Red Negative, Big Green Negative, Big Blue Negative",
		"Big Red Positive, Small Green Positive, Small Blue Negative",
		"Big Red Negative, Small Green Negative, Small Blue Positive",
		"Small Red Negative, Big Green Positive, Small Blue Positive",
		"Small Red Positive, Big Green Negative, Small Blue Negative",
		"Small Red Positive, Small Green Negative, Big Blue Positive",
		"Small Red Negative, Small Green Positive, Big Blue Negative"
		};
		present = new int[6];
		foreach(int i in buttonIndex)
		{
			present[i] = (choices[UnityEngine.Random.Range(0, choices.Length)] - '0');
			hexButtons[i].OnInteract = delegate { pressedBrown(i, present[i]); return false; };
			buttonText[i].text = alpha[present[i] + (8 * UnityEngine.Random.Range(0, 4))] + "";
			choices = choices.Replace(present[i] + "", "");
			Debug.LogFormat("[Colored Hexabuttons #{0}] {1} button's text: {2}", moduleId, positions[i], buttonText[i].text);
			Debug.LogFormat("[Colored Hexabuttons #{0}] {1} button's chemical: {2}", moduleId, positions[i], chemicalNames[present[i]]);
		}
		absent = new int[2];
		for(int aa = 0; aa < 2; aa++)
		{
			absent[aa] = (choices[aa] - '0');
			Debug.LogFormat("[Colored Hexabuttons #{0}] Absent chemical #{1}: {2}", moduleId, (aa + 1), chemicalNames[absent[aa]]);
		}
		choices = "012345";
		int offset = UnityEngine.Random.Range(0, 2);
		while(offset == 0)
			offset = UnityEngine.Random.Range(0, 2);
		solution = new string[6];
		voiceMessage = new string[6];
		alpha = "ALCHEMY";
		for(int aa = 0; aa < 6; aa++)
		{
			int c1 = (choices[UnityEngine.Random.Range(0, choices.Length)] - '0');
			int c2 = (aa + offset) % 2;
			solution[aa] = getResult(chemicals[present[c1]], chemicals[absent[c2]]);
			choices = choices.Replace(c1 + "", "");
			for(int bb = 0; bb < 7; bb++)
			{
				if(potionTable[bb][aa].Equals(solution[aa]))
				{
					voiceMessage[aa] = alpha[bb] + "";
					break;
				}
			}
			Debug.LogFormat("[Colored Hexabuttons #{0}] Potion #{1}: {2}", moduleId, (aa + 1), solution[aa]);
		}
		Debug.LogFormat("[Colored Hexabuttons #{0}] Generated Letters: {1}{2}{3}{4}{5}{6}", moduleId, voiceMessage[0], voiceMessage[1], voiceMessage[2], voiceMessage[3], voiceMessage[4], voiceMessage[5]);
		hexButtons[6].OnInteract = delegate { pressedBrownCenter(); return false; };
		numButtonPresses = 0;
	}
	void pressedBrown(int n, int c)
	{
		if(!(moduleSolved))
		{
			Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
			string results;
			if(flip == -1)
			{
				string[] r = { getResult(chemicals[c], chemicals[absent[0]]), getResult(chemicals[c], chemicals[absent[1]]) };
				if(r[0].Equals(solution[numButtonPresses]) && r[1].Equals(solution[numButtonPresses]))
				{
					flip = -1;
					results = getResult(chemicals[c], chemicals[absent[0]]);
				}
				else if(r[0].Equals(solution[numButtonPresses]))
				{
					flip = 0;
					results = getResult(chemicals[c], chemicals[absent[0]]);
				}
				else
				{
					flip = 1;
					results = getResult(chemicals[c], chemicals[absent[1]]);
				}
			}
			else
				results = getResult(chemicals[c], chemicals[absent[flip]]);
			Debug.LogFormat("[Colored Hexabuttons #{0}] User pressed {1}! This creates a {2}", moduleId, positions[n], results);
			if (results.Equals(solution[numButtonPresses]))
			{
				Vector3 pos = buttonMesh[n].transform.localPosition;
				pos.y = 0.0126f;
				buttonMesh[n].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
				hexButtons[n].OnInteract = null;
				ledMesh[n].material = ledColors[1];
				numButtonPresses++;
				flip = (flip + 1) % 2;
				if(numButtonPresses == 6)
				{
					moduleSolved = true;
					coloredHexabuttons.Solve();
				}
			}
			else
			{
				Debug.LogFormat("[Colored Hexabuttons #{0}] Strike! I was expecting {1}!", moduleId, solution[numButtonPresses]);
				coloredHexabuttons.Strike();
				numButtonPresses = 0;
				flip = -1;
				for (int aa = 0; aa < 6; aa++)
				{
					Vector3 pos = buttonMesh[aa].transform.localPosition;
					pos.y = 0.0169f;
					buttonMesh[aa].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
					ledMesh[aa].material = ledColors[0];
				}
				foreach (int i in buttonIndex)
					hexButtons[i].OnInteract = delegate { pressedBrown(i, present[i]); return false; };
			}
		}
	}
	string getResult(string c1, string c2)
	{
		for(int aa = 0; aa < 3; aa++)
		{
			if (c1[aa * 2] != c2[aa * 2] && c1[(aa * 2) + 1] == c2[(aa * 2) + 1])
				return ("RGB"[aa] + "" + c1[(aa * 2) + 1]);
		}
		return "N";
	}
	void pressedBrownCenter()
	{
		if (!(moduleSolved))
		{
			Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
			Vector3 pos = buttonMesh[6].transform.localPosition;
			pos.y = 0.0126f;
			buttonMesh[6].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
			coloredHexabuttons.StartCoroutine(playAudio());
			numButtonPresses = 0;
			flip = -1;
			for (int aa = 0; aa < 6; aa++)
			{
				pos = buttonMesh[aa].transform.localPosition;
				pos.y = 0.0169f;
				buttonMesh[aa].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
				ledMesh[aa].material = ledColors[0];
			}
			foreach (int i in buttonIndex)
				hexButtons[i].OnInteract = delegate { pressedBrown(i, present[i]); return false; };
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
		hexButtons[6].OnInteract = delegate { pressedBrownCenter(); return false; };
	}
}
