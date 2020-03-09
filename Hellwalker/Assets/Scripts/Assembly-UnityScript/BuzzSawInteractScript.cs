using System;
using UnityEngine;

// Token: 0x02000019 RID: 25
[Serializable]
public class BuzzSawInteractScript : MonoBehaviour
{
	// Token: 0x060000B3 RID: 179 RVA: 0x0000BA54 File Offset: 0x00009C54
	public BuzzSawInteractScript()
	{
		this.rotationamount = new Vector3((float)0, (float)1, (float)0);
	}

	// Token: 0x060000B4 RID: 180 RVA: 0x0000BA70 File Offset: 0x00009C70
	public virtual void Start()
	{
	}

	// Token: 0x060000B5 RID: 181 RVA: 0x0000BA74 File Offset: 0x00009C74
	public virtual void Update()
	{
		bool flag = ((ButtonScript)this.mybutton.GetComponent(typeof(ButtonScript))).buttonstate;
		if (this.reversebuttonstate)
		{
			flag = !flag;
		}
		if (flag)
		{
			this.transform.Rotate(this.rotationamount * Time.deltaTime * this.rotationspeed);
			this.mysound.active = true;
			((DoDamageOverTime)this.GetComponent(typeof(DoDamageOverTime))).enabled = true;
		}
		else
		{
			this.mysound.active = false;
			((DoDamageOverTime)this.GetComponent(typeof(DoDamageOverTime))).enabled = false;
			((DoDamageOverTime)this.GetComponent(typeof(DoDamageOverTime))).hurtimer = (float)0;
			((DoDamageOverTime)this.GetComponent(typeof(DoDamageOverTime))).dohurt = false;
		}
	}

	// Token: 0x060000B6 RID: 182 RVA: 0x0000BB68 File Offset: 0x00009D68
	public virtual void OnTriggerStay(Collider hit)
	{
		bool flag = ((ButtonScript)this.mybutton.GetComponent(typeof(ButtonScript))).buttonstate;
		if (this.reversebuttonstate)
		{
			flag = !flag;
		}
		if (flag && (DestructibleObjectScript)hit.transform.gameObject.GetComponent(typeof(DestructibleObjectScript)))
		{
			((DestructibleObjectScript)hit.transform.gameObject.GetComponent(typeof(DestructibleObjectScript))).myhealth = (float)0;
			((DestructibleObjectScript)hit.transform.gameObject.GetComponent(typeof(DestructibleObjectScript))).doragdoll = false;
		}
	}

	// Token: 0x060000B7 RID: 183 RVA: 0x0000BC20 File Offset: 0x00009E20
	public virtual void Main()
	{
	}

	// Token: 0x0400019A RID: 410
	public float rotationspeed;

	// Token: 0x0400019B RID: 411
	public GameObject mybutton;

	// Token: 0x0400019C RID: 412
	public bool reversebuttonstate;

	// Token: 0x0400019D RID: 413
	public GameObject mysound;

	// Token: 0x0400019E RID: 414
	public Vector3 rotationamount;
}
