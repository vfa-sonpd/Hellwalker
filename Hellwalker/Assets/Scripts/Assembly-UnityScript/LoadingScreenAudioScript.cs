using System;
using UnityEngine;

// Token: 0x0200006C RID: 108
[Serializable]
public class LoadingScreenAudioScript : MonoBehaviour
{
	// Token: 0x060002AE RID: 686 RVA: 0x00019464 File Offset: 0x00017664
	public virtual void Start()
	{
		((AudioSource)this.GetComponent(typeof(AudioSource))).clip = this.clips[UnityEngine.Random.Range(0, this.clips.Length - 1)];
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
	}

	// Token: 0x060002AF RID: 687 RVA: 0x000194C0 File Offset: 0x000176C0
	public virtual void Update()
	{
	}

	// Token: 0x060002B0 RID: 688 RVA: 0x000194C4 File Offset: 0x000176C4
	public virtual void Main()
	{
	}

	// Token: 0x0400031E RID: 798
	public AudioClip[] clips;
}
