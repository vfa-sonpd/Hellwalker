using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200000A RID: 10
[Serializable]
public class ArrowHitScript : MonoBehaviour
{
	// Token: 0x0600003C RID: 60 RVA: 0x00004134 File Offset: 0x00002334
	public virtual void Start()
	{
		if (GameObject.Find("PERSIST"))
		{
			this.persist = (PersistScript)GameObject.Find("PERSIST").GetComponent(typeof(PersistScript));
		}
	}

	// Token: 0x0600003D RID: 61 RVA: 0x0000417C File Offset: 0x0000237C
	public virtual void Update()
	{
		if (((Rigidbody)this.GetComponent(typeof(Rigidbody))).velocity.magnitude < (float)5)
		{
			UnityEngine.Object.Destroy(this.transform.gameObject);
		}
	}

	// Token: 0x0600003E RID: 62 RVA: 0x000041C4 File Offset: 0x000023C4
	public virtual void OnTriggerEnter(Collider hit)
	{
		DestructibleObjectScript destructibleObjectScript = (DestructibleObjectScript)hit.transform.gameObject.GetComponent(typeof(DestructibleObjectScript));
		if (hit.transform.gameObject.tag == "ButtonTag" && ((ButtonScript)hit.transform.gameObject.GetComponent(typeof(ButtonScript))).canshoot)
		{
			ButtonScript buttonScript = (ButtonScript)hit.transform.gameObject.GetComponent(typeof(ButtonScript));
			buttonScript.dopress();
		}
		if (hit.transform.gameObject.tag == "EnemyTag" && this.persist)
		{
			this.persist.pacifistaward = false;
			this.persist.lowtechaward = false;
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
			destructibleObjectScript.doragdoll = true;
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.hitparticles, this.transform.position, Quaternion.identity);
			((ParticleSystem)gameObject.GetComponent(typeof(ParticleSystem))).startColor = destructibleObjectScript.damagecolor;
			AudioClip damagesound = ((DestructibleObjectScript)hit.transform.gameObject.GetComponent(typeof(DestructibleObjectScript))).damagesound;
			if (damagesound != null)
			{
				((AudioSource)gameObject.GetComponent(typeof(AudioSource))).clip = damagesound;
				if (hit.transform.gameObject.tag == "EnemyTag")
				{
					((AudioSource)gameObject.GetComponent(typeof(AudioSource))).spatialBlend = (float)0;
				}
				((AudioSource)gameObject.GetComponent(typeof(AudioSource))).pitch = UnityEngine.Random.Range(0.85f, 1.15f);
				((AudioSource)gameObject.GetComponent(typeof(AudioSource))).Play();
			}
		}
	}

	// Token: 0x0600003F RID: 63 RVA: 0x00004498 File Offset: 0x00002698
	public virtual void riccochet()
	{
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
	}

	// Token: 0x06000040 RID: 64 RVA: 0x000044B4 File Offset: 0x000026B4
	public virtual GameObject FindClosestEnemy(GameObject enem)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("EnemyTag");
		GameObject result = null;
		float num = float.PositiveInfinity;
		Vector3 position = enem.transform.position;
		int i = 0;
		GameObject[] array2 = array;
		int length = array2.Length;
		while (i < length)
		{
			if (array2[i] != enem)
			{
				float sqrMagnitude = (array2[i].transform.position - position).sqrMagnitude;
				if (sqrMagnitude < num)
				{
					result = array2[i];
					num = sqrMagnitude;
				}
			}
			i++;
		}
		return result;
	}

	// Token: 0x06000041 RID: 65 RVA: 0x00004548 File Offset: 0x00002748
	public virtual void Main()
	{
	}

	// Token: 0x04000040 RID: 64
	public bool enhancedrivet;

	// Token: 0x04000041 RID: 65
	public float mydamage;

	// Token: 0x04000042 RID: 66
	public GameObject hitparticles;

	// Token: 0x04000043 RID: 67
	[HideInInspector]
	public PersistScript persist;
}
