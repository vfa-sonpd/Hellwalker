using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000C9 RID: 201
[Serializable]
public class WaterMovementScript : MonoBehaviour
{
	// Token: 0x060004A6 RID: 1190 RVA: 0x0002A8E0 File Offset: 0x00028AE0
	public virtual void Start()
	{
		this.p = GameObject.Find("Player");
	}

	// Token: 0x060004A7 RID: 1191 RVA: 0x0002A8F4 File Offset: 0x00028AF4
	public virtual void Update()
	{
		if (this.delaytimer <= (float)0 && this.dodelay)
		{
			((MyControllerScript)this.p.GetComponent(typeof(MyControllerScript))).inwater = false;
			((MyControllerScript)this.p.GetComponent(typeof(MyControllerScript))).bunnyspeed = (float)0;
			((MyControllerScript)this.p.GetComponent(typeof(MyControllerScript))).gravityforce = 0.3f;
			((AudioSource)this.waterexitsound.GetComponent(typeof(AudioSource))).Play();
			int num = 0;
			Color color = ((Image)GameObject.Find("WaterSprite").GetComponent(typeof(Image))).color;
			float num2 = color.a = (float)num;
			Color color2 = ((Image)GameObject.Find("WaterSprite").GetComponent(typeof(Image))).color = color;
			this.underwatersound.active = false;
			this.bubbles.active = false;
			((MyControllerScript)this.p.GetComponent(typeof(MyControllerScript))).CrouchState = false;
			this.dodelay = false;
		}
		this.delaytimer -= Time.deltaTime;
		if (this.delaytimer < (float)0)
		{
			this.delaytimer = (float)0;
		}
	}

	// Token: 0x060004A8 RID: 1192 RVA: 0x0002AA64 File Offset: 0x00028C64
	public virtual void OnTriggerEnter(Collider hit)
	{
		if (hit.transform.gameObject.layer == 20)
		{
			if (!((MyControllerScript)this.p.GetComponent(typeof(MyControllerScript))).inwater)
			{
				((AudioSource)this.waterentersound.GetComponent(typeof(AudioSource))).Play();
			}
			((MyControllerScript)this.p.GetComponent(typeof(MyControllerScript))).inwater = true;
			((MyControllerScript)this.p.GetComponent(typeof(MyControllerScript))).bunnyspeed = (float)0;
			((MyControllerScript)this.p.GetComponent(typeof(MyControllerScript))).CrouchState = true;
			float a = 0.6f;
			Color color = ((Image)GameObject.Find("WaterSprite").GetComponent(typeof(Image))).color;
			float num = color.a = a;
			Color color2 = ((Image)GameObject.Find("WaterSprite").GetComponent(typeof(Image))).color = color;
			this.underwatersound.active = true;
			this.bubbles.active = true;
		}
	}

	// Token: 0x060004A9 RID: 1193 RVA: 0x0002ABA8 File Offset: 0x00028DA8
	public virtual void OnTriggerStay(Collider hit)
	{
		if (hit.transform.gameObject.layer == 20)
		{
			this.dodelay = false;
			this.delaytimer = (float)0;
		}
	}

	// Token: 0x060004AA RID: 1194 RVA: 0x0002ABDC File Offset: 0x00028DDC
	public virtual void OnTriggerExit(Collider hit)
	{
		if (hit.transform.gameObject.layer == 20)
		{
			this.delaytimer = 0.01f;
			this.dodelay = true;
		}
	}

	// Token: 0x060004AB RID: 1195 RVA: 0x0002AC08 File Offset: 0x00028E08
	public virtual void Main()
	{
	}

	// Token: 0x04000599 RID: 1433
	public GameObject waterentersound;

	// Token: 0x0400059A RID: 1434
	public GameObject waterexitsound;

	// Token: 0x0400059B RID: 1435
	public GameObject underwatersound;

	// Token: 0x0400059C RID: 1436
	public GameObject bubbles;

	// Token: 0x0400059D RID: 1437
	[HideInInspector]
	public GameObject p;

	// Token: 0x0400059E RID: 1438
	[HideInInspector]
	public float delaytimer;

	// Token: 0x0400059F RID: 1439
	[HideInInspector]
	public bool dodelay;
}
