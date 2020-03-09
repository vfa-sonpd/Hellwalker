using System;
using UnityEngine;

// Token: 0x02000043 RID: 67
[Serializable]
public class FadeOutCanvasGroup : MonoBehaviour
{
	// Token: 0x0600018A RID: 394 RVA: 0x0000FF64 File Offset: 0x0000E164
	public virtual void Start()
	{
		this.grp = (CanvasGroup)this.GetComponent(typeof(CanvasGroup));
		this.grp.alpha = (float)1;
	}

	// Token: 0x0600018B RID: 395 RVA: 0x0000FF9C File Offset: 0x0000E19C
	public virtual void Update()
	{
		this.waitimer -= Time.deltaTime;
		if (this.waitimer <= (float)0)
		{
			this.grp.alpha = this.grp.alpha - Time.unscaledDeltaTime * this.fadespeed;
			if (this.grp.alpha < (float)0)
			{
				this.grp.alpha = (float)0;
			}
			this.waitimer = (float)0;
		}
	}

	// Token: 0x0600018C RID: 396 RVA: 0x00010014 File Offset: 0x0000E214
	public virtual void Main()
	{
	}

	// Token: 0x0400025B RID: 603
	public float fadespeed;

	// Token: 0x0400025C RID: 604
	public float waitimer;

	// Token: 0x0400025D RID: 605
	[HideInInspector]
	public CanvasGroup grp;
}
