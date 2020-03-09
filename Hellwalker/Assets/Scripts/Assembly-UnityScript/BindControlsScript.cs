using System;
using IniParser;
using IniParser.Model;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000012 RID: 18
[Serializable]
public class BindControlsScript : MonoBehaviour
{
	// Token: 0x0600007A RID: 122 RVA: 0x0000A4A0 File Offset: 0x000086A0
	public BindControlsScript()
	{
		this.guikey = KeyCode.None;
		this.indextoassign = -1;
	}

	// Token: 0x0600007B RID: 123 RVA: 0x0000A4B8 File Offset: 0x000086B8
	public virtual void Awake()
	{
		this.gbuttons = (GameMenuButtonsScript)this.GetComponent(typeof(GameMenuButtonsScript));
		this.bindingsmenu = ((GameMenuButtonsScript)this.GetComponent(typeof(GameMenuButtonsScript))).menus[6];
		this.inputmanager = (MyInputManager)this.GetComponent(typeof(MyInputManager));
	}

	// Token: 0x0600007C RID: 124 RVA: 0x0000A520 File Offset: 0x00008720
	public virtual void Start()
	{
		this.loadbindings();
		this.createbuttons();
	}

	// Token: 0x0600007D RID: 125 RVA: 0x0000A530 File Offset: 0x00008730
	public virtual void Update()
	{
	}

	// Token: 0x0600007E RID: 126 RVA: 0x0000A534 File Offset: 0x00008734
	public virtual void OnGUI()
	{
		if (this.indextoassign == -1)
		{
			this.bindprompt.active = false;
		}
		Event current = Event.current;
		if (this.indextoassign != -1 && this.bindingsmenu.active)
		{
			if (current.isMouse)
			{
				this.guikey = this.ButtonToKeycode(current.button);
				this.checkduplicate(this.guikey, this.indextoassign);
				this.inputmanager.keycodes[this.indextoassign] = this.guikey;
				this.indextoassign = -1;
				this.savebindings();
				this.updatetext();
				Input.ResetInputAxes();
			}
			if (current.shift)
			{
				this.guikey = KeyCode.LeftShift;
				this.checkduplicate(this.guikey, this.indextoassign);
				this.inputmanager.keycodes[this.indextoassign] = this.guikey;
				this.indextoassign = -1;
				this.savebindings();
				this.updatetext();
			}
			if (Input.GetButton("mousebutton1"))
			{
				this.guikey = KeyCode.Mouse1;
				this.checkduplicate(this.guikey, this.indextoassign);
				this.inputmanager.keycodes[this.indextoassign] = this.guikey;
				this.indextoassign = -1;
				this.savebindings();
				this.updatetext();
			}
			if (Input.GetButton("mousebutton2"))
			{
				this.guikey = KeyCode.Mouse2;
				this.checkduplicate(this.guikey, this.indextoassign);
				this.inputmanager.keycodes[this.indextoassign] = this.guikey;
				this.indextoassign = -1;
				this.savebindings();
				this.updatetext();
			}
			if (Input.GetButton("mousebutton3"))
			{
				this.guikey = KeyCode.Mouse3;
				this.checkduplicate(this.guikey, this.indextoassign);
				this.inputmanager.keycodes[this.indextoassign] = this.guikey;
				this.indextoassign = -1;
				this.savebindings();
				this.updatetext();
			}
			if (Input.GetButton("mousebutton4"))
			{
				this.guikey = KeyCode.Mouse4;
				this.checkduplicate(this.guikey, this.indextoassign);
				this.inputmanager.keycodes[this.indextoassign] = this.guikey;
				this.indextoassign = -1;
				this.savebindings();
				this.updatetext();
			}
			if (Input.GetButton("mousebutton5"))
			{
				this.guikey = KeyCode.Mouse5;
				this.checkduplicate(this.guikey, this.indextoassign);
				this.inputmanager.keycodes[this.indextoassign] = this.guikey;
				this.indextoassign = -1;
				this.savebindings();
				this.updatetext();
			}
			if (Input.GetButton("mousebutton6"))
			{
				this.guikey = KeyCode.Mouse6;
				this.checkduplicate(this.guikey, this.indextoassign);
				this.inputmanager.keycodes[this.indextoassign] = this.guikey;
				this.indextoassign = -1;
				this.savebindings();
				this.updatetext();
			}
			if (current.isKey && current.keyCode != KeyCode.None)
			{
				this.guikey = current.keyCode;
				this.checkduplicate(this.guikey, this.indextoassign);
				this.inputmanager.keycodes[this.indextoassign] = this.guikey;
				this.indextoassign = -1;
				this.savebindings();
				this.updatetext();
			}
		}
	}

	// Token: 0x0600007F RID: 127 RVA: 0x0000A8A0 File Offset: 0x00008AA0
	public virtual void checkduplicate(KeyCode newkey, int index)
	{
		for (int i = 0; i < this.inputmanager.keycodes.Length; i++)
		{
			if (this.inputmanager.keycodes[i] == newkey)
			{
				this.inputmanager.keycodes[i] = this.inputmanager.keycodes[index];
			}
		}
	}

