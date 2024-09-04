using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class brownHexabuttons : MonoBehaviour
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

	private string[] voiceMessage;
	private string[] solution;
	private int numButtonPresses;
	private bool moduleSolved;
	private int[] present;
	private int[] absent;
	private readonly string[] positions = { "TL", "TR", "ML", "MR", "BL", "BR" };
	private readonly int[] buttonIndex = { 0, 1, 2, 3, 4, 5 };
	private readonly string[] chemicals = { "B+B+B+", "B-B-B-", "B+S+S-", "B-S-S+", "S-B+S+", "S+B-S-", "S+S-B+", "S-S+B-" };
	private int flip = -1;
	private readonly string[][] potionTable =
	{
		new string[]{"R-", "R+", "G+", "B-", "G-", "N"},
		new string[]{"G-", "B+", "R-", "N", "R+", "B-"},
		new string[]{"G+", "R-", "N", "R+", "B-", "B+"},
		new string[]{"B+", "N", "G-", "G+", "R-", "R+"},
		new string[]{"R+", "G+", "B-", "B+", "N", "G-"},
		new string[]{"N", "B-", "B+", "G-", "G+", "R-"},
		new string[]{"B-", "G-", "R+", "R-", "B+", "G+"}
	}; 
	private bool deafMode = false;
	void Awake()
	{
		moduleSolved = false;
		moduleId = moduleIdCounter++;
		Debug.LogFormat("[Brown Hexabuttons #{0}] Color Generated: Brown", moduleId);
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
		foreach (int i in buttonIndex)
		{
			present[i] = (choices[Random.Range(0, choices.Length)] - '0');
			hexButtons[i].OnInteract = delegate { pressedButton(i, present[i]); return false; };
			buttonText[i].text = alpha[present[i] + (8 * Random.Range(0, 4))] + "";
			choices = choices.Replace(present[i] + "", "");
			Debug.LogFormat("[Brown Hexabuttons #{0}] {1} button's text: {2}", moduleId, positions[i], buttonText[i].text);
			Debug.LogFormat("[Brown Hexabuttons #{0}] {1} button's chemical: {2}", moduleId, positions[i], chemicalNames[present[i]]);
		}
		absent = new int[2];
		for (int aa = 0; aa < 2; aa++)
		{
			absent[aa] = (choices[aa] - '0');
			Debug.LogFormat("[Brown Hexabuttons #{0}] Absent chemical #{1}: {2}", moduleId, (aa + 1), chemicalNames[absent[aa]]);
		}
		choices = "012345";
		int offset = Random.Range(0, 2);
		while (offset == 0)
			offset = Random.Range(0, 2);
		solution = new string[6];
		voiceMessage = new string[6];
		alpha = "ALCHEMY";
		for (int aa = 0; aa < 6; aa++)
		{
			int c1 = (choices[Random.Range(0, choices.Length)] - '0');
			int c2 = (aa + offset) % 2;
			solution[aa] = getResult(chemicals[present[c1]], chemicals[absent[c2]]);
			choices = choices.Replace(c1 + "", "");
			for (int bb = 0; bb < 7; bb++)
			{
				if (potionTable[bb][aa].Equals(solution[aa]))
				{
					voiceMessage[aa] = alpha[bb] + "";
					break;
				}
			}
			Debug.LogFormat("[Brown Hexabuttons #{0}] Potion #{1}: {2}", moduleId, (aa + 1), solution[aa]);
		}
		Debug.LogFormat("[Brown Hexabuttons #{0}] Generated Letters: {1}{2}{3}{4}{5}{6}", moduleId, voiceMessage[0], voiceMessage[1], voiceMessage[2], voiceMessage[3], voiceMessage[4], voiceMessage[5]);
		hexButtons[6].OnInteract = delegate { pressedCenter(); return false; };
		hexButtons[7].OnInteract = delegate { deafMode = !(deafMode); return false; };
		numButtonPresses = 0;
	}
	void pressedButton(int n, int c)
	{
		Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
		string results;
		if (flip == -1)
		{
			string[] r = { getResult(chemicals[c], chemicals[absent[0]]), getResult(chemicals[c], chemicals[absent[1]]) };
			if (r[0].Equals(solution[numButtonPresses]) && r[1].Equals(solution[numButtonPresses]))
			{
				flip = -1;
				results = getResult(chemicals[c], chemicals[absent[0]]);
			}
			else if (r[0].Equals(solution[numButtonPresses]))
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
		Debug.LogFormat("[Brown Hexabuttons #{0}] User pressed {1}! This creates a {2}", moduleId, positions[n], results);
		if (results.Equals(solution[numButtonPresses]))
		{
			Vector3 pos = buttonMesh[n].transform.localPosition;
			pos.y = 0.0126f;
			buttonMesh[n].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
			hexButtons[n].OnInteract = null;
			ledMesh[n].material = ledColors[1];
			numButtonPresses++;
			if(flip != -1)
				flip = (flip + 1) % 2;
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
			Debug.LogFormat("[Brown Hexabuttons #{0}] Strike! I was expecting {1}!", moduleId, solution[numButtonPresses]);
			module.HandleStrike();
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
				hexButtons[i].OnInteract = delegate { pressedButton(i, present[i]); return false; };
		}
	}
	string getResult(string c1, string c2)
	{
		for (int aa = 0; aa < 3; aa++)
		{
			if (c1[aa * 2] != c2[aa * 2] && c1[(aa * 2) + 1] == c2[(aa * 2) + 1])
				return ("RGB"[aa] + "" + c1[(aa * 2) + 1]);
		}
		return "N";
	}
	void pressedCenter()
	{
		Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
		Vector3 pos = buttonMesh[6].transform.localPosition;
		pos.y = 0.0126f;
		buttonMesh[6].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
		StartCoroutine(playAudio());
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
			hexButtons[i].OnInteract = delegate { pressedButton(i, present[i]); return false; };
	}
	IEnumerator playAudio()
	{
		hexButtons[6].OnInteract = null;
		yield return new WaitForSeconds(0.5f);
		for (int aa = 0; aa < voiceMessage.Length; aa++)
		{
			Audio.PlaySoundAtTransform(voiceMessage[aa], transform);
			if (deafMode)
				buttonText[6].text = voiceMessage[aa];
			yield return new WaitForSeconds(1.2f);
			buttonText[6].text = "";
			yield return new WaitForSeconds(0.3f);
		}
		Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonRelease, transform);
		Vector3 pos = buttonMesh[6].transform.localPosition;
		pos.y = 0.0169f;
		buttonMesh[6].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
		if(!(moduleSolved))
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
		}
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
		redo:
		var q = new Queue<List<int>>();
		var allMoves = new List<StateChange>();
		var startPoint = new List<int>();
		List<int> target;
		for (int i = 0; i < 6; i++)
			if (hexButtons[i].OnInteract == null)
				startPoint.Add(i);
		q.Enqueue(startPoint);
		while (q.Count > 0)
		{
			var next = q.Dequeue();
			if (next.Count == 6)
            {
				target = next.ToList();
				goto readyToSubmit;
			}
			for (int i = 0; i < 6; i++)
            {
				int f = flip;
				if (next.Count != startPoint.Count)
					f = allMoves.Where(x => x.end.SequenceEqual(next)).First().f;
				if (!next.Contains(i))
                {
					string results;
					if (f == -1)
					{
						string[] r = { getResult(chemicals[present[i]], chemicals[absent[0]]), getResult(chemicals[present[i]], chemicals[absent[1]]) };
						if (r[0].Equals(solution[numButtonPresses]) && r[1].Equals(solution[numButtonPresses]))
						{
							f = -1;
							results = getResult(chemicals[present[i]], chemicals[absent[0]]);
						}
						else if (r[0].Equals(solution[numButtonPresses]))
						{
							f = 0;
							results = getResult(chemicals[present[i]], chemicals[absent[0]]);
						}
						else
						{
							f = 1;
							results = getResult(chemicals[present[i]], chemicals[absent[1]]);
						}
					}
					else
						results = getResult(chemicals[present[i]], chemicals[absent[f]]);
					if (results.Equals(solution[next.Count]))
					{
						List<int> newNext = next.ToList();
						newNext.Add(i);
						if (f != -1)
							f = (f + 1) % 2;
						q.Enqueue(newNext);
						allMoves.Add(new StateChange { start = next, end = newNext, btn = i, f = f });
					}
				}
            }
		}
		while (hexButtons[6].OnInteract == null) yield return true;
		hexButtons[6].OnInteract();
		yield return new WaitForSeconds(.2f);
		goto redo;
		readyToSubmit:
		KMSelectable[] hexBtns = new KMSelectable[] { hexButtons[0], hexButtons[1], hexButtons[2], hexButtons[3], hexButtons[4], hexButtons[5] };
		var lastState = allMoves.First(x => x.end.SequenceEqual(target));
		var relevantMoves = new List<StateChange> { lastState };
		while (!lastState.start.SequenceEqual(startPoint))
		{
			lastState = allMoves.First(x => x.end.SequenceEqual(lastState.start));
			relevantMoves.Add(lastState);
		}
		for (int i = 0; i < relevantMoves.Count; i++)
		{
			hexBtns[relevantMoves[relevantMoves.Count - 1 - i].btn].OnInteract();
			yield return new WaitForSeconds(.2f);
		}
	}
	class StateChange
	{
		public List<int> start;
		public List<int> end;
		public int btn;
		public int f;
	}
}
