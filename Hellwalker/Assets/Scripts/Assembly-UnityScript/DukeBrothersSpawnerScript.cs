using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x02000036 RID: 54
[Serializable]
public class DukeBrothersSpawnerScript : MonoBehaviour
{
	// Token: 0x06000144 RID: 324 RVA: 0x0000E6C4 File Offset: 0x0000C8C4
	public virtual void Start()
	{
	}

	// Token: 0x06000145 RID: 325 RVA: 0x0000E6C8 File Offset: 0x0000C8C8
	public virtual void Update()
	{
	}

	// Token: 0x06000146 RID: 326 RVA: 0x0000E6CC File Offset: 0x0000C8CC
	public virtual void OnTriggerEnter(Collider hit)
	{
		if (hit.transform.gameObject.layer == 10)
		{
			this.die(hit);
		}
	}

	// Token: 0x06000147 RID: 327 RVA: 0x0000E6F8 File Offset: 0x0000C8F8
	public virtual void die(Collider hit)
	{
		NavMeshAgent navMeshAgent = (NavMeshAgent)this.myenemy.transform.gameObject.GetComponent(typeof(NavMeshAgent));
		if (navMeshAgent)
		{
			navMeshAgent.Warp(this.transform.position);
		}
		this.myenemy.transform.LookAt(hit.transform);
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.createondie, this.transform.position - this.transform.right * (float)1, Quaternion.Euler((float)-90, (float)0, (float)0));
		((Collider)this.GetComponent(typeof(Collider))).enabled = false;
		UnityEngine.Object.Destroy(this.transform.gameObject);
	}

	// Token: 0x06000148 RID: 328 RVA: 0x0000E7C4 File Offset: 0x0000C9C4
	public virtual void Main()
	{
	}

	// Token: 0x0400021A RID: 538
	public GameObject myenemy;

	// Token: 0x0400021B RID: 539
	public GameObject createondie;
}
