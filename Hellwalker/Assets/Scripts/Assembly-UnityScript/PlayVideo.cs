using System;
using UnityEngine;
using UnityEngine.Video;

// Token: 0x0200008D RID: 141
[Serializable]
public class PlayVideo : MonoBehaviour
{
	// Token: 0x06000376 RID: 886 RVA: 0x00020278 File Offset: 0x0001E478
	public virtual void Start()
	{
		this.movtex.Play();
	}

	// Token: 0x06000377 RID: 887 RVA: 0x00020288 File Offset: 0x0001E488
	public virtual void Update()
	{
	}

	// Token: 0x06000378 RID: 888 RVA: 0x0002028C File Offset: 0x0001E48C
	public virtual void Main()
	{
	}

	// Token: 0x0400045C RID: 1116
	public VideoPlayer movtex;
}
