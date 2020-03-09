using System;
using UnityEngine;

// Token: 0x020000BE RID: 190
[Serializable]
public class ToiletBowlScript : MonoBehaviour
{
	// Token: 0x06000470 RID: 1136 RVA: 0x00029D24 File Offset: 0x00027F24
	public virtual void Start()
	{
		this.soundscript = (PlaySoundOnUseSCript)this.mytoilet.GetComponent(typeof(PlaySoundOnUseSCript));
	}

	// Token: 0x06000471 RID: 1137 RVA: 0x00029D54 File Offset: 0x00027F54
	public virtual void Update()
	{
		if (this.flushobjects)
		{
			this.flushtimer += Time.deltaTime;
			if (this.flushtimer > 0.1f)
			{
				this.flushtimer = (float)0;
				this.flushobjects = false;
			}
		}
	}

	// Token: 0x06000472 RID: 1138 RVA: 0x00029DA0 File Offset: 0x00027FA0
	public virtual void OnTriggerStay(Collider hit)
	{
		if ((hit.transform.gameObject.tag == "PickUpObject" || hit.transform.gameObject.layer == 23) && this.flushobjects)
		{
			UnityEngine.Object.Destroy(hit.transform.gameObject);
			((ParticleSystem)this.myparts.GetComponent(typeof(ParticleSystem))).Play();
		}
	}

	// Token: 0x06000473 RID: 1139 RVA: 0x00029E20 File Offset: 0x00028020
	public virtual void Main()
	{
	}

	// Token: 0x04000575 RID: 1397
	public GameObject mytoilet;

	// Token: 0x04000576 RID: 1398
	public GameObject myparts;

	// Token: 0x04000577 RID: 1399
	[HideInInspector]
	public PlaySoundOnUseSCript soundscript;

	// Token: 0x04000578 RID: 1400
	public bool flushobjects;

	// Token: 0x04000579 RID: 1401
	[HideInInspector]
	public float flushtimer;
}
