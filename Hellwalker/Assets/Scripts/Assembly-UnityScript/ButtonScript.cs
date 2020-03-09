using System;
using TMPro;
using UnityEngine;

// Token: 0x02000017 RID: 23
[Serializable]
public class ButtonScript : MonoBehaviour
{
	// Token: 0x0600009C RID: 156 RVA: 0x0000B054 File Offset: 0x00009254
	public ButtonScript()
	{
		this.altleveltoload = -1;
		this.texttodisplay = string.Empty;
	}

	// Token: 0x0600009D RID: 157 RVA: 0x0000B070 File Offset: 0x00009270
	public virtual void Awake()
	{
		this.setmat();
	}

	// Token: 0x0600009E RID: 158 RVA: 0x0000B078 File Offset: 0x00009278
	public virtual void Start()
	{
		this.sav = (SaveManagerScript)GameObject.Find("SaveManager").GetComponent(typeof(SaveManagerScript));
		this.origcoors = this.transform.position;
	}

	// Token: 0x0600009F RID: 159 RVA: 0x0000B0BC File Offset: 0x000092BC
	public virtual void Update()
	{
		this.saveloadstuff();
		if (this.codebutton)
		{
			this.checkcode();
		}
		if (this.anybutton)
		{
			this.anybuttonstuff();
		}
	}

	// Token: 0x060000A0 RID: 160 RVA: 0x0000B0F4 File Offset: 0x000092F4
	public virtual void dopress()
	{
		this.checkkey();
	}

	// Token: 0x060000A1 RID: 161 RVA: 0x0000B0FC File Offset: 0x000092FC
	public virtual void setmat()
	{
		Renderer renderer = (Renderer)this.GetComponent(typeof(Renderer));
		if (!this.buttonstate)
		{
			renderer.material.SetTexture("_MainTex", this.textures[0]);
		}
		if (this.buttonstate)
		{
			renderer.material.SetTexture("_MainTex", this.textures[1]);
		}
	}

	// Token: 0x060000A2 RID: 162 RVA: 0x0000B168 File Offset: 0x00009368
	public virtual void playsounds()
	{
		if (this.buttonstate)
		{
			((AudioSource)this.GetComponent(typeof(AudioSource))).clip = this.sounds[0];
		}
		if (!this.buttonstate)
		{
			((AudioSource)this.GetComponent(typeof(AudioSource))).clip = this.sounds[1];
		}
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
	}

	// Token: 0x060000A3 RID: 163 RVA: 0x0000B1EC File Offset: 0x000093EC
	public virtual void checkrepress()
	{
		if (!this.canrepress)
		{
			this.transform.gameObject.tag = "Untagged";
			if ((NameDisplayScript)this.GetComponent(typeof(NameDisplayScript)))
			{
				((NameDisplayScript)this.GetComponent(typeof(NameDisplayScript))).myname = string.Empty;
			}
		}
	}

	// Token: 0x060000A4 RID: 164 RVA: 0x0000B258 File Offset: 0x00009458
	public virtual void displaytext()
	{
		if (this.texttodisplay != string.Empty)
		{
			((TextMeshProUGUI)GameObject.Find("TutorialMessageText").GetComponent(typeof(TextMeshProUGUI))).text = this.texttodisplay;
			((ClearMessageAfterTime)GameObject.Find("TutorialMessageText").GetComponent(typeof(ClearMessageAfterTime))).timer = (float)3;
		}
	}

	// Token: 0x060000A5 RID: 165 RVA: 0x0000B2C8 File Offset: 0x000094C8
	public virtual void checkkey()
	{
		bool flag = true;
		SelectionScript selectionScript = (SelectionScript)GameObject.Find("WeaponAnimator").GetComponent(typeof(SelectionScript));
		if (this.needred && !selectionScript.haveredkey)
		{
			flag = false;
		}
		if (this.needblue && !selectionScript.havebluekey)
		{
			flag = false;
		}
		if (this.needyellow && !selectionScript.haveyellowkey)
		{
			flag = false;
		}
		if (flag)
		{
			this.buttonstate = !this.buttonstate;
			if (this.switchmaterial)
			{
				this.setmat();
			}
			if (this.playsound)
			{
				this.playsounds();
			}
			this.checkrepress();
			this.displaytext();
		}
		else
		{
			string rhs = null;
			if (this.needred)
			{
				rhs = "<color=#800000ff>RED</color>";
			}
			if (this.needblue)
			{
				rhs = "<color=#000080ff>BLUE</color>";
			}
			if (this.needyellow)
			{
				rhs = "<color=#808000ff>YELLOW</color>";
			}
			((AudioSource)GameObject.Find("NoKeySound").GetComponent(typeof(AudioSource))).Play();
			((TextMeshProUGUI)GameObject.Find("MessageText").GetComponent(typeof(TextMeshProUGUI))).text = "You need the " + rhs + " key";
			((ClearMessageAfterTime)GameObject.Find("MessageText").GetComponent(typeof(ClearMessageAfterTime))).timer = ((ClearMessageAfterTime)GameObject.Find("MessageText").GetComponent(typeof(ClearMessageAfterTime))).defaulttime;
		}
	}

