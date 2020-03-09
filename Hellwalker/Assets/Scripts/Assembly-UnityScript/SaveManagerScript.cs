using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Boo.Lang;
using TMPro;
using UnityEngine;

// Token: 0x02000099 RID: 153
[Serializable]
public class SaveManagerScript : MonoBehaviour
{
	// Token: 0x060003B6 RID: 950 RVA: 0x00023CF8 File Offset: 0x00021EF8
	public virtual void Start()
	{
		this.filename = Application.dataPath + "/../saves/" + "saved_game";
		this.savebin = Application.dataPath + "/../savebin";
		this.inputmanager = (MyInputManager)GameObject.Find("DasMenu").GetComponent(typeof(MyInputManager));
		this.dmenu = GameObject.Find("DasMenu");
		this.plr = GameObject.Find("Player");
		this.p = (PersistScript)GameObject.Find("PERSIST").GetComponent(typeof(PersistScript));
	}

	// Token: 0x060003B7 RID: 951 RVA: 0x00023DA4 File Offset: 0x00021FA4
	public virtual void Update()
	{
		if (this.dosave)
		{
			((TextMeshProUGUI)GameObject.Find("MessageText").GetComponent(typeof(TextMeshProUGUI))).text = "Game Saved";
			((ClearMessageAfterTime)GameObject.Find("MessageText").GetComponent(typeof(ClearMessageAfterTime))).timer = ((ClearMessageAfterTime)GameObject.Find("MessageText").GetComponent(typeof(ClearMessageAfterTime))).defaulttime;
		}
		this.dosave = false;
		this.doload = false;
		if (!this.alreadyloaded && ES2.Exists(this.savebin + "?tag=doload"))
		{
			int num = ES2.Load<int>(this.savebin + "?tag=doload");
			if (num == 1)
			{
				ES2.Save<int>(0, this.savebin + "?tag=doload");
				this.filename = ES2.Load<string>(this.savebin + "?tag=f1l3");
				this.spawnprefabs();
				this.doload = true;
			}
		}
		this.alreadyloaded = true;
		if (this.menusave)
		{
			this.quicksave();
			this.menusave = false;
		}
		if (!((InGameMenuScript)this.dmenu.GetComponent(typeof(InGameMenuScript))).isPaused)
		{
			if (this.inputmanager.GetKeyInput("quick save", 1))
			{
				this.filename = Application.dataPath + "/../saves/quicksave";
				this.quicksave();
			}
			if (this.inputmanager.GetKeyInput("quick load", 1))
			{
				this.filename = Application.dataPath + "/../saves/quicksave";
				this.quickload(true);
			}
		}
	}

	// Token: 0x060003B8 RID: 952 RVA: 0x00023F60 File Offset: 0x00022160
	public virtual void quicksave()
	{
		if (GameObject.Find("StatObject") && !((StatScript)GameObject.Find("StatObject").GetComponent(typeof(StatScript))).endlessarena)
		{
			//this.StartCoroutine(this.savescreenshot());
			ES2.Delete(this.filename);
			this.dosave = true;
			((TextMeshProUGUI)GameObject.Find("MessageText").GetComponent(typeof(TextMeshProUGUI))).text = "Saving...";
			((ClearMessageAfterTime)GameObject.Find("MessageText").GetComponent(typeof(ClearMessageAfterTime))).timer = ((ClearMessageAfterTime)GameObject.Find("MessageText").GetComponent(typeof(ClearMessageAfterTime))).defaulttime;
			Canvas.ForceUpdateCanvases();
			this.p.lastfilename = this.filename;
			this.p.savedthislevel = true;
			ES2.Save<int>(Application.loadedLevel, this.filename + "?tag=sc3n30n");
		}
	}

