using System;
using UnityEngine;

// Token: 0x0200001A RID: 26
[Serializable]
public class ChangePitchOverTime : MonoBehaviour
{
	// Token: 0x060000B9 RID: 185 RVA: 0x0000BC2C File Offset: 0x00009E2C
	public virtual void Start()
	{
		this.aud = (AudioSource)this.GetComponent(typeof(AudioSource));
		this.orig = this.aud.pitch;
	}

	// Token: 0x060000BA RID: 186 RVA: 0x0000BC68 File Offset: 0x00009E68
	public virtual void Update()
	{
		this.changetime -= Time.deltaTime;
		if (this.changetime < (float)0)
		{
			this.changetime = (float)0;
			this.aud.pitch = this.orig;
		}
		if (this.changetime > (float)0)
		{
			this.aud.pitch = this.aud.pitch + this.changespeed * Time.deltaTime;
			if (this.aud.pitch < this.lowest)
			{
				this.aud.pitch = this.lowest;
			}
			if (this.aud.pitch > this.highest)
			{
				this.aud.pitch = this.highest;
			}
		}
	}

	// Token: 0x060000BB RID: 187 RVA: 0x0000BD2C File Offset: 0x00009F2C
	public virtual void Main()
	{
	}

	// Token: 0x0400019F RID: 415
	public float highest;

	// Token: 0x040001A0 RID: 416
	public float lowest;

	// Token: 0x040001A1 RID: 417
	public float changespeed;

	// Token: 0x040001A2 RID: 418
	public float changetime;

	// Token: 0x040001A3 RID: 419
	[HideInInspector]
	public AudioSource aud;

	// Token: 0x040001A4 RID: 420
	[HideInInspector]
	public float orig;
}
