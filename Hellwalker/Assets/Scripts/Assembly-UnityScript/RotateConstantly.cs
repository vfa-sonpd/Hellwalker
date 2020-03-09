using System;
using UnityEngine;

// Token: 0x02000095 RID: 149
[Serializable]
public class RotateConstantly : MonoBehaviour
{
	// Token: 0x060003A6 RID: 934 RVA: 0x000239F8 File Offset: 0x00021BF8
	public virtual void Start()
	{
		this.rigid = (Rigidbody)this.GetComponent(typeof(Rigidbody));
		this.rotation = new Vector3(this.amountx, this.amounty, this.amountz);
		this.rotationspeed /= (float)45;
		this.rotation = this.rotation.normalized * this.rotationspeed;
		this.deltaRotation = Quaternion.Euler(this.rotation);
	}

	// Token: 0x060003A7 RID: 935 RVA: 0x00023A7C File Offset: 0x00021C7C
	public virtual void FixedUpdate()
	{
		if (this.rigid)
		{
			this.rigid.MoveRotation(this.rigid.rotation * this.deltaRotation);
		}
		else
		{
			this.transform.Rotate(new Vector3(this.amountx, this.amounty, this.amountz) * Time.fixedDeltaTime * this.rotationspeed * (float)45);
		}
	}

	// Token: 0x060003A8 RID: 936 RVA: 0x00023B00 File Offset: 0x00021D00
	public virtual void Main()
	{
	}

	// Token: 0x04000488 RID: 1160
	public float rotationspeed;

	// Token: 0x04000489 RID: 1161
	public float amountx;

	// Token: 0x0400048A RID: 1162
	public float amounty;

	// Token: 0x0400048B RID: 1163
	public float amountz;

	// Token: 0x0400048C RID: 1164
	[HideInInspector]
	public Vector3 rotation;

	// Token: 0x0400048D RID: 1165
	[HideInInspector]
	public Rigidbody rigid;

	// Token: 0x0400048E RID: 1166
	[HideInInspector]
	public Quaternion deltaRotation;
}
