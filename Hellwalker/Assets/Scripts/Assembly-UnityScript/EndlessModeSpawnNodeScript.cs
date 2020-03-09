using System;
using UnityEngine;

// Token: 0x0200003A RID: 58
[Serializable]
public class EndlessModeSpawnNodeScript : MonoBehaviour
{
	// Token: 0x06000157 RID: 343 RVA: 0x0000EAA0 File Offset: 0x0000CCA0
	public virtual void Awake()
	{
		if (this.firstwave > 1)
		{
			this.gameObject.transform.tag = "Untagged";
		}
		this.dospawn = false;
		this.spawn = -1;
	}

	// Token: 0x06000158 RID: 344 RVA: 0x0000EAD4 File Offset: 0x0000CCD4
	public virtual void Start()
	{
		this.controller = (EndlessModeSpawnerControllerScript)GameObject.Find("Endless Mode Controller").GetComponent(typeof(EndlessModeSpawnerControllerScript));
	}

	// Token: 0x06000159 RID: 345 RVA: 0x0000EB08 File Offset: 0x0000CD08
	public virtual void Update()
	{
		if (this.controller.currentwave >= this.firstwave)
		{
			this.gameObject.transform.tag = "EndlessNodeTag";
		}
		if (this.spawnedenemy == null && this.dospawn)
		{
			this.spawntimer += Time.deltaTime;
			if (this.spawntimer > this.spawndelay)
			{
				this.spawnenemy();
				this.spawntimer = (float)0;
			}
		}
	}

	// Token: 0x0600015A RID: 346 RVA: 0x0000EB90 File Offset: 0x0000CD90
	public virtual void spawnenemy()
	{
		if (this.spawn != -1)
		{
			int num = this.spawn;
			if (!this.isallowed(this.spawn))
			{
				num = this.lastallowed;
			}
			else
			{
				this.lastallowed = this.spawn;
			}
			this.spawnedenemy = UnityEngine.Object.Instantiate<GameObject>(this.controller.spawns[num], this.transform.position, Quaternion.identity);
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.indicator, this.transform.position, Quaternion.identity);
			((EnemyIndicatorObjectScript)gameObject.GetComponent(typeof(EnemyIndicatorObjectScript))).myenemy = this.spawnedenemy;
			if ((BasicAIScript)this.spawnedenemy.GetComponent(typeof(BasicAIScript)))
			{
				((BasicAIScript)this.spawnedenemy.GetComponent(typeof(BasicAIScript))).AMAWAKE = true;
				((AudioSource)this.spawnedenemy.GetComponent(typeof(AudioSource))).clip = ((BasicAIScript)this.spawnedenemy.GetComponent(typeof(BasicAIScript))).alertsound;
				((AudioSource)this.spawnedenemy.GetComponent(typeof(AudioSource))).pitch = (float)1;
				((AudioSource)this.spawnedenemy.GetComponent(typeof(AudioSource))).Play();
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.spawnparts, this.transform.position, Quaternion.identity);
				Animator animator = (Animator)this.spawnedenemy.GetComponent(typeof(Animator));
				if (animator)
				{
					animator.SetTrigger("AlertedTrigger");
				}
			}
			this.dospawn = false;
		}
	}

	// Token: 0x0600015B RID: 347 RVA: 0x0000ED54 File Offset: 0x0000CF54
	public virtual bool isallowed(int spawn)
	{
		int i = 0;
		bool result = true;
		while (i < this.disallowedtypes.Length)
		{
			if (this.disallowedtypes[i] == spawn)
			{
				result = false;
			}
			i++;
		}
		return result;
	}

	// Token: 0x0600015C RID: 348 RVA: 0x0000ED90 File Offset: 0x0000CF90
	public virtual void Main()
	{
	}

	// Token: 0x04000229 RID: 553
	public GameObject indicator;

	// Token: 0x0400022A RID: 554
	public GameObject spawnparts;

	// Token: 0x0400022B RID: 555
	[HideInInspector]
	public EndlessModeSpawnerControllerScript controller;

	// Token: 0x0400022C RID: 556
	[HideInInspector]
	public int spawn;

	// Token: 0x0400022D RID: 557
	[HideInInspector]
	public GameObject spawnedenemy;

	// Token: 0x0400022E RID: 558
	[HideInInspector]
	public int spawnindex;

	// Token: 0x0400022F RID: 559
	[HideInInspector]
	public float spawntimer;

	// Token: 0x04000230 RID: 560
	public float spawndelay;

	// Token: 0x04000231 RID: 561
	[HideInInspector]
	public bool dospawn;

	// Token: 0x04000232 RID: 562
	[HideInInspector]
	public int lastallowed;

	// Token: 0x04000233 RID: 563
	public int[] disallowedtypes;

	// Token: 0x04000234 RID: 564
	public int firstwave;
}