	// Token: 0x060003B9 RID: 953 RVA: 0x00024074 File Offset: 0x00022274
	public virtual void quickload(bool loadingscreen)
	{
		if (ES2.Exists(this.filename))
		{
			ES2.Save<int>(1, this.savebin + "?tag=doload");
			ES2.Save<string>(this.filename, this.savebin + "?tag=f1l3");
			int num = ES2.Load<int>(this.filename + "?tag=sc3n30n");
			if (loadingscreen)
			{
				this.p.leveltoload = num;
				this.p.lastfilename = this.filename;
				this.p.savedthislevel = true;
				if (this.p.leveltoload != Application.loadedLevel)
				{
					Application.LoadLevel("LoadingScene");
				}
				else
				{
					Application.LoadLevel(Application.loadedLevel);
				}
			}
			else
			{
				Application.LoadLevel(num);
			}
		}
	}

	// Token: 0x060003BA RID: 954 RVA: 0x00024140 File Offset: 0x00022340
	//public virtual IEnumerator savescreenshot()
	//{
	//	return new SaveManagerScript.$savescreenshot$460(this).GetEnumerator();
	//}

	// Token: 0x060003BB RID: 955 RVA: 0x00024150 File Offset: 0x00022350
	public virtual void spawnprefabs()
	{
		string[] tags = ES2.GetTags(this.filename);
		for (int i = 0; i < tags.Length; i++)
		{
			if (tags[i].Contains("(Clone)"))
			{
				string name = tags[i].Substring(0, tags[i].IndexOf("(Clone"));
				int num = this.findprefab(name);
				if (num != -1 && tags[i].Contains("tr4n5orm"))
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.spawnableitems[num], new Vector3((float)0, (float)0, (float)0), Quaternion.identity);
					ES2.Load<Transform>(this.filename + "?tag=" + tags[i], gameObject.transform);
				}
			}
		}
	}

	// Token: 0x060003BC RID: 956 RVA: 0x00024210 File Offset: 0x00022410
	public virtual int findprefab(string name)
	{
		for (int i = 0; i < this.spawnableitems.Length; i++)
		{
			if (this.spawnableitems[i].name == name)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x060003BD RID: 957 RVA: 0x00024258 File Offset: 0x00022458
	public virtual bool intToBool(int i)
	{
		return i != 0;
	}

	// Token: 0x060003BE RID: 958 RVA: 0x0002426C File Offset: 0x0002246C
	public virtual int boolToInt(bool b)
	{
		return (!b) ? 0 : 1;
	}

	// Token: 0x060003BF RID: 959 RVA: 0x00024280 File Offset: 0x00022480
	public virtual void Main()
	{
	}

	// Token: 0x0400049B RID: 1179
	[HideInInspector]
	public bool dosave;

	// Token: 0x0400049C RID: 1180
	[HideInInspector]
	public bool doload;

	// Token: 0x0400049D RID: 1181
	public string filename;

	// Token: 0x0400049E RID: 1182
	public GameObject[] spawnableitems;

	// Token: 0x0400049F RID: 1183
	[HideInInspector]
	public bool alreadyloaded;

	// Token: 0x040004A0 RID: 1184
	[HideInInspector]
	public bool menusave;

	// Token: 0x040004A1 RID: 1185
	[HideInInspector]
	public string savebin;

	// Token: 0x040004A2 RID: 1186
	[HideInInspector]
	public GameObject plr;

	// Token: 0x040004A3 RID: 1187
	[HideInInspector]
	public PersistScript p;

	// Token: 0x040004A4 RID: 1188
	[HideInInspector]
	public MyInputManager inputmanager;

	// Token: 0x040004A5 RID: 1189
	[HideInInspector]
	public GameObject dmenu;

	// Token: 0x0200009A RID: 154
	//[CompilerGenerated]
	//[Serializable]
	//internal sealed class $savescreenshot$460 : GenericGenerator<object>
	//{
	//	// Token: 0x060003C0 RID: 960 RVA: 0x00024284 File Offset: 0x00022484
	//	public $savescreenshot$460(SaveManagerScript self_)
	//	{
	//		this.$self_$463 = self_;
	//	}

	//	// Token: 0x060003C1 RID: 961 RVA: 0x00024294 File Offset: 0x00022494
	//	public override IEnumerator<object> GetEnumerator()
	//	{
	//		return new SaveManagerScript.$savescreenshot$460.$(this.$self_$463);
	//	}

	//	// Token: 0x040004A6 RID: 1190
	//	internal SaveManagerScript $self_$463;
	//}
}
