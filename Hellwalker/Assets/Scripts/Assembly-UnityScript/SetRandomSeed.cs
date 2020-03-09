using System;
using UnityEngine;

// Token: 0x020000A9 RID: 169
[Serializable]
public class SetRandomSeed : MonoBehaviour
{
	// Token: 0x06000403 RID: 1027 RVA: 0x00027654 File Offset: 0x00025854
	public SetRandomSeed()
	{
		this.stillneedsset = true;
	}

	// Token: 0x06000404 RID: 1028 RVA: 0x00027664 File Offset: 0x00025864
	public virtual void Start()
	{
	}

	// Token: 0x06000405 RID: 1029 RVA: 0x00027668 File Offset: 0x00025868
	public virtual void Update()
	{
		this.timer -= Time.deltaTime;
		if (this.timer <= (float)0 && this.stillneedsset)
		{
			UnityEngine.Random.seed = 121093;
			UnityEngine.Random.seed = UnityEngine.Random.seed;
			this.timer = (float)0;
			this.stillneedsset = false;
		}
	}

	// Token: 0x06000406 RID: 1030 RVA: 0x000276C4 File Offset: 0x000258C4
	public virtual void Main()
	{
	}

	// Token: 0x04000513 RID: 1299
	public float timer;

	// Token: 0x04000514 RID: 1300
	[HideInInspector]
	public bool stillneedsset;
}
