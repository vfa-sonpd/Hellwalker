using System;
using UnityEngine;

// Token: 0x02000096 RID: 150
[Serializable]
public class RotateConstantlyLoading : MonoBehaviour
{
	// Token: 0x060003AA RID: 938 RVA: 0x00023B0C File Offset: 0x00021D0C
	public virtual void Start()
	{
	}

	// Token: 0x060003AB RID: 939 RVA: 0x00023B10 File Offset: 0x00021D10
	public virtual void FixedUpdate()
	{
		this.transform.Rotate(new Vector3(this.amountx, this.amounty, this.amountz) * Time.fixedDeltaTime * this.rotationspeed);
	}

	// Token: 0x060003AC RID: 940 RVA: 0x00023B4C File Offset: 0x00021D4C
	public virtual void Main()
	{
	}

	// Token: 0x0400048F RID: 1167
	public float rotationspeed;

	// Token: 0x04000490 RID: 1168
	public float amountx;

	// Token: 0x04000491 RID: 1169
	public float amounty;

	// Token: 0x04000492 RID: 1170
	public float amountz;
}
