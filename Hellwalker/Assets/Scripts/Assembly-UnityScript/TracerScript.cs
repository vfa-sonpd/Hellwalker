using System;
using UnityEngine;

// Token: 0x020000C2 RID: 194
[Serializable]
public class TracerScript : MonoBehaviour
{
	// Token: 0x06000486 RID: 1158 RVA: 0x0002A03C File Offset: 0x0002823C
	public TracerScript()
	{
		this.attached = true;
	}

	// Token: 0x06000487 RID: 1159 RVA: 0x0002A04C File Offset: 0x0002824C
	public virtual void Start()
	{
		this.lr = (LineRenderer)this.GetComponent(typeof(LineRenderer));
		this.lr.SetPosition(0, GameObject.Find("TracerAnchor2").transform.position);
	}

	// Token: 0x06000488 RID: 1160 RVA: 0x0002A094 File Offset: 0x00028294
	public virtual void Update()
	{
		this.lifetimer += Time.deltaTime;
		this.endcolor.a = (float)1 - this.lifetimer / this.lifetime;
		this.startcolor.a = 0.1f - this.lifetimer / this.lifetime;
		this.lr.SetColors(this.startcolor, this.endcolor);
		if (this.attached && GameObject.Find("TracerAnchor2"))
		{
			this.lr.SetPosition(0, GameObject.Find("TracerAnchor2").transform.position);
		}
		if (this.lifetimer >= this.lifetime)
		{
			UnityEngine.Object.Destroy(this.transform.gameObject);
		}
	}

	// Token: 0x06000489 RID: 1161 RVA: 0x0002A164 File Offset: 0x00028364
	public virtual void Main()
	{
	}

	// Token: 0x04000580 RID: 1408
	public float lifetime;

	// Token: 0x04000581 RID: 1409
	public Color startcolor;

	// Token: 0x04000582 RID: 1410
	public Color endcolor;

	// Token: 0x04000583 RID: 1411
	public bool attached;

	// Token: 0x04000584 RID: 1412
	[HideInInspector]
	public float lifetimer;

	// Token: 0x04000585 RID: 1413
	[HideInInspector]
	public LineRenderer lr;
}
