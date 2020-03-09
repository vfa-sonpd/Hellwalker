using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000094 RID: 148
[Serializable]
public class RivetHitScript : MonoBehaviour
{
	// Token: 0x060003A0 RID: 928 RVA: 0x000236EC File Offset: 0x000218EC
	public virtual void Start()
	{
		if (GameObject.Find("PERSIST"))
		{
			this.persist = (PersistScript)GameObject.Find("PERSIST").GetComponent(typeof(PersistScript));
		}
		if (this.enhancedrivet)
		{
			this.transform.gameObject.layer = 16;
		}
	}

	// Token: 0x060003A1 RID: 929 RVA: 0x00023750 File Offset: 0x00021950
	public virtual void Update()
	{
		if (((Rigidbody)this.GetComponent(typeof(Rigidbody))).velocity.magnitude < (float)5)
		{
			UnityEngine.Object.Destroy(this.transform.gameObject);
		}
	}

	// Token: 0x060003A2 RID: 930 RVA: 0x00023798 File Offset: 0x00021998
	public virtual void OnCollisionEnter(Collision hit)
	{
		DestructibleObjectScript destructibleObjectScript = (DestructibleObjectScript)hit.transform.gameObject.GetComponent(typeof(DestructibleObjectScript));
		if (hit.transform.gameObject.tag == "EnemyTag" && this.persist)
		{
			this.persist.pacifistaward = false;
			this.persist.lowtechaward = false;
		}
		if (!this.enhancedrivet)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.explosion, this.transform.position, Quaternion.identity);
			((ExplosionScript)gameObject.GetComponent(typeof(ExplosionScript))).playerspawnedme = true;
		}
		if (destructibleObjectScript)
		{
			if (!((DestructibleObjectScript)hit.transform.gameObject.GetComponent(typeof(DestructibleObjectScript))).invincible)
			{
				float a = 0.8f;
				Color color = ((Image)GameObject.Find("Crosshair2").GetComponent(typeof(Image))).color;
				float num = color.a = a;
				Color color2 = ((Image)GameObject.Find("Crosshair2").GetComponent(typeof(Image))).color = color;
			}
			if (!this.enhancedrivet)
			{
				destructibleObjectScript.myhealth -= this.mydamage;
			}
			else
			{
				destructibleObjectScript.myhealth -= this.mydamage * (float)2;
			}
			destructibleObjectScript.dampen = false;
			destructibleObjectScript.doragdoll = false;
			if (hit.transform.gameObject.tag == "EnemyTag" && (BasicAIScript)hit.transform.gameObject.GetComponent(typeof(BasicAIScript)))
			{
				BasicAIScript basicAIScript = (BasicAIScript)hit.transform.gameObject.GetComponent(typeof(BasicAIScript));
				basicAIScript.MyTarget = GameObject.Find("Player");
			}
			UnityEngine.Object.Destroy(this.transform.gameObject);
		}
		else
		{
			UnityEngine.Object.Destroy(this.transform.gameObject);
		}
	}

	// Token: 0x060003A3 RID: 931 RVA: 0x000239D0 File Offset: 0x00021BD0
	public virtual void riccochet()
	{
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
	}

	// Token: 0x060003A4 RID: 932 RVA: 0x000239EC File Offset: 0x00021BEC
	public virtual void Main()
	{
	}

	// Token: 0x04000484 RID: 1156
	public bool enhancedrivet;

	// Token: 0x04000485 RID: 1157
	public GameObject explosion;

	// Token: 0x04000486 RID: 1158
	public float mydamage;

	// Token: 0x04000487 RID: 1159
	[HideInInspector]
	public PersistScript persist;
}
