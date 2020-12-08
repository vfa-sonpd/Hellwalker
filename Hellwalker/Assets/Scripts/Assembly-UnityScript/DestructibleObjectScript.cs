using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200002C RID: 44
[Serializable]
public class DestructibleObjectScript : MonoBehaviour
{
	// Token: 0x0600010F RID: 271 RVA: 0x0000CE0C File Offset: 0x0000B00C
	public virtual void Start()
	{
		this.impactsoundtimer = (float)1;
		this.rigid = (Rigidbody)this.GetComponent(typeof(Rigidbody));
		this.aud = (AudioSource)this.GetComponent(typeof(AudioSource));

		this.origcoors = this.transform.position;
		this.healthlastframe = this.myhealth;
		this.orighealth = this.myhealth;
		if (GameObject.Find("PERSIST"))
		{
			this.persist = (PersistScript)GameObject.Find("PERSIST").GetComponent(typeof(PersistScript));
		}
	}

	// Token: 0x06000110 RID: 272 RVA: 0x0000CEDC File Offset: 0x0000B0DC
	public virtual void Update()
	{
		this.impactsoundtimer -= Time.deltaTime;
		if (this.impactsoundtimer < (float)0)
		{
			this.impactsoundtimer = (float)0;
		}
		this.dosavestuff();
		if (this.invincibilityframes > (float)0)
		{
			this.myhealth = this.orighealth;
			if ((BasicAIScript)this.GetComponent(typeof(BasicAIScript)))
			{
				((BasicAIScript)this.GetComponent(typeof(BasicAIScript))).MyTarget = GameObject.Find("Player");
			}
		}
		this.invincibilityframes -= Time.deltaTime;
		if (this.invincibilityframes < (float)0)
		{
			this.invincibilityframes = (float)0;
		}
		this.wasdamaged = false;
		if (this.healthlastframe > this.myhealth)
		{
			this.wasdamaged = true;
			this.damageamount = this.healthlastframe - this.myhealth;
		}
		this.healthlastframe = this.myhealth;
		if (this.wasdamaged && this.fireondamage && !this.inwater)
		{
			this.onfire = true;
		}
		this.catchfire();
		this.checkhealth();
		if (this.rigid && this.rigid.velocity.magnitude < (float)10)
		{
			this.candodamage = false;
		}
	}

	// Token: 0x06000111 RID: 273 RVA: 0x0000D040 File Offset: 0x0000B240
	public virtual void FixedUpdate()
	{
		if (this.rigid)
		{
			this.lastvelocity = this.rigid.velocity.magnitude;
		}
		if (this.playimpactsound)
		{
			if (this.aud && this.rigid && this.rigid.velocity.magnitude > 0.7f && Time.timeScale == (float)1)
			{
				this.aud.Play();
			}
			this.playimpactsound = false;
		}
		if (this.stopvelocity)
		{
			this.rigid.velocity = new Vector3((float)0, (float)0, (float)0);
			this.stopvelocity = false;
		}
	}

	// Token: 0x06000112 RID: 274 RVA: 0x0000D104 File Offset: 0x0000B304
	public virtual void bosshealthstuff()
	{
		BasicAIScript basicAIScript = (BasicAIScript)this.GetComponent(typeof(BasicAIScript));
		if (basicAIScript && basicAIScript.isboss)
		{
			((BossControllerScript)GameObject.Find("Player").GetComponent(typeof(BossControllerScript))).BossCurrentHealth = ((BossControllerScript)GameObject.Find("Player").GetComponent(typeof(BossControllerScript))).BossCurrentHealth - this.damageamount;
			this.damageamount = (float)0;
		}
	}

	// Token: 0x06000113 RID: 275 RVA: 0x0000D194 File Offset: 0x0000B394
	public virtual void checkhealth()
	{
		if (this.invincible)
		{
			this.myhealth = (float)100;
		}
		if (this.invincibilityframes > (float)0)
		{
			this.myhealth = this.orighealth;
		}
		if (this.myhealth <= (float)0)
		{
			this.die();
		}
	}

