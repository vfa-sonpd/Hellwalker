using System;
using Boo.Lang.Runtime;
using TMPro;
using UnityEngine;
using UnityScript.Lang;

// Token: 0x020000B2 RID: 178
[Serializable]
public class StatScript : MonoBehaviour
{
	// Token: 0x0600042D RID: 1069 RVA: 0x00027FD8 File Offset: 0x000261D8
	public StatScript()
	{
		this.levelname = "Unnamed";
		this.episodeandlevel = "E#M#";
		this.partime = "00.00.00";
		this.difficultymultiplier = (float)1;
		this.difficulty = 1;
		this.addtomultiplier = 1;
		this.multiplierdecayspeed = (float)4;
		this.multiplierlimit = 99;
	}

	// Token: 0x0600042E RID: 1070 RVA: 0x00028034 File Offset: 0x00026234
	public virtual void Start()
	{
		this.addtomultiplier = 1;
		this.addtomultiplierperwave = 0;
		this.multiplierdecayspeed = (float)4;
		this.multiplierlimit = 25;
		if (GameObject.Find("SmallDifficultyLabel"))
		{
			this.difficultylabel = (TextMeshProUGUI)GameObject.Find("SmallDifficultyLabel").GetComponent(typeof(TextMeshProUGUI));
		}
		if (GameObject.Find("PERSIST"))
		{
			this.p = (PersistScript)GameObject.Find("PERSIST").GetComponent(typeof(PersistScript));
		}
		this.scoredisplay = (TextMeshProUGUI)GameObject.Find("InGameScore").GetComponent(typeof(TextMeshProUGUI));
		this.savedtimes = new UnityScript.Lang.Array(4);
		this.inputmanager = (MyInputManager)GameObject.Find("DasMenu").GetComponent(typeof(MyInputManager));
		this.multiplier = (float)1;
		this.startingenemies = this.countenemies();
		this.startingsecrets = this.countsecrets();
		this.disbekills = (TextMeshProUGUI)GameObject.Find("KillLabel").GetComponent(typeof(TextMeshProUGUI));
		this.smallLevelName = (TextMeshProUGUI)GameObject.Find("SmallLevelName").GetComponent(typeof(TextMeshProUGUI));
		this.smallLevelName.text = this.levelname;
		this.disbesecrets = (TextMeshProUGUI)GameObject.Find("SecretsLabel").GetComponent(typeof(TextMeshProUGUI));
		this.disbetime = (TextMeshProUGUI)GameObject.Find("LevelTime").GetComponent(typeof(TextMeshProUGUI));
		this.disbemultiplier = (TextMeshProUGUI)GameObject.Find("MultiplierText").GetComponent(typeof(TextMeshProUGUI));
		this.disbescore = (TextMeshProUGUI)GameObject.Find("PointScoreText").GetComponent(typeof(TextMeshProUGUI));
		this.disbesavedtime = (TextMeshProUGUI)GameObject.Find("SavedTime").GetComponent(typeof(TextMeshProUGUI));
		this.disbesavedtime.text = string.Empty;
		this.sav = (SaveManagerScript)GameObject.Find("SaveManager").GetComponent(typeof(SaveManagerScript));
		this.origcoors = this.transform.position;
		int num = 0;
		Color color = this.disbetime.color;
		float num2 = color.a = (float)num;
		Color color2 = this.disbetime.color = color;
		int num3 = 0;
		Color color3 = this.disbekills.color;
		float num4 = color3.a = (float)num3;
		Color color4 = this.disbekills.color = color3;
		int num5 = 0;
		Color color5 = this.disbesecrets.color;
		float num6 = color5.a = (float)num5;
		Color color6 = this.disbesecrets.color = color5;
		int num7 = 0;
		Color color7 = this.smallLevelName.color;
		float num8 = color7.a = (float)num7;
		Color color8 = this.smallLevelName.color = color7;
		int num9 = 0;
		Color color9 = this.disbesavedtime.color;
		float num10 = color9.a = (float)num9;
		Color color10 = this.disbesavedtime.color = color9;
		int num11 = 0;
		Color color11 = this.difficultylabel.color;
		float num12 = color11.a = (float)num11;
		Color color12 = this.difficultylabel.color = color11;
	}

	// Token: 0x0600042F RID: 1071 RVA: 0x000283D8 File Offset: 0x000265D8
	public virtual void Update()
	{
		this.timerstuff();
		this.checkbutton();
	}

