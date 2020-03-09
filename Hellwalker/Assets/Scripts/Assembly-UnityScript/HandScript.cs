using System;
using UnityEngine;

// Token: 0x02000052 RID: 82
[Serializable]
public class HandScript : MonoBehaviour
{
	// Token: 0x0600022C RID: 556 RVA: 0x000152C4 File Offset: 0x000134C4
	public virtual void Start()
	{
	}

	// Token: 0x0600022D RID: 557 RVA: 0x000152C8 File Offset: 0x000134C8
	public virtual void Update()
	{
		if (this.followposition)
		{
			this.transform.position = this.whattofollow.transform.position;
		}
		if (this.followrotation)
		{
			this.transform.rotation = this.whattofollow.transform.rotation;
		}
	}

	// Token: 0x0600022E RID: 558 RVA: 0x00015324 File Offset: 0x00013524
	public virtual void Main()
	{
	}

	// Token: 0x04000297 RID: 663
	public bool followposition;

	// Token: 0x04000298 RID: 664
	public bool followrotation;

	// Token: 0x04000299 RID: 665
	public GameObject whattofollow;
}
