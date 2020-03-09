using System;
using UnityEngine;

// Token: 0x0200007E RID: 126
[Serializable]
public class MusicPlayerScript : MonoBehaviour
{
	// Token: 0x06000313 RID: 787 RVA: 0x0001B21C File Offset: 0x0001941C
	public virtual void Start()
	{
		UnityEngine.Object.DontDestroyOnLoad(this.transform.gameObject);
		this.queue = new AudioSource[10];
		this.currentvolume = new float[10];
		this.fadein = new float[10];
		this.looping = new bool[10];
		this.volume = new float[10];
		this.trackint = new int[10];
		this.setalltrackintnegative();
		this.index = 1;
		this.origcoors = this.transform.position;
	}

	// Token: 0x06000314 RID: 788 RVA: 0x0001B2A8 File Offset: 0x000194A8
	public virtual void Update()
	{
		this.switchtimer -= Time.deltaTime;
		if (this.switchtimer < (float)0)
		{
			this.switchtimer = (float)0;
		}
		this.changevolume();
		this.checkplaying();
		this.dosavestuff();
	}

	// Token: 0x06000315 RID: 789 RVA: 0x0001B2E4 File Offset: 0x000194E4
	public virtual void changevolume()
	{
		for (int i = 1; i < this.queue.Length - 1; i++)
		{
			if (this.queue[i])
			{
				float num = this.volume[i];
				if (i == this.index)
				{
					if (this.currentvolume[i] < num)
					{
						this.currentvolume[i] = this.currentvolume[i] + Time.deltaTime * this.fadein[i];
						if (this.currentvolume[i] > num)
						{
							this.currentvolume[i] = num;
						}
					}
				}
				else if (this.currentvolume[i] > (float)0)
				{
					this.currentvolume[i] = this.currentvolume[i] - Time.deltaTime * this.fadein[this.index];
					if (this.currentvolume[i] < (float)0)
					{
						this.currentvolume[i] = (float)0;
					}
				}
				float num2 = (float)0;
				if (this.name == "MusicPlayer")
				{
					num2 = (float)1 - this.mastermusicvolume;
				}
				this.queue[i].volume = this.currentvolume[i] - num2;
			}
		}
	}

	// Token: 0x06000316 RID: 790 RVA: 0x0001B404 File Offset: 0x00019604
	public virtual void checkplaying()
	{
		if (this.queue[this.index] && !this.queue[this.index].isPlaying && this.switchtimer <= (float)0 && !this.looping[this.index])
		{
			this.index++;
			this.constrainindex();
			if (this.queue[this.index])
			{
				this.queue[this.index].Play();
				this.switchtimer = 0.2f;
			}
		}
	}

	// Token: 0x06000317 RID: 791 RVA: 0x0001B4B0 File Offset: 0x000196B0
	public virtual void constrainindex()
	{
		if (this.index >= this.queue.Length - 1)
		{
			this.index = 1;
		}
	}

	// Token: 0x06000318 RID: 792 RVA: 0x0001B4D4 File Offset: 0x000196D4
	public virtual void dosavestuff()
	{
		string rhs = null;
		GameObject gameObject = GameObject.Find("SaveManager");
		if (gameObject)
		{
			this.sav = (SaveManagerScript)gameObject.GetComponent(typeof(SaveManagerScript));
			if (this.sav.dosave || this.sav.doload)
			{
				rhs = "?tag=" + this.transform.gameObject.name + this.origcoors.ToString();
			}
			if (this.sav.dosave)
			{
				int num = this.findnearestlooping();
				ES2.Save<int>(this.trackint[num], this.sav.filename + rhs + "1nd3x");
				ES2.Save<float>(this.volume[num], this.sav.filename + rhs + "v0lum3");
			}
			if (this.sav.doload && ES2.Exists(this.sav.filename + rhs + "1nd3x"))
			{
				this.currentvolume[2] = ES2.Load<float>(this.sav.filename + rhs + "v0lum3");
				this.trackint[2] = ES2.Load<int>(this.sav.filename + rhs + "1nd3x");
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.musicsource);
				this.queue[2] = (AudioSource)gameObject2.GetComponent(typeof(AudioSource));
				this.queue[2].clip = this.tracks[this.trackint[2]];
				this.queue[2].volume = this.currentvolume[2];
				this.queue[2].loop = true;
				this.queue[2].Play();
				this.looping[2] = true;
				this.index = 2;
				this.switchtimer = 0.2f;
			}
		}
	}

	// Token: 0x06000319 RID: 793 RVA: 0x0001B6E4 File Offset: 0x000198E4
	public virtual int findnearestlooping()
	{
		bool flag = false;
		int num = this.index;
		while (!flag)
		{
			if (this.looping[num])
			{
				return num;
			}
			num++;
		}
		return 0;
	}

	// Token: 0x0600031A RID: 794 RVA: 0x0001B730 File Offset: 0x00019930
	public virtual void setalltrackintnegative()
	{
		for (int i = 0; i < this.trackint.Length; i++)
		{
			this.trackint[i] = -1;
		}
	}

	// Token: 0x0600031B RID: 795 RVA: 0x0001B764 File Offset: 0x00019964
	public virtual void Main()
	{
	}

	// Token: 0x0400036D RID: 877
	public AudioSource[] queue;

	// Token: 0x0400036E RID: 878
	public float[] currentvolume;

	// Token: 0x0400036F RID: 879
	public int index;

	// Token: 0x04000370 RID: 880
	public float[] fadein;

	// Token: 0x04000371 RID: 881
	public bool[] looping;

	// Token: 0x04000372 RID: 882
	public float[] volume;

	// Token: 0x04000373 RID: 883
	public float switchtimer;

	// Token: 0x04000374 RID: 884
	public int[] trackint;

	// Token: 0x04000375 RID: 885
	public AudioClip[] tracks;

	// Token: 0x04000376 RID: 886
	public GameObject musicsource;

	// Token: 0x04000377 RID: 887
	public SaveManagerScript sav;

	// Token: 0x04000378 RID: 888
	public Vector3 origcoors;

	// Token: 0x04000379 RID: 889
	public float mastermusicvolume;
}
