using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000C5 RID: 197
[Serializable]
public class UIFadeScript : MonoBehaviour
{
	// Token: 0x06000496 RID: 1174 RVA: 0x0002A420 File Offset: 0x00028620
	public virtual void Start()
	{
		this.im = (Image)this.GetComponent(typeof(Image));
	}

	// Token: 0x06000497 RID: 1175 RVA: 0x0002A440 File Offset: 0x00028640
	public virtual void Update()
	{
		if (this.im.color.a < this.fadegoal)
		{
			float a = this.im.color.a + this.changespeed * Time.deltaTime;
			Color color = this.im.color;
			float num = color.a = a;
			Color color2 = this.im.color = color;
		}
		if (this.im.color.a > this.fadegoal)
		{
			float a2 = this.im.color.a - this.changespeed * Time.deltaTime;
			Color color3 = this.im.color;
			float num2 = color3.a = a2;
			Color color4 = this.im.color = color3;
		}
	}

	// Token: 0x06000498 RID: 1176 RVA: 0x0002A530 File Offset: 0x00028730
	public virtual void Main()
	{
	}

	// Token: 0x04000590 RID: 1424
	public float fadegoal;

	// Token: 0x04000591 RID: 1425
	public float changespeed;

	// Token: 0x04000592 RID: 1426
	[HideInInspector]
	public Image im;
}
