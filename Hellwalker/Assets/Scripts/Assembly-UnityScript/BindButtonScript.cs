using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000011 RID: 17
[Serializable]
public class BindButtonScript : MonoBehaviour
{
	// Token: 0x06000075 RID: 117 RVA: 0x0000A2DC File Offset: 0x000084DC
	public BindButtonScript()
	{
		this.myname = string.Empty;
	}

	// Token: 0x06000076 RID: 118 RVA: 0x0000A2F0 File Offset: 0x000084F0
	public virtual void Start()
	{
		this.inputmanager = (MyInputManager)GameObject.Find("DasMenu").GetComponent(typeof(MyInputManager));
		this.myindex = this.inputmanager.FindIndex(this.myname);
		this.mytextcomponent = (Text)this.mytext.GetComponent(typeof(Text));
		this.bindscript = (BindControlsScript)GameObject.Find("DasMenu").GetComponent(typeof(BindControlsScript));
		this.gamemenuscript = (GameMenuButtonsScript)GameObject.Find("DasMenu").GetComponent(typeof(GameMenuButtonsScript));
		this.mykeytext = this.inputmanager.keycodes[this.myindex].ToString();
		if (this.mykeytext == "LeftShift")
		{
			this.mykeytext = "Shift";
		}
	}

	// Token: 0x06000077 RID: 119 RVA: 0x0000A3E4 File Offset: 0x000085E4
	public virtual void Update()
	{
		this.mytextcomponent.text = this.myname + ":  '" + this.inputmanager.keycodes[this.myindex] + "'";
		if (this.inputmanager.keycodes[this.myindex] == KeyCode.RightShift || this.inputmanager.keycodes[this.myindex] == KeyCode.LeftShift)
		{
			this.mytextcomponent.text = this.myname + ":  Shift'";
		}
	}

	// Token: 0x06000078 RID: 120 RVA: 0x0000A488 File Offset: 0x00008688
	public virtual void dobind()
	{
		this.bindscript.bindkey(this.myname);
	}

	// Token: 0x06000079 RID: 121 RVA: 0x0000A49C File Offset: 0x0000869C
	public virtual void Main()
	{
	}

	// Token: 0x0400015D RID: 349
	[HideInInspector]
	public BindControlsScript bindscript;

	// Token: 0x0400015E RID: 350
	[HideInInspector]
	public GameMenuButtonsScript gamemenuscript;

	// Token: 0x0400015F RID: 351
	[HideInInspector]
	public Text mytextcomponent;

	// Token: 0x04000160 RID: 352
	[HideInInspector]
	public MyInputManager inputmanager;

	// Token: 0x04000161 RID: 353
	[HideInInspector]
	public int myindex;

	// Token: 0x04000162 RID: 354
	public string myname;

	// Token: 0x04000163 RID: 355
	public GameObject mytext;

	// Token: 0x04000164 RID: 356
	[HideInInspector]
	public string mykeytext;
}
