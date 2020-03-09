using System;
using UnityEngine;

// Token: 0x020000D0 RID: 208
[Serializable]
public class visceracookscript : MonoBehaviour
{
	// Token: 0x060004C6 RID: 1222 RVA: 0x0002B104 File Offset: 0x00029304
	public virtual void Start()
	{
		this.cooktimer = (float)2;
		this.d = (DestructibleObjectScript)this.GetComponent(typeof(DestructibleObjectScript));
	}

	// Token: 0x060004C7 RID: 1223 RVA: 0x0002B12C File Offset: 0x0002932C
	public virtual void Update()
	{
		if (this.d.onfire)
		{
			this.cooktimer -= Time.deltaTime;
			if (this.cooktimer < (float)0)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.cookedgib, this.transform.position, Quaternion.identity);
				UnityEngine.Object.Destroy(this.transform.gameObject);
				UnityEngine.Object.Destroy(this.d.firesystem);
			}
		}
	}

	// Token: 0x060004C8 RID: 1224 RVA: 0x0002B1A4 File Offset: 0x000293A4
	public virtual void Main()
	{
	}

	// Token: 0x040005B4 RID: 1460
	public GameObject cookedgib;

	// Token: 0x040005B5 RID: 1461
	[HideInInspector]
	public float cooktimer;

	// Token: 0x040005B6 RID: 1462
	[HideInInspector]
	public DestructibleObjectScript d;
}
