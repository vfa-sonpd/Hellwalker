using System;
using UnityEngine;

// Token: 0x02000013 RID: 19
[Serializable]
public class BlowingDustScript : MonoBehaviour
{
	// Token: 0x0600008A RID: 138 RVA: 0x0000AC3C File Offset: 0x00008E3C
	public virtual void Start()
	{
	}

	// Token: 0x0600008B RID: 139 RVA: 0x0000AC40 File Offset: 0x00008E40
	public virtual void Update()
	{
	}

	// Token: 0x0600008C RID: 140 RVA: 0x0000AC44 File Offset: 0x00008E44
	public virtual void FixedUpdate()
	{
		if (UnityEngine.Random.Range((float)0, this.windfrequency) < (float)1 && !((ParticleSystem)this.GetComponent(typeof(ParticleSystem))).isPlaying)
		{
			((ParticleSystem)this.GetComponent(typeof(ParticleSystem))).Play();
			((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		}
	}

	// Token: 0x0600008D RID: 141 RVA: 0x0000ACB8 File Offset: 0x00008EB8
	public virtual void Main()
	{
	}

	// Token: 0x04000173 RID: 371
	public float windfrequency;
}
