using System;
using UnityEngine;

// Token: 0x0200008B RID: 139
[Serializable]
public class PlaySoundObjectDestroyScript : MonoBehaviour
{
	// Token: 0x0600036D RID: 877 RVA: 0x00020160 File Offset: 0x0001E360
	public virtual void Start()
	{
	}

	// Token: 0x0600036E RID: 878 RVA: 0x00020164 File Offset: 0x0001E364
	public virtual void Update()
	{
		if (this.myobject == null && !this.playedsound)
		{
			this.delay -= Time.deltaTime;
			if (this.delay <= (float)0)
			{
				((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
				this.playedsound = true;
			}
		}
		if (this.delay < (float)0)
		{
			this.delay = (float)0;
		}
	}

	// Token: 0x0600036F RID: 879 RVA: 0x000201E4 File Offset: 0x0001E3E4
	public virtual void Main()
	{
	}

	// Token: 0x04000457 RID: 1111
	public GameObject myobject;

	// Token: 0x04000458 RID: 1112
	public float delay;

	// Token: 0x04000459 RID: 1113
	[HideInInspector]
	public bool playedsound;
}
