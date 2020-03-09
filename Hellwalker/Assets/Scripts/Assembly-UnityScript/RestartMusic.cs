using System;
using UnityEngine;

// Token: 0x02000093 RID: 147
[Serializable]
public class RestartMusic : MonoBehaviour
{
	// Token: 0x0600039C RID: 924 RVA: 0x00023650 File Offset: 0x00021850
	public virtual void Awake()
	{
		this.music = GameObject.Find("MusicPlayer");
		this.ambient = GameObject.Find("AmbientPlayer");
		if (this.music)
		{
			((MusicPlayerScript)this.music.GetComponent(typeof(MusicPlayerScript))).setalltrackintnegative();
		}
		if (this.ambient)
		{
			((MusicPlayerScript)this.ambient.GetComponent(typeof(MusicPlayerScript))).setalltrackintnegative();
		}
	}

	// Token: 0x0600039D RID: 925 RVA: 0x000236DC File Offset: 0x000218DC
	public virtual void Update()
	{
	}

	// Token: 0x0600039E RID: 926 RVA: 0x000236E0 File Offset: 0x000218E0
	public virtual void Main()
	{
	}

	// Token: 0x04000482 RID: 1154
	public GameObject music;

	// Token: 0x04000483 RID: 1155
	public GameObject ambient;
}
