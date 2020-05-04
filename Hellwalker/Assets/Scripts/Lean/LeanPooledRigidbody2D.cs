using System;
using UnityEngine;

namespace Lean
{
	// Token: 0x020001C1 RID: 449
	[RequireComponent(typeof(Rigidbody2D))]
	public class LeanPooledRigidbody2D : MonoBehaviour
	{
		// Token: 0x06000DB3 RID: 3507 RVA: 0x00022F4A File Offset: 0x0002134A
		protected virtual void OnSpawn()
		{
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x00022F4C File Offset: 0x0002134C
		protected virtual void OnDespawn()
		{
			Rigidbody2D component = base.GetComponent<Rigidbody2D>();
			component.velocity = Vector2.zero;
			component.angularVelocity = 0f;
		}
	}
}
