using System;
using UnityEngine;

// Token: 0x0200000D RID: 13
[Serializable]
public class AuthenticFlashlightScript : MonoBehaviour
{
	// Token: 0x06000053 RID: 83 RVA: 0x00007A54 File Offset: 0x00005C54
	public virtual void Start()
	{
		this.plr = GameObject.Find("MainCamera");
		this.r = (Rigidbody)this.GetComponent(typeof(Rigidbody));
	}

	// Token: 0x06000054 RID: 84 RVA: 0x00007A84 File Offset: 0x00005C84
	public virtual void Update()
	{
		RaycastHit raycastHit = default(RaycastHit);
		if (Physics.Raycast(this.plr.transform.position, this.plr.transform.forward, out raycastHit, this.maxdistance, this.lightlayers))
		{
			Vector3 b = default(Vector3);
			b = raycastHit.normal * (float)1;
			Vector3 a = raycastHit.point + b;
			Vector3 velocity = (a - this.transform.position) * (this.updatespeed * Time.deltaTime);
			this.r.velocity = velocity;
		}
		this.lastrotation = this.plr.transform.localEulerAngles;
	}

	// Token: 0x06000055 RID: 85 RVA: 0x00007B40 File Offset: 0x00005D40
	public virtual void Main()
	{
	}

	// Token: 0x040000DB RID: 219
	[HideInInspector]
	public GameObject plr;

	// Token: 0x040000DC RID: 220
	[HideInInspector]
	public Rigidbody r;

	// Token: 0x040000DD RID: 221
	[HideInInspector]
	public Vector3 lastrotation;

	// Token: 0x040000DE RID: 222
	public LayerMask lightlayers;

	// Token: 0x040000DF RID: 223
	public float maxdistance;

	// Token: 0x040000E0 RID: 224
	public float updatespeed;
}
