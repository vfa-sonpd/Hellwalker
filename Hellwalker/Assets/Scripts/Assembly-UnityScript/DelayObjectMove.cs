using System;
using UnityEngine;

// Token: 0x02000028 RID: 40
[Serializable]
public class DelayObjectMove : MonoBehaviour
{
	// Token: 0x060000FD RID: 253 RVA: 0x0000CC3C File Offset: 0x0000AE3C
	public DelayObjectMove()
	{
		this.currentYRotation2 = (float)4;
	}

	// Token: 0x060000FF RID: 255 RVA: 0x0000CC58 File Offset: 0x0000AE58
	public virtual void Start()
	{
	}

	// Token: 0x06000100 RID: 256 RVA: 0x0000CC5C File Offset: 0x0000AE5C
	public virtual void Update()
	{
		DelayObjectMove.lookSensitivity = 5f;
		this.yRotation += Input.GetAxis("Mouse X") * DelayObjectMove.lookSensitivity;
		this.xRotation -= Input.GetAxis("Mouse Y") * DelayObjectMove.lookSensitivity;
		this.xRotation = Mathf.Clamp(this.xRotation, (float)-80, (float)80);
		this.currentXRotation = Mathf.SmoothDamp(this.currentXRotation, this.xRotation, ref this.xRotationV, this.lookSmoothDamp);
		this.currentYRotation = Mathf.SmoothDamp(this.currentYRotation, this.yRotation, ref this.yRotationV, this.lookSmoothDamp);
		this.transform.position = this.cam.transform.position;
		this.transform.localEulerAngles = new Vector3(this.currentXRotation, this.currentYRotation, (float)0);
	}

	// Token: 0x06000101 RID: 257 RVA: 0x0000CD44 File Offset: 0x0000AF44
	public virtual void Main()
	{
	}

	// Token: 0x040001CA RID: 458
	[NonSerialized]
	public static float lookSensitivity = (float)5;

	// Token: 0x040001CB RID: 459
	[HideInInspector]
	public float yRotation;

	// Token: 0x040001CC RID: 460
	[HideInInspector]
	public float xRotation;

	// Token: 0x040001CD RID: 461
	[HideInInspector]
	public float currentYRotation;

	// Token: 0x040001CE RID: 462
	[HideInInspector]
	public float currentXRotation;

	// Token: 0x040001CF RID: 463
	[HideInInspector]
	public float currentYRotation2;

	// Token: 0x040001D0 RID: 464
	[HideInInspector]
	public float yRotationV;

	// Token: 0x040001D1 RID: 465
	[HideInInspector]
	public float xRotationV;

	// Token: 0x040001D2 RID: 466
	public float lookSmoothDamp;

	// Token: 0x040001D3 RID: 467
	public GameObject cam;
}
