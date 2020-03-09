using System;
using TMPro;
using UnityEngine;

// Token: 0x02000062 RID: 98
[Serializable]
public class LevelNameScript : MonoBehaviour
{
	// Token: 0x06000280 RID: 640 RVA: 0x00017D74 File Offset: 0x00015F74
	public LevelNameScript()
	{
		this.solidtime = (float)1;
		this.fadespeed = (float)1;
	}

	// Token: 0x06000281 RID: 641 RVA: 0x00017D8C File Offset: 0x00015F8C
	public virtual void Start()
	{
		if (GameObject.Find("StatObject"))
		{
			this.myname = ((StatScript)GameObject.Find("StatObject").GetComponent(typeof(StatScript))).levelname;
		}
		else
		{
			this.myname = string.Empty;
		}
		this.textmesh = (TextMeshProUGUI)this.GetComponent(typeof(TextMeshProUGUI));
		int num = 0;
		Color color = this.textmesh.color;
		float num2 = color.a = (float)num;
		Color color2 = this.textmesh.color = color;
		this.textmesh.text = this.myname;
	}

	// Token: 0x06000282 RID: 642 RVA: 0x00017E44 File Offset: 0x00016044
	public virtual void Update()
	{
		this.initialdelay -= Time.deltaTime;
		if (this.initialdelay <= (float)0)
		{
			this.initialdelay = (float)0;
			if (!this.fadingout)
			{
				float a = this.textmesh.color.a + Time.deltaTime * this.fadespeed;
				Color color = this.textmesh.color;
				float num = color.a = a;
				Color color2 = this.textmesh.color = color;
				if (this.textmesh.color.a >= (float)1)
				{
					int num2 = 1;
					Color color3 = this.textmesh.color;
					float num3 = color3.a = (float)num2;
					Color color4 = this.textmesh.color = color3;
					this.fadingout = true;
				}
			}
			else
			{
				this.holdtimer += Time.deltaTime;
				if (this.holdtimer >= this.solidtime)
				{
					this.holdtimer = this.solidtime;
					float a2 = this.textmesh.color.a - Time.deltaTime * this.fadespeed;
					Color color5 = this.textmesh.color;
					float num4 = color5.a = a2;
					Color color6 = this.textmesh.color = color5;
					if (this.textmesh.color.a < (float)0)
					{
						int num5 = 0;
						Color color7 = this.textmesh.color;
						float num6 = color7.a = (float)num5;
						Color color8 = this.textmesh.color = color7;
					}
				}
			}
		}
	}

	// Token: 0x06000283 RID: 643 RVA: 0x00018008 File Offset: 0x00016208
	public virtual void Main()
	{
	}

	// Token: 0x040002E5 RID: 741
	public float initialdelay;

	// Token: 0x040002E6 RID: 742
	public float solidtime;

	// Token: 0x040002E7 RID: 743
	public float fadespeed;

	// Token: 0x040002E8 RID: 744
	[HideInInspector]
	public string myname;

	// Token: 0x040002E9 RID: 745
	[HideInInspector]
	public TextMeshProUGUI textmesh;

	// Token: 0x040002EA RID: 746
	[HideInInspector]
	public float myalpha;

	// Token: 0x040002EB RID: 747
	[HideInInspector]
	public bool fadingout;

	// Token: 0x040002EC RID: 748
	[HideInInspector]
	public float holdtimer;
}
