using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000026 RID: 38
[Serializable]
public class DecreaseBarOverTime : MonoBehaviour
{
	// Token: 0x060000F6 RID: 246 RVA: 0x0000CA80 File Offset: 0x0000AC80
	public virtual void Start()
	{
		if (this.setCanvasInactive != null)
		{
			this.icon = (Image)this.setCanvasInactive.GetComponent(typeof(Image));
		}
		this.bar = (Image)this.GetComponent(typeof(Image));
	}

	// Token: 0x060000F7 RID: 247 RVA: 0x0000CADC File Offset: 0x0000ACDC
	public virtual void Update()
	{
		if (this.time > this.timeLastFrame)
		{
			this.origTime = this.time;
		}
		this.time -= Time.deltaTime;
		if (this.time < (float)0)
		{
			if (this.setCanvasInactive != null)
			{
				this.icon.enabled = false;
			}
			this.time = (float)0;
		}
		else if (this.setCanvasInactive != null)
		{
			this.icon.enabled = true;
		}
		this.bar.fillAmount = this.time / this.origTime;
		if (this.time <= (float)0)
		{
			this.bar.fillAmount = (float)0;
		}
		this.timeLastFrame = this.time;
	}

	// Token: 0x060000F8 RID: 248 RVA: 0x0000CBAC File Offset: 0x0000ADAC
	public virtual void Main()
	{
	}

	// Token: 0x040001C3 RID: 451
	public float time;

	// Token: 0x040001C4 RID: 452
	public GameObject setCanvasInactive;

	// Token: 0x040001C5 RID: 453
	[HideInInspector]
	public Image icon;

	// Token: 0x040001C6 RID: 454
	[HideInInspector]
	public Image bar;

	// Token: 0x040001C7 RID: 455
	[HideInInspector]
	public float origTime;

	// Token: 0x040001C8 RID: 456
	[HideInInspector]
	public float timeLastFrame;
}
