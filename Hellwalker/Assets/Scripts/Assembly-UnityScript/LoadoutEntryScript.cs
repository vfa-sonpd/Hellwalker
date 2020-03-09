using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200006D RID: 109
[Serializable]
public class LoadoutEntryScript : MonoBehaviour
{
	// Token: 0x060002B1 RID: 689 RVA: 0x000194C8 File Offset: 0x000176C8
	public LoadoutEntryScript()
	{
		this.interval = 1;
	}

	// Token: 0x060002B2 RID: 690 RVA: 0x000194D8 File Offset: 0x000176D8
	public virtual void Start()
	{
		this.ammo = 0;
	}

	// Token: 0x060002B3 RID: 691 RVA: 0x000194E4 File Offset: 0x000176E4
	public virtual void Update()
	{
		((Text)this.mynumbers.GetComponent(typeof(Text))).text = this.ammo.ToString("000");
		if (this.haveweapon)
		{
			((CanvasGroup)this.GetComponent(typeof(CanvasGroup))).alpha = (float)1;
		}
		else
		{
			((CanvasGroup)this.GetComponent(typeof(CanvasGroup))).alpha = 0.5f;
		}
		if (this.matchthisstate && ((LoadoutEntryScript)this.matchthisstate.GetComponent(typeof(LoadoutEntryScript))).haveweapon)
		{
			this.haveweapon = true;
		}
	}

	// Token: 0x060002B4 RID: 692 RVA: 0x000195A8 File Offset: 0x000177A8
	public virtual void plus()
	{
		this.ammo += this.interval;
		if (this.ammo > this.max)
		{
			this.ammo = this.max;
		}
		if (this.matchthis != null)
		{
			((LoadoutEntryScript)this.matchthis.GetComponent(typeof(LoadoutEntryScript))).ammo = this.ammo;
		}
	}

	// Token: 0x060002B5 RID: 693 RVA: 0x0001961C File Offset: 0x0001781C
	public virtual void minus()
	{
		this.ammo -= this.interval;
		if (this.ammo < 0)
		{
			this.ammo = 0;
		}
		if (this.matchthis != null)
		{
			((LoadoutEntryScript)this.matchthis.GetComponent(typeof(LoadoutEntryScript))).ammo = this.ammo;
		}
	}

	// Token: 0x060002B6 RID: 694 RVA: 0x00019688 File Offset: 0x00017888
	public virtual void weaponbutton()
	{
		this.haveweapon = !this.haveweapon;
	}

	// Token: 0x060002B7 RID: 695 RVA: 0x0001969C File Offset: 0x0001789C
	public virtual void Main()
	{
	}

	// Token: 0x0400031F RID: 799
	public GameObject matchthis;

	// Token: 0x04000320 RID: 800
	public GameObject matchthisstate;

	// Token: 0x04000321 RID: 801
	public GameObject mynumbers;

	// Token: 0x04000322 RID: 802
	public GameObject myname;

	// Token: 0x04000323 RID: 803
	public int max;

	// Token: 0x04000324 RID: 804
	public int interval;

	// Token: 0x04000325 RID: 805
	[HideInInspector]
	public int ammo;

	// Token: 0x04000326 RID: 806
	[HideInInspector]
	public bool haveweapon;
}
