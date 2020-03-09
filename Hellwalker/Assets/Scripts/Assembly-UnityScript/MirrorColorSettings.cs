using System;
using Boo.Lang.Runtime;
using UnityEngine;
using UnityScript.Lang;

// Token: 0x0200007A RID: 122
[Serializable]
public class MirrorColorSettings : MonoBehaviour
{
	// Token: 0x060002FE RID: 766 RVA: 0x0001ACB8 File Offset: 0x00018EB8
	public virtual void Start()
	{
		this.mirrorthis = GameObject.Find("WeaponCam");
	}

	// Token: 0x060002FF RID: 767 RVA: 0x0001ACCC File Offset: 0x00018ECC
	public virtual void Update()
	{
		RuntimeServices.SetProperty(this.GetComponent("SimpleLUT"), "Saturation", UnityRuntimeServices.GetProperty(this.mirrorthis.GetComponent("SimpleLUT"), "Saturation"));
		RuntimeServices.SetProperty(this.GetComponent("SimpleLUT"), "Brightness", UnityRuntimeServices.GetProperty(this.mirrorthis.GetComponent("SimpleLUT"), "Brightness"));
		RuntimeServices.SetProperty(this.GetComponent("SimpleLUT"), "Contrast", UnityRuntimeServices.GetProperty(this.mirrorthis.GetComponent("SimpleLUT"), "Contrast"));
		RuntimeServices.SetProperty(this.GetComponent("SimpleLUT"), "Hue", UnityRuntimeServices.GetProperty(this.mirrorthis.GetComponent("SimpleLUT"), "Hue"));
		RuntimeServices.SetProperty(this.GetComponent("SimpleLUT"), "TintColor", UnityRuntimeServices.GetProperty(this.mirrorthis.GetComponent("SimpleLUT"), "TintColor"));
	}

	// Token: 0x06000300 RID: 768 RVA: 0x0001ADCC File Offset: 0x00018FCC
	public virtual void Main()
	{
	}

	// Token: 0x0400035E RID: 862
	[HideInInspector]
	public GameObject mirrorthis;
}
