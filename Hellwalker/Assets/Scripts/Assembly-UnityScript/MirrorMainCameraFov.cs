using System;
using UnityEngine;

// Token: 0x0200007B RID: 123
[Serializable]
public class MirrorMainCameraFov : MonoBehaviour
{
	// Token: 0x06000302 RID: 770 RVA: 0x0001ADD8 File Offset: 0x00018FD8
	public virtual void Start()
	{
		this.cam = (Camera)GameObject.Find("MainCamera").GetComponent(typeof(Camera));
		this.mycam = (Camera)this.GetComponent(typeof(Camera));
	}

	// Token: 0x06000303 RID: 771 RVA: 0x0001AE24 File Offset: 0x00019024
	public virtual void Update()
	{
		this.mycam.fieldOfView = this.cam.fieldOfView;
	}

	// Token: 0x06000304 RID: 772 RVA: 0x0001AE3C File Offset: 0x0001903C
	public virtual void FixedUpdate()
	{
	}

	// Token: 0x06000305 RID: 773 RVA: 0x0001AE40 File Offset: 0x00019040
	public virtual void Main()
	{
	}

	// Token: 0x0400035F RID: 863
	[HideInInspector]
	public Camera cam;

	// Token: 0x04000360 RID: 864
	[HideInInspector]
	public Camera mycam;
}
