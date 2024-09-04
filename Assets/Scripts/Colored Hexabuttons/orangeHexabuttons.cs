using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;

public class orangeHexabuttons : MonoBehaviour
{
	public KMBombModule module;
	public KMAudio Audio;
	private static int moduleIdCounter = 1;
	private int moduleId;
	public KMSelectable[] hexButtons;
	public MeshRenderer[] buttonMesh;
	public Material[] ledColors;
	public MeshRenderer[] ledMesh;
	public TextMesh[] buttonText;
	public TextMesh centerText;

	private bool moduleSolved;
	private string[] voiceMessage;
	private string solution;
	private string scramble;
	private int numButtonPresses;
	private readonly string[] positions = { "TL", "TR", "ML", "MR", "BL", "BR" };
	private readonly int[] buttonIndex = { 0, 1, 2, 3, 4, 5 };
	private bool deafMode = false;
	void Awake()
	{
		moduleSolved = false;
		moduleId = moduleIdCounter++;
		numButtonPresses = 0;
		string[] wordList =
			{
					"ABACUS", "ACTION", "ADVICE", "AFFECT", "AGENCY", "ALMOND", "AMOUNT", "ANARCH", "APPEAR", "ARRIVE",
					"BALLAD", "BAKERY", "BEACON", "BINARY", "BLEACH", "BRONZE", "BOXING", "BREEZE", "BELIEF", "BITTER",
					"CACTUS", "CEREAL", "CHERRY", "CITRUS", "CLOSET", "COFFEE", "CRISIS", "CURSOR", "CONVEX", "CELLAR",
					"DANGER", "DEBRIS", "DINNER", "DOODLE", "DRIVER", "DUSTER", "DEFEAT", "DIRECT", "DOMINO", "DRAWER",
					"EASTER", "EDITOR", "EFFECT", "EGGNOG", "EMBLEM", "ENROLL", "EQUALS", "ERASER", "ESCAPE", "EXPERT",
					"FABRIC", "FELINE", "FILTER", "FLAVOR", "FOREST", "FREEZE", "FUTURE", "FACADE", "FOLLOW", "FINISH",
					"GALLON", "GEYSER", "GALAXY", "GLANCE", "GROWTH", "GUTTER", "GAMBLE", "GERBIL", "GINGER", "GIVING",
					"HAMMER", "HEIGHT", "HIDING", "HOLLOW", "HUNTER", "HYBRID", "HANDLE", "HELMET", "HAZARD", "HURDLE",
					"ICICLE", "IMPORT", "INSERT", "ITALIC", "IMPAIR", "INCOME", "IMPACT", "INSULT", "INSECT", "INTENT",
					"JESTER", "JINGLE", "JOGGER", "JUNGLE", "JERSEY", "JOCKEY", "JUGGLE", "JUMBLE", "JUNIOR", "JAILER",
					"KETTLE", "KIDNEY", "KNIGHT", "KENNEL", "KINGLY", "KITTEN", "KRAKEN", "KINDLY", "KERNEL", "KEEPER",
					"LAGOON", "LEADER", "LIMBER", "LOCKET", "LUXURY", "LYCHEE", "LADDER", "LEGACY", "LIQUID", "LOTION",
					"MAGNET", "MEADOW", "MIDDLE", "MOMENT", "MUSEUM", "MYSTIC", "MATRIX", "MELODY", "MIRROR", "MUFFIN",
					"NAPKIN", "NEEDLE", "NICKEL", "NOBODY", "NUTMEG", "NATION", "NECTAR", "NINETY", "NOTICE", "NARROW",
					"OBJECT", "OCELOT", "OFFICE", "OPTION", "ORANGE", "OUTPUT", "OXYGEN", "OYSTER", "OFFSET", "OUTFIT",
					"PALACE", "PEBBLE", "PICNIC", "PLAQUE", "POCKET", "PROFIT", "PUDDLE", "PENCIL", "PIGEON", "POETRY",
					"QUARTZ", "QUIVER", "QUARRY", "QUEASY", "RABBIT", "REFLEX", "RHYTHM", "RIBBON", "ROCKET", "RAFFLE",
					"RECIPE", "RUBBER", "RADIUS", "RECORD", "SAILOR", "SCHEME", "SEARCH", "SHADOW", "SIGNAL", "SLEIGH",
					"SMUDGE", "SNEEZE", "SOCIAL", "SQUEAK", "TAILOR", "TEACUP", "THIRST", "TICKET", "TOGGLE", "TRAVEL",
					"TUNNEL", "TWITCH", "TEMPLE", "THEORY", "UNISON", "UPWARD", "UTMOST", "UTOPIA", "UNIQUE", "UNREST",
					"UNSEEN", "UNWRAP", "UNVIEL", "UPHOLD", "VACUUM", "VECTOR", "VIEWER", "VORTEX", "VALLEY", "VERBAL",
					"VICTIM", "VOLUME", "VANISH", "VERMIN", "WAFFLE", "WEALTH", "WHEEZE", "WIDGET", "WOLVES", "WRENCH",
					"WALNUT", "WEIGHT", "WISDOM", "WONDER", "YEARLY", "YELLOW", "YONDER", "ZEALOT", "ZEBRAS", "ZODIAC"
			};
		solution = wordList[UnityEngine.Random.Range(0, wordList.Length)].ToUpperInvariant();
		scramble = new string(solution.ToCharArray().Shuffle());
		Debug.LogFormat("[Orange Hexabuttons #{0}] Generated Word: {1}", moduleId, solution);
		Debug.LogFormat("[Orange Hexabuttons #{0}] Scrambled Word: {1}", moduleId, scramble);
		foreach (int i in buttonIndex)
			hexButtons[i].OnInteract = delegate { pressedButton(i); return false; };
		hexButtons[6].OnInteract = delegate { pressedCenter(); return false; };
		hexButtons[7].OnInteract = delegate { deafMode = !(deafMode); return false; };
		string alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ", temp;
		switch (UnityEngine.Random.Range(0, 7))
		{
			case 0://Atbash

				voiceMessage = new string[1];
				voiceMessage[0] = "ATBASH";
				for (int aa = 0; aa < 6; aa++)
					buttonText[aa].text = alpha[25 - alpha.IndexOf(scramble[aa])] + "";
				Debug.LogFormat("[Orange Hexabuttons #{0}] Encryption using ATBASH: {1}{2}{3}{4}{5}{6}", moduleId, buttonText[0].text, buttonText[1].text, buttonText[2].text, buttonText[3].text, buttonText[4].text, buttonText[5].text);
				break;
			case 1://Caesar
				voiceMessage = new string[3];
				voiceMessage[0] = "CAESAR";
				int r1 = UnityEngine.Random.Range(1, 26);
				if (r1 < 10)
				{
					voiceMessage[1] = "0";
					voiceMessage[2] = r1 + "";
				}
				else
				{
					voiceMessage[1] = (r1 / 10) + "";
					voiceMessage[2] = (r1 % 10) + "";
				}
				for (int aa = 0; aa < 6; aa++)
					buttonText[aa].text = alpha[mod(alpha.IndexOf(scramble[aa]) - r1, 26)] + "";
				Debug.LogFormat("[Orange Hexabuttons #{0}] Encryption using CAESAR {1}{2}: {3}{4}{5}{6}{7}{8}", moduleId, voiceMessage[1], voiceMessage[2], buttonText[0].text, buttonText[1].text, buttonText[2].text, buttonText[3].text, buttonText[4].text, buttonText[5].text);
				break;
			case 2://Condi
				voiceMessage = new string[3];
				voiceMessage[0] = "CONDI";
				int r2 = UnityEngine.Random.Range(1, 26);
				if (r2 < 10)
				{
					voiceMessage[1] = "0";
					voiceMessage[2] = r2 + "";
				}
				else
				{
					voiceMessage[1] = (r2 / 10) + "";
					voiceMessage[2] = (r2 % 10) + "";
				}
				for (int aa = 0; aa < 6; aa++)
				{
					buttonText[aa].text = alpha[mod(alpha.IndexOf(scramble[aa]) + r2, 26)] + "";
					r2 = alpha.IndexOf(scramble[aa]) + 1;
				}
				Debug.LogFormat("[Orange Hexabuttons #{0}] Encryption using CONDI {1}{2}: {3}{4}{5}{6}{7}{8}", moduleId, voiceMessage[1], voiceMessage[2], buttonText[0].text, buttonText[1].text, buttonText[2].text, buttonText[3].text, buttonText[4].text, buttonText[5].text);
				break;
			case 3://Monoalphabetic
				voiceMessage = new string[7];
				voiceMessage[0] = "MONOALPHABETIC";
				temp = alpha.ToUpper();
				for (int aa = 1; aa < 7; aa++)
				{
					voiceMessage[aa] = temp[UnityEngine.Random.Range(0, temp.Length)] + "";
					temp = temp.Replace(voiceMessage[aa], "");
				}
				for (int aa = 6; aa >= 1; aa--)
					temp = voiceMessage[aa] + "" + temp;
				for (int aa = 0; aa < 6; aa++)
				{
					buttonText[aa].text = temp[alpha.IndexOf(scramble[aa])] + "";
				}
				Debug.LogFormat("[Orange Hexabuttons #{0}] Encryption using MONOALPHABETIC {1}{2}{3}{4}{5}{6}: {7}{8}{9}{10}{11}{12}", moduleId, voiceMessage[1], voiceMessage[2], voiceMessage[3], voiceMessage[4], voiceMessage[5], voiceMessage[6], buttonText[0].text, buttonText[1].text, buttonText[2].text, buttonText[3].text, buttonText[4].text, buttonText[5].text);
				break;
			case 4://Porta
				voiceMessage = new string[7];
				voiceMessage[0] = "PORTA";
				temp = alpha.ToUpper();
				for (int aa = 1; aa < 7; aa++)
				{
					voiceMessage[aa] = temp[UnityEngine.Random.Range(0, temp.Length)] + "";
					temp = temp.Replace(voiceMessage[aa], "");
				}
				string[] portaChart =
				{
					"NOPQRSTUVWXYZ",
					"OPQRSTUVWXYZN",
					"PQRSTUVWXYZNO",
					"QRSTUVWXYZNOP",
					"RSTUVWXYZNOPQ",
					"STUVWXYZNOPQR",
					"TUVWXYZNOPQRS",
					"UVWXYZNOPQRST",
					"VWXYZNOPQRSTU",
					"WXYZNOPQRSTUV",
					"XYZNOPQRSTUVW",
					"YZNOPQRSTUVWX",
					"ZNOPQRSTUVWXY"
				};
				for (int aa = 0; aa < 6; aa++)
				{
					if (alpha.IndexOf(scramble[aa]) < 13)
						buttonText[aa].text = portaChart[alpha.IndexOf(voiceMessage[aa + 1]) / 2][alpha.IndexOf(scramble[aa])] + "";
					else
						buttonText[aa].text = alpha[portaChart[alpha.IndexOf(voiceMessage[aa + 1]) / 2].IndexOf(scramble[aa])] + "";
				}
				Debug.LogFormat("[Orange Hexabuttons #{0}] Encryption using PORTA {1}{2}{3}{4}{5}{6}: {7}{8}{9}{10}{11}{12}", moduleId, voiceMessage[1], voiceMessage[2], voiceMessage[3], voiceMessage[4], voiceMessage[5], voiceMessage[6], buttonText[0].text, buttonText[1].text, buttonText[2].text, buttonText[3].text, buttonText[4].text, buttonText[5].text);
				break;
			case 5://Ragbaby
				voiceMessage = new string[3];
				voiceMessage[0] = "RAGBABY";
				int r3 = UnityEngine.Random.Range(0, 26);
				if (r3 < 10)
				{
					voiceMessage[1] = "0";
					voiceMessage[2] = r3 + "";
				}
				else
				{
					voiceMessage[1] = (r3 / 10) + "";
					voiceMessage[2] = (r3 % 10) + "";
				}
				for (int aa = 0; aa < 6; aa++)
				{
					buttonText[aa].text = alpha[mod(alpha.IndexOf(scramble[aa]) + r3, 26)] + "";
					r3++;
				}
				Debug.LogFormat("[Orange Hexabuttons #{0}] Encryption using RAGBABY {1}{2}: {3}{4}{5}{6}{7}{8}", moduleId, voiceMessage[1], voiceMessage[2], buttonText[0].text, buttonText[1].text, buttonText[2].text, buttonText[3].text, buttonText[4].text, buttonText[5].text);
				break;
			default://Vigenere
				voiceMessage = new string[7];
				voiceMessage[0] = "VIGENERE";
				temp = alpha.ToUpper();
				string alpha2 = "ZABCDEFGHIJKLMNOPQRSTUVWXY";
				for (int aa = 1; aa < 7; aa++)
				{
					voiceMessage[aa] = temp[UnityEngine.Random.Range(0, temp.Length)] + "";
					temp = temp.Replace(voiceMessage[aa], "");
					int r4 = mod(alpha2.IndexOf(scramble[aa - 1]) + alpha2.IndexOf(voiceMessage[aa]), 26);
					buttonText[aa - 1].text = alpha2[r4] + "";
				}
				Debug.LogFormat("[Orange Hexabuttons #{0}] Encryption using VIGENERE {1}{2}{3}{4}{5}{6}: {7}{8}{9}{10}{11}{12}", moduleId, voiceMessage[1], voiceMessage[2], voiceMessage[3], voiceMessage[4], voiceMessage[5], voiceMessage[6], buttonText[0].text, buttonText[1].text, buttonText[2].text, buttonText[3].text, buttonText[4].text, buttonText[5].text);
				break;
		}
	}
	private int mod(int n, int m)
	{
		while (n < 0)
			n += m;
		return (n % m);
	}
	void pressedButton(int n)
	{
		Debug.LogFormat("[Orange Hexabuttons #{0}] User pressed {1}. Which is the decrypted letter {2}.", moduleId, positions[n], scramble[n]);
		Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
		if (solution[numButtonPresses] == scramble[n])
		{
			Vector3 pos = buttonMesh[n].transform.localPosition;
			pos.y = 0.0126f;
			buttonMesh[n].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
			hexButtons[n].OnInteract = null;
			ledMesh[n].material = ledColors[1];
			if (numButtonPresses == 5)
			{
				moduleSolved = true;
				hexButtons[7].OnInteract = null;
				module.HandlePass();
			}
			else
				numButtonPresses++;
		}
		else
		{
			numButtonPresses = 0;
			Debug.LogFormat("[Orange Hexabuttons #{0}] Strike! I was expecting {1}!", moduleId, solution[numButtonPresses]);
			module.HandleStrike();
			for (int aa = 0; aa < 6; aa++)
			{
				Vector3 pos = buttonMesh[aa].transform.localPosition;
				pos.y = 0.0169f;
				buttonMesh[aa].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
				ledMesh[aa].material = ledColors[0];
			}
			foreach (int i in buttonIndex)
				hexButtons[i].OnInteract = delegate { pressedButton(i); return false; };
		}
	}
	void pressedCenter()
	{
		if (!(moduleSolved))
		{
			Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
			Vector3 pos = buttonMesh[6].transform.localPosition;
			pos.y = 0.0126f;
			buttonMesh[6].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
			StartCoroutine(playAudio());
		}
	}
	IEnumerator playAudio()
	{
		hexButtons[6].OnInteract = null;
		yield return new WaitForSeconds(0.5f);
		for (int aa = 0; aa < voiceMessage.Length; aa++)
		{
			Audio.PlaySoundAtTransform(voiceMessage[aa], transform);
			if(deafMode)
				centerText.text = voiceMessage[aa].Substring(0, Math.Min(voiceMessage[aa].Length, 2));
			yield return new WaitForSeconds(1.2f);
			centerText.text = "";
			yield return new WaitForSeconds(0.3f);
		}
		Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonRelease, transform);
		Vector3 pos = buttonMesh[6].transform.localPosition;
		pos.y = 0.0169f;
		buttonMesh[6].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
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
		while (!moduleSolved)
        {
			for (int i = 0; i < 6; i++)
            {
				if (solution[numButtonPresses] == scramble[i] && hexButtons[i].OnInteract != null)
                {
					hexButtons[i].OnInteract();
					yield return new WaitForSeconds(0.2f);
					break;
				}
            }
        }
    }
}