	// Token: 0x06000114 RID: 276 RVA: 0x0000D1E4 File Offset: 0x0000B3E4
	public virtual void die()
	{
		if (this.transform.gameObject.tag == "EnemyTag")
		{
			//StatScript statScript = (StatScript)GameObject.Find("StatObject").GetComponent(typeof(StatScript));
			//float num = (float)10 * Mathf.Ceil(statScript.multiplier) * Mathf.Clamp(statScript.difficultymultiplier, 0.001f, (float)4);
			//statScript.score = (int)((float)statScript.score + num);
			//statScript.multiplier += (float)statScript.addtomultiplier;
			//statScript.multiplier = Mathf.Ceil(statScript.multiplier);
			//if (statScript.endlessarena)
			//{
			//	statScript.spawnpoints(this.transform.position, num);
			//}
		}
		this.transform.gameObject.active = false;
		//UnityEngine.Object.Destroy(this.transform.gameObject);
		if (this.spawnitems)
		{
			this.doitemspawn();
		}
		if (this.spawnenemies)
		{
			this.doenemyspawn();
		}
		if ((TreasureChestScript)this.GetComponent(typeof(TreasureChestScript)))
		{
			TreasureChestScript treasureChestScript = (TreasureChestScript)this.GetComponent(typeof(TreasureChestScript));
			if (((NameDisplayScript)this.GetComponent(typeof(NameDisplayScript))).myname != string.Empty)
			{
				UnityEngine.Object.Instantiate<GameObject>(treasureChestScript.myitem, this.transform.position, treasureChestScript.myitem.transform.rotation);
			}
		}
		if (this.firesystem != null)
		{
			UnityEngine.Object.Destroy(this.firesystem);
		}
		if (this.doragdoll && this.allowragdoll)
		{
			this.ragdollfunction();
		}
		else
		{
			this.gib();
		}
	}

	// Token: 0x06000115 RID: 277 RVA: 0x0000D3B0 File Offset: 0x0000B5B0
	public virtual void gib()
	{
        GibFactory<GibView> factory = new GibFactory<GibView>();
        GibContext context = new GibContext(this.transform.position, Quaternion.Euler((float)-90, (float)0, (float)0), this.dampenvelocity);
        factory.Create(context);
    }
    //public virtual void ragdollfunction()
    //{
    //    GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ragdoll, this.transform.position, Quaternion.identity);
    //    gameObject.transform.eulerAngles = this.transform.eulerAngles;
    //    if ((Rigidbody)this.GetComponent(typeof(Rigidbody)))
    //    {
    //        ((Rigidbody)this.GetComponent(typeof(Rigidbody))).velocity = this.ragdollvelocity;
    //    }
    //    int i = 0;
    //    Component[] componentsInChildren = gameObject.GetComponentsInChildren(typeof(Rigidbody));
    //    int length = componentsInChildren.Length;
    //    while (i < length)
    //    {
    //        ((Rigidbody)componentsInChildren[i]).velocity = this.ragdollvelocity;
    //        i++;
    //    }
    //    if ((AudioSource)gameObject.GetComponent(typeof(AudioSource)))
    //    {
    //        ((AudioSource)gameObject.GetComponent(typeof(AudioSource))).Play();
    //    }
    //}
    // Token: 0x06000116 RID: 278 RVA: 0x0000D40C File Offset: 0x0000B60C
    public virtual void ragdollfunction()
	{
        SoldierRagdollFactory<SoldierRagdollView> ragdollFactory = new SoldierRagdollFactory<SoldierRagdollView>();

        SoldierRagdollContext context = new SoldierRagdollContext(this.transform.position, transform.rotation, this.ragdollvelocity);

        ragdollFactory.Create(context);
    }

	// Token: 0x06000117 RID: 279 RVA: 0x0000D504 File Offset: 0x0000B704
	public virtual void catchfire()
	{
		if (this.onfire)
		{
			this.myhealth -= this.firedamagepersecond * Time.deltaTime;
			if (this.firesystem == null)
			{
				this.firesystem = UnityEngine.Object.Instantiate<GameObject>(this.fire, this.transform.position, Quaternion.Euler((float)-90, (float)0, (float)0));
			}
			else
			{
				this.firesystem.transform.position = this.transform.position;
			}
		}
	}

