using System;
using UnityEngine;

// Token: 0x02000037 RID: 55
[Serializable]
public class EndLevelTriggerScript : MonoBehaviour
{
	// Token: 0x0600014A RID: 330 RVA: 0x0000E7D0 File Offset: 0x0000C9D0
	public virtual void Start()
	{
	}

	// Token: 0x0600014B RID: 331 RVA: 0x0000E7D4 File Offset: 0x0000C9D4
	public virtual void Update()
	{
		if (this.tripped)
		{
			this.delaytime -= Time.deltaTime;
			if (this.delaytime <= (float)0)
			{
				InteractScript interactScript = (InteractScript)GameObject.Find("MainCamera").GetComponent(typeof(InteractScript));
				if (this.gototext)
				{
					((PersistScript)GameObject.Find("PERSIST").GetComponent(typeof(PersistScript))).textnext = true;
				}
				//this.StartCoroutine(interactScript.endlevel(Application.loadedLevel, -1));
			}
		}
	}

	// Token: 0x0600014C RID: 332 RVA: 0x0000E870 File Offset: 0x0000CA70
	public virtual void OnTriggerEnter(Collider hit)
	{
		if (hit.transform.gameObject.layer == 10)
		{
			this.tripped = true;
		}
	}

	// Token: 0x0600014D RID: 333 RVA: 0x0000E890 File Offset: 0x0000CA90
	public virtual void Main()
	{
	}

	// Token: 0x0400021C RID: 540
	[HideInInspector]
	public bool tripped;

	// Token: 0x0400021D RID: 541
	public float delaytime;

	// Token: 0x0400021E RID: 542
	public bool gototext;
}
