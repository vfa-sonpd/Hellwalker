using System;
using TMPro;
using UnityEngine;

// Token: 0x0200003B RID: 59
[Serializable]
public class EndlessModeSpawnerControllerScript : MonoBehaviour
{
	// Token: 0x0600015E RID: 350 RVA: 0x0000ED9C File Offset: 0x0000CF9C
	public virtual void Start()
	{
	}

	// Token: 0x0600015F RID: 351 RVA: 0x0000EDA0 File Offset: 0x0000CFA0
	public virtual void Update()
	{
		if (this.checkstates())
		{
			this.initiatespawns();
		}
	}

	// Token: 0x06000160 RID: 352 RVA: 0x0000EDB4 File Offset: 0x0000CFB4
	public virtual void initiatespawns()
	{
		this.currentwave++;
		((StatScript)GameObject.Find("StatObject").GetComponent(typeof(StatScript))).multiplier = ((StatScript)GameObject.Find("StatObject").GetComponent(typeof(StatScript))).multiplier + (float)((StatScript)GameObject.Find("StatObject").GetComponent(typeof(StatScript))).addtomultiplierperwave;
		((TextMeshProUGUI)GameObject.Find("WaveText").GetComponent(typeof(TextMeshProUGUI))).text = "WAVE " + this.currentwave.ToString();
		((ClearMessageAfterTime)GameObject.Find("WaveText").GetComponent(typeof(ClearMessageAfterTime))).timer = (float)3;
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.upgradenodes();
		this.setstates();
	}

	// Token: 0x06000161 RID: 353 RVA: 0x0000EEC0 File Offset: 0x0000D0C0
	public virtual bool checkstates()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("EndlessNodeTag");
		int i = 0;
		bool result = true;
		while (i < array.Length)
		{
			if (((EndlessModeSpawnNodeScript)array[i].GetComponent(typeof(EndlessModeSpawnNodeScript))).spawnedenemy != null || ((EndlessModeSpawnNodeScript)array[i].GetComponent(typeof(EndlessModeSpawnNodeScript))).dospawn)
			{
				result = false;
			}
			i++;
		}
		return result;
	}

	// Token: 0x06000162 RID: 354 RVA: 0x0000EF40 File Offset: 0x0000D140
	public virtual void setstates()
	{
		UnityEngine.Random.seed = this.currentwave + this.seednudge;
		GameObject[] array = GameObject.FindGameObjectsWithTag("EndlessNodeTag");
		for (int i = 0; i < array.Length; i++)
		{
			if (((EndlessModeSpawnNodeScript)array[i].GetComponent(typeof(EndlessModeSpawnNodeScript))).spawn != -1)
			{
				((EndlessModeSpawnNodeScript)array[i].GetComponent(typeof(EndlessModeSpawnNodeScript))).dospawn = true;
			}
		}
	}

	// Token: 0x06000163 RID: 355 RVA: 0x0000EFC0 File Offset: 0x0000D1C0
	public virtual void upgradenodes()
	{
		UnityEngine.Random.seed = this.currentwave + this.seednudge;
		GameObject[] array = GameObject.FindGameObjectsWithTag("EndlessNodeTag");
		for (int i = 0; i < this.numupgrades; i++)
		{
			int num = UnityEngine.Random.Range(0, array.Length);
			EndlessModeSpawnNodeScript endlessModeSpawnNodeScript = (EndlessModeSpawnNodeScript)array[num].GetComponent(typeof(EndlessModeSpawnNodeScript));
			endlessModeSpawnNodeScript.spawn++;
			if (endlessModeSpawnNodeScript.spawn > this.spawns.Length - 1)
			{
				endlessModeSpawnNodeScript.spawn -= this.spawns.Length - 1;
			}
		}
	}

	// Token: 0x06000164 RID: 356 RVA: 0x0000F068 File Offset: 0x0000D268
	public virtual void Main()
	{
	}

	// Token: 0x04000235 RID: 565
	public int seednudge;

	// Token: 0x04000236 RID: 566
	public int currentwave;

	// Token: 0x04000237 RID: 567
	public GameObject[] spawns;

	// Token: 0x04000238 RID: 568
	public int numupgrades;
}
