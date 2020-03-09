using System;
using TMPro;
using UnityEngine;

// Token: 0x020000AE RID: 174
[Serializable]
public class SinkScript : MonoBehaviour
{
	// Token: 0x0600041D RID: 1053 RVA: 0x00027C40 File Offset: 0x00025E40
	public virtual void Start()
	{
		((Renderer)this.parts.GetComponent(typeof(Renderer))).enabled = false;
	}

	// Token: 0x0600041E RID: 1054 RVA: 0x00027C70 File Offset: 0x00025E70
	public virtual void Update()
	{
		this.sinktimer -= Time.deltaTime;
		if (this.sinktimer < (float)0)
		{
			this.sinktimer = (float)0;
		}
	}

	// Token: 0x0600041F RID: 1055 RVA: 0x00027C9C File Offset: 0x00025E9C
	public virtual void turnonwater()
	{
		if (this.sinktimer <= (float)0)
		{
			((Renderer)this.parts.GetComponent(typeof(Renderer))).enabled = !((Renderer)this.parts.GetComponent(typeof(Renderer))).enabled;
			if (!((AudioSource)this.parts.GetComponent(typeof(AudioSource))).isPlaying && ((Renderer)this.parts.GetComponent(typeof(Renderer))).enabled)
			{
				((AudioSource)this.parts.GetComponent(typeof(AudioSource))).Play();
			}
			if (((AudioSource)this.parts.GetComponent(typeof(AudioSource))).isPlaying && !((Renderer)this.parts.GetComponent(typeof(Renderer))).enabled)
			{
				((AudioSource)this.parts.GetComponent(typeof(AudioSource))).Stop();
			}
			PlayerHealthManagement playerHealthManagement = (PlayerHealthManagement)GameObject.Find("Player").GetComponent(typeof(PlayerHealthManagement));
			if (((Renderer)this.parts.GetComponent(typeof(Renderer))).enabled)
			{
				if (playerHealthManagement.myhealth < (float)100)
				{
					playerHealthManagement.myhealth += (float)1;
					((TextMeshProUGUI)GameObject.Find("MessageText").GetComponent(typeof(TextMeshProUGUI))).text = "+1 Health";
					((ClearMessageAfterTime)GameObject.Find("MessageText").GetComponent(typeof(ClearMessageAfterTime))).timer = ((ClearMessageAfterTime)GameObject.Find("MessageText").GetComponent(typeof(ClearMessageAfterTime))).defaulttime;
				}
				this.sinktimer = 0.7f;
			}
		}
	}

	// Token: 0x06000420 RID: 1056 RVA: 0x00027EA0 File Offset: 0x000260A0
	public virtual void Main()
	{
	}

	// Token: 0x0400051F RID: 1311
	public GameObject parts;

	// Token: 0x04000520 RID: 1312
	[HideInInspector]
	public float sinktimer;
}
