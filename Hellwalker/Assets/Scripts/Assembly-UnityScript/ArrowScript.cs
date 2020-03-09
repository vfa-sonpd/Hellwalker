using System;
using UnityEngine;

// Token: 0x0200000B RID: 11
[Serializable]
public class ArrowScript : MonoBehaviour
{
	// Token: 0x06000043 RID: 67 RVA: 0x00004554 File Offset: 0x00002754
	public virtual void Start()
	{
		this.t = UnityEngine.Object.Instantiate<GameObject>(this.trail, this.transform.position, Quaternion.identity);
		((FollowOb)this.t.GetComponent(typeof(FollowOb))).ob = this.transform.gameObject;
	}

	// Token: 0x06000044 RID: 68 RVA: 0x000045AC File Offset: 0x000027AC
	public virtual void Update()
	{
		this.startdelay -= Time.deltaTime;
		if (this.startdelay <= (float)0 && !this.started && this.t != null)
		{
			((ParticleSystem)this.t.GetComponent(typeof(ParticleSystem))).Play();
			this.started = true;
		}
	}

	// Token: 0x06000045 RID: 69 RVA: 0x0000461C File Offset: 0x0000281C
	public virtual void Main()
	{
	}

	// Token: 0x04000044 RID: 68
	public GameObject trail;

	// Token: 0x04000045 RID: 69
	[HideInInspector]
	public GameObject t;

	// Token: 0x04000046 RID: 70
	public float startdelay;

	// Token: 0x04000047 RID: 71
	[HideInInspector]
	public bool started;
}
