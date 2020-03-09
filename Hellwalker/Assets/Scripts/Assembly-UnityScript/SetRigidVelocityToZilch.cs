using System;
using UnityEngine;

// Token: 0x020000AA RID: 170
[Serializable]
public class SetRigidVelocityToZilch : MonoBehaviour
{
	// Token: 0x06000408 RID: 1032 RVA: 0x000276D0 File Offset: 0x000258D0
	public virtual void Start()
	{
		this.rigid = (Rigidbody)this.GetComponent(typeof(Rigidbody));
	}

	// Token: 0x06000409 RID: 1033 RVA: 0x000276F0 File Offset: 0x000258F0
	public virtual void Update()
	{
		if (this.rigid.velocity.magnitude > (float)0)
		{
			this.rigid.velocity = new Vector3((float)0, (float)0, (float)0);
		}
	}

	// Token: 0x0600040A RID: 1034 RVA: 0x00027730 File Offset: 0x00025930
	public virtual void Main()
	{
	}

	// Token: 0x04000515 RID: 1301
	[HideInInspector]
	public Rigidbody rigid;
}
