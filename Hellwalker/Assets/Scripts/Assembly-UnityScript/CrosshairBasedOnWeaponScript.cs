using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200001F RID: 31
[Serializable]
public class CrosshairBasedOnWeaponScript : MonoBehaviour
{
	// Token: 0x060000D9 RID: 217 RVA: 0x0000C6C4 File Offset: 0x0000A8C4
	public virtual void Start()
	{
		this.hairs = (CrosshairStyleScript)this.GetComponent(typeof(CrosshairStyleScript));
		if (GameObject.Find("WeaponAnimator"))
		{
			this.select = (SelectionScript)GameObject.Find("WeaponAnimator").GetComponent(typeof(SelectionScript));
		}
		else
		{
			this.select = null;
		}
		this.cross = (Image)this.GetComponent(typeof(Image));
	}

	// Token: 0x060000DA RID: 218 RVA: 0x0000C74C File Offset: 0x0000A94C
	public virtual void Update()
	{
		if (this.crosshairbasedonweapon)
		{
			this.hairs.enabled = false;
			if (this.select)
			{
				this.cross.sprite = this.hairs.crosshairarray[this.weaponcrosshairs[this.select.weapontogetto]];
			}
		}
		else
		{
			this.hairs.enabled = true;
		}
	}

	// Token: 0x060000DB RID: 219 RVA: 0x0000C7BC File Offset: 0x0000A9BC
	public virtual void Main()
	{
	}

	// Token: 0x040001AE RID: 430
	public bool crosshairbasedonweapon;

	// Token: 0x040001AF RID: 431
	public int[] weaponcrosshairs;

	// Token: 0x040001B0 RID: 432
	[HideInInspector]
	public CrosshairStyleScript hairs;

	// Token: 0x040001B1 RID: 433
	[HideInInspector]
	public SelectionScript select;

	// Token: 0x040001B2 RID: 434
	[HideInInspector]
	public Image cross;
}
