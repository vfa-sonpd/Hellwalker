using System;
using UnityEngine;

// Token: 0x02000034 RID: 52
[Serializable]
public class DoDamageOverTime : MonoBehaviour
{
	// Token: 0x0600013B RID: 315 RVA: 0x0000DF94 File Offset: 0x0000C194
	public virtual void Start()
	{
		this.s = (SelectionScript)GameObject.Find("WeaponAnimator").GetComponent(typeof(SelectionScript));
	}

	// Token: 0x0600013C RID: 316 RVA: 0x0000DFC8 File Offset: 0x0000C1C8
	public virtual void Update()
	{
		if (!this.dohurt)
		{
			this.hurtimer += Time.deltaTime;
		}
		if (this.hurtimer >= this.hurtime)
		{
			this.hurtimer = (float)0;
			this.dohurt = true;
		}
	}

	// Token: 0x0600013D RID: 317 RVA: 0x0000E008 File Offset: 0x0000C208
	public virtual void OnTriggerStay(Collider hit)
	{
		if (hit.transform.gameObject.layer == 10)
		{
			bool flag = true;
			if (this.s.ignorelava && this.islava)
			{
				flag = false;
			}
			if (this.dohurt && flag)
			{
				((PlayerHealthManagement)hit.transform.gameObject.GetComponent(typeof(PlayerHealthManagement))).takedamage(this.hurtamount);
				this.dohurt = false;
			}
		}
	}

	// Token: 0x0600013E RID: 318 RVA: 0x0000E090 File Offset: 0x0000C290
	public virtual void Main()
	{
	}

	// Token: 0x0400020B RID: 523
	public float hurtamount;

	// Token: 0x0400020C RID: 524
	public float hurtime;

	// Token: 0x0400020D RID: 525
	public bool islava;

	// Token: 0x0400020E RID: 526
	[HideInInspector]
	public float hurtimer;

	// Token: 0x0400020F RID: 527
	[HideInInspector]
	public bool dohurt;

	// Token: 0x04000210 RID: 528
	[HideInInspector]
	public SelectionScript s;
}
