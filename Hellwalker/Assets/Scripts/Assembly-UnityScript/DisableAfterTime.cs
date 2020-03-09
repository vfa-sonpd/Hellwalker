using System;
using UnityEngine;

// Token: 0x0200002D RID: 45
[Serializable]
public class DisableAfterTime : MonoBehaviour
{
	// Token: 0x06000120 RID: 288 RVA: 0x0000DBBC File Offset: 0x0000BDBC
	public virtual void Start()
	{
		this.origlifetime = this.lifetime;
	}

	// Token: 0x06000121 RID: 289 RVA: 0x0000DBCC File Offset: 0x0000BDCC
	public virtual void Update()
	{
		this.lifetime -= Time.deltaTime;
		if (this.lifetime < (float)0)
		{
			this.lifetime = this.origlifetime;
			this.transform.gameObject.active = false;
		}
	}

	// Token: 0x06000122 RID: 290 RVA: 0x0000DC18 File Offset: 0x0000BE18
	public virtual void Main()
	{
	}

	// Token: 0x040001FF RID: 511
	public float lifetime;

	// Token: 0x04000200 RID: 512
	[HideInInspector]
	public float origlifetime;
}
