using System;
using UnityEngine;

// Token: 0x0200002A RID: 42
[Serializable]
public class DestroyObjectAfterTime : MonoBehaviour
{
	// Token: 0x06000107 RID: 263 RVA: 0x0000CD7C File Offset: 0x0000AF7C
	public virtual void Start()
	{
	}

	// Token: 0x06000108 RID: 264 RVA: 0x0000CD80 File Offset: 0x0000AF80
	public virtual void Update()
	{
		this.lifetime -= Time.deltaTime;
		if (this.lifetime < (float)0)
		{
			UnityEngine.Object.Destroy(this.transform.gameObject);
		}
	}

	// Token: 0x06000109 RID: 265 RVA: 0x0000CDB4 File Offset: 0x0000AFB4
	public virtual void Main()
	{
	}

	// Token: 0x040001D4 RID: 468
	public float lifetime;
}