	// Token: 0x06000080 RID: 128 RVA: 0x0000A8FC File Offset: 0x00008AFC
	public virtual void bindkey(string name)
	{
		this.bindprompt.active = true;
		this.indextoassign = this.inputmanager.FindIndex(name);
	}

	// Token: 0x06000081 RID: 129 RVA: 0x0000A91C File Offset: 0x00008B1C
	public virtual void updatetext()
	{
	}

	// Token: 0x06000082 RID: 130 RVA: 0x0000A920 File Offset: 0x00008B20
	public virtual void savebindings()
	{
		for (int i = 0; i < this.inputmanager.keycodes.Length; i++)
		{
			this.SaveConfig(this.inputmanager.actionnames[i], this.inputmanager.keycodes[i]);
		}
		this.gbuttons.parser.WriteFile(this.gbuttons.filename, this.gbuttons.data, null);
	}

	// Token: 0x06000083 RID: 131 RVA: 0x0000A99C File Offset: 0x00008B9C
	public virtual void loadbindings()
	{
		for (int i = 0; i < this.inputmanager.keycodes.Length; i++)
		{
			this.inputmanager.keycodes[i] = this.LoadConfigKey(this.inputmanager.actionnames[i]);
		}
	}

	// Token: 0x06000084 RID: 132 RVA: 0x0000A9EC File Offset: 0x00008BEC
	public virtual KeyCode LoadConfigKey(string key)
	{
		string value = this.gbuttons.data["CONTROLS"][key];
		return (KeyCode)Enum.Parse(typeof(KeyCode), value);
	}

	// Token: 0x06000085 RID: 133 RVA: 0x0000AA30 File Offset: 0x00008C30
	public virtual void SaveConfig(string key, object contents)
	{
		string value = contents.ToString();
		this.gbuttons.data["CONTROLS"][key] = value;
	}

	// Token: 0x06000086 RID: 134 RVA: 0x0000AA60 File Offset: 0x00008C60
	public virtual void createbuttons()
	{
		float y = this.controlmenulength;
		Vector2 sizeDelta = ((RectTransform)this.bindingbuttonparent.GetComponent(typeof(RectTransform))).sizeDelta;
		float num = sizeDelta.y = y;
		Vector2 vector = ((RectTransform)this.bindingbuttonparent.GetComponent(typeof(RectTransform))).sizeDelta = sizeDelta;
		for (int i = 0; i < this.inputmanager.keycodes.Length; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabbutton);
			((BindButtonScript)gameObject.GetComponent(typeof(BindButtonScript))).myname = this.inputmanager.actionnames[i];
			((RectTransform)gameObject.GetComponent(typeof(RectTransform))).anchoredPosition3D = new Vector3((float)0, this.bindingbuttonspacing * (float)i + this.bindingbuttoninitialoffset, (float)0);
			((Text)((BindButtonScript)gameObject.GetComponent(typeof(BindButtonScript))).mytext.GetComponent(typeof(Text))).text = this.inputmanager.actionnames[i];
			gameObject.transform.SetParent(this.bindingbuttonparent.transform, false);
		}
	}

	// Token: 0x06000087 RID: 135 RVA: 0x0000ABAC File Offset: 0x00008DAC
	public virtual KeyCode ButtonToKeycode(int button)
	{
		KeyCode keyCode = KeyCode.Numlock;
		return (button != 0) ? ((button != 1) ? ((button != 2) ? ((button != 3) ? ((button != 4) ? ((button != 5) ? ((button != 6) ? keyCode : KeyCode.Mouse6) : KeyCode.Mouse5) : KeyCode.Mouse4) : KeyCode.Mouse3) : KeyCode.Mouse2) : KeyCode.Mouse1) : KeyCode.Mouse0;
	}

	// Token: 0x06000088 RID: 136 RVA: 0x0000AC30 File Offset: 0x00008E30
	public virtual void Main()
	{
	}

	// Token: 0x04000165 RID: 357
	public float bindingbuttonspacing;

	// Token: 0x04000166 RID: 358
	public float bindingbuttoninitialoffset;

	// Token: 0x04000167 RID: 359
	public GameObject bindingbuttonparent;

	// Token: 0x04000168 RID: 360
	public float controlmenulength;

	// Token: 0x04000169 RID: 361
	public GameObject prefabbutton;

	// Token: 0x0400016A RID: 362
	public GameObject bindprompt;

	// Token: 0x0400016B RID: 363
	[HideInInspector]
	public KeyCode guikey;

	// Token: 0x0400016C RID: 364
	[HideInInspector]
	public int indextoassign;

	// Token: 0x0400016D RID: 365
	[HideInInspector]
	public MyInputManager inputmanager;

	// Token: 0x0400016E RID: 366
	[HideInInspector]
	public GameObject bindingsmenu;

	// Token: 0x0400016F RID: 367
	[HideInInspector]
	public FileIniDataParser parser;

	// Token: 0x04000170 RID: 368
	[HideInInspector]
	public string filename;

	// Token: 0x04000171 RID: 369
	[HideInInspector]
	public IniData data;

	// Token: 0x04000172 RID: 370
	[HideInInspector]
	public GameMenuButtonsScript gbuttons;
}
