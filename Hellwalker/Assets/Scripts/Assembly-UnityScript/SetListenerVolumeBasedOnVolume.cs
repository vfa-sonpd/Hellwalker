using System;
using UnityEngine;

// Token: 0x020000A6 RID: 166
[Serializable]
public class SetListenerVolumeBasedOnVolume : MonoBehaviour
{
	// Token: 0x060003F7 RID: 1015 RVA: 0x000274E8 File Offset: 0x000256E8
	public virtual void Start()
	{
		this.aud = (AudioListener)this.GetComponent(typeof(AudioListener));
		this.p = GameObject.Find("PERSIST");
		this.origvolume = (float)1;
	}

	// Token: 0x060003F8 RID: 1016 RVA: 0x00027520 File Offset: 0x00025720
	public virtual void Update()
	{
		if (this.p)
		{
			AudioListener.volume = this.origvolume * ((PersistScript)this.p.GetComponent(typeof(PersistScript))).mavolume;
		}
	}

	// Token: 0x060003F9 RID: 1017 RVA: 0x00027560 File Offset: 0x00025760
	public virtual void Main()
	{
	}

	// Token: 0x0400050C RID: 1292
	[HideInInspector]
	public AudioListener aud;

	// Token: 0x0400050D RID: 1293
	[HideInInspector]
	public GameObject p;

	// Token: 0x0400050E RID: 1294
	[HideInInspector]
	public float origvolume;
}
