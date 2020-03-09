using System;
using UnityEngine;

// Token: 0x02000097 RID: 151
[Serializable]
public class RotationPulseScript : MonoBehaviour
{
	// Token: 0x060003AE RID: 942 RVA: 0x00023B58 File Offset: 0x00021D58
	public virtual void Start()
	{
		this.rot = (RotateConstantlyLoading)this.GetComponent(typeof(RotateConstantlyLoading));
	}

	// Token: 0x060003AF RID: 943 RVA: 0x00023B78 File Offset: 0x00021D78
	public virtual void Update()
	{
		this.timer += Time.deltaTime * (float)(1 + 60 / this.bpm);
		if (this.timer > (float)1)
		{
			this.rot.rotationspeed = this.maxrot;
			this.timer = (float)0;
		}
		this.rot.rotationspeed = this.rot.rotationspeed / ((float)1 + Time.deltaTime * this.rotdecrease);
		if (this.rot.rotationspeed < (float)0)
		{
			this.rot.rotationspeed = (float)0;
		}
	}

	// Token: 0x060003B0 RID: 944 RVA: 0x00023C10 File Offset: 0x00021E10
	public virtual void Main()
	{
	}

	// Token: 0x04000493 RID: 1171
	public int bpm;

	// Token: 0x04000494 RID: 1172
	public float maxrot;

	// Token: 0x04000495 RID: 1173
	public float rotdecrease;

	// Token: 0x04000496 RID: 1174
	[HideInInspector]
	public float timer;

	// Token: 0x04000497 RID: 1175
	[HideInInspector]
	public RotateConstantlyLoading rot;
}
