using System;
using UnityEngine;

// Token: 0x020000CB RID: 203
[Serializable]
public class WeaponFlashScript : MonoBehaviour
{
	// Token: 0x060004B1 RID: 1201 RVA: 0x0002AD20 File Offset: 0x00028F20
	public virtual void Start()
	{
	}

	// Token: 0x060004B2 RID: 1202 RVA: 0x0002AD24 File Offset: 0x00028F24
	public virtual void Update()
	{
		((Light)this.GetComponent(typeof(Light))).range = this.flash;
		this.flash -= Time.deltaTime * this.flashdecrease;
		if (this.flash < (float)0)
		{
			this.flash = (float)0;
		}
	}

	// Token: 0x060004B3 RID: 1203 RVA: 0x0002AD80 File Offset: 0x00028F80
	public virtual void Main()
	{
	}

	// Token: 0x040005A2 RID: 1442
	public float flash;

	// Token: 0x040005A3 RID: 1443
	public float flashdecrease;
}
