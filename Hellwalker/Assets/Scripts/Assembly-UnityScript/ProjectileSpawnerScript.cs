using System;
using UnityEngine;

// Token: 0x02000091 RID: 145
[Serializable]
public class ProjectileSpawnerScript : MonoBehaviour
{
	// Token: 0x06000393 RID: 915 RVA: 0x000233BC File Offset: 0x000215BC
	public virtual void Start()
	{
		this.statscript = (StatScript)GameObject.Find("StatObject").GetComponent(typeof(StatScript));
		this.aud = (AudioSource)this.GetComponent(typeof(AudioSource));
	}

	// Token: 0x06000394 RID: 916 RVA: 0x00023408 File Offset: 0x00021608
	public virtual void Update()
	{
		this.projectiletimer += Time.deltaTime;
		if (this.projectiletimer > this.projectiletime)
		{
			this.spawnprojectile();
			this.projectiletimer = (float)0;
			this.aud.Play();
		}
	}

	// Token: 0x06000395 RID: 917 RVA: 0x00023454 File Offset: 0x00021654
	public virtual void spawnprojectile()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.myprojectile, this.transform.position + this.transform.forward * this.projectileoffset, Quaternion.identity);
		gameObject.transform.forward = this.transform.forward;
		((Rigidbody)gameObject.GetComponent(typeof(Rigidbody))).velocity = gameObject.transform.forward * this.projectilespeed;
		gameObject.transform.gameObject.layer = 15;
		if ((BasicEnemyProjectileScript)gameObject.GetComponent(typeof(BasicEnemyProjectileScript)))
		{
			((BasicEnemyProjectileScript)gameObject.GetComponent(typeof(BasicEnemyProjectileScript))).whospawnedme = this.transform.gameObject;
			((BasicEnemyProjectileScript)gameObject.GetComponent(typeof(BasicEnemyProjectileScript))).mydamage = this.mydamage * this.statscript.difficultymultiplier;
			((BasicEnemyProjectileScript)gameObject.GetComponent(typeof(BasicEnemyProjectileScript))).mygooddamage = this.mygooddamage;
		}
	}

	// Token: 0x06000396 RID: 918 RVA: 0x00023580 File Offset: 0x00021780
	public virtual void Main()
	{
	}

	// Token: 0x04000476 RID: 1142
	public GameObject myprojectile;

	// Token: 0x04000477 RID: 1143
	public float projectileoffset;

	// Token: 0x04000478 RID: 1144
	public float projectilespeed;

	// Token: 0x04000479 RID: 1145
	public float mydamage;

	// Token: 0x0400047A RID: 1146
	public float mygooddamage;

	// Token: 0x0400047B RID: 1147
	public float projectiletime;

	// Token: 0x0400047C RID: 1148
	public float projectiletimer;

	// Token: 0x0400047D RID: 1149
	[HideInInspector]
	public StatScript statscript;

	// Token: 0x0400047E RID: 1150
	[HideInInspector]
	public AudioSource aud;
}
