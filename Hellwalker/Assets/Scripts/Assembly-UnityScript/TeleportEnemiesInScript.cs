using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x020000B6 RID: 182
[Serializable]
public class TeleportEnemiesInScript : MonoBehaviour
{
	// Token: 0x06000449 RID: 1097 RVA: 0x00029184 File Offset: 0x00027384
	public virtual void Start()
	{
	}

	// Token: 0x0600044A RID: 1098 RVA: 0x00029188 File Offset: 0x00027388
	public virtual void Update()
	{
		if (this.buttontrigger && ((ButtonScript)this.mybutton.GetComponent(typeof(ButtonScript))).buttonstate)
		{
			this.teleportfoes();
		}
	}

	// Token: 0x0600044B RID: 1099 RVA: 0x000291C0 File Offset: 0x000273C0
	public virtual void OnTriggerEnter(Collider hit)
	{
		if (hit.transform.gameObject.layer == 10)
		{
			this.teleportfoes();
		}
	}

	// Token: 0x0600044C RID: 1100 RVA: 0x000291E0 File Offset: 0x000273E0
	public virtual void teleportfoes()
	{
		for (int i = 0; i < this.enemies.Length; i++)
		{
			if (this.enemies[i] != null)
			{
				((TeleporterGoalScript)this.goals[i].GetComponent(typeof(TeleporterGoalScript))).dogibfloat = 0.1f;
				((TeleporterGoalScript)this.goals[i].GetComponent(typeof(TeleporterGoalScript))).ignorethisthing = this.enemies[i].transform.gameObject;
				UnityEngine.Object.Instantiate<GameObject>(this.parts, this.goals[i].transform.position, Quaternion.identity);
				this.enemies[i].transform.position = this.goals[i].transform.position;
				NavMeshAgent navMeshAgent = (NavMeshAgent)this.enemies[i].transform.gameObject.GetComponent(typeof(NavMeshAgent));
				if (navMeshAgent)
				{
					navMeshAgent.Warp(this.goals[i].transform.position);
				}
				if ((BasicAIScript)this.enemies[i].GetComponent(typeof(BasicAIScript)))
				{
					((BasicAIScript)this.enemies[i].GetComponent(typeof(BasicAIScript))).AMAWAKE = this.awakenEnemies;
				}
			}
		}
		UnityEngine.Object.Destroy(this.transform.gameObject);
	}

	// Token: 0x0600044D RID: 1101 RVA: 0x00029364 File Offset: 0x00027564
	public virtual void Main()
	{
	}

	// Token: 0x04000553 RID: 1363
	public GameObject parts;

	// Token: 0x04000554 RID: 1364
	public GameObject mybutton;

	// Token: 0x04000555 RID: 1365
	public GameObject[] enemies;

	// Token: 0x04000556 RID: 1366
	public GameObject[] goals;

	// Token: 0x04000557 RID: 1367
	public bool buttontrigger;

	// Token: 0x04000558 RID: 1368
	public bool awakenEnemies;
}
