using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000015 RID: 21
[Serializable]
public class BossControllerScript : MonoBehaviour
{
	// Token: 0x06000092 RID: 146 RVA: 0x0000AD1C File Offset: 0x00008F1C
	public BossControllerScript()
	{
		this.BossName = string.Empty;
	}

	// Token: 0x06000093 RID: 147 RVA: 0x0000AD30 File Offset: 0x00008F30
	public virtual void Start()
	{
		this.bossindex = 0;
		this.bosses = new GameObject[10];
		this.BossHealth = GameObject.Find("BossHealth");
		this.BossBarImage = GameObject.Find("BossBarImage");
		this.BossBar = GameObject.Find("BossBar");
		this.BossBar.active = false;
	}

	// Token: 0x06000094 RID: 148 RVA: 0x0000AD90 File Offset: 0x00008F90
	public virtual void FixedUpdate()
	{
		if (this.BossCurrentHealth > (float)0)
		{
			this.BossBar.active = true;
		}
		else
		{
			this.BossBar.active = false;
		}
		if (this.BossCurrentHealth < (float)0)
		{
			this.BossCurrentHealth = (float)0;
		}
		if (this.BossBar.active)
		{
			((Text)this.BossHealth.GetComponent(typeof(Text))).text = this.BossCurrentHealth.ToString() + " / " + this.BossStartingHealth.ToString();
			((Text)this.BossBar.GetComponent(typeof(Text))).text = this.BossName;
			((Image)this.BossBarImage.GetComponent(typeof(Image))).fillAmount = this.BossCurrentHealth / this.BossStartingHealth;
		}
		this.BossCurrentHealth = (float)0;
		for (int i = 0; i < this.bosses.Length; i++)
		{
			if (this.bosses[i] != null)
			{
				this.BossCurrentHealth += ((DestructibleObjectScript)this.bosses[i].GetComponent(typeof(DestructibleObjectScript))).myhealth;
			}
		}
		this.BossCurrentHealth = Mathf.Round(this.BossCurrentHealth);
	}

	// Token: 0x06000095 RID: 149 RVA: 0x0000AEF8 File Offset: 0x000090F8
	public virtual void addtolist(GameObject g)
	{
		this.bosses[this.bossindex] = g;
		this.bossindex++;
		if (this.bossindex > this.bosses.Length - 1)
		{
			this.bossindex = this.bosses.Length - 1;
		}
	}

	// Token: 0x06000096 RID: 150 RVA: 0x0000AF4C File Offset: 0x0000914C
	public virtual void Main()
	{
	}

	// Token: 0x04000174 RID: 372
	public GameObject BossBar;

	// Token: 0x04000175 RID: 373
	public GameObject BossHealth;

	// Token: 0x04000176 RID: 374
	public GameObject BossBarImage;

	// Token: 0x04000177 RID: 375
	public float BossCurrentHealth;

	// Token: 0x04000178 RID: 376
	public float BossStartingHealth;

	// Token: 0x04000179 RID: 377
	public string BossName;

	// Token: 0x0400017A RID: 378
	public GameObject[] bosses;

	// Token: 0x0400017B RID: 379
	public int bossindex;
}
