using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class blackHexabuttons : MonoBehaviour
{
	public KMBombModule module;
	public KMAudio Audio;
	public AudioClip[] morseSounds;
	private int moduleId;
	private static int moduleIdCounter = 1;
	public KMSelectable[] hexButtons;
	public MeshRenderer[] buttonMesh;
	public Material[] ledColors;
	public MeshRenderer[] ledMesh;
	public Light centerLight;
	public Material[] buttonColors;

	private string[] blackMorse;
	private int[] blackLights;
	private ArrayList blackButtonSeq;
	private bool flag;
	private int[] solution;
	private int numButtonPresses;
	private string[] positions = { "TL", "TR", "ML", "MR", "BL", "BR" };
	private int[] buttonIndex = { 0, 1, 2, 3, 4, 5 };
	private bool deafMode = false;
	void Awake()
	{
		moduleId = moduleIdCounter++;
		string alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
		string[] morse =
		{
			".-","-...","-.-.","-..",".","..-.","--.","....","..",".---","-.-",".-..","--",
			"-.","---",".--.","--.-",".-.","...","-","..-","...-",".--","-..-","-.--","--..",
			"-----",".----","..---","...--","....-",".....","-....","--...","---..","----."
		};
		string[] letterTable =
		{
			"LHTGWYRAQPJZODKXVFESCBMIUN9816347502",
			"UPQCOTBEXAHYIRLKZMFDSJGNVW5209734816",
			"TWIZYBASJONUDFVRXQGCHEKMPL8047269135",
			"FXOKZWPLMNCDQHGBISTYRAUJEV2438571690",
			"JOPQFGTDRVMSXLZWACNHBUYKIE1972805364",
			"ZCMWHONFDKBGVQXLTJRIEYAUSP4685923071"
		};
		int[] buttonTable =
		{
			4, 1, 0, 3, 5, 2,
			2, 5, 3, 1, 4, 0,
			3, 0, 1, 5, 2, 4,
			0, 4, 5, 2, 1, 3,
			1, 3, 2, 4, 0, 5,
			5, 2, 4, 0, 3, 1
		};
		string letters = "";
		blackMorse = new string[6];
		foreach (int i in buttonIndex)
		{
			letters = letters + "" + alpha[UnityEngine.Random.Range(0, alpha.Length)];
			Debug.LogFormat("[Black Hexabuttons #{0}] {1} button is transmitting {2}", moduleId, positions[i], letters[i]);
			blackMorse[i] = morse[alpha.IndexOf(letters[i])];
			hexButtons[i].OnInteract = delegate { StartCoroutine(pressedButton(i)); return false; };
		}
		blackLights = new int[7];
		foreach (int i in buttonIndex)
		{
			blackLights[i] = UnityEngine.Random.Range(0, 6);
			hexButtons[i].OnHighlight = delegate { ledMesh[blackLights[i]].material = ledColors[2]; };
			hexButtons[i].OnHighlightEnded = delegate { ledMesh[blackLights[i]].material = ledColors[0]; };
		}
		blackLights[6] = UnityEngine.Random.Range(0, 6);
		hexButtons[6].OnHighlight = delegate { ledMesh[blackLights[6]].material = ledColors[2]; };
		hexButtons[6].OnHighlightEnded = delegate { ledMesh[blackLights[6]].material = ledColors[0]; };
		hexButtons[7].OnInteract = delegate { deafMode = !(deafMode); return false; };
		blackButtonSeq = new ArrayList();
		blackButtonSeq.Add(blackLights[6]);
		string temp = letters[(int)blackButtonSeq[0]] + "";
		while (blackButtonSeq.Count < 6)
		{
			int row = ((int)blackButtonSeq[blackButtonSeq.Count - 1]) * 6;
			int col = blackLights[((int)blackButtonSeq[blackButtonSeq.Count - 1])];
			int index = row + col;
			while (blackButtonSeq.Contains(buttonTable[index]))
				index = (index + 1) % buttonTable.Length;
			blackButtonSeq.Add(buttonTable[index]);
			temp = temp + "" + letters[(int)blackButtonSeq[blackButtonSeq.Count - 1]];
		}
		letters = temp.ToUpperInvariant();
		Debug.LogFormat("[Black Hexabuttons #{0}] Order for the buttons to be read: {1}, {2}, {3}, {4}, {5}, {6}", moduleId, positions[(int)blackButtonSeq[0]], positions[(int)blackButtonSeq[1]], positions[(int)blackButtonSeq[2]], positions[(int)blackButtonSeq[3]], positions[(int)blackButtonSeq[4]], positions[(int)blackButtonSeq[5]]);
		Debug.LogFormat("[Black Hexabuttons #{0}] Character Sequence: {1}", moduleId, letters);
		temp = "";
		solution = new int[12];
		for (int aa = 0; aa < 6; aa++)
		{
			temp = temp + "" + letterTable[(int)blackButtonSeq[aa]][alpha.IndexOf(letters[aa])];
			solution[aa * 2] = (alpha.IndexOf(temp[aa]) / 6) + 1;
			solution[(aa * 2) + 1] = (alpha.IndexOf(temp[aa]) % 6) + 1;
			Debug.LogFormat("[Black Hexabuttons #{0}] {1} -> {2} -> {3}{4}", moduleId, letters[aa], temp[aa], solution[aa * 2], solution[(aa * 2) + 1]);
		}
		numButtonPresses = 0;
		hexButtons[6].OnInteract = delegate { pressedCenter(); return false; };
		hexButtons[6].OnInteractEnded = delegate { releasedCenter(); };
		centerLight.color = Color.white;
		centerLight.intensity = 30;
		centerLight.range = 0.020f;
		centerLight.enabled = false;
		flag = false;
	}
	void Start()
	{
		float scalar = transform.lossyScale.x;
		centerLight.range *= scalar;
	}
	IEnumerator pressedButton(int p)
	{
		yield return new WaitForSeconds(0.0f);
		Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
		Vector3 pos = buttonMesh[p].transform.localPosition;
		pos.y = 0.0126f;
		buttonMesh[p].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
		hexButtons[p].OnInteract = null;
		yield return new WaitForSeconds(0.5f);
		if (deafMode)
		{
			foreach (char c in blackMorse[p])
			{
				if (c == '.')
				{
					buttonMesh[p].material = buttonColors[1];
					yield return new WaitForSeconds(0.2f);
					buttonMesh[p].material = buttonColors[0];
					yield return new WaitForSeconds(0.2f);
				}
				else
				{
					buttonMesh[p].material = buttonColors[1];
					yield return new WaitForSeconds(0.6f);
					buttonMesh[p].material = buttonColors[0];
					yield return new WaitForSeconds(0.2f);
				}
			}
		}
		else
		{
			foreach (char c in blackMorse[p])
			{
				if (c == '.')
				{
					Audio.PlaySoundAtTransform(morseSounds[0].name, transform);
					yield return new WaitForSeconds(0.2f);
				}
				else
				{
					Audio.PlaySoundAtTransform(morseSounds[1].name, transform);
					yield return new WaitForSeconds(0.4f);
				}
			}
		}
		yield return new WaitForSeconds(0.5f);
		Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonRelease, transform);
		pos = buttonMesh[p].transform.localPosition;
		pos.y = 0.0169f;
		buttonMesh[p].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
		if (hexButtons[p].OnInteract == null)
			hexButtons[p].OnInteract = delegate { StartCoroutine(pressedButton(p)); return false; };
	}
	void submitButton(int p)
	{
		Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
		Vector3 pos = buttonMesh[p].transform.localPosition;
		pos.y = 0.0126f;
		buttonMesh[p].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
	}
	void releasedButton(int p)
	{
		Debug.LogFormat("[Black Hexabuttons #{0}] User pressed {1}", moduleId, positions[p]);
		Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonRelease, transform);
		Vector3 pos = buttonMesh[p].transform.localPosition;
		pos.y = 0.0169f;
		buttonMesh[p].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
		if (solution[numButtonPresses] == (p + 1))
		{
			if (numButtonPresses % 2 == 0)
				ledMesh[(int)blackButtonSeq[numButtonPresses / 2]].material = ledColors[3];
			else
				ledMesh[(int)blackButtonSeq[numButtonPresses / 2]].material = ledColors[1];
			numButtonPresses++;
			if (numButtonPresses == 12)
			{
				flag = false;
				foreach (int i in buttonIndex)
				{
					hexButtons[i].OnInteract = null;
					hexButtons[i].OnInteractEnded = null;
				}
				hexButtons[6].OnInteract = null;
				hexButtons[6].OnInteractEnded = null;
				hexButtons[7].OnInteract = null;
				module.HandlePass();
			}
		}
		else
		{
			Debug.LogFormat("[Black Hexabuttons #{0}] Strike! I was expecting {1}", moduleId, positions[solution[numButtonPresses] - 1]);
			module.HandleStrike();
			hexButtons[6].OnHighlight = delegate { ledMesh[blackLights[6]].material = ledColors[2]; };
			hexButtons[6].OnHighlightEnded = delegate { ledMesh[blackLights[6]].material = ledColors[0]; };
			foreach (int i in buttonIndex)
			{
				hexButtons[i].OnInteract = delegate { StartCoroutine(pressedButton(i)); return false; };
				hexButtons[i].OnInteractEnded = null;
				hexButtons[i].OnHighlight = delegate { ledMesh[blackLights[i]].material = ledColors[2]; };
				hexButtons[i].OnHighlightEnded = delegate { ledMesh[blackLights[i]].material = ledColors[0]; };
				ledMesh[i].material = ledColors[0];
			}
			numButtonPresses = 0;
			flag = false;
		}
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
			hexButtons[6].OnHighlight = delegate { ledMesh[blackLights[6]].material = ledColors[2]; };
			hexButtons[6].OnHighlightEnded = delegate { ledMesh[blackLights[6]].material = ledColors[0]; };
			foreach (int i in buttonIndex)
			{
				hexButtons[i].OnInteract = delegate { StartCoroutine(pressedButton(i)); return false; };
				hexButtons[i].OnInteractEnded = null;
				hexButtons[i].OnHighlight = delegate { ledMesh[blackLights[i]].material = ledColors[2]; };
				hexButtons[i].OnHighlightEnded = delegate { ledMesh[blackLights[i]].material = ledColors[0]; };
				ledMesh[i].material = ledColors[0];
			}
			numButtonPresses = 0;
		}
		else
		{
			hexButtons[6].OnHighlight = null;
			hexButtons[6].OnHighlightEnded = null;
			StartCoroutine(blackFlasher());
			foreach (int i in buttonIndex)
			{
				hexButtons[i].OnInteract = delegate { submitButton(i); return false; };
				hexButtons[i].OnInteractEnded = delegate { releasedButton(i); };
				hexButtons[i].OnHighlight = null;
				hexButtons[i].OnHighlightEnded = null;
				ledMesh[i].material = ledColors[0];
			}
		}
		flag = !(flag);
	}
	IEnumerator blackFlasher()
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
	private readonly string TwitchHelpMessage = @"!{0} press|p tl/1 tr/2 ml/3 mr/4 bl/5 br/6 c/7 sl presses the top-left, top-right, middle-left, middle-right, bottom-left, bottom-right, center, and the status light in that order. !{0} hover|h tl/1 tr/2 ml/3 mr/4 bl/5 br/6 c/7 will hover the buttons in the same fashion.";
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
		else if ((Regex.IsMatch(param[0], @"^\s*HOVER\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant) || Regex.IsMatch(param[0], @"^\s*H\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant)) && param.Length > 1)
		{
			if (isPos2(param))
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
