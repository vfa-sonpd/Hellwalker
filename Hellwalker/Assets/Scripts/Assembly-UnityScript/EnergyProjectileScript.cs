using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200003F RID: 63
[Serializable]
public class EnergyProjectileScript : MonoBehaviour
{
	// Token: 0x06000175 RID: 373 RVA: 0x0000F4DC File Offset: 0x0000D6DC
	public virtual void Start()
	{
	}

	// Token: 0x06000176 RID: 374 RVA: 0x0000F4E0 File Offset: 0x0000D6E0
	public virtual void Update()
	{
		if (((Rigidbody)this.GetComponent(typeof(Rigidbody))).velocity.magnitude < (float)30)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.hitparticles, this.transform.position, Quaternion.identity);
			((ParticleSystem)gameObject.GetComponent(typeof(ParticleSystem))).startColor = new Color(0.6f, (float)1, 0.4f);
			UnityEngine.Object.Destroy(this.transform.gameObject);
		}
	}

	// Token: 0x06000177 RID: 375 RVA: 0x0000F570 File Offset: 0x0000D770
	public virtual void OnCollisionEnter(Collision hit)
	{
		DestructibleObjectScript destructibleObjectScript = (DestructibleObjectScript)hit.transform.gameObject.GetComponent(typeof(DestructibleObjectScript));
		if (destructibleObjectScript)
		{
			if (!((DestructibleObjectScript)hit.transform.gameObject.GetComponent(typeof(DestructibleObjectScript))).invincible)
			{
				float a = 0.8f;
				Color color = ((Image)GameObject.Find("Crosshair2").GetComponent(typeof(Image))).color;
				float num = color.a = a;
				Color color2 = ((Image)GameObject.Find("Crosshair2").GetComponent(typeof(Image))).color = color;
			}
			destructibleObjectScript.myhealth -= this.mydamage;
			destructibleObjectScript.dampen = false;
			destructibleObjectScript.doragdoll = false;
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.hitparticles, this.transform.position, Quaternion.identity);
			((ParticleSystem)gameObject.GetComponent(typeof(ParticleSystem))).startColor = destructibleObjectScript.damagecolor;
			AudioClip damagesound = ((DestructibleObjectScript)hit.transform.gameObject.GetComponent(typeof(DestructibleObjectScript))).damagesound;
			if (damagesound != null)
			{
				((AudioSource)gameObject.GetComponent(typeof(AudioSource))).clip = damagesound;
				((AudioSource)gameObject.GetComponent(typeof(AudioSource))).pitch = UnityEngine.Random.Range(0.85f, 1.15f);
				((AudioSource)gameObject.GetComponent(typeof(AudioSource))).Play();
			}
			if (hit.transform.gameObject.tag == "EnemyTag" && (BasicAIScript)hit.transform.gameObject.GetComponent(typeof(BasicAIScript)))
			{
				BasicAIScript basicAIScript = (BasicAIScript)hit.transform.gameObject.GetComponent(typeof(BasicAIScript));
				basicAIScript.MyTarget = GameObject.Find("Player");
			}
			UnityEngine.Object.Destroy(this.transform.gameObject);
		}
		else
		{
			this.riccochet();
		}
	}

	// Token: 0x06000178 RID: 376 RVA: 0x0000F7BC File Offset: 0x0000D9BC
	public virtual void riccochet()
	{
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
	}

	// Token: 0x06000179 RID: 377 RVA: 0x0000F7D8 File Offset: 0x0000D9D8
	public virtual void Main()
	{
	}

	// Token: 0x0400024B RID: 587
	public float mydamage;

	// Token: 0x0400024C RID: 588
	public GameObject hitparticles;
}
