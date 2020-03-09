using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Boo.Lang;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

// Token: 0x02000070 RID: 112
[Serializable]
public class LogoStuffScript : MonoBehaviour
{
	// Token: 0x060002C4 RID: 708 RVA: 0x00019B4C File Offset: 0x00017D4C
	public virtual void Start()
	{
		//this.StartCoroutine(this.LoadAsync());
	}

	// Token: 0x060002C5 RID: 709 RVA: 0x00019B5C File Offset: 0x00017D5C
	public virtual void Update()
	{
		this.initialtimer -= Time.deltaTime;
		if (this.initialtimer <= (float)0)
		{
			this.initialtimer = (float)0;
			if (!this.didinitialstuff)
			{
				this.didinitialstuff = true;
				this.initialstuff();
			}
			this.holdtimer -= Time.deltaTime;
			if (this.holdtimer <= (float)0 && this.i < this.myfadetimes.Length)
			{
				float a = this.img.color.a - Time.deltaTime * this.myfadetimes[this.i];
				Color color = this.img.color;
				float num = color.a = a;
				Color color2 = this.img.color = color;
				this.holdtimer = (float)0;
			}
			if (this.img.color.a <= (float)0)
			{
				this.betweentimer -= Time.deltaTime;
				int num2 = 0;
				Color color3 = this.img.color;
				float num3 = color3.a = (float)num2;
				Color color4 = this.img.color = color3;
			}
			if (this.betweentimer <= (float)0)
			{
				this.switchtext();
			}
		}
	}

	// Token: 0x060002C6 RID: 710 RVA: 0x00019CBC File Offset: 0x00017EBC
	public virtual void switchtext()
	{
		if (this.i < this.myimages.Length)
		{
			this.myimages[this.i].Stop();
		}
		this.i++;
		if (this.i < this.myimages.Length)
		{
			this.holdtimer = this.myholdtimes[this.i];
			((Bloom)GameObject.Find("Main Camera").GetComponent(typeof(Bloom))).bloomIntensity = this.bloomamount[this.i];
			this.betweentimer = this.mybetweentimes[this.i];
			int num = 1;
			Color color = this.img.color;
			float num2 = color.a = (float)num;
			Color color2 = this.img.color = color;
			this.img.texture = this.myimages[this.i];
			this.myimages[this.i].Play();
		}
		else
		{
			this.logosdone = true;
		}
	}

	// Token: 0x060002C7 RID: 711 RVA: 0x00019DD0 File Offset: 0x00017FD0
	public virtual void initialstuff()
	{
		this.i = 0;
		this.holdtimer = this.myholdtimes[0];
		this.betweentimer = this.mybetweentimes[0];
		this.img = (RawImage)this.image.GetComponent(typeof(RawImage));
		((Bloom)GameObject.Find("Main Camera").GetComponent(typeof(Bloom))).bloomIntensity = this.bloomamount[0];
		int num = 1;
		Color color = this.img.color;
		float num2 = color.a = (float)num;
		Color color2 = this.img.color = color;
		this.img.texture = this.myimages[0];
		this.myimages[0].Play();
		AudioSource audioSource = (AudioSource)this.GetComponent(typeof(AudioSource));
		audioSource.clip = this.mysounds[this.i];
		audioSource.Play();
	}

    // Token: 0x060002C8 RID: 712 RVA: 0x00019ECC File Offset: 0x000180CC
 //   public virtual IEnumerator LoadAsync()
	//{

	//}

	// Token: 0x060002C9 RID: 713 RVA: 0x00019EDC File Offset: 0x000180DC
	public virtual void Main()
	{
	}

	// Token: 0x04000337 RID: 823
	public float initialtimer;

	// Token: 0x04000338 RID: 824
	[HideInInspector]
	public bool didinitialstuff;

	// Token: 0x04000339 RID: 825
	public MovieTexture[] myimages;

	// Token: 0x0400033A RID: 826
	public float[] myfadetimes;

	// Token: 0x0400033B RID: 827
	public float[] myholdtimes;

	// Token: 0x0400033C RID: 828
	public float[] mybetweentimes;

	// Token: 0x0400033D RID: 829
	public AudioClip[] mysounds;

	// Token: 0x0400033E RID: 830
	public float[] bloomamount;

	// Token: 0x0400033F RID: 831
	public GameObject image;

	// Token: 0x04000340 RID: 832
	[HideInInspector]
	public int i;

	// Token: 0x04000341 RID: 833
	[HideInInspector]
	public float holdtimer;

	// Token: 0x04000342 RID: 834
	[HideInInspector]
	public float betweentimer;

	// Token: 0x04000343 RID: 835
	[HideInInspector]
	public RawImage img;

	// Token: 0x04000344 RID: 836
	[HideInInspector]
	public bool logosdone;
}
