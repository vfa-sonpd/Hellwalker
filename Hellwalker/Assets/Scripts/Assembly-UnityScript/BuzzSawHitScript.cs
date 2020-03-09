using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000018 RID: 24
[Serializable]
public class BuzzSawHitScript : MonoBehaviour
{
	// Token: 0x060000AC RID: 172 RVA: 0x0000B6A0 File Offset: 0x000098A0
	public virtual void Start()
	{
		if (this.enhancedbuzzsaw)
		{
			((Rigidbody)this.GetComponent(typeof(Rigidbody))).useGravity = false;
		}
		GameObject gameObject = GameObject.Find("Player");
		float num = gameObject.transform.InverseTransformDirection(((CharacterController)gameObject.GetComponent(typeof(CharacterController))).velocity).z;
		if (num < (float)0)
		{
			num = (float)0;
		}
		Vector3 b = num * gameObject.transform.forward;
		((Rigidbody)this.GetComponent(typeof(Rigidbody))).velocity = ((Rigidbody)this.GetComponent(typeof(Rigidbody))).velocity + b;
	}

	// Token: 0x060000AD RID: 173 RVA: 0x0000B764 File Offset: 0x00009964
	public virtual void Update()
	{
		this.alivetime -= Time.deltaTime;
		if (this.alivetime < (float)0)
		{
			this.die();
		}
		if (this.detonate)
		{
			this.die();
		}
	}

	// Token: 0x060000AE RID: 174 RVA: 0x0000B7A8 File Offset: 0x000099A8
	public virtual void OnCollisionEnter(Collision hit)
	{
		DestructibleObjectScript exists = (DestructibleObjectScript)hit.transform.gameObject.GetComponent(typeof(DestructibleObjectScript));
		if (((Rigidbody)this.GetComponent(typeof(Rigidbody))).velocity.magnitude >= (float)1)
		{
			if (hit.transform.gameObject.tag == "EnemyTag")
			{
			}
			if (hit.transform.gameObject.name == "Player")
			{
			}
			if (exists)
			{
				if (!((DestructibleObjectScript)hit.transform.gameObject.GetComponent(typeof(DestructibleObjectScript))).invincible)
				{
					float a = 0.8f;
					Color color = ((Image)GameObject.Find("Crosshair2").GetComponent(typeof(Image))).color;
					float num = color.a = a;
					Color color2 = ((Image)GameObject.Find("Crosshair2").GetComponent(typeof(Image))).color = color;
				}
				if (hit.transform.gameObject.tag == "EnemyTag" && (BasicAIScript)hit.transform.gameObject.GetComponent(typeof(BasicAIScript)))
				{
					BasicAIScript basicAIScript = (BasicAIScript)hit.transform.gameObject.GetComponent(typeof(BasicAIScript));
					basicAIScript.MyTarget = GameObject.Find("Player");
				}
			}
			else
			{
				this.riccochet();
			}
		}
	}

	// Token: 0x060000AF RID: 175 RVA: 0x0000B95C File Offset: 0x00009B5C
	public virtual void riccochet()
	{
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
	}

	// Token: 0x060000B0 RID: 176 RVA: 0x0000B978 File Offset: 0x00009B78
	public virtual void die()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.explosion, this.transform.position, Quaternion.identity);
		((ExplosionScript)gameObject.GetComponent(typeof(ExplosionScript))).playerspawnedme = true;
		UnityEngine.Object.Destroy(this.transform.gameObject);
	}

	// Token: 0x060000B1 RID: 177 RVA: 0x0000B9CC File Offset: 0x00009BCC
	public virtual GameObject FindClosestEnemy()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("EnemyTag");
		GameObject result = null;
		float num = float.PositiveInfinity;
		Vector3 position = this.transform.position;
		int i = 0;
		GameObject[] array2 = array;
		int length = array2.Length;
		while (i < length)
		{
			float sqrMagnitude = (array2[i].transform.position - position).sqrMagnitude;
			if (sqrMagnitude < num)
			{
				result = array2[i];
				num = sqrMagnitude;
			}
			i++;
		}
		return result;
	}

	// Token: 0x060000B2 RID: 178 RVA: 0x0000BA50 File Offset: 0x00009C50
	public virtual void Main()
	{
	}

	// Token: 0x04000194 RID: 404
	public bool enhancedbuzzsaw;

	// Token: 0x04000195 RID: 405
	public GameObject explosion;

	// Token: 0x04000196 RID: 406
	public GameObject hitparticles;

	// Token: 0x04000197 RID: 407
	public float alivetime;

	// Token: 0x04000198 RID: 408
	public float mydamage;

	// Token: 0x04000199 RID: 409
	[HideInInspector]
	public bool detonate;
}
