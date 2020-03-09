using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000027 RID: 39
[Serializable]
public class DecreaseUIAlphaOverTime : MonoBehaviour
{
	// Token: 0x060000FA RID: 250 RVA: 0x0000CBB8 File Offset: 0x0000ADB8
	public virtual void Start()
	{
	}

	// Token: 0x060000FB RID: 251 RVA: 0x0000CBBC File Offset: 0x0000ADBC
	public virtual void Update()
	{
		Image image = (Image)this.GetComponent(typeof(Image));
		if (image.color.a > (float)0)
		{
			float a = image.color.a - Time.deltaTime * this.decreasespeed;
			Color color = image.color;
			float num = color.a = a;
			Color color2 = image.color = color;
		}
	}

	// Token: 0x060000FC RID: 252 RVA: 0x0000CC38 File Offset: 0x0000AE38
	public virtual void Main()
	{
	}

	// Token: 0x040001C9 RID: 457
	public float decreasespeed;
}
