using System;
using UnityEngine;

// Token: 0x020000CE RID: 206
[Serializable]
public class ZoomScript : MonoBehaviour
{
	// Token: 0x060004BD RID: 1213 RVA: 0x0002ADE0 File Offset: 0x00028FE0
	public virtual void Start()
	{
		this.inputmanager = (MyInputManager)GameObject.Find("DasMenu").GetComponent(typeof(MyInputManager));
		this.w = (SelectionScript)GameObject.Find("WeaponAnimator").GetComponent(typeof(SelectionScript));
		this.attack = (AttackScript)GameObject.Find("WeaponAnimator").GetComponent(typeof(AttackScript));
		((MyMouseLook)GameObject.Find("MainCamera").GetComponent(typeof(MyMouseLook))).lookSensitivity = this.normalsensitivity;
		this.cam = (Camera)this.GetComponent(typeof(Camera));
	}

	// Token: 0x060004BE RID: 1214 RVA: 0x0002AEA0 File Offset: 0x000290A0
	public virtual void Update()
	{
		int num = this.w.selectedweapon - 1;
		if (num < 0)
		{
			num = 0;
		}
		if (this.attack.zoomedin)
		{
			if (this.cam.fieldOfView > this.normalfov / this.w.zoomamount[num])
			{
				((MyMouseLook)GameObject.Find("MainCamera").GetComponent(typeof(MyMouseLook))).lookSensitivity = this.normalsensitivity / this.w.zoomamount[num];
				this.cam.fieldOfView = this.cam.fieldOfView - Time.deltaTime * (this.zoomspeed / Time.timeScale);
			}
		}
		else
		{
			if (this.cam.fieldOfView < this.normalfov)
			{
				((MyMouseLook)GameObject.Find("MainCamera").GetComponent(typeof(MyMouseLook))).lookSensitivity = this.normalsensitivity;
				this.cam.fieldOfView = this.cam.fieldOfView + Time.deltaTime * (this.zoomspeed / Time.timeScale);
			}
			if (this.cam.fieldOfView > this.normalfov)
			{
				this.cam.fieldOfView = this.normalfov;
			}
		}
		this.cam.fieldOfView = Mathf.Clamp(this.cam.fieldOfView, this.normalfov / this.w.zoomamount[num], this.normalfov);
	}

	// Token: 0x060004BF RID: 1215 RVA: 0x0002B024 File Offset: 0x00029224
	public virtual void Main()
	{
	}

	// Token: 0x040005A9 RID: 1449
	public float zoomspeed;

	// Token: 0x040005AA RID: 1450
	public float normalfov;

	// Token: 0x040005AB RID: 1451
	public float zoomfov;

	// Token: 0x040005AC RID: 1452
	public float normalsensitivity;

	// Token: 0x040005AD RID: 1453
	public float zoomedsensitivity;

	// Token: 0x040005AE RID: 1454
	public GameObject cross;

	// Token: 0x040005AF RID: 1455
	[HideInInspector]
	public Camera cam;

	// Token: 0x040005B0 RID: 1456
	[HideInInspector]
	public SelectionScript w;

	// Token: 0x040005B1 RID: 1457
	[HideInInspector]
	public AttackScript attack;

	// Token: 0x040005B2 RID: 1458
	[HideInInspector]
	public MyInputManager inputmanager;
}
