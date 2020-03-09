using System;
using UnityEngine;

// Token: 0x02000006 RID: 6
[Serializable]
public class AlertTriggerScript : MonoBehaviour
{
	// Token: 0x06000026 RID: 38 RVA: 0x00003BFC File Offset: 0x00001DFC
	public virtual void Start()
	{
	}

	// Token: 0x06000027 RID: 39 RVA: 0x00003C00 File Offset: 0x00001E00
	public virtual void Update()
	{
	}

	// Token: 0x06000028 RID: 40 RVA: 0x00003C04 File Offset: 0x00001E04
	public virtual void OnTriggerEnter(Collider hit)
	{
		if (hit.transform.gameObject.layer == 10)
		{
			this.activate();
			UnityEngine.Object.Destroy(this.transform.gameObject);
		}
	}

	// Token: 0x06000029 RID: 41 RVA: 0x00003C40 File Offset: 0x00001E40
	public virtual void activate()
	{
		for (int i = 0; i < this.enemies.Length; i++)
		{
			if (this.enemies[i])
			{
				BasicAIScript basicAIScript = (BasicAIScript)this.enemies[i].GetComponent(typeof(BasicAIScript));
				basicAIScript.AMAWAKE = true;
				basicAIScript.awakestuff();
			}
		}
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00003CA8 File Offset: 0x00001EA8
	public virtual void Main()
	{
	}

	// Token: 0x0400002F RID: 47
	public GameObject[] enemies;
}
