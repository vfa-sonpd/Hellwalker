using System;
using TMPro;
using UnityEngine;

// Token: 0x02000063 RID: 99
[Serializable]
public class LevelWinStuffScript : MonoBehaviour
{
	// Token: 0x06000284 RID: 644 RVA: 0x0001800C File Offset: 0x0001620C
	public LevelWinStuffScript()
	{
		this.timermultiply = (float)1;
	}

	// Token: 0x06000285 RID: 645 RVA: 0x0001801C File Offset: 0x0001621C
	public virtual void Start()
	{
		this.didsound = new bool[4];
		this.p = (PersistScript)GameObject.Find("PERSIST").GetComponent(typeof(PersistScript));
		if (this.p.killsfromlastlevel >= this.p.startingenemiesfromlastlevel && this.p.secretsfromlastlevel >= this.p.startingsecretsfromlastlevel)
		{
			this.p.completionistaward = true;
		}
		this.beststuff();
		string lhs = Application.dataPath + "/../config/scores" + "?tag=" + this.p.lastlevelnumber.ToString();
		this.minutes = this.p.minutesfromlastlevel;
		this.seconds = this.p.secondsfromlastlevel;
		((TextMeshProUGUI)GameObject.Find("LevelNameText").GetComponent(typeof(TextMeshProUGUI))).text = this.p.lastlevelname + " (" + this.p.lastlevelnumbers + ")";
		((TextMeshProUGUI)GameObject.Find("TimeText").GetComponent(typeof(TextMeshProUGUI))).text = "TIME";
		((TextMeshProUGUI)GameObject.Find("TotalTimeText").GetComponent(typeof(TextMeshProUGUI))).text = "TOTAL";
		((TextMeshProUGUI)GameObject.Find("KillText").GetComponent(typeof(TextMeshProUGUI))).text = "KILLS";
		((TextMeshProUGUI)GameObject.Find("SecretText").GetComponent(typeof(TextMeshProUGUI))).text = "SECRETS";
		((TextMeshProUGUI)GameObject.Find("ParText").GetComponent(typeof(TextMeshProUGUI))).text = "DEV TIME: " + this.p.partime;
		this.totaltime = this.p.totalhours.ToString("00") + "." + this.p.totalminutes.ToString("00") + "." + this.p.totalseconds.ToString("00.00");
		((TextMeshProUGUI)GameObject.Find("TimeNumbers").GetComponent(typeof(TextMeshProUGUI))).text = string.Empty;
		((TextMeshProUGUI)GameObject.Find("TotalTimeNumbers").GetComponent(typeof(TextMeshProUGUI))).text = string.Empty;
		((TextMeshProUGUI)GameObject.Find("KillNumbers").GetComponent(typeof(TextMeshProUGUI))).text = string.Empty;
		((TextMeshProUGUI)GameObject.Find("SecretsNumbers").GetComponent(typeof(TextMeshProUGUI))).text = string.Empty;
		if (ES2.Exists(lhs + "levelbeaten"))
		{
			((TextMeshProUGUI)GameObject.Find("BestTimeText").GetComponent(typeof(TextMeshProUGUI))).text = "BEST TIME: " + ES2.Load<float>(lhs + "minutes").ToString("00") + "." + ES2.Load<float>(lhs + "seconds").ToString("00.00");
		}
		else
		{
			((TextMeshProUGUI)GameObject.Find("BestTimeText").GetComponent(typeof(TextMeshProUGUI))).text = "BEST TIME: " + this.minutes.ToString("00") + "." + this.seconds.ToString("00.00");
		}
		if (this.p.pacifistaward)
		{
			this.awards.active = true;
			this.pacifistaward.active = true;
		}
		if (this.p.ninjaaward)
		{
			this.awards.active = true;
			this.ninjaaward.active = true;
		}
		if (this.p.lowtechaward)
		{
			this.awards.active = true;
			this.lowtechaward.active = true;
		}
		if (this.p.completionistaward)
		{
			this.awards.active = true;
			this.completionistaward.active = true;
		}
	}

