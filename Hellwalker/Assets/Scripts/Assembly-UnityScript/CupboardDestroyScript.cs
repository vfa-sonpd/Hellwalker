using System;
using UnityEngine;

// Token: 0x02000024 RID: 36
[Serializable]
public class CupboardDestroyScript : MonoBehaviour
{
	// Token: 0x060000ED RID: 237 RVA: 0x0000CA48 File Offset: 0x0000AC48
	public virtual void Start()
	{
	}

	// Token: 0x060000EE RID: 238 RVA: 0x0000CA4C File Offset: 0x0000AC4C
	public virtual void Update()
	{
	}

	// Token: 0x060000EF RID: 239 RVA: 0x0000CA50 File Offset: 0x0000AC50
	public virtual void OnDestroy()
	{
		UnityEngine.Object.Destroy(this.mybase);
	}

	// Token: 0x060000F0 RID: 240 RVA: 0x0000CA60 File Offset: 0x0000AC60
	public virtual void Main()
	{
	}

	// Token: 0x040001C0 RID: 448
	public GameObject mybase;
}
