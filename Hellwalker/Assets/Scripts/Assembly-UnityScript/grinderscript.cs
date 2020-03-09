using System;
using UnityEngine;

// Token: 0x020000CF RID: 207
[Serializable]
public class grinderscript : MonoBehaviour
{
	// Token: 0x060004C1 RID: 1217 RVA: 0x0002B030 File Offset: 0x00029230
	public virtual void Start()
	{
	}

	// Token: 0x060004C2 RID: 1218 RVA: 0x0002B034 File Offset: 0x00029234
	public virtual void Update()
	{
		this.transform.Rotate((float)0, this.myspeed * Time.deltaTime, (float)0);
	}

	// Token: 0x060004C3 RID: 1219 RVA: 0x0002B054 File Offset: 0x00029254
	public virtual void OnTriggerEnter(Collider hit)
	{
		DestructibleObjectScript destructibleObjectScript = (DestructibleObjectScript)hit.transform.gameObject.GetComponent(typeof(DestructibleObjectScript));
		if (destructibleObjectScript)
		{
			destructibleObjectScript.myhealth = (float)0;
			destructibleObjectScript.doragdoll = false;
		}
		Rigidbody rigidbody = (Rigidbody)hit.transform.gameObject.GetComponent(typeof(Rigidbody));
		if (rigidbody)
		{
			rigidbody.velocity = (hit.transform.position - this.transform.position).normalized * (float)40;
		}
	}

	// Token: 0x060004C4 RID: 1220 RVA: 0x0002B0F8 File Offset: 0x000292F8
	public virtual void Main()
	{
	}

	// Token: 0x040005B3 RID: 1459
	public float myspeed;
}
