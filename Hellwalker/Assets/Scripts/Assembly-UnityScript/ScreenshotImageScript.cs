using System;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

// Token: 0x0200009F RID: 159
[Serializable]
public class ScreenshotImageScript : MonoBehaviour
{
	// Token: 0x060003D6 RID: 982 RVA: 0x00024B74 File Offset: 0x00022D74
	public ScreenshotImageScript()
	{
		this.speed = (float)1;
		this.rotatespeed = (float)1;
	}

	// Token: 0x060003D7 RID: 983 RVA: 0x00024B8C File Offset: 0x00022D8C
	public virtual void Start()
	{
		((Image)this.GetComponent(typeof(Image))).sprite = ((PersistScript)GameObject.Find("PERSIST").GetComponent(typeof(PersistScript))).screenshotimage;
		this.bloomtimer = (float)1;
	}

	// Token: 0x060003D8 RID: 984 RVA: 0x00024BE0 File Offset: 0x00022DE0
	public virtual void Update()
	{
		this.startdelay -= Time.deltaTime;
		if (this.startdelay <= (float)0)
		{
			this.bloomtimer -= Time.deltaTime * this.bloomspeed;
			if (this.bloomtimer < (float)0)
			{
				this.bloomtimer = (float)0;
			}
			((Image)this.GetComponent(typeof(Image))).color = new Color((float)1, (float)0, (float)0, ((Image)this.GetComponent(typeof(Image))).color.a);
			((Bloom)GameObject.Find("Main Camera").GetComponent(typeof(Bloom))).bloomIntensity = this.bloomamount * this.bloomtimer;
			((Image)this.GetComponent(typeof(Image))).material = this.brightmat;
			((ImageFadeScript)this.GetComponent(typeof(ImageFadeScript))).enabled = true;
			this.startdelay = (float)0;
			float num = Time.deltaTime * this.speed;
			this.transform.localScale = this.transform.localScale + new Vector3(num, num, num);
			if (this.transform.localScale.x < (float)0)
			{
				this.transform.localScale = new Vector3((float)0, (float)0, (float)0);
			}
			if (this.transform.localScale.x > (float)100)
			{
				this.transform.localScale = new Vector3((float)100, (float)100, (float)100);
			}
			float z = this.transform.localEulerAngles.z + Time.deltaTime * this.rotatespeed;
			Vector3 localEulerAngles = this.transform.localEulerAngles;
			float num2 = localEulerAngles.z = z;
			Vector3 vector = this.transform.localEulerAngles = localEulerAngles;
			if (this.transform.localEulerAngles.z > (float)360)
			{
				float z2 = this.transform.localEulerAngles.z - (float)360;
				Vector3 localEulerAngles2 = this.transform.localEulerAngles;
				float num3 = localEulerAngles2.z = z2;
				Vector3 vector2 = this.transform.localEulerAngles = localEulerAngles2;
			}
		}
	}

	// Token: 0x060003D9 RID: 985 RVA: 0x00024E4C File Offset: 0x0002304C
	public virtual void Main()
	{
	}

	// Token: 0x040004B6 RID: 1206
	public float speed;

	// Token: 0x040004B7 RID: 1207
	public float rotatespeed;

	// Token: 0x040004B8 RID: 1208
	public float startdelay;

	// Token: 0x040004B9 RID: 1209
	public Material brightmat;

	// Token: 0x040004BA RID: 1210
	[HideInInspector]
	public float bloomtimer;

	// Token: 0x040004BB RID: 1211
	public float bloomspeed;

	// Token: 0x040004BC RID: 1212
	public float bloomamount;
}
