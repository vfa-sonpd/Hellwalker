using System;
using UnityEngine;

namespace Lean
{
	// Token: 0x020001C0 RID: 448
	[RequireComponent(typeof(Rigidbody))]
	public class LeanPooledRigidbody : MonoBehaviour
	{
		// Token: 0x06000DB0 RID: 3504 RVA: 0x00022F15 File Offset: 0x00021315
		protected virtual void OnSpawn()
		{
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x00022F18 File Offset: 0x00021318
		protected virtual void OnDespawn()
		{
			Rigidbody component = base.GetComponent<Rigidbody>();
			component.velocity = Vector3.zero;
			component.angularVelocity = Vector3.zero;
		}
	}
}
