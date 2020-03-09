using System;
using UnityEngine;

// Token: 0x02000057 RID: 87
[Serializable]
public class ImagePulseScript : MonoBehaviour
{
	// Token: 0x06000242 RID: 578 RVA: 0x00015930 File Offset: 0x00013B30
	public virtual void Start()
	{
		this.img = (CanvasGroup)this.GetComponent(typeof(CanvasGroup));
	}

	// Token: 0x06000243 RID: 579 RVA: 0x00015950 File Offset: 0x00013B50
	public virtual void Update()
	{
		this.timer += Time.deltaTime * (float)(1 + 60 / this.bpm);
		if (this.timer > (float)1)
		{
			this.img.alpha = (float)1;
			this.timer = (float)0;
		}
	}

	// Token: 0x06000244 RID: 580 RVA: 0x000159A0 File Offset: 0x00013BA0
	public virtual void Main()
	{
	}

	// Token: 0x040002B0 RID: 688
	public int bpm;

	// Token: 0x040002B1 RID: 689
	[HideInInspector]
	public float timer;

	// Token: 0x040002B2 RID: 690
	[HideInInspector]
	public CanvasGroup img;
}
