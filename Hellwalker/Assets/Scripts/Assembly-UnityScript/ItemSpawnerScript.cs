using System;
using UnityEngine;

// Token: 0x0200005E RID: 94
[Serializable]
public class ItemSpawnerScript : MonoBehaviour
{
	// Token: 0x0600026F RID: 623 RVA: 0x00017648 File Offset: 0x00015848
	public virtual void Start()
	{
		this.itemispawned = null;
	}

	// Token: 0x06000270 RID: 624 RVA: 0x00017654 File Offset: 0x00015854
	public virtual void Update()
	{
		this.spawnbegindelay -= Time.deltaTime;
		if (this.spawnbegindelay <= (float)0)
		{
			if (this.itemispawned == null)
			{
				this.spawntimer += Time.deltaTime;
			}
			if (this.spawntimer > this.spawntime)
			{
				this.spawnenemy();
				this.spawntimer = (float)0;
			}
			this.spawnbegindelay = (float)0;
		}
	}

	// Token: 0x06000271 RID: 625 RVA: 0x000176CC File Offset: 0x000158CC
	public virtual void spawnenemy()
	{
		int num = UnityEngine.Random.Range(0, this.enemies.Length);
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.enemies[num], this.transform.position, this.enemies[num].transform.rotation);
		GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.spawnparts, this.transform.position, Quaternion.identity);
		this.itemispawned = gameObject;
		if (this.onlyspawnonce)
		{
			UnityEngine.Object.Destroy(this.transform.gameObject);
		}
	}

	// Token: 0x06000272 RID: 626 RVA: 0x00017754 File Offset: 0x00015954
	public virtual void Main()
	{
	}

	// Token: 0x040002D0 RID: 720
	public GameObject[] enemies;

	// Token: 0x040002D1 RID: 721
	public GameObject spawnparts;

	// Token: 0x040002D2 RID: 722
	public float spawntime;

	// Token: 0x040002D3 RID: 723
	public bool onlyspawnonce;

	// Token: 0x040002D4 RID: 724
	public float spawnbegindelay;

	// Token: 0x040002D5 RID: 725
	[HideInInspector]
	public GameObject itemispawned;

	// Token: 0x040002D6 RID: 726
	[HideInInspector]
	public float spawntimer;
}
