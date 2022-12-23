using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;

public class redHexabuttons : MonoBehaviour
{
	public KMBombModule module;
	public KMAudio Audio;
	private static int moduleIdCounter = 1;
	private int moduleId;
	public KMSelectable[] hexButtons;
	public MeshRenderer[] buttonMesh;
	public Material[] ledColors;
	public MeshRenderer[] ledMesh;
	public TextMesh centerText;
	private bool moduleSolved;
	private string[] voiceMessage;
	private int[] solution;
	private int numButtonPresses;
	private string[] positions = { "TL", "TR", "ML", "MR", "BL", "BR" };
	private int[] buttonIndex = { 0, 1, 2, 3, 4, 5 };
	private bool deafMode = false;
	void Awake()
	{
		moduleSolved = false;
		moduleId = moduleIdCounter++;
		foreach (int i in buttonIndex)
			hexButtons[i].OnInteract = delegate { pressedButton(buttonIndex[i]); return false; };
		hexButtons[6].OnInteract = delegate { pressedCenter(); return false; };
		hexButtons[7].OnInteract = delegate { deafMode = !(deafMode); return false; };
		int[][] chart =
		{
			new int[6]{2, 4, 5, 1, 0, 3},
			new int[6]{1, 5, 3, 0, 2, 4},
			new int[6]{5, 4, 3, 1, 2, 0},
			new int[6]{1, 0, 2, 3, 4, 5},
			new int[6]{3, 0, 1, 2, 5, 4},
			new int[6]{5, 0, 1, 2, 4, 3},
			new int[6]{4, 2, 3, 5, 0, 1},
			new int[6]{0, 4, 5, 1, 3, 2},
			new int[6]{0, 5, 3, 2, 1, 4},
			new int[6]{4, 3, 5, 1, 2, 0},
			new int[6]{2, 0, 3, 4, 5, 1},
			new int[6]{2, 5, 4, 3, 1, 0},
			new int[6]{3, 1, 0, 4, 2, 5},
			new int[6]{1, 4, 5, 2, 3, 0},
			new int[6]{3, 2, 4, 5, 0, 1},
			new int[6]{5, 2, 4, 3, 0, 1},
			new int[6]{5, 3, 2, 0, 1, 4},
			new int[6]{5, 1, 0, 4, 3, 2},
			new int[6]{4, 0, 1, 2, 3, 5},
			new int[6]{1, 3, 4, 5, 0, 2},
			new int[6]{3, 5, 2, 1, 4, 0},
			new int[6]{0, 1, 2, 3, 4, 5},
			new int[6]{0, 2, 1, 4, 5, 3},
			new int[6]{2, 1, 0, 5, 3, 4},
			new int[6]{4, 1, 0, 3, 5, 2},
			new int[6]{0, 3, 4, 5, 2, 1}
		};
		int pos1 = Random.Range(0, chart.Length);
		int pos2 = Random.Range(0, 6);
		voiceMessage = new string[2] { "ABCDEFGHIJKLMNOPQRSTUVWXYZ"[pos1] + "", (pos2 + 1) + "" };
		Debug.LogFormat("[Red Hexabuttons #{0}] Generated Message: {1}{2}", moduleId, voiceMessage[0], voiceMessage[1]);
		solution = new int[6];
		string output = "";
		for (int aa = 0; aa < 6; aa++)
		{
			solution[aa] = chart[pos1][(pos2 + aa) % 6];
			output = output + "" + positions[solution[aa]] + " ";
		}
		Debug.LogFormat("[Red Hexabuttons #{0}] Solution: {1}", moduleId, output);
		numButtonPresses = 0;
	}
	void pressedButton(int n)
	{
		Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
		Debug.LogFormat("[Red Hexabuttons #{0}] User pressed {1}", moduleId, positions[n]);
		if (solution[numButtonPresses] == n)
		{
			Vector3 pos = buttonMesh[n].transform.localPosition;
			pos.y = 0.0126f;
			buttonMesh[n].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
			hexButtons[n].OnInteract = null;
			ledMesh[n].material = ledColors[1];
			if (numButtonPresses == 5)
			{
				moduleSolved = true;
				hexButtons[6].OnInteract = null;
				hexButtons[7].OnInteract = null;
				module.HandlePass();
			}
			else
				numButtonPresses++;
		}
		else
		{
			numButtonPresses = 0;
			Debug.LogFormat("[Red Hexabuttons #{0}] Strike! I was expecting {1}!", moduleId, positions[solution[numButtonPresses]]);
			module.HandleStrike();
			for (int aa = 0; aa < 6; aa++)
			{
				Vector3 pos = buttonMesh[aa].transform.localPosition;
				pos.y = 0.0169f;
				buttonMesh[aa].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
				ledMesh[aa].material = ledColors[0];
			}
			foreach (int i in buttonIndex)
				hexButtons[i].OnInteract = delegate { pressedButton(buttonIndex[i]); return false; };
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
			if(deafMode)
				centerText.text = voiceMessage[aa] + "";
			yield return new WaitForSeconds(1.5f);
		}
		centerText.text = "";
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
					if(hexButtons[cursor].OnInteract != null)
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