	// Token: 0x06000286 RID: 646 RVA: 0x000184AC File Offset: 0x000166AC
	public virtual void Update()
	{
		if (((ScreenshotImageScript)GameObject.Find("ScreenshotImage").GetComponent(typeof(ScreenshotImageScript))).startdelay <= (float)0)
		{
			if (!((AudioSource)this.backgroundmusic.GetComponent(typeof(AudioSource))).isPlaying)
			{
				((AudioSource)this.backgroundmusic.GetComponent(typeof(AudioSource))).Play();
				((AudioSource)this.impactsound.GetComponent(typeof(AudioSource))).Play();
			}
			this.dastimer += Time.deltaTime * this.timermultiply;
			if (this.dastimer >= this.timetime)
			{
				((TextMeshProUGUI)GameObject.Find("TimeNumbers").GetComponent(typeof(TextMeshProUGUI))).text = this.minutes.ToString("00") + "." + this.seconds.ToString("00.00");
				if (!this.didsound[0])
				{
					((AudioSource)this.gunsound.GetComponent(typeof(AudioSource))).Play();
					this.didsound[0] = true;
				}
			}
			if (this.dastimer >= this.totaltimetime)
			{
				((TextMeshProUGUI)GameObject.Find("TotalTimeNumbers").GetComponent(typeof(TextMeshProUGUI))).text = this.totaltime;
				if (!this.didsound[1])
				{
					((AudioSource)this.gunsound.GetComponent(typeof(AudioSource))).Play();
					this.didsound[1] = true;
				}
			}
			if (this.dastimer >= this.killstime)
			{
				((TextMeshProUGUI)GameObject.Find("KillNumbers").GetComponent(typeof(TextMeshProUGUI))).text = this.p.killsfromlastlevel + " / " + this.p.startingenemiesfromlastlevel;
				if (!this.didsound[2])
				{
					((AudioSource)this.gunsound.GetComponent(typeof(AudioSource))).Play();
					this.didsound[2] = true;
				}
			}
			if (this.dastimer >= this.secretstime)
			{
				((TextMeshProUGUI)GameObject.Find("SecretsNumbers").GetComponent(typeof(TextMeshProUGUI))).text = this.p.secretsfromlastlevel + " / " + this.p.startingsecretsfromlastlevel;
				if (!this.didsound[3])
				{
					((AudioSource)this.gunsound.GetComponent(typeof(AudioSource))).Play();
					this.didsound[3] = true;
					this.dastimer = (float)1000;
				}
			}
			if (this.dastimer > (float)1000)
			{
				this.dastimer = (float)1000;
			}
			if (Input.anyKeyDown)
			{
				if (this.dastimer < (float)1000)
				{
					this.dastimer = (float)1000;
				}
				else
				{
					this.p.leveltoload = this.p.lastlevelnumber + 1;
					if (this.p.textnext)
					{
						Application.LoadLevel(13);
						this.p.textnext = false;
					}
					else
					{
						Application.LoadLevel("LoadingScene");
					}
				}
			}
		}
	}

