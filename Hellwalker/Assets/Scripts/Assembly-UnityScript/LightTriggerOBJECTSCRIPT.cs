using System;
using UnityEngine;

// Token: 0x02000066 RID: 102
[Serializable]
public class LightTriggerOBJECTSCRIPT : MonoBehaviour
{
	// Token: 0x06000294 RID: 660 RVA: 0x00018E64 File Offset: 0x00017064
	public virtual void Start()
	{
        this.shade = Essential.Instance.shaderScript;
		this.masterscript = Essential.Instance.lightMASTERScript;
        this.rend = (Renderer)this.GetComponent(typeof(Renderer));
		this.triggersinrange = new int[this.masterscript.triggerlocation.Length];
	}

	// Token: 0x06000295 RID: 661 RVA: 0x00018EF0 File Offset: 0x000170F0
	public virtual void FixedUpdate()
	{
		if (!this.shade.dynamiclighting)
		{
			if (this.lightanchor)
			{
				this.myloc = this.lightanchor.transform.position;
			}
			else
			{
				this.myloc = this.transform.position;
			}
			this.lightupdatetimer += Time.deltaTime;
			if (this.lastlocation != this.myloc)
			{
				this.lastlocation = this.myloc;
				int num = -1;
				int num2 = this.checksun();
				if (num2 != -1)
				{
					num = num2;
				}
				int num3 = this.findnearesttrigger();
				if (num3 != -1)
				{
					if (!this.masterscript.overridesun[num3] && num != -1)
					{
						num3 = num;
					}
				}
				else
				{
					num3 = num;
				}
				if (num3 != -1)
				{
					this.setlighting(this.masterscript.triggercolor[num3]);
				}
				else if (this.masterscript.useambient)
				{
					this.setlighting(this.masterscript.ambientcolor);
				}
				this.lightupdatetimer = (float)0;
			}
		}
	}

	// Token: 0x06000296 RID: 662 RVA: 0x0001901C File Offset: 0x0001721C
	public virtual int checksun()
	{
		for (int i = 0; i < this.masterscript.triggersun.Length; i++)
		{
			if (this.masterscript.triggersun[i] && !Physics.Raycast(this.myloc, Vector3.up, Vector3.Distance(this.myloc, this.masterscript.triggerlocation[i]), this.masterscript.blockinglayers))
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x06000297 RID: 663 RVA: 0x000190B4 File Offset: 0x000172B4
	public virtual int findnearesttrigger()
	{
		int i = 0;
		int num = -1;
		float num2 = 0f;
		while (i < this.masterscript.triggerlocation.Length)
		{
			num2 = Vector3.Distance(this.masterscript.triggerlocation[i], this.myloc);
			if (num2 <= this.masterscript.triggerrange[i])
			{
				if (num == -1)
				{
					num = i;
				}
				if (num2 < Vector3.Distance(this.masterscript.triggerlocation[num], this.myloc))
				{
					num = i;
				}
			}
			i++;
		}
		return num;
	}

	// Token: 0x06000298 RID: 664 RVA: 0x00019154 File Offset: 0x00017354
	public virtual void setlighting(Color c)
	{
		this.rend.material.SetColor("_EmissionColor", c);
	}

	// Token: 0x06000299 RID: 665 RVA: 0x0001916C File Offset: 0x0001736C
	public virtual void ReEnableProbes()
	{
	}

	// Token: 0x0600029A RID: 666 RVA: 0x00019170 File Offset: 0x00017370
	public virtual void Main()
	{
	}

	// Token: 0x0400030E RID: 782
	public GameObject lightanchor;

	// Token: 0x0400030F RID: 783
	[HideInInspector]
	public LightTriggerMASTERSCRIPT masterscript;

	// Token: 0x04000310 RID: 784
	[HideInInspector]
	public float lightupdatetimer;

	// Token: 0x04000311 RID: 785
	[HideInInspector]
	public Renderer rend;

	// Token: 0x04000312 RID: 786
	[HideInInspector]
	public Vector3 myloc;

	// Token: 0x04000313 RID: 787
	[HideInInspector]
	public ShaderScript shade;

	// Token: 0x04000314 RID: 788
	[HideInInspector]
	public Vector3 lastlocation;

	// Token: 0x04000315 RID: 789
	[HideInInspector]
	public int[] triggersinrange;
}
