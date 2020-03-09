using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x0200001B RID: 27
[Serializable]
public class CheatScript : MonoBehaviour
{
	// Token: 0x060000BC RID: 188 RVA: 0x0000BD30 File Offset: 0x00009F30
	public CheatScript()
	{
		this.cache = string.Empty;
		this.c = string.Empty;
	}

	// Token: 0x060000BD RID: 189 RVA: 0x0000BD50 File Offset: 0x00009F50
	public virtual void Start()
	{
		this.initialcache = "12345678901234567890";
		this.cache = this.initialcache;
	}

	// Token: 0x060000BE RID: 190 RVA: 0x0000BD6C File Offset: 0x00009F6C
	public virtual void Update()
	{
		this.c = Input.inputString;
		if (this.c != string.Empty)
		{
			this.cache = this.cache.Substring(1, this.cache.Length - 1) + this.c;
			this.cache = this.cache.ToLower();
		}
		if (this.cache.Contains("idkfa"))
		{
			this.givekeys();
			this.giveweapons();
			this.givehealth();
			((TextMeshProUGUI)GameObject.Find("MessageText").GetComponent(typeof(TextMeshProUGUI))).text = "Notably exhuberant munitions acquired";
			((ClearMessageAfterTime)GameObject.Find("MessageText").GetComponent(typeof(ClearMessageAfterTime))).timer = ((ClearMessageAfterTime)GameObject.Find("MessageText").GetComponent(typeof(ClearMessageAfterTime))).defaulttime;
			this.cache = this.initialcache;
		}
		if (this.cache.Contains("nbkeys"))
		{
			this.givekeys();
			this.cache = this.initialcache;
		}
		if (this.cache.Contains("nbdeity"))
		{
			this.godmode();
			this.cache = this.initialcache;
		}
	}

	// Token: 0x060000BF RID: 191 RVA: 0x0000BEC4 File Offset: 0x0000A0C4
	public virtual void godmode()
	{
		PlayerHealthManagement playerHealthManagement = (PlayerHealthManagement)GameObject.Find("Player").GetComponent(typeof(PlayerHealthManagement));
		if (playerHealthManagement)
		{
			playerHealthManagement.godmode = !playerHealthManagement.godmode;
		}
	}

	// Token: 0x060000C0 RID: 192 RVA: 0x0000BF0C File Offset: 0x0000A10C
	public virtual void givekeys()
	{
		SelectionScript selectionScript = (SelectionScript)GameObject.Find("WeaponAnimator").GetComponent(typeof(SelectionScript));
		if (selectionScript)
		{
			selectionScript.haveredkey = true;
			float a = 0.8f;
			Color color = ((Image)GameObject.Find("RedIndicator").GetComponent(typeof(Image))).color;
			float num = color.a = a;
			Color color2 = ((Image)GameObject.Find("RedIndicator").GetComponent(typeof(Image))).color = color;
			selectionScript.havebluekey = true;
			float a2 = 0.8f;
			Color color3 = ((Image)GameObject.Find("BlueIndicator").GetComponent(typeof(Image))).color;
			float num2 = color3.a = a2;
			Color color4 = ((Image)GameObject.Find("BlueIndicator").GetComponent(typeof(Image))).color = color3;
			selectionScript.haveyellowkey = true;
			float a3 = 0.8f;
			Color color5 = ((Image)GameObject.Find("YellowIndicator").GetComponent(typeof(Image))).color;
			float num3 = color5.a = a3;
			Color color6 = ((Image)GameObject.Find("YellowIndicator").GetComponent(typeof(Image))).color = color5;
		}
	}

	// Token: 0x060000C1 RID: 193 RVA: 0x0000C090 File Offset: 0x0000A290
	public virtual void giveweapons()
	{
		SelectionScript selectionScript = (SelectionScript)GameObject.Find("WeaponAnimator").GetComponent(typeof(SelectionScript));
		if (selectionScript)
		{
			for (int i = 0; i < selectionScript.weaponinventory.Length; i++)
			{
				selectionScript.weaponinventory[i] = true;
				selectionScript.ammoinventory[i] = selectionScript.maxammo[i];
			}
			selectionScript.havedualpistols = true;
			selectionScript.permduals = true;
			selectionScript.permdaikatana = true;
			selectionScript.havedaikatana = true;
			selectionScript.permshotguns = true;
			selectionScript.havedualshotguns = true;
		}
	}

	// Token: 0x060000C2 RID: 194 RVA: 0x0000C130 File Offset: 0x0000A330
	public virtual void givehealth()
	{
		PlayerHealthManagement playerHealthManagement = (PlayerHealthManagement)GameObject.Find("Player").GetComponent(typeof(PlayerHealthManagement));
		if (playerHealthManagement)
		{
			playerHealthManagement.myhealth = (float)200;
			playerHealthManagement.myarmor = (float)100;
		}
	}

	// Token: 0x060000C3 RID: 195 RVA: 0x0000C17C File Offset: 0x0000A37C
	public virtual bool warplevel(string thecheat, string thecache)
	{
		int num = thecache.IndexOf(thecheat);
		int length = thecheat.Length;
		string s = string.Empty;
		bool result = false;
		if (num + length + 1 <= thecache.Length - 1)
		{
			s = thecache.Substring(num + length, 2);
			float num2 = (float)0;
			if (float.TryParse(s, out num2))
			{
				int num3 = (int)(num2 + (float)2);
				if (num3 < 3)
				{
					num3 = 3;
				}
				if (num3 > SceneManager.sceneCountInBuildSettings - 5)
				{
					num3 = SceneManager.sceneCountInBuildSettings - 2;
				}
				Time.timeScale = (float)1;
				Time.timeScale = (float)1;
				((PersistScript)GameObject.Find("PERSIST").GetComponent(typeof(PersistScript))).leveltoload = num3;
				((PersistScript)GameObject.Find("PERSIST").GetComponent(typeof(PersistScript))).reseteverything();
				Application.LoadLevel("LoadingScene");
				result = true;
			}
		}
		return result;
	}

	// Token: 0x060000C4 RID: 196 RVA: 0x0000C25C File Offset: 0x0000A45C
	public virtual void givedualshotguns()
	{
		SelectionScript selectionScript = (SelectionScript)GameObject.Find("WeaponAnimator").GetComponent(typeof(SelectionScript));
		if (selectionScript)
		{
			selectionScript.weaponinventory[2] = true;
			selectionScript.ammoinventory[2] = selectionScript.maxammo[2];
			selectionScript.havedualshotguns = true;
			selectionScript.permshotguns = true;
			selectionScript.weapontogetto = 3;
		}
	}

	// Token: 0x060000C5 RID: 197 RVA: 0x0000C2CC File Offset: 0x0000A4CC
	public virtual void Main()
	{
	}

	// Token: 0x040001A5 RID: 421
	[HideInInspector]
	public string cache;

	// Token: 0x040001A6 RID: 422
	[HideInInspector]
	public string c;

	// Token: 0x040001A7 RID: 423
	[HideInInspector]
	public string initialcache;
}
