using System;
using UnityEngine;

// Token: 0x020000AD RID: 173
[Serializable]
public class ShowerScript : MonoBehaviour
{
	// Token: 0x06000418 RID: 1048 RVA: 0x00027B4C File Offset: 0x00025D4C
	public virtual void Start()
	{
	}

	// Token: 0x06000419 RID: 1049 RVA: 0x00027B50 File Offset: 0x00025D50
	public virtual void Update()
	{
	}

	// Token: 0x0600041A RID: 1050 RVA: 0x00027B54 File Offset: 0x00025D54
	public virtual void turnonwater()
	{
		if (this.mywater != null)
		{
			this.mywater.active = !this.mywater.active;
			if (!((AudioSource)this.mywater.GetComponent(typeof(AudioSource))).isPlaying && this.mywater.active)
			{
				((AudioSource)this.mywater.GetComponent(typeof(AudioSource))).Play();
			}
			if (((AudioSource)this.mywater.GetComponent(typeof(AudioSource))).isPlaying && !this.mywater.active)
			{
				((AudioSource)this.mywater.GetComponent(typeof(AudioSource))).Stop();
			}
		}
	}

	// Token: 0x0600041B RID: 1051 RVA: 0x00027C34 File Offset: 0x00025E34
	public virtual void Main()
	{
	}

	// Token: 0x0400051E RID: 1310
	public GameObject mywater;
}
