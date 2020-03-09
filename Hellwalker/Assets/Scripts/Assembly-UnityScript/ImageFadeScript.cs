using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000056 RID: 86
[Serializable]
public class ImageFadeScript : MonoBehaviour
{
	// Token: 0x0600023E RID: 574 RVA: 0x0001588C File Offset: 0x00013A8C
	public virtual void Start()
	{
		this.r = (Image)this.GetComponent(typeof(Image));
	}

	// Token: 0x0600023F RID: 575 RVA: 0x000158AC File Offset: 0x00013AAC
	public virtual void Update()
	{
		if (this.r.color.a > (float)0)
		{
			float a = this.r.color.a - Time.deltaTime * this.fadespeed;
			Color color = this.r.color;
			float num = color.a = a;
			Color color2 = this.r.color = color;
		}
	}

	// Token: 0x06000240 RID: 576 RVA: 0x00015924 File Offset: 0x00013B24
	public virtual void Main()
	{
	}

	// Token: 0x040002AE RID: 686
	public float fadespeed;

	// Token: 0x040002AF RID: 687
	[HideInInspector]
	public Image r;
}
