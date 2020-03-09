using System;
using UnityEngine;

// Token: 0x02000021 RID: 33
[Serializable]
public class CrosshairSizeScript : MonoBehaviour
{
	// Token: 0x060000E1 RID: 225 RVA: 0x0000C808 File Offset: 0x0000AA08
	public virtual void Start()
	{
		this.plr = GameObject.Find("Player");
	}

	// Token: 0x060000E2 RID: 226 RVA: 0x0000C81C File Offset: 0x0000AA1C
	public virtual void Update()
	{
		if (this.plus > (float)0)
		{
			this.plus -= Time.deltaTime * this.sizetime;
		}
		if (this.plus < (float)0)
		{
			this.plus = (float)0;
		}
		float num = (float)1;
		float num2 = this.plus;
		if (this.plr)
		{
			num = ((ScreenSizeScript)this.plr.GetComponent(typeof(ScreenSizeScript))).currentsize * this.menusize;
			num2 = this.plus * ((ScreenSizeScript)this.plr.GetComponent(typeof(ScreenSizeScript))).currentsize;
		}
		this.transform.localScale = new Vector3(num, num, num) + new Vector3(num2, num2, num2);
	}

	// Token: 0x060000E3 RID: 227 RVA: 0x0000C8EC File Offset: 0x0000AAEC
	public virtual void Main()
	{
	}

	// Token: 0x040001B5 RID: 437
	public float plus;

	// Token: 0x040001B6 RID: 438
	public float sizetime;

	// Token: 0x040001B7 RID: 439
	public float menusize;

	// Token: 0x040001B8 RID: 440
	[HideInInspector]
	public GameObject plr;
}
