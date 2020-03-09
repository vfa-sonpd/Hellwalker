using System;
using UnityEngine;

// Token: 0x0200003E RID: 62
[Serializable]
public class EnemySpawnerScript : MonoBehaviour
{
	// Token: 0x06000170 RID: 368 RVA: 0x0000F368 File Offset: 0x0000D568
	public virtual void Start()
	{
	}

	// Token: 0x06000171 RID: 369 RVA: 0x0000F36C File Offset: 0x0000D56C
	public virtual void Update()
	{
		this.spawnbegindelay -= Time.deltaTime;
		if (this.spawnbegindelay <= (float)0)
		{
			this.spawntimer += Time.deltaTime;
			if (this.spawntimer > this.spawntime)
			{
				this.spawnenemy();
				this.spawntimer = (float)0;
			}
			this.spawnbegindelay = (float)0;
		}
	}

	// Token: 0x06000172 RID: 370 RVA: 0x0000F3D4 File Offset: 0x0000D5D4
	public virtual void spawnenemy()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.enemies[UnityEngine.Random.Range(0, this.enemies.Length)], this.transform.position, Quaternion.identity);
		if ((BasicAIScript)gameObject.GetComponent(typeof(BasicAIScript)))
		{
			((BasicAIScript)gameObject.GetComponent(typeof(BasicAIScript))).AMAWAKE = true;
			((AudioSource)gameObject.GetComponent(typeof(AudioSource))).clip = ((BasicAIScript)gameObject.GetComponent(typeof(BasicAIScript))).alertsound;
			((AudioSource)gameObject.GetComponent(typeof(AudioSource))).pitch = (float)1;
			((AudioSource)gameObject.GetComponent(typeof(AudioSource))).Play();
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.spawnparts, this.transform.position, Quaternion.identity);
		}
	}

	// Token: 0x06000173 RID: 371 RVA: 0x0000F4D0 File Offset: 0x0000D6D0
	public virtual void Main()
	{
	}

	// Token: 0x04000246 RID: 582
	public GameObject[] enemies;

	// Token: 0x04000247 RID: 583
	public GameObject spawnparts;

	// Token: 0x04000248 RID: 584
	public float spawntime;

	// Token: 0x04000249 RID: 585
	public float spawnbegindelay;

	// Token: 0x0400024A RID: 586
	[HideInInspector]
	public float spawntimer;
}
