using System;
using UnityEngine;

// Token: 0x02000092 RID: 146
[Serializable]
public class PulseMaterials : MonoBehaviour
{
	// Token: 0x06000398 RID: 920 RVA: 0x0002358C File Offset: 0x0002178C
	public virtual void Start()
	{
	}

	// Token: 0x06000399 RID: 921 RVA: 0x00023590 File Offset: 0x00021790
	public virtual void Update()
	{
		this.currentEmissionAmount += Time.deltaTime * this.pulseSpeed;
		if (this.currentEmissionAmount > (float)1)
		{
			this.currentEmissionAmount = (float)1;
			this.pulseSpeed = -this.pulseSpeed;
		}
		if (this.currentEmissionAmount < (float)0)
		{
			this.currentEmissionAmount = (float)0;
			this.pulseSpeed = -this.pulseSpeed;
		}
		for (int i = 0; i < this.mats.Length; i++)
		{
			this.mats[i].SetColor("_EmissionColor", new Color(this.currentEmissionAmount, this.currentEmissionAmount, this.currentEmissionAmount, (float)1));
		}
	}

	// Token: 0x0600039A RID: 922 RVA: 0x00023644 File Offset: 0x00021844
	public virtual void Main()
	{
	}

	// Token: 0x0400047F RID: 1151
	public Material[] mats;

	// Token: 0x04000480 RID: 1152
	public float pulseSpeed;

	// Token: 0x04000481 RID: 1153
	[HideInInspector]
	public float currentEmissionAmount;
}
