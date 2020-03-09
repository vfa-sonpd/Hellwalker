using System;
using UnityEngine;

// Token: 0x02000010 RID: 16
[Serializable]
public class BerserkScript : MonoBehaviour
{
	// Token: 0x06000071 RID: 113 RVA: 0x0000A284 File Offset: 0x00008484
	public virtual void Start()
	{
	}

	// Token: 0x06000072 RID: 114 RVA: 0x0000A288 File Offset: 0x00008488
	public virtual void Update()
	{
	}

	// Token: 0x06000073 RID: 115 RVA: 0x0000A28C File Offset: 0x0000848C
	public virtual void OnTriggerEnter(Collider hit)
	{
		BasicAIScript basicAIScript = (BasicAIScript)hit.GetComponent(typeof(BasicAIScript));
		if (basicAIScript)
		{
			basicAIScript.berserkenemy = true;
			basicAIScript.MyTarget = basicAIScript.FindClosestEnemy(hit.transform.gameObject);
		}
	}

	// Token: 0x06000074 RID: 116 RVA: 0x0000A2D8 File Offset: 0x000084D8
	public virtual void Main()
	{
	}
}
