using System;
using UnityEngine;

// Token: 0x0200004E RID: 78
[Serializable]
public class FreezePlayerScript : MonoBehaviour
{
	// Token: 0x060001BB RID: 443 RVA: 0x00010B54 File Offset: 0x0000ED54
	public virtual void Start()
	{
	}

	// Token: 0x060001BC RID: 444 RVA: 0x00010B58 File Offset: 0x0000ED58
	public virtual void Update()
	{
	}

	// Token: 0x060001BD RID: 445 RVA: 0x00010B5C File Offset: 0x0000ED5C
	public virtual void OnTriggerEnter(Collider hit)
	{
		if (hit.transform.gameObject.layer == 10)
		{
			((MyControllerScript)GameObject.Find("Player").GetComponent(typeof(MyControllerScript))).freezetimer = this.freezetime;
			UnityEngine.Object.Destroy(this.transform.gameObject);
		}
	}

	// Token: 0x060001BE RID: 446 RVA: 0x00010BBC File Offset: 0x0000EDBC
	public virtual void Main()
	{
	}

	// Token: 0x04000279 RID: 633
	public float freezetime;
}
