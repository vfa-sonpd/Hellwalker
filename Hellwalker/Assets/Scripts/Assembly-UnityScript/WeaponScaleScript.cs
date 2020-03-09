using System;
using UnityEngine;

// Token: 0x020000CD RID: 205
[Serializable]
public class WeaponScaleScript : MonoBehaviour
{
	// Token: 0x060004B9 RID: 1209 RVA: 0x0002ADA0 File Offset: 0x00028FA0
	public virtual void Start()
	{
	}

	// Token: 0x060004BA RID: 1210 RVA: 0x0002ADA4 File Offset: 0x00028FA4
	public virtual void FixedUpdate()
	{
		this.transform.localScale = new Vector3(this.size, this.size, this.size);
	}

	// Token: 0x060004BB RID: 1211 RVA: 0x0002ADD4 File Offset: 0x00028FD4
	public virtual void Main()
	{
	}

	// Token: 0x040005A8 RID: 1448
	public float size;
}