	// Token: 0x06000118 RID: 280 RVA: 0x0000D590 File Offset: 0x0000B790
	public virtual void OnCollisionEnter(Collision hit)
	{
		DestructibleObjectScript destructibleObjectScript = (DestructibleObjectScript)hit.transform.gameObject.GetComponent(typeof(DestructibleObjectScript));
		if (this.candodamage)
		{
			this.myhealth -= this.impactdamage;
			if (this.impactsoundtimer <= (float)0 && this.transform.gameObject.layer != 23)
			{
				this.playimpactsound = true;
			}
			this.impactsoundtimer = 0.5f;
		}
		if (destructibleObjectScript != null)
		{
			if (this.candodamage)
			{
				if (!((DestructibleObjectScript)hit.transform.gameObject.GetComponent(typeof(DestructibleObjectScript))).invincible)
				{
					float a = 0.8f;
					Color color = ((Image)GameObject.Find("Crosshair2").GetComponent(typeof(Image))).color;
					float num = color.a = a;
					Color color2 = ((Image)GameObject.Find("Crosshair2").GetComponent(typeof(Image))).color = color;
				}
				destructibleObjectScript.myhealth -= this.throwdamage;
				destructibleObjectScript.doragdoll = false;
				if (this.rigid)
				{
					this.stopvelocity = true;
					destructibleObjectScript.ragdollvelocity = ((Rigidbody)this.GetComponent(typeof(Rigidbody))).velocity / (float)2;
				}
				if (hit.transform.gameObject.tag == "EnemyTag" && this.persist)
				{
					this.persist.pacifistaward = false;
				}
				this.candodamage = false;
			}
			if (this.onfire && destructibleObjectScript.allowfire)
			{
				destructibleObjectScript.onfire = true;
			}
		}
	}

