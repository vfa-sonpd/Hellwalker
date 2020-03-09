using System;
using UnityEngine;

// Token: 0x0200003C RID: 60
[Serializable]
public class EnemyDeathSensorScript : MonoBehaviour
{
	// Token: 0x06000166 RID: 358 RVA: 0x0000F074 File Offset: 0x0000D274
	public virtual void Start()
	{
		this.teleportlocation.transform.parent = null;
	}

	// Token: 0x06000167 RID: 359 RVA: 0x0000F088 File Offset: 0x0000D288
	public virtual void Update()
	{
		if (this.checkdead())
		{
			this.waitTime -= Time.deltaTime;
			if (this.waitTime < (float)0)
			{
				GameObject.Find("Player").transform.position = this.teleportlocation.transform.position;
				((AudioSource)GameObject.Find("LaughSound").GetComponent(typeof(AudioSource))).Play();
				this.transform.gameObject.active = false;
			}
		}
	}

	// Token: 0x06000168 RID: 360 RVA: 0x0000F118 File Offset: 0x0000D318
	public virtual void OnTriggerEnter(Collider hit)
	{
		if (hit.transform.gameObject.layer == 14)
		{
			this.enemies[this.populateIndex] = hit.transform.gameObject;
			this.populateIndex++;
			if (this.populateIndex >= this.enemies.Length - 1)
			{
				this.populateIndex = this.enemies.Length - 1;
			}
		}
	}

	// Token: 0x06000169 RID: 361 RVA: 0x0000F190 File Offset: 0x0000D390
	public virtual bool checkdead()
	{
		for (int i = 0; i <= this.enemies.Length - 1; i++)
		{
			if (this.enemies[i] != null)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x0600016A RID: 362 RVA: 0x0000F1D8 File Offset: 0x0000D3D8
	public virtual void Main()
	{
	}

	// Token: 0x04000239 RID: 569
	public GameObject teleportlocation;

	// Token: 0x0400023A RID: 570
	public float waitTime;

	// Token: 0x0400023B RID: 571
	public GameObject[] enemies;

	// Token: 0x0400023C RID: 572
	[HideInInspector]
	public int populateIndex;
}
