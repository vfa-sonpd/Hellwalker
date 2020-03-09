using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000020 RID: 32
[Serializable]
public class CrosshairColorScript : MonoBehaviour
{
	// Token: 0x060000DD RID: 221 RVA: 0x0000C7C8 File Offset: 0x0000A9C8
	public virtual void Start()
	{
		this.img = (Image)this.GetComponent(typeof(Image));
	}

	// Token: 0x060000DE RID: 222 RVA: 0x0000C7E8 File Offset: 0x0000A9E8
	public virtual void Update()
	{
		this.img.color = this.mycolor;
	}

	// Token: 0x060000DF RID: 223 RVA: 0x0000C7FC File Offset: 0x0000A9FC
	public virtual void Main()
	{
	}

	// Token: 0x040001B3 RID: 435
	public Color mycolor;

	// Token: 0x040001B4 RID: 436
	[HideInInspector]
	public Image img;
}
