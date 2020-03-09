using System;
using UnityEngine;

// Token: 0x02000058 RID: 88
[Serializable]
public class InGameMenuScript : MonoBehaviour
{
	// Token: 0x06000246 RID: 582 RVA: 0x000159AC File Offset: 0x00013BAC
	public virtual void Start()
	{
		this.gscript = (GameMenuButtonsScript)this.GetComponent(typeof(GameMenuButtonsScript));
		if (GameObject.Find("Player"))
		{
			this.playerdeathstate = ((PlayerHealthManagement)GameObject.Find("Player").GetComponent(typeof(PlayerHealthManagement))).iamdead;
		}
		this.deactivatemenuitems();
	}

	// Token: 0x06000247 RID: 583 RVA: 0x00015A18 File Offset: 0x00013C18
	public virtual void Update()
	{
		if (this.isPaused)
		{
			Screen.lockCursor = false;
			Cursor.visible = false;
		}
		else
		{
			Screen.lockCursor = true;
			Cursor.visible = false;
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			this.togglemenu();
		}
	}

	// Token: 0x06000248 RID: 584 RVA: 0x00015A60 File Offset: 0x00013C60
	public virtual void deactivatemenuitems()
	{
		for (int i = 0; i < this.gscript.menus.Length; i++)
		{
			this.gscript.menus[i].active = false;
		}
		this.gscript.logo.active = false;
		this.gscript.back.active = false;
	}

	// Token: 0x06000249 RID: 585 RVA: 0x00015AC4 File Offset: 0x00013CC4
	public virtual void togglemenu()
	{
		this.isPaused = !this.isPaused;
		if (this.isPaused)
		{
			this.gscript.menus[this.mainmenu].active = true;
			this.gscript.logo.active = true;
			this.gscript.back.active = true;
			this.gscript.lookatoptions.active = this.gscript.intro;
		}
		if (!this.isPaused)
		{
			((GameMenuButtonsScript)this.GetComponent(typeof(GameMenuButtonsScript))).PressBackMainButton();
			this.deactivatemenuitems();
		}
		if (GameObject.Find("Player"))
		{
			if (this.isPaused)
			{
				Time.timeScale = (float)0;
				((MyMouseLook)GameObject.Find("MainCamera").GetComponent(typeof(MyMouseLook))).enabled = false;
				if (GameObject.Find("WeaponAnimator"))
				{
					((AttackScript)GameObject.Find("WeaponAnimator").GetComponent(typeof(AttackScript))).enabled = false;
					((SelectionScript)GameObject.Find("WeaponAnimator").GetComponent(typeof(SelectionScript))).enabled = false;
				}
			}
			else
			{
				Time.timeScale = (float)1;
				if (!this.playerdeathstate)
				{
					((MyMouseLook)GameObject.Find("MainCamera").GetComponent(typeof(MyMouseLook))).enabled = true;
					((AttackScript)GameObject.Find("WeaponAnimator").GetComponent(typeof(AttackScript))).enabled = true;
					((SelectionScript)GameObject.Find("WeaponAnimator").GetComponent(typeof(SelectionScript))).enabled = true;
				}
			}
		}
	}

	// Token: 0x0600024A RID: 586 RVA: 0x00015C94 File Offset: 0x00013E94
	public virtual void Main()
	{
	}

	// Token: 0x040002B3 RID: 691
	public GameObject menuparent;

	// Token: 0x040002B4 RID: 692
	public bool isPaused;

	// Token: 0x040002B5 RID: 693
	[HideInInspector]
	public GameMenuButtonsScript gscript;

	// Token: 0x040002B6 RID: 694
	[HideInInspector]
	public bool playerdeathstate;

	// Token: 0x040002B7 RID: 695
	[HideInInspector]
	public int mainmenu;
}
