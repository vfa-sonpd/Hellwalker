using System;
using UnityEngine;

// Token: 0x02000007 RID: 7
[Serializable]
public class AmbientSoundNodeScript : MonoBehaviour
{
	// Token: 0x0600002C RID: 44 RVA: 0x00003CB4 File Offset: 0x00001EB4
	public virtual void Start()
	{
		this.soundplayer = (AmbientSoundPlayerScript)GameObject.Find("AmbientSoundPlayer").GetComponent(typeof(AmbientSoundPlayerScript));
		this.activationtoggle = true;
	}

	// Token: 0x0600002D RID: 45 RVA: 0x00003CE4 File Offset: 0x00001EE4
	public virtual void Update()
	{
	}

	// Token: 0x0600002E RID: 46 RVA: 0x00003CE8 File Offset: 0x00001EE8
	public virtual void OnTriggerEnter(Collider hit)
	{
		if (hit.transform.gameObject.name == "Player" && this.activationtoggle)
		{
			this.soundplayer.setsound(this.mysound, this.myvolume);
			this.activationtoggle = false;
		}
	}

	// Token: 0x0600002F RID: 47 RVA: 0x00003D40 File Offset: 0x00001F40
	public virtual void OnTriggerExit(Collider hit)
	{
		if (hit.transform.gameObject.name == "Player")
		{
			this.activationtoggle = true;
		}
	}

	// Token: 0x06000030 RID: 48 RVA: 0x00003D74 File Offset: 0x00001F74
	public virtual void Main()
	{
	}

	// Token: 0x04000030 RID: 48
	public AudioClip mysound;

	// Token: 0x04000031 RID: 49
	public float myvolume;

	// Token: 0x04000032 RID: 50
	[HideInInspector]
	public AmbientSoundPlayerScript soundplayer;

	// Token: 0x04000033 RID: 51
	[HideInInspector]
	public bool activationtoggle;
}