	// Token: 0x06000119 RID: 281 RVA: 0x0000D770 File Offset: 0x0000B970
	public virtual void doitemspawn()
	{
		int num = UnityEngine.Random.Range(0, this.itemstospawn.Length);
		if (this.itemstospawn[num] != null)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.itemstospawn[num], this.transform.position + new Vector3((float)0, this.spawnoffset, (float)0), this.itemstospawn[num].transform.rotation);
		}
	}

	// Token: 0x0600011A RID: 282 RVA: 0x0000D7E4 File Offset: 0x0000B9E4
	public virtual void doenemyspawn()
	{
		int i = 0;
		Vector3 b = default(Vector3);
		while (i < this.numenemies)
		{
			i++;
			b = new Vector3(UnityEngine.Random.Range(-this.maxenemyoffset.x, this.maxenemyoffset.x), UnityEngine.Random.Range(-this.maxenemyoffset.y, this.maxenemyoffset.y), UnityEngine.Random.Range(-this.maxenemyoffset.z, this.maxenemyoffset.z));
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.enemytospawn, this.transform.position + b, Quaternion.identity);
			if ((BasicAIScript)gameObject.GetComponent(typeof(BasicAIScript)))
			{
				((BasicAIScript)gameObject.GetComponent(typeof(BasicAIScript))).AMAWAKE = true;
				((DestructibleObjectScript)gameObject.GetComponent(typeof(DestructibleObjectScript))).invincibilityframes = 0.7f;
			}
			if ((Rigidbody)gameObject.GetComponent(typeof(Rigidbody)))
			{
				((Rigidbody)gameObject.GetComponent(typeof(Rigidbody))).velocity = new Vector3(UnityEngine.Random.Range(-6f, 6f), UnityEngine.Random.Range(-6f, 6f), UnityEngine.Random.Range(-6f, 6f));
			}
		}
	}

	// Token: 0x0600011B RID: 283 RVA: 0x0000D954 File Offset: 0x0000BB54
	public virtual void OnTriggerStay(Collider hit)
	{
		if (hit.transform.gameObject.layer == 20 || hit.transform.gameObject.layer == 21)
		{
			this.inwater = true;
			if (this.onfire)
			{
				UnityEngine.Object.Destroy(this.firesystem);
				this.firesystem = null;
				this.onfire = false;
			}
		}
	}

	// Token: 0x0600011C RID: 284 RVA: 0x0000D9BC File Offset: 0x0000BBBC
	public virtual void OnTriggerExit(Collider hit)
	{
		if (hit.transform.gameObject.layer == 20 || hit.transform.gameObject.layer == 21)
		{
			this.inwater = false;
		}
	}

	// Token: 0x0600011D RID: 285 RVA: 0x0000DA00 File Offset: 0x0000BC00
	public virtual void dosavestuff()
	{
        return; 
		string rhs = null;
		if (this.sav.dosave || this.sav.doload)
		{
			rhs = "?tag=" + this.transform.gameObject.name + this.origcoors.ToString();
		}
		if (this.sav.dosave)
		{
			ES2.Save<Transform>(this.transform, this.sav.filename + rhs + "tr4n5orm");
			ES2.Save<float>(this.myhealth, this.sav.filename + rhs + "he41th");
			ES2.Save<int>(this.sav.boolToInt(this.onfire), this.sav.filename + rhs + "onf1re");
		}
		if (this.sav.doload)
		{
			if (ES2.Exists(this.sav.filename + rhs + "tr4n5orm"))
			{
				ES2.Load<Transform>(this.sav.filename + rhs + "tr4n5orm", this.transform);
				this.myhealth = ES2.Load<float>(this.sav.filename + rhs + "he41th");
				this.onfire = this.sav.intToBool(ES2.Load<int>(this.sav.filename + rhs + "onf1re"));
			}
			else
			{
				UnityEngine.Object.Destroy(this.transform.gameObject);
			}
		}
	}

	// Token: 0x0600011E RID: 286 RVA: 0x0000DBB0 File Offset: 0x0000BDB0
	public virtual void Main()
	{
	}

	// Token: 0x040001D5 RID: 469
	public bool spawnitems;

	// Token: 0x040001D6 RID: 470
	public GameObject[] itemstospawn;

	// Token: 0x040001D7 RID: 471
	public bool spawnenemies;

	// Token: 0x040001D8 RID: 472
	public GameObject enemytospawn;

	// Token: 0x040001D9 RID: 473
	public Vector3 maxenemyoffset;

	// Token: 0x040001DA RID: 474
	public int numenemies;

	// Token: 0x040001DB RID: 475
	public float spawnoffset;

	// Token: 0x040001DC RID: 476
	public bool invincible;

	// Token: 0x040001DD RID: 477
	public bool onfire;

	// Token: 0x040001DE RID: 478
	public bool fireondamage;

	// Token: 0x040001DF RID: 479
	public float firedamagepersecond;

	// Token: 0x040001E0 RID: 480
	public float impactdamage;

	// Token: 0x040001E1 RID: 481
	public float throwdamage;

	// Token: 0x040001E2 RID: 482
	public Color damagecolor;

	// Token: 0x040001E3 RID: 483
	public AudioClip damagesound;

	// Token: 0x040001E4 RID: 484
	public float myhealth;

	// Token: 0x040001E5 RID: 485
	[HideInInspector]
	public bool dampen;

	// Token: 0x040001E6 RID: 486
	public float dampenvelocity;

	// Token: 0x040001E8 RID: 488
	public GameObject ragdoll;

	// Token: 0x040001E9 RID: 489
	public GameObject fire;

	// Token: 0x040001EA RID: 490
	[HideInInspector]
	public bool doragdoll;

	// Token: 0x040001EB RID: 491
	public bool allowragdoll;

	// Token: 0x040001EC RID: 492
	public bool allowfire;

	// Token: 0x040001ED RID: 493
	public Vector3 ragdollvelocity;

	// Token: 0x040001EE RID: 494
	[HideInInspector]
	public GameObject firesystem;

	// Token: 0x040001EF RID: 495
	[HideInInspector]
	public bool candodamage;

	// Token: 0x040001F0 RID: 496
	[HideInInspector]
	public bool wasdamaged;

	// Token: 0x040001F1 RID: 497
	[HideInInspector]
	public float damageamount;

	// Token: 0x040001F2 RID: 498
	[HideInInspector]
	public float healthlastframe;

	// Token: 0x040001F3 RID: 499
	[HideInInspector]
	public float orighealth;

	// Token: 0x040001F4 RID: 500
	[HideInInspector]
	public bool inwater;

	// Token: 0x040001F5 RID: 501
	[HideInInspector]
	public float invincibilityframes;

	// Token: 0x040001F6 RID: 502
	[HideInInspector]
	public SaveManagerScript sav;

	// Token: 0x040001F7 RID: 503
	[HideInInspector]
	public Vector3 origcoors;

	// Token: 0x040001F8 RID: 504
	[HideInInspector]
	public PersistScript persist;

	// Token: 0x040001F9 RID: 505
	[HideInInspector]
	public AudioSource aud;

	// Token: 0x040001FA RID: 506
	[HideInInspector]
	public Rigidbody rigid;

	// Token: 0x040001FB RID: 507
	[HideInInspector]
	public float lastvelocity;

	// Token: 0x040001FC RID: 508
	[HideInInspector]
	public bool playimpactsound;

	// Token: 0x040001FD RID: 509
	[HideInInspector]
	public float impactsoundtimer;

	// Token: 0x040001FE RID: 510
	[HideInInspector]
	public bool stopvelocity;
}