	// Token: 0x06000430 RID: 1072 RVA: 0x000283E8 File Offset: 0x000265E8
	public virtual void FixedUpdate()
	{
		this.difficultylabel.text = string.Empty + this.episodeandlevel + string.Empty;
		string rhs = string.Empty;
		if (this.difficulty == 0)
		{
			rhs = " - ACCESSIBLE";
		}
		if (this.difficulty == 1)
		{
			rhs = " - GO EASY";
		}
		if (this.difficulty == 2)
		{
			rhs = " - I CAN TAKE IT";
		}
		if (this.difficulty == 3)
		{
			rhs = " - CERO MIEDO";
		}
		if (this.difficulty == 4)
		{
			rhs = " - DUSKMARE";
		}
		this.difficultylabel.text = this.difficultylabel.text + rhs;
		if (this.p && this.p.hardcore)
		{
			this.difficultylabel.text = this.difficultylabel.text + " - intruder";
		}
		this.dosavestuff();
		this.SetDifficultyMultiplier();
		if (this.countenemies() < 2)
		{
			this.showenemyindicators = true;
		}
		else
		{
			this.showenemyindicators = false;
		}
		this.multiplier -= Time.deltaTime / this.multiplierdecayspeed;
		if (this.multiplier > (float)this.multiplierlimit)
		{
			this.multiplier = (float)this.multiplierlimit;
		}
		if (this.multiplier < (float)1)
		{
			this.multiplier = (float)1;
		}
		if (this.endlessarena)
		{
			this.disbemultiplier.text = "x" + this.multiplier.ToString("0");
		}
		else
		{
			this.disbemultiplier.text = string.Empty;
		}
		if (this.score > 999999999)
		{
			this.score = 999999999;
		}
		if (!this.endlessarena)
		{
			this.disbekills.text = "Kills - " + (this.startingenemies - this.countenemies()).ToString("00") + " / " + this.startingenemies.ToString("00");
			this.disbesecrets.text = "Secrets - " + (this.startingsecrets - this.countsecrets()).ToString("00") + " / " + this.startingsecrets.ToString("00");
			this.disbetime.text = "Time - " + this.minutes.ToString("00") + "." + this.seconds.ToString("00.00");
			this.scoredisplay.text = string.Empty;
			this.dosavedtimetext();
		}
		else
		{
			this.endlesskills();
			this.disbekills.text = "Kills - " + this.endlessenemykills;
			this.disbesecrets.text = string.Empty;
			this.disbetime.text = "Time - " + this.minutes.ToString("00") + "." + this.seconds.ToString("00.00");
			this.scoredisplay.text = this.score.ToString("000000000");
			this.smallLevelName.text = string.Empty;
		}
	}

	// Token: 0x06000431 RID: 1073 RVA: 0x00028758 File Offset: 0x00026958
	public virtual void timerstuff()
	{
		if (Time.timeScale > (float)0)
		{
			this.seconds += Time.unscaledDeltaTime;
		}
		if (this.seconds > (float)60)
		{
			this.minutes += (float)1;
			this.seconds = (float)0;
		}
		if (this.minutes > (float)99)
		{
			this.minutes = (float)99;
		}
	}

