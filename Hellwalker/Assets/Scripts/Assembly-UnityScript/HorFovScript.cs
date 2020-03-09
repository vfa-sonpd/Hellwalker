using System;
using UnityEngine;

// Token: 0x02000055 RID: 85
[Serializable]
public class HorFovScript : MonoBehaviour
{
	// Token: 0x06000238 RID: 568 RVA: 0x00015748 File Offset: 0x00013948
	public virtual void Start()
	{
		this.zoomscript = (ZoomScript)this.GetComponent(typeof(ZoomScript));
		this.cam = (Camera)this.GetComponent(typeof(Camera));
		this.horfov = ((GameMenuButtonsScript)GameObject.Find("DasMenu").GetComponent(typeof(GameMenuButtonsScript))).LoadConfigFloat("fov");
		this.setcamfov();
	}

	// Token: 0x06000239 RID: 569 RVA: 0x000157C0 File Offset: 0x000139C0
	public virtual void Update()
	{
		if (this.zoomscript)
		{
			this.zoomscript.normalfov = this.HorizontalToVerticalFOV(this.horfov, this.cam.aspect);
		}
	}

	// Token: 0x0600023A RID: 570 RVA: 0x00015800 File Offset: 0x00013A00
	public virtual float HorizontalToVerticalFOV(float horizontalFOV, float aspect)
	{
		return 114.59156f * Mathf.Atan(Mathf.Tan(horizontalFOV * 0.017453292f / 2f) / aspect);
	}

	// Token: 0x0600023B RID: 571 RVA: 0x00015824 File Offset: 0x00013A24
	public virtual void setcamfov()
	{
		if (!this.cam)
		{
			this.cam = (Camera)this.GetComponent(typeof(Camera));
		}
		this.cam.fieldOfView = this.HorizontalToVerticalFOV(this.horfov, this.cam.aspect);
	}

	// Token: 0x0600023C RID: 572 RVA: 0x00015880 File Offset: 0x00013A80
	public virtual void Main()
	{
	}

	// Token: 0x040002AB RID: 683
	public float horfov;

	// Token: 0x040002AC RID: 684
	[HideInInspector]
	public ZoomScript zoomscript;

	// Token: 0x040002AD RID: 685
	[HideInInspector]
	public Camera cam;
}
