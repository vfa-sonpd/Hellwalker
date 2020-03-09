using System;
using UnityEngine;

// Token: 0x02000016 RID: 22
[Serializable]
public class BounceSparks : MonoBehaviour
{
	// Token: 0x06000098 RID: 152 RVA: 0x0000AF58 File Offset: 0x00009158
	public virtual void Start()
	{
		this.rigid = (Rigidbody)this.GetComponent(typeof(Rigidbody));
		this.plrpickupscript = (PickUpScriptV2)GameObject.Find("MainCamera").GetComponent(typeof(PickUpScriptV2));
	}

	// Token: 0x06000099 RID: 153 RVA: 0x0000AFA4 File Offset: 0x000091A4
	public virtual void Update()
	{
	}

	// Token: 0x0600009A RID: 154 RVA: 0x0000AFA8 File Offset: 0x000091A8
	public virtual void OnCollisionEnter(Collision hit)
	{
		if (this.rigid.velocity.magnitude >= this.minimumvelocity && this.plrpickupscript.obihave != this.transform.gameObject)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.sparks, this.transform.position, Quaternion.Euler((float)-90, (float)0, (float)0));
			if ((AudioSource)this.GetComponent(typeof(AudioSource)))
			{
				((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
			}
		}
	}

	// Token: 0x0600009B RID: 155 RVA: 0x0000B050 File Offset: 0x00009250
	public virtual void Main()
	{
	}

	// Token: 0x0400017C RID: 380
	public GameObject sparks;

	// Token: 0x0400017D RID: 381
	public float minimumvelocity;

	// Token: 0x0400017E RID: 382
	[HideInInspector]
	public Rigidbody rigid;

	// Token: 0x0400017F RID: 383
	[HideInInspector]
	public PickUpScriptV2 plrpickupscript;
}
