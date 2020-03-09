using System;
using UnityEngine;

// Token: 0x020000B3 RID: 179
[Serializable]
public class StingerScript : MonoBehaviour
{
	// Token: 0x0600043C RID: 1084 RVA: 0x00028EDC File Offset: 0x000270DC
	public virtual void Start()
	{
		this.a = (AudioSource)this.GetComponent(typeof(AudioSource));
	}

	// Token: 0x0600043D RID: 1085 RVA: 0x00028EFC File Offset: 0x000270FC
	public virtual void Update()
	{
		if (!this.a.isPlaying && this.wastriggered)
		{
			UnityEngine.Object.Destroy(this.transform.gameObject);
		}
	}

	// Token: 0x0600043E RID: 1086 RVA: 0x00028F38 File Offset: 0x00027138
	public virtual void OnTriggerEnter(Collider hit)
	{
		if (hit.transform.gameObject.layer == 10 && !this.wastriggered)
		{
			this.a.volume = this.myvolume;
			this.a.Play();
			this.wastriggered = true;
		}
	}

	// Token: 0x0600043F RID: 1087 RVA: 0x00028F8C File Offset: 0x0002718C
	public virtual void Main()
	{
	}

	// Token: 0x04000547 RID: 1351
	public float myvolume;

	// Token: 0x04000548 RID: 1352
	[HideInInspector]
	public bool wastriggered;

	// Token: 0x04000549 RID: 1353
	[HideInInspector]
	public AudioSource a;
}
