using System;
using UnityEngine;

// Token: 0x02000008 RID: 8
[Serializable]
public class AmbientSoundPlayerScript : MonoBehaviour
{
	// Token: 0x06000032 RID: 50 RVA: 0x00003D80 File Offset: 0x00001F80
	public virtual void Start()
	{
		this.player1audio = (AudioSource)this.Player1.GetComponent(typeof(AudioSource));
		this.player2audio = (AudioSource)this.Player2.GetComponent(typeof(AudioSource));
	}

	// Token: 0x06000033 RID: 51 RVA: 0x00003DD0 File Offset: 0x00001FD0
	public virtual void Update()
	{
		if (this.player1audio.volume > this.player1target)
		{
			this.player1audio.volume = this.player1audio.volume - Time.deltaTime * this.crossfadespeed;
		}
		if (this.player1audio.volume < this.player1target)
		{
			this.player1audio.volume = this.player1audio.volume + Time.deltaTime * this.crossfadespeed;
		}
		if (this.player2audio.volume > this.player2target)
		{
			this.player2audio.volume = this.player2audio.volume - Time.deltaTime * this.crossfadespeed;
		}
		if (this.player2audio.volume < this.player2target)
		{
			this.player2audio.volume = this.player2audio.volume + Time.deltaTime * this.crossfadespeed;
		}
	}

	// Token: 0x06000034 RID: 52 RVA: 0x00003EC4 File Offset: 0x000020C4
	public virtual void setsound(AudioClip mysound, float myvolume)
	{
		if (!this.playertoggle)
		{
			if (((AudioSource)this.Player1.GetComponent(typeof(AudioSource))).clip != mysound)
			{
				this.playertoggle = true;
			}
		}
		else if (((AudioSource)this.Player2.GetComponent(typeof(AudioSource))).clip != mysound)
		{
			this.playertoggle = false;
		}
		if (!this.playertoggle)
		{
			if (((AudioSource)this.Player1.GetComponent(typeof(AudioSource))).clip != mysound)
			{
				((AudioSource)this.Player1.GetComponent(typeof(AudioSource))).clip = mysound;
				((AudioSource)this.Player1.GetComponent(typeof(AudioSource))).Play();
			}
			this.player1target = myvolume;
			this.player2target = (float)0;
		}
		if (this.playertoggle)
		{
			if (((AudioSource)this.Player2.GetComponent(typeof(AudioSource))).clip != mysound)
			{
				((AudioSource)this.Player2.GetComponent(typeof(AudioSource))).clip = mysound;
				((AudioSource)this.Player2.GetComponent(typeof(AudioSource))).Play();
			}
			this.player2target = myvolume;
			this.player1target = (float)0;
		}
	}

	// Token: 0x06000035 RID: 53 RVA: 0x0000404C File Offset: 0x0000224C
	public virtual void Main()
	{
	}

	// Token: 0x04000034 RID: 52
	public GameObject Player1;

	// Token: 0x04000035 RID: 53
	public GameObject Player2;

	// Token: 0x04000036 RID: 54
	public float crossfadespeed;

	// Token: 0x04000037 RID: 55
	[HideInInspector]
	public bool playertoggle;

	// Token: 0x04000038 RID: 56
	[HideInInspector]
	public float player1target;

	// Token: 0x04000039 RID: 57
	[HideInInspector]
	public float player2target;

	// Token: 0x0400003A RID: 58
	[HideInInspector]
	public AudioSource player1audio;

	// Token: 0x0400003B RID: 59
	[HideInInspector]
	public AudioSource player2audio;
}
