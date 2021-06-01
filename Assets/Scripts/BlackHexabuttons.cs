using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHexabuttons{
	private ColorfulButtonSeries coloredHexabuttons;
	private KMAudio Audio;
	private AudioClip[] morseSounds;
	private int moduleId;
	private KMSelectable[] hexButtons;
	private MeshRenderer[] buttonMesh;
	private Material[] ledColors;
	private MeshRenderer[] ledMesh;
	private Light[] lights;
	private Transform transform;
	
	private string[] blackMorse;
	private int[] blackLights;
	private ArrayList blackButtonSeq;
	private bool flag;
	private int[] solution;
	private int numButtonPresses;
	private bool moduleSolved;
	private string[] positions = { "TL", "TR", "ML", "MR", "BL", "BR" };
	private int[] buttonIndex = { 0, 1, 2, 3, 4, 5 };
	public BlackHexabuttons(ColorfulButtonSeries m, KMAudio aud, AudioClip[] MS, int MI, KMSelectable[] HB, MeshRenderer[] BM, Material[] LC, MeshRenderer[] LM, Light[] L, Transform T)
	{
		coloredHexabuttons = m;
		Audio = aud;
		morseSounds = MS;
		moduleId = MI;
		hexButtons = HB;
		buttonMesh = BM;
		ledColors = LC;
		ledMesh = LM;
		lights = L;
		transform = T;
	}
	public void run()
	{
		Debug.LogFormat("[Colored Hexabuttons #{0}] Color Generated: Black", moduleId);
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
			Debug.LogFormat("[Colored Hexabuttons #{0}] {1} button is transmitting {2}", moduleId, positions[i], letters[i]);
			blackMorse[i] = morse[alpha.IndexOf(letters[i])];
			hexButtons[i].OnInteract = delegate { coloredHexabuttons.StartCoroutine(pressedBlack(i)); return false; };
		}
		blackLights = new int[7];
		foreach (int i in buttonIndex)
		{
			blackLights[i] = UnityEngine.Random.Range(0, 6);
			hexButtons[i].OnHighlight = delegate { ledMesh[blackLights[i]].material = ledColors[2];};
			hexButtons[i].OnHighlightEnded = delegate { ledMesh[blackLights[i]].material = ledColors[0];};
		}
		blackLights[6] = UnityEngine.Random.Range(0, 6);
		hexButtons[6].OnHighlight = delegate { ledMesh[blackLights[6]].material = ledColors[2];};
		hexButtons[6].OnHighlightEnded = delegate { ledMesh[blackLights[6]].material = ledColors[0];};
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
		Debug.LogFormat("[Colored Hexabuttons #{0}] Order for the buttons to be read: {1}, {2}, {3}, {4}, {5}, {6}", moduleId, positions[(int)blackButtonSeq[0]], positions[(int)blackButtonSeq[1]], positions[(int)blackButtonSeq[2]], positions[(int)blackButtonSeq[3]], positions[(int)blackButtonSeq[4]], positions[(int)blackButtonSeq[5]]);
		Debug.LogFormat("[Colored Hexabuttons #{0}] Character Sequence: {1}", moduleId, letters);
		temp = "";
		solution = new int[12];
		for (int aa = 0; aa < 6; aa++)
		{
			temp = temp + "" + letterTable[(int)blackButtonSeq[aa]][alpha.IndexOf(letters[aa])];
			solution[aa * 2] = (alpha.IndexOf(temp[aa]) / 6) + 1;
			solution[(aa * 2) + 1] = (alpha.IndexOf(temp[aa]) % 6) + 1;
			Debug.LogFormat("[Colored Hexabuttons #{0}] {1} -> {2} -> {3}{4}", moduleId, letters[aa], temp[aa], solution[aa * 2], solution[(aa * 2) + 1]);
		}
		numButtonPresses = 0;
		hexButtons[6].OnInteract = delegate { pressedBlackCenter(); return false; };
		hexButtons[6].OnInteractEnded = delegate { releasedBlackCenter(); };
		lights[6].color = Color.white;
		lights[6].intensity = 30;
		lights[6].range = 0.020f;
		flag = false;
	}
	IEnumerator pressedBlack(int p)
	{
		yield return new WaitForSeconds(0.0f);
		if (!(moduleSolved))
		{
			Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
			Vector3 pos = buttonMesh[p].transform.localPosition;
			pos.y = 0.0126f;
			buttonMesh[p].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
			hexButtons[p].OnInteract = null;
			yield return new WaitForSeconds(0.5f);
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
					yield return new WaitForSeconds(0.3f);
				}
			}
			yield return new WaitForSeconds(0.5f);
			Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonRelease, transform);
			pos = buttonMesh[p].transform.localPosition;
			pos.y = 0.0169f;
			buttonMesh[p].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
			hexButtons[p].OnInteract = delegate { coloredHexabuttons.StartCoroutine(pressedBlack(p)); return false; };
		}
	}
	void pressedBlackSubmit(int p)
	{
		if (!(moduleSolved))
		{
			Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
			Vector3 pos = buttonMesh[p].transform.localPosition;
			pos.y = 0.0126f;
			buttonMesh[p].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
		}
	}
	void releasedBlackSubmit(int p)
	{
		if (!(moduleSolved))
		{
			Debug.LogFormat("[Colored Hexabuttons #{0}] User pressed {1}", moduleId, positions[p]);
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
					moduleSolved = true;
					coloredHexabuttons.Solve();
				}
			}
			else
			{
				Debug.LogFormat("[Colored Hexabuttons #{0}] Strike! I was expecting {1}", moduleId, positions[solution[numButtonPresses] - 1]);
				coloredHexabuttons.Strike();
				hexButtons[6].OnHighlight = delegate { ledMesh[blackLights[6]].material = ledColors[2];};
				hexButtons[6].OnHighlightEnded = delegate { ledMesh[blackLights[6]].material = ledColors[0];};
				foreach (int i in buttonIndex)
				{
					hexButtons[i].OnInteract = delegate { coloredHexabuttons.StartCoroutine(pressedBlack(i)); return false; };
					hexButtons[i].OnInteractEnded = null;
					hexButtons[i].OnHighlight = delegate { ledMesh[blackLights[i]].material = ledColors[2]; };
					hexButtons[i].OnHighlightEnded = delegate { ledMesh[blackLights[i]].material = ledColors[0]; };
					ledMesh[i].material = ledColors[0];
				}
				numButtonPresses = 0;
				flag = false;
			}
		}
	}
	void pressedBlackCenter()
	{
		if (!(moduleSolved))
		{
			Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
			Vector3 pos = buttonMesh[6].transform.localPosition;
			pos.y = 0.0126f;
			buttonMesh[6].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
		}
	}
	void releasedBlackCenter()
	{
		if (!(moduleSolved))
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
					hexButtons[i].OnInteract = delegate { coloredHexabuttons.StartCoroutine(pressedBlack(i)); return false; };
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
				coloredHexabuttons.StartCoroutine(blackFlasher());
				foreach (int i in buttonIndex)
				{
					hexButtons[i].OnInteract = delegate { pressedBlackSubmit(i); return false; };
					hexButtons[i].OnInteractEnded = delegate { releasedBlackSubmit(i); };
					hexButtons[i].OnHighlight = null;
					hexButtons[i].OnHighlightEnded = null;
					ledMesh[i].material = ledColors[0];
				}
			}
			flag = !(flag);
		}
	}
	IEnumerator blackFlasher()
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
