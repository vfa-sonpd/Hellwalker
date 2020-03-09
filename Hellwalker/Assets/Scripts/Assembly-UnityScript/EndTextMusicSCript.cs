using System;
using UnityEngine;

// Token: 0x02000038 RID: 56
[Serializable]
public class EndTextMusicSCript : MonoBehaviour
{
	// Token: 0x0600014F RID: 335 RVA: 0x0000E89C File Offset: 0x0000CA9C
	public virtual void Start()
	{
	}

	// Token: 0x06000150 RID: 336 RVA: 0x0000E8A0 File Offset: 0x0000CAA0
	public virtual void Update()
	{
		if (!((AudioSource)this.intro.GetComponent(typeof(AudioSource))).isPlaying && !this.playingloop)
		{
			((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
			this.playingloop = true;
		}
	}

	// Token: 0x06000151 RID: 337 RVA: 0x0000E900 File Offset: 0x0000CB00
	public virtual void Main()
	{
	}

	// Token: 0x0400021F RID: 543
	public GameObject intro;

	// Token: 0x04000220 RID: 544
	public bool playingloop;
}
