using System;
using UnityEngine;

// Token: 0x020000A7 RID: 167
[Serializable]
public class SetObjectInactiveAfterTime : MonoBehaviour
{
	// Token: 0x060003FB RID: 1019 RVA: 0x0002756C File Offset: 0x0002576C
	public virtual void Start()
	{
	}

	// Token: 0x060003FC RID: 1020 RVA: 0x00027570 File Offset: 0x00025770
	public virtual void Update()
	{
		if (this.transform.gameObject.active)
		{
			this.activetimer += Time.deltaTime;
			if (this.activetimer >= this.activetime)
			{
				this.activetimer = (float)0;
				this.transform.gameObject.active = false;
			}
		}
	}

	// Token: 0x060003FD RID: 1021 RVA: 0x000275D0 File Offset: 0x000257D0
	public virtual void Main()
	{
	}

	// Token: 0x0400050F RID: 1295
	[HideInInspector]
	public float activetimer;

	// Token: 0x04000510 RID: 1296
	public float activetime;
}
