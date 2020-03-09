using System;
using UnityEngine;

// Token: 0x020000A4 RID: 164
[Serializable]
public class SetFogColor : MonoBehaviour
{
	// Token: 0x060003EE RID: 1006 RVA: 0x000273FC File Offset: 0x000255FC
	public virtual void Start()
	{
	}

	// Token: 0x060003EF RID: 1007 RVA: 0x00027400 File Offset: 0x00025600
	public virtual void Update()
	{
	}

	// Token: 0x060003F0 RID: 1008 RVA: 0x00027404 File Offset: 0x00025604
	public virtual void OnTriggerEnter(Collider hit)
	{
		if (hit.transform.gameObject.layer == 10)
		{
			RenderSettings.fogColor = this.color;
			RenderSettings.fogEndDistance = this.end;
			RenderSettings.fogStartDistance = this.start;
		}
	}

	// Token: 0x060003F1 RID: 1009 RVA: 0x0002744C File Offset: 0x0002564C
	public virtual void Main()
	{
	}

	// Token: 0x04000507 RID: 1287
	public Color color;

	// Token: 0x04000508 RID: 1288
	public float end;

	// Token: 0x04000509 RID: 1289
	public float start;
}
