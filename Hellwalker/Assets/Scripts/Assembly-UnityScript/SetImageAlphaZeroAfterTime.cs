using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000A5 RID: 165
[Serializable]
public class SetImageAlphaZeroAfterTime : MonoBehaviour
{
	// Token: 0x060003F3 RID: 1011 RVA: 0x00027458 File Offset: 0x00025658
	public virtual void Start()
	{
		this.im = (Image)this.GetComponent(typeof(Image));
	}

	// Token: 0x060003F4 RID: 1012 RVA: 0x00027478 File Offset: 0x00025678
	public virtual void Update()
	{
		this.mytime -= Time.deltaTime;
		if (this.mytime < (float)0)
		{
			this.mytime = (float)0;
			int num = 0;
			Color color = this.im.color;
			float num2 = color.a = (float)num;
			Color color2 = this.im.color = color;
		}
	}

	// Token: 0x060003F5 RID: 1013 RVA: 0x000274DC File Offset: 0x000256DC
	public virtual void Main()
	{
	}

	// Token: 0x0400050A RID: 1290
	public float mytime;

	// Token: 0x0400050B RID: 1291
	[HideInInspector]
	public Image im;
}
