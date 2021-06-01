using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenHexabuttons {

	private ColorfulButtonSeries coloredHexabuttons;
	private KMAudio Audio;
	private AudioClip[] notes;
	private int moduleId;
	private KMSelectable[] hexButtons;
	private MeshRenderer[] buttonMesh;
	private TextMesh[] buttonText; 
	private Material[] ledColors;
	private MeshRenderer[] ledMesh;
	private Light[] lights;
	private Transform transform;
	private string[] voiceMessage;
	private string solution;
	private int numButtonPresses;
	private bool moduleSolved;
	private string greenNotes;
	private bool flag;
	private string[] positions = { "TL", "TR", "ML", "MR", "BL", "BR" };
	private int[] buttonIndex = { 0, 1, 2, 3, 4, 5 };
	public GreenHexabuttons(ColorfulButtonSeries m, KMAudio aud, AudioClip[] N, int MI, KMSelectable[] HB, MeshRenderer[] BM, TextMesh[] BT, Material[] LC, MeshRenderer[] LM, Light[] L, Transform T)
	{
		coloredHexabuttons = m;
		Audio = aud;
		notes = N;
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
		Debug.LogFormat("[Colored Hexabuttons #{0}] Color Generated: Green", moduleId);
		string[] sheetMusic =
		{
			"9A5230","983472","62018A","162830","A62857","937A81","064892","93150A","725936","4230A7",
			"7A4820","18A760","3491A5","A68239","75491A","674318","605827","4208A9","0145A2","980341",
			"056A14","02386A","2A8136","A56439","04371A","957031","5086A1","359801","369185","2850A3",
			"362581","718395","1A7405","712980","78A362","062591","5A7902","091A86","305761","97A042",
			"84A712","047685","780936","928435","36A597","61702A","913756","0A6397","A38902","250971",
			"790132","254107","21306A","843591","413A02","476089","38752A","1A4086","504A93","941076",
			"9738A4","698415","9705A3","13A854","42380A","9216A5","795082","619387","67859A","37A580",
			"46A507","896421","738062","601235","92517A","472A50","406531","235A71","768519","129534",
			"731925","09A431","462905","396510","A13640","186A24","640923","746359","395240","027853",
			"07163A","4A6780","612435","42A915","054276","903215","562410","75A180","3A1824","762358",
			"179265","8140A5","15032A","A36105","645802","49A017","047629","27634A","632781","240A17",
			"264870","496178","2A1437","083761","427018","016284","094A52","836470","358210","53A942",
			"053816","02A178","810463","473A29","A51986","0A6751","A67014","382154","326405","863941",
			"75A980","981A52","7A3652","A51428","543876","09A685","470261","847015","148605","697810",
			"246A15","3814A6","04285A","942573","412A79","85102A","9058A4","0A4128","342186","570649",
			"210549","142576","536724","21A037","926487","087951","73A650","578932","490258","6714A2",
			"504173","02A598","842516","23817A","102375","625380","5A8427","715A20","5679A2","2A3597",
			"4A2931","91A356","710456","815062","710843","257A89","8097A3","2A3854","072A69","768593",
			"36091A","08A972","169720","634528","65A172","6A0951","582019","38A129","A49601","056397",
			"962754","403216","42715A","4981A0","5963A8","4A2863","62A590","75403A","85126A","8102A9",
			"0A6429","048927","453A78","79354A","23A069","0A6412","61A392","4A3967","A51839","482159",
			"743689","384260","625089","869345","296A47","80A369","963840","392A85","84A536","453201",
			"287043","9713A4","498073","752184","346591","952380","3042A5","892A43","836921","031967",
			"4869A3","960578","426A05","57468A","380192","809342","0468A9","329876","51A867","92601A",
			"34A850","371692","148723","1A4563","271034","81A497","502A86","A81034","317249","580247",
			"3A7861","89A246","7A5694","183907","234706","904281","7063A2","516072","245308","709A68",
			"32748A","631A57","820739","A19465","269134","93841A","601983","03647A","A90214","931647",
			"871062","A14829","A46753","03A861","201367","A65403","A27640","753081","8653A1","5A0263",
			"057281","421679","145836","20A468","10853A","4A3209","812690","076529","29346A","043A62",
			"905734","47892A","984350","1937A5","475091","635908","28096A","643A90","A12038","97A328",
			"45A097","A58937","319526","803795","A54978","302574","2A4136","376025","104792","701659",
			"432A57","390274","42856A","60A479","671325","846157","509784","942615","19706A","592063",
			"039627","0A8934","172346","935406","713892","756083","918762","508634","87A521","581437",
			"617850","89256A","876A02","9156A7","A65418","642973","905341","409615","81A523","32685A",
			"742985","489506","875234","5A7301","481795","A90465","A86507","73A049","2954A6","62A705",
			"316490","03695A","49527A","43A176","4619A2","69A218","46A352","8A3072","187264","875946",
			"324867","051894","8439A5","971543","562879","397485","710849","1689A4","39A021","062319",
			"9517A8","1A4735","A85036","871625","3A7196","34A708","293564","19327A","704538","70458A",
			"416807","3785A1","745213","9A4078","85A429","A91708","295703","785291","823140","548A60",
			"76192A","A06378","093647","206A51","3A6819","289165","192430","214580","978620","7A6982",
			"7612A8","834A06","238107","246875","32591A","4A7581","A79130","890641","8A1746","352819",
			"53214A","A21590","51476A","419A85","4091A5","7523A0","7132A6","7A9164","27A396","865713",
			"05938A","457163","A72019","A05827","81950A","063475","568190","794512","749301","982413",
			"1074A8","2A3907","257183","679514","1A9436","430671","056A97","805134","74A138","75A910",
			"76452A","238569","A29718","A75368","6A8459","314A50","6A9871","298360","8371A0","A84697",
			"32596A","486952","1309A8","574902","892463","A39867","046921","87A436","A65213","241798",
			"2A4867","973418"
		};
		int measure = UnityEngine.Random.Range(0, sheetMusic.Length);
		Debug.LogFormat("[Colored Hexabuttons #{0}] Generated Measure: {1}", moduleId, measure);
		buttonText[6].text = (measure + 1) + "";
		solution = sheetMusic[measure].ToUpper();
		string temp = solution.ToUpper();
		Debug.LogFormat("[Colored Hexabuttons #{0}] Notes in measure: {1} {2} {3} {4} {5} {6}", moduleId, "-0123456789A".IndexOf(temp[0]), "-0123456789A".IndexOf(temp[1]), "-0123456789A".IndexOf(temp[2]), "-0123456789A".IndexOf(temp[3]), "-0123456789A".IndexOf(temp[4]), "-0123456789A".IndexOf(temp[5]));
		greenNotes = "";
		foreach (int i in buttonIndex)
		{
			greenNotes = greenNotes + "" + temp[UnityEngine.Random.Range(0, temp.Length)];
			hexButtons[i].OnInteract = delegate { pressedGreen(i); return false; };
			hexButtons[i].OnInteractEnded = delegate { pressedGreenRelease(i); };
			temp = temp.Replace(greenNotes[i] + "", "");
		}
		Debug.LogFormat("[Colored Hexabuttons #{0}] Notes on buttons in reading order: {1} {2} {3} {4} {5} {6}", moduleId, "-0123456789A".IndexOf(greenNotes[0]), "-0123456789A".IndexOf(greenNotes[1]), "-0123456789A".IndexOf(greenNotes[2]), "-0123456789A".IndexOf(greenNotes[3]), "-0123456789A".IndexOf(greenNotes[4]), "-0123456789A".IndexOf(greenNotes[5]));
		hexButtons[6].OnInteract = delegate { pressedGreenCenter(); return false; };
		hexButtons[6].OnInteractEnded = delegate { pressedGreenCenterRelease(); };
		flag = false;
	}
	void pressedGreen(int n)
	{
		if (!(moduleSolved))
		{
			Audio.PlaySoundAtTransform(notes["0123456789A".IndexOf(greenNotes[n])].name, transform);
			Vector3 pos = buttonMesh[n].transform.localPosition;
			pos.y = 0.0126f;
			buttonMesh[n].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
		}
	}
	void pressedGreenRelease(int n)
	{
		if (!(moduleSolved))
		{
			Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonRelease, transform);
			Vector3 pos = buttonMesh[n].transform.localPosition;
			pos.y = 0.0169f;
			buttonMesh[n].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
		}
	}
	void pressedGreenSubmit(int n)
	{
		if (!(moduleSolved))
		{
			Debug.LogFormat("[Colored Hexabuttons #{0}] User submitted {1}, which plays the note {2}.", moduleId, positions[n], "-0123456789A".IndexOf(greenNotes[n]));
			Audio.PlaySoundAtTransform(notes["0123456789A".IndexOf(greenNotes[n])].name, transform);
			if (solution[numButtonPresses] == greenNotes[n])
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
				Debug.LogFormat("[Colored Hexabuttons #{0}] Strike! I was expecting note {1} which is on the {2} button!", moduleId, "-0123456789A".IndexOf(solution[n]), positions[greenNotes.IndexOf(solution[numButtonPresses])]);
				coloredHexabuttons.Strike();
				foreach (int i in buttonIndex)
				{
					Vector3 pos = buttonMesh[i].transform.localPosition;
					pos.y = 0.0169f;
					buttonMesh[i].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
					hexButtons[i].OnInteract = delegate { pressedGreen(i); return false; };
					hexButtons[i].OnInteractEnded = delegate { pressedGreenRelease(i); };
					ledMesh[i].material = ledColors[0];
				}
				flag = false;
			}
		}
	}
	void pressedGreenCenter()
	{
		if (!(moduleSolved))
		{
			Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
			Vector3 pos = buttonMesh[6].transform.localPosition;
			pos.y = 0.0126f;
			buttonMesh[6].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
			if (flag)
			{
				flag = false;
				foreach (int i in buttonIndex)
				{
					Vector3 pos2 = buttonMesh[i].transform.localPosition;
					pos2.y = 0.0169f;
					buttonMesh[i].transform.localPosition = new Vector3(pos2.x, pos2.y, pos2.z);
					hexButtons[i].OnInteract = delegate { pressedGreen(i); return false; };
					hexButtons[i].OnInteractEnded = delegate { pressedGreenRelease(i); };
					ledMesh[i].material = ledColors[0];
				}
			}
			else
			{
				flag = true;
				numButtonPresses = 0;
				foreach (int i in buttonIndex)
				{
					hexButtons[i].OnInteract = delegate { pressedGreenSubmit(i); return false; };
					hexButtons[i].OnInteractEnded = null;
				}
				coloredHexabuttons.StartCoroutine(greenFlasher());
			}
		}
	}
	void pressedGreenCenterRelease()
	{
		if (!(moduleSolved))
		{
			Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonRelease, transform);
			Vector3 pos = buttonMesh[6].transform.localPosition;
			pos.y = 0.0169f;
			buttonMesh[6].transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
		}
	}
	IEnumerator greenFlasher()
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
