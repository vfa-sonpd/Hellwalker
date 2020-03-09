using System;
using UnityEngine;

// Token: 0x020000B7 RID: 183
[Serializable]
public class TeleporterGoalScript : MonoBehaviour
{
	// Token: 0x0600044F RID: 1103 RVA: 0x00029370 File Offset: 0x00027570
	public virtual void Start()
	{
		this.transform.parent = null;
	}

	// Token: 0x06000450 RID: 1104 RVA: 0x00029380 File Offset: 0x00027580
	public virtual void Update()
	{
		this.dogibfloat -= Time.deltaTime;
		if (this.dogibfloat < (float)0)
		{
			this.dogibfloat = (float)0;
			this.ignorethisthing = null;
		}
	}

	// Token: 0x06000451 RID: 1105 RVA: 0x000293BC File Offset: 0x000275BC
	public virtual void OnTriggerStay(Collider hit)
	{
		if (this.dogibfloat > (float)0 && hit.transform.gameObject != this.ignorethisthing)
		{
			MonoBehaviour.print("dfweq");
			DestructibleObjectScript destructibleObjectScript = (DestructibleObjectScript)hit.transform.gameObject.GetComponent(typeof(DestructibleObjectScript));
			if (destructibleObjectScript)
			{
				destructibleObjectScript.dampen = false;
				destructibleObjectScript.doragdoll = false;
				destructibleObjectScript.myhealth = (float)0;
			}
		}
	}

	// Token: 0x06000452 RID: 1106 RVA: 0x0002943C File Offset: 0x0002763C
	public virtual void Main()
	{
	}

	// Token: 0x04000559 RID: 1369
	public float dogibfloat;

	// Token: 0x0400055A RID: 1370
	public GameObject ignorethisthing;
}
