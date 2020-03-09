using System;
using UnityEngine;

// Token: 0x0200002B RID: 43
[Serializable]
public class DestroyParticleOnDeath : MonoBehaviour
{
	// Token: 0x0600010B RID: 267 RVA: 0x0000CDC0 File Offset: 0x0000AFC0
	public virtual void Start()
	{
	}

	// Token: 0x0600010C RID: 268 RVA: 0x0000CDC4 File Offset: 0x0000AFC4
	public virtual void Update()
	{
		if (!((ParticleSystem)this.GetComponent(typeof(ParticleSystem))).isPlaying)
		{
			UnityEngine.Object.Destroy(this.transform.gameObject);
		}
	}

	// Token: 0x0600010D RID: 269 RVA: 0x0000CE00 File Offset: 0x0000B000
	public virtual void Main()
	{
	}
}
