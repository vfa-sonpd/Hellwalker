using System;
using UnityEngine;

// Token: 0x02000089 RID: 137
[Serializable]
public class PitcheBasedOnTimescale : MonoBehaviour
{
	// Token: 0x06000365 RID: 869 RVA: 0x00020074 File Offset: 0x0001E274
	public virtual void Start()
	{
		this.a = (AudioSource)this.GetComponent(typeof(AudioSource));
		this.originalpitch = this.a.pitch;
	}

	// Token: 0x06000366 RID: 870 RVA: 0x000200B0 File Offset: 0x0001E2B0
	public virtual void Update()
	{
		this.a.pitch = this.originalpitch * Time.timeScale;
	}

	// Token: 0x06000367 RID: 871 RVA: 0x000200CC File Offset: 0x0001E2CC
	public virtual void Main()
	{
	}

	// Token: 0x04000455 RID: 1109
	[HideInInspector]
	public float originalpitch;

	// Token: 0x04000456 RID: 1110
	[HideInInspector]
	public AudioSource a;
}
