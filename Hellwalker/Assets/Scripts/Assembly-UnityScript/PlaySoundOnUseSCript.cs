using System;
using UnityEngine;

// Token: 0x0200008C RID: 140
[Serializable]
public class PlaySoundOnUseSCript : MonoBehaviour
{
	// Token: 0x06000371 RID: 881 RVA: 0x000201F0 File Offset: 0x0001E3F0
	public virtual void Start()
	{
		this.waittimer = this.waittime;
	}

	// Token: 0x06000372 RID: 882 RVA: 0x00020200 File Offset: 0x0001E400
	public virtual void Update()
	{
		this.waittimer += Time.deltaTime;
		if (this.waittimer > this.waittime)
		{
			this.waittimer = this.waittime;
		}
	}

	// Token: 0x06000373 RID: 883 RVA: 0x00020234 File Offset: 0x0001E434
	public virtual void playsound()
	{
		if (this.waittimer >= this.waittime)
		{
			((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
			this.waittimer = (float)0;
		}
	}

	// Token: 0x06000374 RID: 884 RVA: 0x0002026C File Offset: 0x0001E46C
	public virtual void Main()
	{
	}

	// Token: 0x0400045A RID: 1114
	public float waittime;

	// Token: 0x0400045B RID: 1115
	[HideInInspector]
	public float waittimer;
}
