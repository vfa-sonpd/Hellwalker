using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x020000B8 RID: 184
[Serializable]
public class TeleporterScript : MonoBehaviour
{
	// Token: 0x06000454 RID: 1108 RVA: 0x00029448 File Offset: 0x00027648
	public virtual void Start()
	{
	}

	// Token: 0x06000455 RID: 1109 RVA: 0x0002944C File Offset: 0x0002764C
	public virtual void Update()
	{
	}

	// Token: 0x06000456 RID: 1110 RVA: 0x00029450 File Offset: 0x00027650
	public virtual void OnTriggerEnter(Collider hit)
	{
		if (hit.transform.gameObject.layer != 19 && hit.transform.gameObject.tag != "ExplosionTag")
		{
			((TeleporterGoalScript)this.goal.GetComponent(typeof(TeleporterGoalScript))).dogibfloat = 0.1f;
			((TeleporterGoalScript)this.goal.GetComponent(typeof(TeleporterGoalScript))).ignorethisthing = hit.transform.gameObject;
			UnityEngine.Object.Instantiate<GameObject>(this.parts, hit.transform.position, Quaternion.identity);
			hit.transform.position = this.goal.transform.position;
			NavMeshAgent navMeshAgent = (NavMeshAgent)hit.transform.gameObject.GetComponent(typeof(NavMeshAgent));
			if (navMeshAgent)
			{
				navMeshAgent.Warp(this.goal.transform.position);
			}
			UnityEngine.Object.Instantiate<GameObject>(this.parts, hit.transform.position, Quaternion.identity);
		}
	}

	// Token: 0x06000457 RID: 1111 RVA: 0x00029578 File Offset: 0x00027778
	public virtual void Main()
	{
	}

	// Token: 0x0400055B RID: 1371
	public GameObject goal;

	// Token: 0x0400055C RID: 1372
	public GameObject parts;
}
