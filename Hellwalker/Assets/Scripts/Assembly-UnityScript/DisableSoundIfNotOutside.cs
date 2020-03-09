using System;
using UnityEngine;

// Token: 0x02000032 RID: 50
[Serializable]
public class DisableSoundIfNotOutside : MonoBehaviour
{
	// Token: 0x06000132 RID: 306 RVA: 0x0000DD88 File Offset: 0x0000BF88
	public virtual void Start()
	{
		this.aud = (AudioSource)this.GetComponent(typeof(AudioSource));
		this.originalvolume = this.aud.volume;
		this.plr = GameObject.Find("Player");
	}

	// Token: 0x06000133 RID: 307 RVA: 0x0000DDD4 File Offset: 0x0000BFD4
	public virtual void Update()
	{
		if (this.checkoutside())
		{
			this.aud.volume = this.aud.volume + Time.deltaTime * this.volumefadeinmultiplier;
		}
		else
		{
			this.aud.volume = this.aud.volume - Time.deltaTime * this.volumefadeinmultiplier;
		}
		if (this.aud.volume > this.originalvolume)
		{
			this.aud.volume = this.originalvolume;
		}
		if (this.aud.volume < (float)0)
		{
			this.aud.volume = (float)0;
		}
	}

	// Token: 0x06000134 RID: 308 RVA: 0x0000DE80 File Offset: 0x0000C080
	public virtual bool checkoutside()
	{
		return !Physics.Raycast(this.plr.transform.position, Vector3.up, (float)10000, this.blockinglayers);
	}

	// Token: 0x06000135 RID: 309 RVA: 0x0000DECC File Offset: 0x0000C0CC
	public virtual void Main()
	{
	}

	// Token: 0x04000206 RID: 518
	[HideInInspector]
	public float originalvolume;

	// Token: 0x04000207 RID: 519
	[HideInInspector]
	public AudioSource aud;

	// Token: 0x04000208 RID: 520
	[HideInInspector]
	public GameObject plr;

	// Token: 0x04000209 RID: 521
	public float volumefadeinmultiplier;

	// Token: 0x0400020A RID: 522
	public LayerMask blockinglayers;
}