	// Token: 0x06000432 RID: 1074 RVA: 0x000287C0 File Offset: 0x000269C0
	public virtual int countenemies()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("EnemyTag");
		GameObject[] array2 = GameObject.FindGameObjectsWithTag("ScarecrowSpawnerTag");
		return array.Length + array2.Length;
	}

	// Token: 0x06000433 RID: 1075 RVA: 0x000287F0 File Offset: 0x000269F0
	public virtual int countsecrets()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("SecretTag");
		return array.Length;
	}

	// Token: 0x06000434 RID: 1076 RVA: 0x00028810 File Offset: 0x00026A10
	public virtual void checkbutton()
	{
		if (this.inputmanager.GetKeyInput("toggle stats", 1))
		{
			if (this.disbetime.color.a == (float)0)
			{
				float a = 0.8f;
				Color color = this.disbetime.color;
				float num = color.a = a;
				Color color2 = this.disbetime.color = color;
				float a2 = 0.8f;
				Color color3 = this.disbekills.color;
				float num2 = color3.a = a2;
				Color color4 = this.disbekills.color = color3;
				float a3 = 0.8f;
				Color color5 = this.disbesecrets.color;
				float num3 = color5.a = a3;
				Color color6 = this.disbesecrets.color = color5;
				float a4 = 0.8f;
				Color color7 = this.smallLevelName.color;
				float num4 = color7.a = a4;
				Color color8 = this.smallLevelName.color = color7;
				float a5 = 0.8f;
				Color color9 = this.disbesavedtime.color;
				float num5 = color9.a = a5;
				Color color10 = this.disbesavedtime.color = color9;
				float a6 = 0.8f;
				Color color11 = this.difficultylabel.color;
				float num6 = color11.a = a6;
				Color color12 = this.difficultylabel.color = color11;
			}
			else
			{
				int num7 = 0;
				Color color13 = this.disbetime.color;
				float num8 = color13.a = (float)num7;
				Color color14 = this.disbetime.color = color13;
				int num9 = 0;
				Color color15 = this.disbekills.color;
				float num10 = color15.a = (float)num9;
				Color color16 = this.disbekills.color = color15;
				int num11 = 0;
				Color color17 = this.disbesecrets.color;
				float num12 = color17.a = (float)num11;
				Color color18 = this.disbesecrets.color = color17;
				int num13 = 0;
				Color color19 = this.smallLevelName.color;
				float num14 = color19.a = (float)num13;
				Color color20 = this.smallLevelName.color = color19;
				int num15 = 0;
				Color color21 = this.disbesavedtime.color;
				float num16 = color21.a = (float)num15;
				Color color22 = this.disbesavedtime.color = color21;
				int num17 = 0;
				Color color23 = this.difficultylabel.color;
				float num18 = color23.a = (float)num17;
				Color color24 = this.difficultylabel.color = color23;
			}
		}
		if (this.inputmanager.GetKeyInput("record time", 1))
		{
			this.savedtimes.Push(this.minutes.ToString("00") + "." + this.seconds.ToString("00.00"));
			this.savedtimes.RemoveAt(0);
		}
	}

	// Token: 0x06000435 RID: 1077 RVA: 0x00028B50 File Offset: 0x00026D50
	public virtual void endlesskills()
	{
		int num = this.countenemies();
		if (this.endlesscurrentenemies < num)
		{
			this.endlesscurrentenemies = num;
		}
		if (this.endlesscurrentenemies > num)
		{
			this.endlessenemykills++;
			this.endlesscurrentenemies = num;
		}
	}

	// Token: 0x06000436 RID: 1078 RVA: 0x00028B98 File Offset: 0x00026D98
	public virtual void SetDifficultyMultiplier()
	{
		if (this.difficulty == 0)
		{
			this.difficultymultiplier = 0.1f;
		}
		if (this.difficulty == 1)
		{
			this.difficultymultiplier = 0.5f;
		}
		if (this.difficulty == 2)
		{
			this.difficultymultiplier = (float)1;
		}
		if (this.difficulty == 3)
		{
			this.difficultymultiplier = (float)2;
		}
		if (this.difficulty == 4)
		{
			this.difficultymultiplier = (float)100;
		}
	}

	// Token: 0x06000437 RID: 1079 RVA: 0x00028C10 File Offset: 0x00026E10
	public virtual void dosavedtimetext()
	{
		this.disbesavedtime.text = string.Empty;
		for (int i = 0; i < this.savedtimes.length; i++)
		{
			if (i == this.savedtimes.length - 1 && !RuntimeServices.EqualityOperator(this.savedtimes[i], null))
			{
				this.disbesavedtime.text = this.disbesavedtime.text + ("-" + this.savedtimes[i] + "-" + "\n");
			}
			else
			{
				this.disbesavedtime.text = this.disbesavedtime.text + (" " + this.savedtimes[i] + "\n");
			}
		}
	}

	// Token: 0x06000438 RID: 1080 RVA: 0x00028CF8 File Offset: 0x00026EF8
	public virtual void dosavestuff()
	{
		string rhs = null;
		if (this.sav.dosave || this.sav.doload)
		{
			rhs = "?tag=" + this.transform.gameObject.name + this.origcoors.ToString();
		}
		if (this.sav.dosave)
		{
			ES2.Save<Transform>(this.transform, this.sav.filename + rhs + "tr4n5orm");
			ES2.Save<float>(this.minutes, this.sav.filename + rhs + "m1nut35");
			ES2.Save<float>(this.seconds, this.sav.filename + rhs + "53c0nd5");
		}
		if (this.sav.doload)
		{
			if (ES2.Exists(this.sav.filename + rhs + "tr4n5orm"))
			{
				this.minutes = ES2.Load<float>(this.sav.filename + rhs + "m1nut35");
				this.seconds = ES2.Load<float>(this.sav.filename + rhs + "53c0nd5");
			}
			else
			{
				UnityEngine.Object.Destroy(this.transform.gameObject);
			}
		}
	}

	// Token: 0x06000439 RID: 1081 RVA: 0x00028E6C File Offset: 0x0002706C
	public virtual void spawnpoints(Vector3 pos, float points)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ingamepoints, pos, Quaternion.identity);
		((TextMesh)gameObject.GetComponent(typeof(TextMesh))).text = points.ToString();
		((Endless3DPointsScript)gameObject.GetComponent(typeof(Endless3DPointsScript))).mypoints = this.multiplier;
	}

	// Token: 0x0600043A RID: 1082 RVA: 0x00028ED0 File Offset: 0x000270D0
	public virtual void Main()
	{
	}

	// Token: 0x04000524 RID: 1316
	public GameObject ingamepoints;

	// Token: 0x04000525 RID: 1317
	[HideInInspector]
	public float minutes;

	// Token: 0x04000526 RID: 1318
	[HideInInspector]
	public float seconds;

	// Token: 0x04000527 RID: 1319
	[HideInInspector]
	public int startingenemies;

	// Token: 0x04000528 RID: 1320
	[HideInInspector]
	public int startingsecrets;

	// Token: 0x04000529 RID: 1321
	[HideInInspector]
	public TextMeshProUGUI disbekills;

	// Token: 0x0400052A RID: 1322
	[HideInInspector]
	public TextMeshProUGUI disbesecrets;

	// Token: 0x0400052B RID: 1323
	[HideInInspector]
	public TextMeshProUGUI disbetime;

	// Token: 0x0400052C RID: 1324
	public bool endlessarena;

	// Token: 0x0400052D RID: 1325
	public bool brokenflashlight;

	// Token: 0x0400052E RID: 1326
	public string levelname;

	// Token: 0x0400052F RID: 1327
	public string episodeandlevel;

	// Token: 0x04000530 RID: 1328
	public string partime;

	// Token: 0x04000531 RID: 1329
	[HideInInspector]
	public int endlessenemykills;

	// Token: 0x04000532 RID: 1330
	[HideInInspector]
	public int endlesscurrentenemies;

	// Token: 0x04000533 RID: 1331
	[HideInInspector]
	public float multiplier;

	// Token: 0x04000534 RID: 1332
	[HideInInspector]
	public TextMeshProUGUI disbemultiplier;

	// Token: 0x04000535 RID: 1333
	[HideInInspector]
	public TextMeshProUGUI disbescore;

	// Token: 0x04000536 RID: 1334
	[HideInInspector]
	public TextMeshProUGUI disbesavedtime;

	// Token: 0x04000537 RID: 1335
	[HideInInspector]
	public int score;

	// Token: 0x04000538 RID: 1336
	[HideInInspector]
	public bool showenemyindicators;

	// Token: 0x04000539 RID: 1337
	[HideInInspector]
	public float difficultymultiplier;

	// Token: 0x0400053A RID: 1338
	[HideInInspector]
	public int difficulty;

	// Token: 0x0400053B RID: 1339
	[HideInInspector]
	public MyInputManager inputmanager;

	// Token: 0x0400053C RID: 1340
	[HideInInspector]
	public UnityScript.Lang.Array savedtimes;

	// Token: 0x0400053D RID: 1341
	[HideInInspector]
	public TextMeshProUGUI smallLevelName;

	// Token: 0x0400053E RID: 1342
	[HideInInspector]
	public SaveManagerScript sav;

	// Token: 0x0400053F RID: 1343
	[HideInInspector]
	public Vector3 origcoors;

	// Token: 0x04000540 RID: 1344
	[HideInInspector]
	public TextMeshProUGUI scoredisplay;

	// Token: 0x04000541 RID: 1345
	[HideInInspector]
	public TextMeshProUGUI difficultylabel;

	// Token: 0x04000542 RID: 1346
	[HideInInspector]
	public PersistScript p;

	// Token: 0x04000543 RID: 1347
	public int addtomultiplier;

	// Token: 0x04000544 RID: 1348
	public int addtomultiplierperwave;

	// Token: 0x04000545 RID: 1349
	public float multiplierdecayspeed;

	// Token: 0x04000546 RID: 1350
	public int multiplierlimit;
}
