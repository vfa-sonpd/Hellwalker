using System;
using UnityEngine;

// Token: 0x0200003D RID: 61
[Serializable]
public class EnemyIndicatorObjectScript : MonoBehaviour
{
	// Token: 0x0600016C RID: 364 RVA: 0x0000F1E4 File Offset: 0x0000D3E4
	public virtual void Start()
	{
		this.r = (Renderer)this.GetComponent(typeof(Renderer));
		this.r.enabled = false;
		this.stats = (StatScript)GameObject.Find("StatObject").GetComponent(typeof(StatScript));
		this.transform.parent = this.myenemy.transform;
		this.transform.localPosition = new Vector3((float)0, this.myoffset, (float)0);
		this.bobi = (float)UnityEngine.Random.Range(0, 1);
	}

	// Token: 0x0600016D RID: 365 RVA: 0x0000F27C File Offset: 0x0000D47C
	public virtual void Update()
	{
		this.transform.Rotate(Vector3.up * Time.deltaTime * this.myrotatespeed);
		this.bobi += Time.deltaTime * this.bobspeed;
		if (this.bobi > (float)1)
		{
			this.bobi -= (float)1;
		}
		float y = this.myoffset + this.bobcurve.Evaluate(this.bobi) * this.bobamount;
		Vector3 localPosition = this.transform.localPosition;
		float num = localPosition.y = y;
		Vector3 vector = this.transform.localPosition = localPosition;
		if (this.stats.showenemyindicators)
		{
			this.r.enabled = true;
		}
		else
		{
			this.r.enabled = false;
		}
	}

	// Token: 0x0600016E RID: 366 RVA: 0x0000F35C File Offset: 0x0000D55C
	public virtual void Main()
	{
	}

	// Token: 0x0400023D RID: 573
	[HideInInspector]
	public GameObject myenemy;

	// Token: 0x0400023E RID: 574
	public float myoffset;

	// Token: 0x0400023F RID: 575
	public float myrotatespeed;

	// Token: 0x04000240 RID: 576
	public float bobspeed;

	// Token: 0x04000241 RID: 577
	public float bobamount;

	// Token: 0x04000242 RID: 578
	public AnimationCurve bobcurve;

	// Token: 0x04000243 RID: 579
	[HideInInspector]
	public float bobi;

	// Token: 0x04000244 RID: 580
	[HideInInspector]
	public Renderer r;

	// Token: 0x04000245 RID: 581
	[HideInInspector]
	public StatScript stats;
}
