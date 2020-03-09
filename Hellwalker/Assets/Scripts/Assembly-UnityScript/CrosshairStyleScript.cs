using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000022 RID: 34
[Serializable]
public class CrosshairStyleScript : MonoBehaviour
{
	// Token: 0x060000E5 RID: 229 RVA: 0x0000C8F8 File Offset: 0x0000AAF8
	public virtual void Start()
	{
		this.img = (Image)this.GetComponent(typeof(Image));
	}

	// Token: 0x060000E6 RID: 230 RVA: 0x0000C918 File Offset: 0x0000AB18
	public virtual void Update()
	{
		this.img.sprite = this.crosshairarray[this.mystyle];
	}

	// Token: 0x060000E7 RID: 231 RVA: 0x0000C934 File Offset: 0x0000AB34
	public virtual void Main()
	{
	}

	// Token: 0x040001B9 RID: 441
	public Sprite[] crosshairarray;

	// Token: 0x040001BA RID: 442
	public int mystyle;

	// Token: 0x040001BB RID: 443
	[HideInInspector]
	public Image img;
}
