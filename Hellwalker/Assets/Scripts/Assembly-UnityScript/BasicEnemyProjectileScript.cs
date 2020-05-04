using System;
using UnityEngine;

// Token: 0x0200000F RID: 15
[Serializable]
public class BasicEnemyProjectileScript : MonoBehaviour
{
	// Token: 0x0600006C RID: 108 RVA: 0x00009EE4 File Offset: 0x000080E4
	public virtual void Start()
	{
		this.rigid = (Rigidbody)this.GetComponent(typeof(Rigidbody));
	}

	// Token: 0x0600006D RID: 109 RVA: 0x00009F04 File Offset: 0x00008104
	public virtual void Update()
	{
		if (this.isguided)
		{
			this.mytarget = GameObject.Find("MainCamera").transform.position;
			Vector3 target = this.mytarget - this.transform.position;
			Vector3 forward = this.transform.forward;
			Vector3 velocity = ((Rigidbody)this.GetComponent(typeof(Rigidbody))).velocity;
			this.transform.forward = Vector3.RotateTowards(this.transform.forward, target, Time.deltaTime * this.guidedturnspeed, (float)0);
			this.rigid.velocity = this.transform.forward.normalized * this.origspeed;
		}
	}

	// Token: 0x0600006E RID: 110 RVA: 0x00009FC8 File Offset: 0x000081C8
	public virtual void OnCollisionEnter(Collision hit)
	{
		if (hit.transform.gameObject.name == "Player")
		{
			((PlayerHealthManagement)hit.transform.gameObject.GetComponent(typeof(PlayerHealthManagement))).takedamage(this.mydamage);
		}
		if (hit.transform.gameObject != this.whospawnedme)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.hitparticles, this.transform.position, Quaternion.identity);
			((ParticleSystem)gameObject.GetComponent(typeof(ParticleSystem))).startColor = this.defaultparticlecolor;
			if (this.whospawnedme != null)
			{
				gameObject.transform.LookAt(this.whospawnedme.transform);
			}
			DestructibleObjectScript destructibleObjectScript = (DestructibleObjectScript)hit.transform.gameObject.GetComponent(typeof(DestructibleObjectScript));
			AudioClip damagesound = null;
			if (destructibleObjectScript)
			{
				destructibleObjectScript.myhealth -= this.mygooddamage;
				destructibleObjectScript.dampen = false;
				destructibleObjectScript.doragdoll = true;
				((ParticleSystem)gameObject.GetComponent(typeof(ParticleSystem))).startColor = destructibleObjectScript.damagecolor;
				damagesound = ((DestructibleObjectScript)hit.transform.gameObject.GetComponent(typeof(DestructibleObjectScript))).damagesound;
			}
			if (damagesound == null)
			{
				damagesound = this.defaultsound;
				((AudioSource)gameObject.GetComponent(typeof(AudioSource))).volume = (float)1;
			}
			if (damagesound != null && hit.transform.gameObject.layer != 10)
			{
				((AudioSource)gameObject.GetComponent(typeof(AudioSource))).clip = damagesound;
				((AudioSource)gameObject.GetComponent(typeof(AudioSource))).pitch = UnityEngine.Random.Range(0.85f, 1.15f);
				((AudioSource)gameObject.GetComponent(typeof(AudioSource))).Play();
			}
			if (hit.transform.gameObject.layer == 10)
			{
				((ParticleSystem)gameObject.GetComponent(typeof(ParticleSystem))).startColor = new Color(0.7f, (float)0, (float)0);
			}
			if (hit.transform.gameObject.tag == "EnemyTag")
			{
                try
                {
                    print("alachol 3");
                    BasicAIScript basicAIScript = (BasicAIScript)hit.transform.gameObject.GetComponent(typeof(BasicAIScript));
                    basicAIScript.MyTarget = this.whospawnedme;
                }
                catch(Exception e)
                {
                    Debug.Log("no BasicAIScript!");
                }

			}
			UnityEngine.Object.Destroy(this.transform.gameObject);
		}
	}

	// Token: 0x0600006F RID: 111 RVA: 0x0000A278 File Offset: 0x00008478
	public virtual void Main()
	{
	}

	// Token: 0x04000152 RID: 338
	public float mydamage;

	// Token: 0x04000153 RID: 339
	public float mygooddamage;

	// Token: 0x04000154 RID: 340
	public bool isguided;

	// Token: 0x04000155 RID: 341
	public float guidedturnspeed;

	// Token: 0x04000156 RID: 342
	public GameObject hitparticles;

	// Token: 0x04000157 RID: 343
	public Color defaultparticlecolor;

	// Token: 0x04000158 RID: 344
	public AudioClip defaultsound;

	// Token: 0x04000159 RID: 345
	[HideInInspector]
	public float origspeed;

	// Token: 0x0400015A RID: 346
	[HideInInspector]
	public Vector3 mytarget;

	// Token: 0x0400015B RID: 347
	[HideInInspector]
	public GameObject whospawnedme;

	// Token: 0x0400015C RID: 348
	[HideInInspector]
	public Rigidbody rigid;
}
