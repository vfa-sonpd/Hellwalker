using System;
using UnityEngine;

// Token: 0x02000009 RID: 9
[Serializable]
public class AnimatedRagdollScript : MonoBehaviour
{
	// Token: 0x06000037 RID: 55 RVA: 0x00004058 File Offset: 0x00002258
	public virtual void Start()
	{
		this.dest = (DestructibleObjectScript)this.GetComponent(typeof(DestructibleObjectScript));
		this.r = (Rigidbody)this.GetComponent(typeof(Rigidbody));
	}

	// Token: 0x06000038 RID: 56 RVA: 0x0000409C File Offset: 0x0000229C
	public virtual void Update()
	{
		this.dest.dampen = true;
		this.switchtime -= Time.deltaTime;
		if (this.switchtime <= (float)0)
		{
			this.switchtime = (float)0;
			this.transform.gameObject.layer = 23;
		}
		this.freezetimer -= Time.deltaTime;
		if (this.freezetimer <= (float)0)
		{
			this.freezetimer = (float)0;
		}
	}

	// Token: 0x06000039 RID: 57 RVA: 0x00004118 File Offset: 0x00002318
	public virtual void OnCollisionStay()
	{
		if (this.freezetimer == (float)0)
		{
		}
	}

	// Token: 0x0600003A RID: 58 RVA: 0x00004128 File Offset: 0x00002328
	public virtual void Main()
	{
	}

	// Token: 0x0400003C RID: 60
	public float switchtime;

	// Token: 0x0400003D RID: 61
	public float freezetimer;

	// Token: 0x0400003E RID: 62
	[HideInInspector]
	public DestructibleObjectScript dest;

	// Token: 0x0400003F RID: 63
	[HideInInspector]
	public Rigidbody r;
}
