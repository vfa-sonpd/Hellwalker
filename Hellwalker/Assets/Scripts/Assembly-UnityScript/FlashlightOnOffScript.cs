using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200004A RID: 74
[Serializable]
public class FlashlightOnOffScript : MonoBehaviour
{
	// Token: 0x060001AA RID: 426 RVA: 0x00010654 File Offset: 0x0000E854
	public virtual void Start()
	{
		this.inputmanager = (MyInputManager)GameObject.Find("DasMenu").GetComponent(typeof(MyInputManager));
		this.indicator = GameObject.Find("FlashlightOnIndicator");
		this.stat = (StatScript)GameObject.Find("StatObject").GetComponent(typeof(StatScript));
		this.indicator.active = false;
	}

	// Token: 0x060001AB RID: 427 RVA: 0x000106C8 File Offset: 0x0000E8C8
	public virtual void Update()
	{
		if (this.inputmanager.GetKeyInput("flashlight", 1))
		{
			if (!this.stat.brokenflashlight)
			{
				this.togglelight();
			}
			else
			{
				((TextMeshProUGUI)GameObject.Find("TutorialMessageText").GetComponent(typeof(TextMeshProUGUI))).text = "THE FALL BROKE YOUR FLASHLIGHT";
				((ClearMessageAfterTime)GameObject.Find("TutorialMessageText").GetComponent(typeof(ClearMessageAfterTime))).timer = (float)3;
			}
		}
		if (((Light)this.flash.GetComponent(typeof(Light))).enabled)
		{
			if (this.flashlightcharge < (float)0)
			{
				this.flashlightcharge = (float)0;
				this.togglelight();
			}
			float a = this.flashlightcharge;
			Color color = ((Image)this.indicator.GetComponent(typeof(Image))).color;
			float num = color.a = a;
			Color color2 = ((Image)this.indicator.GetComponent(typeof(Image))).color = color;
		}
		if (!((Light)this.flash.GetComponent(typeof(Light))).enabled)
		{
			this.flashlightcharge = (float)1;
			if (this.flashlightcharge > (float)1)
			{
				this.flashlightcharge = (float)1;
			}
		}
	}

	// Token: 0x060001AC RID: 428 RVA: 0x00010830 File Offset: 0x0000EA30
	public virtual void togglelight()
	{
		((Light)this.flash.GetComponent(typeof(Light))).enabled = !((Light)this.flash.GetComponent(typeof(Light))).enabled;
		this.indicator.active = ((Light)this.flash.GetComponent(typeof(Light))).enabled;
		((AudioSource)this.flashlightsound.GetComponent(typeof(AudioSource))).Play();
	}

	// Token: 0x060001AD RID: 429 RVA: 0x000108C8 File Offset: 0x0000EAC8
	public virtual void Main()
	{
	}

	// Token: 0x0400026C RID: 620
	public GameObject flash;

	// Token: 0x0400026D RID: 621
	public GameObject indicator;

	// Token: 0x0400026E RID: 622
	public GameObject flashlightsound;

	// Token: 0x0400026F RID: 623
	public float flashlightcharge;

	// Token: 0x04000270 RID: 624
	public float flashlightdecreaserate;

	// Token: 0x04000271 RID: 625
	[HideInInspector]
	public MyInputManager inputmanager;

	// Token: 0x04000272 RID: 626
	[HideInInspector]
	public StatScript stat;
}
