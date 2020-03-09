using System;
using UnityEngine;

// Token: 0x02000065 RID: 101
[Serializable]
public class LightTriggerNODE : MonoBehaviour
{
	// Token: 0x0600028F RID: 655 RVA: 0x00018E20 File Offset: 0x00017020
	public virtual void Start()
	{
	}

	// Token: 0x06000290 RID: 656 RVA: 0x00018E24 File Offset: 0x00017024
	public virtual void Update()
	{
	}

	// Token: 0x06000291 RID: 657 RVA: 0x00018E28 File Offset: 0x00017028
	public virtual void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere(this.transform.position, this.myrange);
	}

	// Token: 0x06000292 RID: 658 RVA: 0x00018E58 File Offset: 0x00017058
	public virtual void Main()
	{
	}

	// Token: 0x0400030A RID: 778
	public Color mycolor;

	// Token: 0x0400030B RID: 779
	public float myrange;

	// Token: 0x0400030C RID: 780
	public bool sun;

	// Token: 0x0400030D RID: 781
	public bool overridesun;
}
