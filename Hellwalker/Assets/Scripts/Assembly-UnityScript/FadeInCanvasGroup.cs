using System;
using UnityEngine;

// Token: 0x02000042 RID: 66
[Serializable]
public class FadeInCanvasGroup : MonoBehaviour
{
	// Token: 0x06000186 RID: 390 RVA: 0x0000FED0 File Offset: 0x0000E0D0
	public virtual void Start()
	{
		this.grp = (CanvasGroup)this.GetComponent(typeof(CanvasGroup));
		this.grp.alpha = (float)0;
	}

	// Token: 0x06000187 RID: 391 RVA: 0x0000FF08 File Offset: 0x0000E108
	public virtual void Update()
	{
		this.grp.alpha = this.grp.alpha + Time.unscaledDeltaTime * this.fadespeed;
		if (this.grp.alpha > (float)1)
		{
			this.grp.alpha = (float)1;
		}
	}

	// Token: 0x06000188 RID: 392 RVA: 0x0000FF58 File Offset: 0x0000E158
	public virtual void Main()
	{
	}

	// Token: 0x04000259 RID: 601
	public float fadespeed;

	// Token: 0x0400025A RID: 602
	[HideInInspector]
	public CanvasGroup grp;
}
