using System;
using UnityEngine;

// Token: 0x02000044 RID: 68
[Serializable]
public class FadeTriggerScript : MonoBehaviour
{
	// Token: 0x0600018E RID: 398 RVA: 0x00010020 File Offset: 0x0000E220
	public virtual void Start()
	{
	}

	// Token: 0x0600018F RID: 399 RVA: 0x00010024 File Offset: 0x0000E224
	public virtual void Update()
	{
	}

	// Token: 0x06000190 RID: 400 RVA: 0x00010028 File Offset: 0x0000E228
	public virtual void OnTriggerEnter(Collider hit)
	{
		if (hit.transform.gameObject.layer == 10)
		{
			UIFadeScript uifadeScript = (UIFadeScript)GameObject.Find("FirstBlackImage").GetComponent(typeof(UIFadeScript));
			uifadeScript.fadegoal = this.fadeamount;
			uifadeScript.changespeed = this.fadespeed;
			UnityEngine.Object.Destroy(this.transform.gameObject);
		}
	}

	// Token: 0x06000191 RID: 401 RVA: 0x00010094 File Offset: 0x0000E294
	public virtual void Main()
	{
	}

	// Token: 0x0400025E RID: 606
	public float fadeamount;

	// Token: 0x0400025F RID: 607
	public float fadespeed;
}
