using System;
using UnityEngine;

// Token: 0x020000A2 RID: 162
[Serializable]
public class SetAudioSourceVolumeScript : MonoBehaviour
{
	// Token: 0x060003E6 RID: 998 RVA: 0x00027300 File Offset: 0x00025500
	public virtual void Start()
	{
		this.aud = (AudioSource)this.GetComponent(typeof(AudioSource));
		this.p = GameObject.Find("PERSIST");
		this.origvolume = this.aud.volume;
	}

	// Token: 0x060003E7 RID: 999 RVA: 0x0002734C File Offset: 0x0002554C
	public virtual void Update()
	{
		if (this.p)
		{
			if (!this.music)
			{
				this.aud.volume = this.origvolume * ((PersistScript)this.p.GetComponent(typeof(PersistScript))).mavolume;
			}
			else
			{
				this.aud.volume = this.origvolume * ((PersistScript)this.p.GetComponent(typeof(PersistScript))).muvolume;
			}
		}
	}

	// Token: 0x060003E8 RID: 1000 RVA: 0x000273DC File Offset: 0x000255DC
	public virtual void Main()
	{
	}

	// Token: 0x04000503 RID: 1283
	public bool music;

	// Token: 0x04000504 RID: 1284
	[HideInInspector]
	public AudioSource aud;

	// Token: 0x04000505 RID: 1285
	[HideInInspector]
	public GameObject p;

	// Token: 0x04000506 RID: 1286
	[HideInInspector]
	public float origvolume;
}
