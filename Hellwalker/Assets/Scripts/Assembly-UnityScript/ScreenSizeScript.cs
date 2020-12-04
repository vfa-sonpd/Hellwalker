using System;
using UnityEngine;

// Token: 0x0200009E RID: 158
[Serializable]
public class ScreenSizeScript : MonoBehaviour
{
	// Token: 0x060003D1 RID: 977 RVA: 0x0002468C File Offset: 0x0002288C
	public virtual void Awake()
	{
        try
        {
            this.togglescript = (ToggleUIScript)GameObject.Find("HUDObjects").GetComponent(typeof(ToggleUIScript));
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
	}

	// Token: 0x060003D2 RID: 978 RVA: 0x000246C0 File Offset: 0x000228C0
	public virtual void Start()
	{
		this.maincam = (Camera)GameObject.Find("MainCamera").GetComponent(typeof(Camera));
		this.weaponcam = (Camera)GameObject.Find("WeaponCam").GetComponent(typeof(Camera));
		this.skycam = (Camera)GameObject.Find("SkyCamera").GetComponent(typeof(Camera));
		this.can = (Canvas)GameObject.Find("Canvas").GetComponent(typeof(Canvas));
	}

	// Token: 0x060003D3 RID: 979 RVA: 0x00024760 File Offset: 0x00022960
	public virtual void Update()
	{
		if (!this.didfirstrefresh)
		{
			this.sethudstuff();
			this.didfirstrefresh = true;
		}
		if (Input.GetButtonDown("screensizeup"))
		{
			this.currentsize += 0.1f;
			if (this.currentsize > (float)1)
			{
				this.huddetail++;
				if (this.huddetail > 4)
				{
					this.huddetail = 4;
				}
			}
			this.sethudstuff();
		}
		if (Input.GetButtonDown("screensizedown"))
		{
			if (this.huddetail <= 0)
			{
				this.currentsize -= 0.1f;
				this.huddetail = 0;
			}
			else
			{
				this.huddetail--;
			}
			this.sethudstuff();
		}
		if (this.currentsize < 0.2f)
		{
			this.currentsize = 0.2f;
		}
		if (this.currentsize > (float)1)
		{
			this.currentsize = (float)1;
		}
		this.maincam.rect = new Rect(0.5f - this.currentsize / (float)2, 0.5f - this.currentsize / (float)2, this.currentsize, this.currentsize);
		this.skycam.rect = new Rect(0.5f - this.currentsize / (float)2, 0.5f - this.currentsize / (float)2, this.currentsize, this.currentsize);
		this.weaponcam.rect = new Rect(0.5f - this.currentsize / (float)2, 0.5f - this.currentsize / (float)2, this.currentsize, this.currentsize);
	}

	// Token: 0x060003D4 RID: 980 RVA: 0x00024908 File Offset: 0x00022B08
	public virtual void sethudstuff()
	{
        if(this.togglescript == null)
        {
            return;
        }

		if (this.huddetail == 0)
		{
			this.togglescript.hidestuff(true);
			this.togglescript.hidespecificstuff(34, false);
			this.togglescript.hidespecificstuff(35, false);
			this.togglescript.hidespecificstuff(36, false);
		}
		if (this.huddetail == 1)
		{
			this.togglescript.hidestuff(true);
			this.togglescript.hidespecificstuff(20, false);
			this.togglescript.hidespecificstuff(34, false);
			this.togglescript.hidespecificstuff(35, false);
			this.togglescript.hidespecificstuff(36, false);
		}
		if (this.huddetail == 2)
		{
			this.togglescript.hidestuff(true);
			this.togglescript.hidespecificstuff(20, false);
			this.togglescript.hidespecificstuff(34, false);
			this.togglescript.hidespecificstuff(35, false);
			this.togglescript.hidespecificstuff(36, false);
			this.togglescript.hidespecificstuff(1, false);
			this.togglescript.hidespecificstuff(2, false);
			this.togglescript.hidespecificstuff(3, false);
			this.togglescript.hidespecificstuff(4, false);
			this.togglescript.hidespecificstuff(5, false);
			this.togglescript.hidespecificstuff(6, false);
			this.togglescript.hidespecificstuff(7, false);
			this.togglescript.hidespecificstuff(8, false);
			this.togglescript.hidespecificstuff(9, false);
			this.togglescript.hidespecificstuff(10, false);
			this.togglescript.hidespecificstuff(11, false);
			this.togglescript.hidespecificstuff(12, false);
			this.togglescript.hidespecificstuff(25, false);
			this.togglescript.hidespecificstuff(26, false);
			this.togglescript.hidespecificstuff(27, false);
			this.togglescript.hidespecificstuff(28, false);
			this.togglescript.hidespecificstuff(29, false);
			this.togglescript.hidespecificstuff(30, false);
			this.togglescript.hidespecificstuff(31, false);
			this.togglescript.hidespecificstuff(32, false);
			this.togglescript.hidespecificstuff(33, false);
		}
		if (this.huddetail == 3)
		{
			this.togglescript.hidestuff(false);
			this.togglescript.hidespecificstuff(34, true);
			this.togglescript.hidespecificstuff(35, true);
			this.togglescript.hidespecificstuff(36, true);
		}
		if (this.huddetail == 4)
		{
			this.togglescript.hidestuff(false);
		}
	}

	// Token: 0x060003D5 RID: 981 RVA: 0x00024B70 File Offset: 0x00022D70
	public virtual void Main()
	{
	}

	// Token: 0x040004AE RID: 1198
	public float currentsize;

	// Token: 0x040004AF RID: 1199
	public int huddetail;

	// Token: 0x040004B0 RID: 1200
	[HideInInspector]
	public Camera maincam;

	// Token: 0x040004B1 RID: 1201
	[HideInInspector]
	public Camera weaponcam;

	// Token: 0x040004B2 RID: 1202
	[HideInInspector]
	public Camera skycam;

	// Token: 0x040004B3 RID: 1203
	[HideInInspector]
	public Canvas can;

	// Token: 0x040004B4 RID: 1204
	[HideInInspector]
	public ToggleUIScript togglescript;

	// Token: 0x040004B5 RID: 1205
	[HideInInspector]
	public bool didfirstrefresh;
}
