using System;
using UnityEngine;

// Token: 0x0200007D RID: 125
[Serializable]
public class MusicNodeScript : MonoBehaviour
{
	// Token: 0x0600030B RID: 779 RVA: 0x0001AEB4 File Offset: 0x000190B4
	public MusicNodeScript()
	{
		this.destroy = true;
	}

	// Token: 0x0600030C RID: 780 RVA: 0x0001AEC4 File Offset: 0x000190C4
	public virtual void Start()
	{
		GameObject gameObject = GameObject.Find(this.mymastername);
		if (gameObject)
		{
			this.master = (MusicPlayerScript)gameObject.GetComponent(typeof(MusicPlayerScript));
		}
		else
		{
			this.master = null;
		}
	}

	// Token: 0x0600030D RID: 781 RVA: 0x0001AF10 File Offset: 0x00019110
	public virtual void Update()
	{
		if (this.dotriggerwhenobjectsdead && this.checkenemies())
		{
			this.trackstuff();
		}
		if (this.buttontrigger && ((ButtonScript)this.mybutton.GetComponent(typeof(ButtonScript))).buttonstate)
		{
			this.trackstuff();
		}
	}

	// Token: 0x0600030E RID: 782 RVA: 0x0001AF70 File Offset: 0x00019170
	public virtual void OnTriggerEnter(Collider hit)
	{
		if (hit.transform.gameObject.name == "Player" && !this.dotriggerwhenobjectsdead)
		{
			this.trackstuff();
		}
	}

	// Token: 0x0600030F RID: 783 RVA: 0x0001AFB0 File Offset: 0x000191B0
	public virtual bool checkenemies()
	{
		for (int i = 0; i < this.objects.Length; i++)
		{
			if (this.objects[i] != null)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06000310 RID: 784 RVA: 0x0001AFF4 File Offset: 0x000191F4
	public virtual void trackstuff()
	{
		if (this.master)
		{
			if (this.master.trackint[this.master.index] != this.mytrack[0])
			{
				int i = 0;
				int num = this.master.index + 1;
				if (num >= this.master.queue.Length - 1)
				{
					num = 1;
				}
				while (i < this.mytrack.Length)
				{
					this.master.index = this.master.index + 1;
					this.master.constrainindex();
					this.master.fadein[this.master.index] = this.fadeinmultiplier[i];
					this.master.looping[this.master.index] = this.looping[i];
					this.master.volume[this.master.index] = this.myvolume[i];
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.master.musicsource);
					this.master.queue[this.master.index] = (AudioSource)gameObject.GetComponent(typeof(AudioSource));
					this.master.queue[this.master.index].clip = this.master.tracks[this.mytrack[i]];
					this.master.trackint[this.master.index] = this.mytrack[i];
					this.master.queue[this.master.index].loop = this.looping[i];
					this.master.switchtimer = 0.2f;
					i++;
				}
				this.master.queue[num].Play();
				this.master.index = num;
			}
			if (this.destroy)
			{
				UnityEngine.Object.Destroy(this.transform.gameObject);
			}
		}
	}

	// Token: 0x06000311 RID: 785 RVA: 0x0001B210 File Offset: 0x00019410
	public virtual void Main()
	{
	}

	// Token: 0x04000362 RID: 866
	public bool destroy;

	// Token: 0x04000363 RID: 867
	public int[] mytrack;

	// Token: 0x04000364 RID: 868
	public float[] myvolume;

	// Token: 0x04000365 RID: 869
	public float[] fadeinmultiplier;

	// Token: 0x04000366 RID: 870
	public bool[] looping;

	// Token: 0x04000367 RID: 871
	public string mymastername;

	// Token: 0x04000368 RID: 872
	public bool dotriggerwhenobjectsdead;

	// Token: 0x04000369 RID: 873
	public GameObject[] objects;

	// Token: 0x0400036A RID: 874
	public bool buttontrigger;

	// Token: 0x0400036B RID: 875
	public GameObject mybutton;

	// Token: 0x0400036C RID: 876
	[HideInInspector]
	public MusicPlayerScript master;
}
