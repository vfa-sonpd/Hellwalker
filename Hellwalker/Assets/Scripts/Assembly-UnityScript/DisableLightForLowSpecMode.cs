using System;
using UnityEngine;

// Token: 0x02000030 RID: 48
[Serializable]
public class DisableLightForLowSpecMode : MonoBehaviour
{
	// Token: 0x0600012A RID: 298 RVA: 0x0000DC60 File Offset: 0x0000BE60
	public virtual void Start()
	{
        this.s = Essential.Instance.lowSpecModeScript;
		this.plight = (Light)this.GetComponent(typeof(Light));
	}

	// Token: 0x0600012B RID: 299 RVA: 0x0000DCAC File Offset: 0x0000BEAC
	public virtual void Update()
	{
		if (this.s.dolowspecmode && this.plight.enabled)
		{
			this.plight.enabled = false;
		}
		if (!this.s.dolowspecmode && !this.plight.enabled)
		{
			this.plight.enabled = true;
		}
	}

	// Token: 0x0600012C RID: 300 RVA: 0x0000DD14 File Offset: 0x0000BF14
	public virtual void Main()
	{
	}

	// Token: 0x04000202 RID: 514
	[HideInInspector]
	public LowSpecModeScript s;

	// Token: 0x04000203 RID: 515
	[HideInInspector]
	public Light plight;
}
