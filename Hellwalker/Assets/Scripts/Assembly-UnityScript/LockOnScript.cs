using System;
using UnityEngine;

// Token: 0x0200006F RID: 111
[Serializable]
public class LockOnScript : MonoBehaviour
{
	// Token: 0x060002BF RID: 703 RVA: 0x00019AAC File Offset: 0x00017CAC
	public virtual void Start()
	{
	}

	// Token: 0x060002C0 RID: 704 RVA: 0x00019AB0 File Offset: 0x00017CB0
	public virtual void Update()
	{
		this.dolock();
	}

	// Token: 0x060002C1 RID: 705 RVA: 0x00019AB8 File Offset: 0x00017CB8
	public virtual void dolock()
	{
		RaycastHit raycastHit = default(RaycastHit);
		if (Physics.SphereCast(this.cam.transform.position, this.lockonamount, this.cam.transform.forward, out raycastHit, this.lockondist, this.locklayers))
		{
			this.transform.LookAt(raycastHit.transform);
		}
		else
		{
			this.transform.localEulerAngles = new Vector3((float)0, (float)0, (float)0);
		}
	}

	// Token: 0x060002C2 RID: 706 RVA: 0x00019B40 File Offset: 0x00017D40
	public virtual void Main()
	{
	}

	// Token: 0x04000333 RID: 819
	public LayerMask locklayers;

	// Token: 0x04000334 RID: 820
	public float lockonamount;

	// Token: 0x04000335 RID: 821
	public float lockondist;

	// Token: 0x04000336 RID: 822
	public GameObject cam;
}
