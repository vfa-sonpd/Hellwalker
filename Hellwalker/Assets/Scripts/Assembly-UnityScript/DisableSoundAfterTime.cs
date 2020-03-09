using System;
using UnityEngine;

// Token: 0x02000031 RID: 49
[Serializable]
public class DisableSoundAfterTime : MonoBehaviour
{
	// Token: 0x0600012E RID: 302 RVA: 0x0000DD20 File Offset: 0x0000BF20
	public virtual void Start()
	{
		this.aud = (AudioSource)this.GetComponent(typeof(AudioSource));
	}

	// Token: 0x0600012F RID: 303 RVA: 0x0000DD40 File Offset: 0x0000BF40
	public virtual void Update()
	{
		this.timer -= Time.deltaTime;
		if (this.timer < (float)0)
		{
			this.aud.volume = (float)0;
		}
	}

	// Token: 0x06000130 RID: 304 RVA: 0x0000DD7C File Offset: 0x0000BF7C
	public virtual void Main()
	{
	}

	// Token: 0x04000204 RID: 516
	public float timer;

	// Token: 0x04000205 RID: 517
	[HideInInspector]
	public AudioSource aud;
}
