using System;
using UnityEngine;

// Token: 0x02000048 RID: 72
[Serializable]
public class FireAnomolyScript : MonoBehaviour
{
	// Token: 0x060001A1 RID: 417 RVA: 0x0001044C File Offset: 0x0000E64C
	public virtual void Start()
	{
		this.firetime = UnityEngine.Random.Range((float)0, this.firedelay);
	}

	// Token: 0x060001A2 RID: 418 RVA: 0x00010464 File Offset: 0x0000E664
	public virtual void Update()
	{
		this.firetime += Time.deltaTime;
		if (this.firetime >= this.firedelay)
		{
			this.firetime = (float)0;
			this.firestate = !this.firestate;
			this.myflames.active = this.firestate;
			((DoDamageOverTime)this.GetComponent(typeof(DoDamageOverTime))).enabled = this.firestate;
			((DoDamageOverTime)this.GetComponent(typeof(DoDamageOverTime))).hurtimer = (float)0;
			((DoDamageOverTime)this.GetComponent(typeof(DoDamageOverTime))).dohurt = this.firestate;
		}
	}

	// Token: 0x060001A3 RID: 419 RVA: 0x00010518 File Offset: 0x0000E718
	public virtual void OnTriggerStay(Collider hit)
	{
		if (this.firestate)
		{
			if ((DestructibleObjectScript)hit.transform.gameObject.GetComponent(typeof(DestructibleObjectScript)))
			{
				DestructibleObjectScript destructibleObjectScript = (DestructibleObjectScript)hit.transform.gameObject.GetComponent(typeof(DestructibleObjectScript));
				if (destructibleObjectScript.allowfire)
				{
					destructibleObjectScript.onfire = true;
					destructibleObjectScript.catchfire();
				}
			}
			if ((Rigidbody)hit.transform.gameObject.GetComponent(typeof(Rigidbody)))
			{
				Rigidbody rigidbody = (Rigidbody)hit.transform.gameObject.GetComponent(typeof(Rigidbody));
				float d = 0f;
				if (rigidbody.velocity.magnitude <= (float)4)
				{
					d = (float)4;
				}
				else
				{
					d = rigidbody.velocity.magnitude;
				}
				rigidbody.velocity = Vector3.up * d;
			}
		}
	}

	// Token: 0x060001A4 RID: 420 RVA: 0x00010620 File Offset: 0x0000E820
	public virtual void Main()
	{
	}

	// Token: 0x04000268 RID: 616
	public float firedelay;

	// Token: 0x04000269 RID: 617
	public GameObject myflames;

	// Token: 0x0400026A RID: 618
	[HideInInspector]
	public bool firestate;

	// Token: 0x0400026B RID: 619
	[HideInInspector]
	public float firetime;
}
