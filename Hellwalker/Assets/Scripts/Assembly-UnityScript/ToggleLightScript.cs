using System;
using UnityEngine;
using UnityScript.Lang;

// Token: 0x020000BC RID: 188
[Serializable]
public class ToggleLightScript : MonoBehaviour
{
	// Token: 0x06000466 RID: 1126 RVA: 0x00029A70 File Offset: 0x00027C70
	public virtual void Start()
	{
		bool buttonstate = ((ButtonScript)this.mybutton.GetComponent(typeof(ButtonScript))).buttonstate;
		this.laststate = !buttonstate;
	}

	// Token: 0x06000467 RID: 1127 RVA: 0x00029AA8 File Offset: 0x00027CA8
	public virtual void Update()
	{
		bool buttonstate = ((ButtonScript)this.mybutton.GetComponent(typeof(ButtonScript))).buttonstate;
		if (this.laststate != buttonstate)
		{
			for (int i = 0; i <= Extensions.get_length(this.mylights) - 1; i++)
			{
				((Light)this.mylights[i].GetComponent(typeof(Light))).enabled = buttonstate;
			}
			((LensFlare)this.GetComponent(typeof(LensFlare))).enabled = buttonstate;
			if (buttonstate)
			{
				((Renderer)this.GetComponent(typeof(Renderer))).material = this.litmat;
			}
			else
			{
				((Renderer)this.GetComponent(typeof(Renderer))).material = this.unlitmat;
			}
		}
		this.laststate = buttonstate;
	}

	// Token: 0x06000468 RID: 1128 RVA: 0x00029B90 File Offset: 0x00027D90
	public virtual void Main()
	{
	}

	// Token: 0x0400056E RID: 1390
	public GameObject mybutton;

	// Token: 0x0400056F RID: 1391
	public Material litmat;

	// Token: 0x04000570 RID: 1392
	public Material unlitmat;

	// Token: 0x04000571 RID: 1393
	public GameObject[] mylights;

	// Token: 0x04000572 RID: 1394
	[HideInInspector]
	public bool laststate;
}