	// Token: 0x06000287 RID: 647 RVA: 0x0001887C File Offset: 0x00016A7C
	public virtual void beststuff()
	{
		string lhs = Application.dataPath + "/../config/scores" + "?tag=" + this.p.markthiscomplete.ToString();
		if (ES2.Exists(lhs + "levelbeaten"))
		{
			if (this.p.killsfromlastlevel > ES2.Load<int>(lhs + "kills"))
			{
				ES2.Save<int>(this.p.killsfromlastlevel, lhs + "kills");
			}
			if (this.p.secretsfromlastlevel > ES2.Load<int>(lhs + "secrets"))
			{
				ES2.Save<int>(this.p.secretsfromlastlevel, lhs + "secrets");
			}
			if (this.p.minutesfromlastlevel * (float)60 + this.p.secondsfromlastlevel < ES2.Load<float>(lhs + "minutes") * (float)60 + ES2.Load<float>(lhs + "seconds"))
			{
				ES2.Save<float>(this.p.minutesfromlastlevel, lhs + "minutes");
				ES2.Save<float>(this.p.secondsfromlastlevel, lhs + "seconds");
			}
			if (this.p.pacifistaward)
			{
				ES2.Save<int>(1, lhs + "pacifist");
			}
			if (this.p.ninjaaward)
			{
				ES2.Save<int>(1, lhs + "ninja");
			}
			if (this.p.completionistaward)
			{
				ES2.Save<int>(1, lhs + "completionist");
			}
			if (this.p.lowtechaward)
			{
				ES2.Save<int>(1, lhs + "lowtech");
			}
		}
		else
		{
			ES2.Save<int>(this.p.markthiscomplete, lhs + "levelbeaten");
			ES2.Save<int>(this.p.killsfromlastlevel, lhs + "kills");
			ES2.Save<int>(this.p.secretsfromlastlevel, lhs + "secrets");
			ES2.Save<float>(this.p.minutesfromlastlevel, lhs + "minutes");
			ES2.Save<float>(this.p.secondsfromlastlevel, lhs + "seconds");
			ES2.Save<string>(this.p.lastlevelname, lhs + "name");
			ES2.Save<int>(this.p.startingsecretsfromlastlevel, lhs + "startingsecrets");
			ES2.Save<int>(this.p.startingenemiesfromlastlevel, lhs + "startingenemies");
			if (this.p.pacifistaward)
			{
				ES2.Save<int>(1, lhs + "pacifist");
			}
			if (this.p.ninjaaward)
			{
				ES2.Save<int>(1, lhs + "ninja");
			}
			if (this.p.completionistaward)
			{
				ES2.Save<int>(1, lhs + "completionist");
			}
			if (this.p.lowtechaward)
			{
				ES2.Save<int>(1, lhs + "lowtech");
			}
		}
	}

	// Token: 0x06000288 RID: 648 RVA: 0x00018BA4 File Offset: 0x00016DA4
	public virtual void Main()
	{
	}

	// Token: 0x040002ED RID: 749
	[HideInInspector]
	public PersistScript p;

	// Token: 0x040002EE RID: 750
	[HideInInspector]
	public GameObject loadingimage;

	// Token: 0x040002EF RID: 751
	[HideInInspector]
	public float dastimer;

	// Token: 0x040002F0 RID: 752
	[HideInInspector]
	public float minutes;

	// Token: 0x040002F1 RID: 753
	[HideInInspector]
	public float seconds;

	// Token: 0x040002F2 RID: 754
	[HideInInspector]
	public string totaltime;

	// Token: 0x040002F3 RID: 755
	public float timetime;

	// Token: 0x040002F4 RID: 756
	public float totaltimetime;

	// Token: 0x040002F5 RID: 757
	public float killstime;

	// Token: 0x040002F6 RID: 758
	public float secretstime;

	// Token: 0x040002F7 RID: 759
	public float timermultiply;

	// Token: 0x040002F8 RID: 760
	public GameObject backgroundmusic;

	// Token: 0x040002F9 RID: 761
	public GameObject impactsound;

	// Token: 0x040002FA RID: 762
	public GameObject gunsound;

	// Token: 0x040002FB RID: 763
	public GameObject[] explosions;

	// Token: 0x040002FC RID: 764
	[HideInInspector]
	public bool[] didsound;

	// Token: 0x040002FD RID: 765
	public GameObject pacifistaward;

	// Token: 0x040002FE RID: 766
	public GameObject ninjaaward;

	// Token: 0x040002FF RID: 767
	public GameObject completionistaward;

	// Token: 0x04000300 RID: 768
	public GameObject lowtechaward;

	// Token: 0x04000301 RID: 769
	public GameObject awards;
}
