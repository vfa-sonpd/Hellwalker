using System;
using UnityEngine;

// Token: 0x0200004B RID: 75
[Serializable]
public class FloatingPointNumbersScript : MonoBehaviour
{
	// Token: 0x060001AF RID: 431 RVA: 0x000108D4 File Offset: 0x0000EAD4
	public virtual void Start()
	{
		this.txt = (TextMesh)this.GetComponent(typeof(TextMesh));
	}

	// Token: 0x060001B0 RID: 432 RVA: 0x000108F4 File Offset: 0x0000EAF4
	public virtual void Update()
	{
		float a = this.txt.color.a - Time.deltaTime * this.fadespeed;
		Color color = this.txt.color;
		float num = color.a = a;
		Color color2 = this.txt.color = color;
		if (this.txt.color.a <= (float)0)
		{
			UnityEngine.Object.Destroy(this.transform.gameObject);
		}
	}

	// Token: 0x060001B1 RID: 433 RVA: 0x0001097C File Offset: 0x0000EB7C
	public virtual void Main()
	{
	}

	// Token: 0x04000273 RID: 627
	public float fadespeed;

	// Token: 0x04000274 RID: 628
	[HideInInspector]
	public TextMesh txt;
}