	// Token: 0x060000A6 RID: 166 RVA: 0x0000B45C File Offset: 0x0000965C
	public virtual void checkcode()
	{
		bool flag = true;
		for (int i = 0; i < this.codebuttons.Length; i++)
		{
			if (((ButtonScript)this.codebuttons[i].GetComponent(typeof(ButtonScript))).buttonstate != this.codestates[i])
			{
				flag = false;
			}
		}
		if (flag)
		{
			this.buttonstate = true;
		}
	}

	// Token: 0x060000A7 RID: 167 RVA: 0x0000B4D0 File Offset: 0x000096D0
	public virtual void anybuttonstuff()
	{
		for (int i = 0; i < this.inheritfrom.Length; i++)
		{
			if (((ButtonScript)this.inheritfrom[i].GetComponent(typeof(ButtonScript))).buttonstate)
			{
				this.buttonstate = true;
			}
		}
	}

	// Token: 0x060000A8 RID: 168 RVA: 0x0000B528 File Offset: 0x00009728
	public virtual void OnTriggerEnter(Collider hit)
	{
		if (this.istrigger && hit.transform.gameObject.layer == 10)
		{
			this.buttonstate = true;
		}
	}

	// Token: 0x060000A9 RID: 169 RVA: 0x0000B554 File Offset: 0x00009754
	public virtual void saveloadstuff()
	{
		string rhs = string.Empty;
		if (this.sav.dosave || this.sav.doload)
		{
			rhs = "?tag=" + this.transform.gameObject.name + this.origcoors.ToString();
		}
		if (this.sav.dosave)
		{
			ES2.Save<int>(this.sav.boolToInt(this.buttonstate), this.sav.filename + rhs + "butt0nst4t3");
			ES2.Save<string>(this.transform.gameObject.tag, this.sav.filename + rhs + "t44444g");
		}
		if (this.sav.doload)
		{
			this.buttonstate = this.sav.intToBool(ES2.Load<int>(this.sav.filename + rhs + "butt0nst4t3"));
			this.transform.gameObject.tag = ES2.Load<string>(this.sav.filename + rhs + "t44444g");
			this.setmat();
		}
	}

	// Token: 0x060000AA RID: 170 RVA: 0x0000B694 File Offset: 0x00009894
	public virtual void Main()
	{
	}

	// Token: 0x04000180 RID: 384
	public bool needred;

	// Token: 0x04000181 RID: 385
	public bool needblue;

	// Token: 0x04000182 RID: 386
	public bool needyellow;

	// Token: 0x04000183 RID: 387
	public bool buttonstate;

	// Token: 0x04000184 RID: 388
	public bool canrepress;

	// Token: 0x04000185 RID: 389
	public bool canshoot;

	// Token: 0x04000186 RID: 390
	public bool istrigger;

	// Token: 0x04000187 RID: 391
	public bool switchmaterial;

	// Token: 0x04000188 RID: 392
	public bool playsound;

	// Token: 0x04000189 RID: 393
	public Texture2D[] textures;

	// Token: 0x0400018A RID: 394
	public AudioClip[] sounds;

	// Token: 0x0400018B RID: 395
	public int altleveltoload;

	// Token: 0x0400018C RID: 396
	public string texttodisplay;

	// Token: 0x0400018D RID: 397
	public bool codebutton;

	// Token: 0x0400018E RID: 398
	public GameObject[] codebuttons;

	// Token: 0x0400018F RID: 399
	public bool[] codestates;

	// Token: 0x04000190 RID: 400
	public bool anybutton;

	// Token: 0x04000191 RID: 401
	public GameObject[] inheritfrom;

	// Token: 0x04000192 RID: 402
	[HideInInspector]
	public SaveManagerScript sav;

	// Token: 0x04000193 RID: 403
	[HideInInspector]
	public Vector3 origcoors;
}
