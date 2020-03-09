using System;
using UnityEngine;

// Token: 0x02000051 RID: 81
[Serializable]
public class GrappleTracerScript : MonoBehaviour
{
	// Token: 0x06000228 RID: 552 RVA: 0x000151F8 File Offset: 0x000133F8
	public virtual void Start()
	{
	}

	// Token: 0x06000229 RID: 553 RVA: 0x000151FC File Offset: 0x000133FC
	public virtual void Update()
	{
		this.lifetimer += Time.deltaTime;
		this.endcolor.a = (float)1 - this.lifetimer / this.lifetime;
		this.startcolor.a = (float)1 - this.lifetimer / this.lifetime;
		LineRenderer lineRenderer = (LineRenderer)this.GetComponent(typeof(LineRenderer));
		lineRenderer.SetColors(this.startcolor, this.endcolor);
		if (this.lifetimer >= this.lifetime)
		{
			UnityEngine.Object.Destroy(this.transform.gameObject);
		}
		lineRenderer.SetPosition(0, GameObject.Find("Player").transform.position);
	}

	// Token: 0x0600022A RID: 554 RVA: 0x000152B8 File Offset: 0x000134B8
	public virtual void Main()
	{
	}

	// Token: 0x04000293 RID: 659
	public float lifetime;

	// Token: 0x04000294 RID: 660
	public Color startcolor;

	// Token: 0x04000295 RID: 661
	public Color endcolor;

	// Token: 0x04000296 RID: 662
	[HideInInspector]
	public float lifetimer;
}
