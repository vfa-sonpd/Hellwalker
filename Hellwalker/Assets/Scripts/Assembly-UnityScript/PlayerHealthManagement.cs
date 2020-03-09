using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200008E RID: 142
[Serializable]
public class PlayerHealthManagement : MonoBehaviour
{
	// Token: 0x06000379 RID: 889 RVA: 0x00020290 File Offset: 0x0001E490
	public PlayerHealthManagement()
	{
		this.myhealth = (float)100;
		this.myarmor = (float)100;
	}

	// Token: 0x0600037A RID: 890 RVA: 0x000202AC File Offset: 0x0001E4AC
	public virtual void Start()
	{
		this.statscript = (StatScript)GameObject.Find("StatObject").GetComponent(typeof(StatScript));
		if (GameObject.Find("PERSIST"))
		{
			this.persist = (PersistScript)GameObject.Find("PERSIST").GetComponent(typeof(PersistScript));
		}
		this.inputmanager = (MyInputManager)GameObject.Find("DasMenu").GetComponent(typeof(MyInputManager));
		this.healthtext = GameObject.Find("HealthText");
		this.armortext = GameObject.Find("ArmorText");
		this.damageoverlay = GameObject.Find("DamageOverlay");
		this.lowhealthoverlay = GameObject.Find("LowHealthOverlay");
		this.deadtext = GameObject.Find("DeadText");
		this.deadtext.active = false;
	}

	// Token: 0x0600037B RID: 891 RVA: 0x00020398 File Offset: 0x0001E598
	public virtual void Update()
	{
		this.drunkentimer -= Time.deltaTime;
		if (this.drunkentimer < (float)0)
		{
			this.drunkentimer = (float)0;
			if (this.drunkness >= (float)4)
			{
				this.drunkness = (float)0;
				((TextMeshProUGUI)GameObject.Find("MessageText").GetComponent(typeof(TextMeshProUGUI))).text = "Your head feels clearer";
				((ClearMessageAfterTime)GameObject.Find("MessageText").GetComponent(typeof(ClearMessageAfterTime))).timer = (float)4;
			}
		}
		((TextMeshProUGUI)this.healthtext.GetComponent(typeof(TextMeshProUGUI))).text = this.myhealth.ToString();
		((TextMeshProUGUI)this.armortext.GetComponent(typeof(TextMeshProUGUI))).text = this.myarmor.ToString();
		if (this.godmode && this.myhealth < (float)100)
		{
			this.myhealth = (float)100;
		}
		if (this.myhealth < (float)25)
		{
			this.lowhealthoverlay.active = true;
		}
		else
		{
			this.lowhealthoverlay.active = false;
		}
		if (this.myhealth <= (float)0)
		{
			this.myhealth = (float)0;
			((MyMouseLook)GameObject.Find("MainCamera").GetComponent(typeof(MyMouseLook))).enabled = false;
			if (GameObject.Find("PlayerHand"))
			{
				GameObject.Find("PlayerHand").active = false;
			}
			this.deadtext.active = true;
			((MyControllerScript)this.GetComponent(typeof(MyControllerScript))).enabled = false;
			StatScript statScript = (StatScript)GameObject.Find("StatObject").GetComponent(typeof(StatScript));
			if (statScript.endlessarena && !this.iamdead)
			{
				((TextMeshProUGUI)GameObject.Find("ScoreText").GetComponent(typeof(TextMeshProUGUI))).text = "Killed " + statScript.endlessenemykills.ToString() + " enemies in " + statScript.minutes.ToString("00") + " : " + Mathf.Floor(statScript.seconds).ToString("00");
				((TextMeshProUGUI)GameObject.Find("PointScoreText").GetComponent(typeof(TextMeshProUGUI))).text = statScript.score.ToString("000000000");
			}
			this.iamdead = true;
		}
		if (this.iamdead && this.inputmanager.GetKeyInput("use", 1))
		{
			if (this.persist.lastfilename == "none" || !this.persist.savedthislevel)
			{
				Application.LoadLevel(Application.loadedLevel);
			}
			else
			{
				SaveManagerScript saveManagerScript = (SaveManagerScript)GameObject.Find("SaveManager").GetComponent(typeof(SaveManagerScript));
				saveManagerScript.filename = this.persist.lastfilename;
				saveManagerScript.quickload(true);
			}
		}
		if (this.myarmor < (float)0)
		{
			this.myarmor = (float)0;
		}
	}

	// Token: 0x0600037C RID: 892 RVA: 0x000206E8 File Offset: 0x0001E8E8
	public virtual void takedamage(float damage)
	{
		StatScript statScript = (StatScript)GameObject.Find("StatObject").GetComponent(typeof(StatScript));
		statScript.multiplier = (float)0;
		if (this.persist)
		{
			this.persist.ninjaaward = false;
		}
		if (this.myarmor > (float)0)
		{
			if (this.statscript.difficulty < 3)
			{
				this.myhealth -= Mathf.Ceil(damage / (float)2);
				this.myarmor -= Mathf.Ceil(damage / (float)2);
			}
			else
			{
				this.myhealth -= Mathf.Ceil(damage / 1.5f);
				this.myarmor -= Mathf.Ceil(damage / (float)2);
			}
		}
		else
		{
			this.myhealth -= damage;
		}
		float a = Mathf.Clamp(damage * (float)2 / (float)100, (float)0, 1f);
		Color color = ((Image)this.damageoverlay.GetComponent(typeof(Image))).color;
		float num = color.a = a;
		Color color2 = ((Image)this.damageoverlay.GetComponent(typeof(Image))).color = color;
		this.myhealth = Mathf.Ceil(this.myhealth);
		this.myarmor = Mathf.Ceil(this.myarmor);
		((MyMouseLook)GameObject.Find("MainCamera").GetComponent(typeof(MyMouseLook))).buck = ((MyMouseLook)GameObject.Find("MainCamera").GetComponent(typeof(MyMouseLook))).buck - (float)5;
		((AudioSource)this.mydamagesound.GetComponent(typeof(AudioSource))).Play();
	}

	// Token: 0x0600037D RID: 893 RVA: 0x000208C0 File Offset: 0x0001EAC0
	public virtual void Main()
	{
	}

	// Token: 0x0400045D RID: 1117
	public GameObject damageoverlay;

	// Token: 0x0400045E RID: 1118
	public GameObject lowhealthoverlay;

	// Token: 0x0400045F RID: 1119
	public GameObject healthtext;

	// Token: 0x04000460 RID: 1120
	public GameObject armortext;

	// Token: 0x04000461 RID: 1121
	public float myhealth;

	// Token: 0x04000462 RID: 1122
	public float myarmor;

	// Token: 0x04000463 RID: 1123
	public GameObject mydamagesound;

	// Token: 0x04000464 RID: 1124
	public GameObject deadtext;

	// Token: 0x04000465 RID: 1125
	public bool iamdead;

	// Token: 0x04000466 RID: 1126
	public bool godmode;

	// Token: 0x04000467 RID: 1127
	public float drunkness;

	// Token: 0x04000468 RID: 1128
	public float drunkentimer;

	// Token: 0x04000469 RID: 1129
	[HideInInspector]
	public MyInputManager inputmanager;

	// Token: 0x0400046A RID: 1130
	[HideInInspector]
	public PersistScript persist;

	// Token: 0x0400046B RID: 1131
	[HideInInspector]
	public StatScript statscript;
}
