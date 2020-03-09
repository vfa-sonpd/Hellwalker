using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000040 RID: 64
[Serializable]
public class ExplosionScript : MonoBehaviour
{
	// Token: 0x0600017B RID: 379 RVA: 0x0000F7E4 File Offset: 0x0000D9E4
	public virtual void Start()
	{
		this.persist = (PersistScript)GameObject.Find("PERSIST").GetComponent(typeof(PersistScript));
	}

	// Token: 0x0600017C RID: 380 RVA: 0x0000F818 File Offset: 0x0000DA18
	public virtual void Update()
	{
		((Light)this.GetComponent(typeof(Light))).range = ((Light)this.GetComponent(typeof(Light))).range - Time.deltaTime * this.lightdecay;
		this.collidertime -= Time.deltaTime;
		if (this.collidertime < (float)0)
		{
			((SphereCollider)this.GetComponent(typeof(SphereCollider))).enabled = false;
		}
	}

	// Token: 0x0600017D RID: 381 RVA: 0x0000F8A0 File Offset: 0x0000DAA0
	public virtual void OnTriggerEnter(Collider hit)
	{
		if (!this.fireexplosion)
		{
			int layer = hit.transform.gameObject.layer;
			hit.transform.gameObject.layer = 11;
			if (hit.transform.gameObject.tag == "ExplosionDestroyWallTag")
			{
				((ExplosionWallScript)hit.transform.gameObject.GetComponent(typeof(ExplosionWallScript))).die();
			}
			if (!Physics.Raycast(this.transform.position, (hit.transform.position - this.transform.position).normalized, Vector3.Distance(this.transform.position, hit.transform.position), this.explosionblockinglayers))
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
					destructibleObjectScript.candodamage = true;
				}
				Rigidbody rigidbody = (Rigidbody)hit.transform.gameObject.GetComponent(typeof(Rigidbody));
				if (rigidbody)
				{
					rigidbody.velocity = (hit.transform.position - this.transform.position).normalized * (float)20;
				}
				if (hit.transform.gameObject.tag == "EnemyTag" && this.persist)
				{
					this.persist.pacifistaward = false;
					if (this.playerspawnedme)
					{
						this.persist.lowtechaward = false;
					}
				}
				if (layer == 10)
				{
					MyControllerScript myControllerScript = (MyControllerScript)hit.transform.gameObject.GetComponent(typeof(MyControllerScript));
					RaycastHit raycastHit = default(RaycastHit);
					if (!Physics.Raycast(hit.transform.position, new Vector3((float)0, (float)-1, (float)0), out raycastHit, 1.1f, this.jumplayers))
					{
						Vector3 b = new Vector3(hit.transform.position.x, hit.transform.position.y - ((CharacterController)hit.transform.gameObject.GetComponent(typeof(CharacterController))).height / (float)2, hit.transform.position.z);
						float num2 = ((float)2 - Vector3.Distance(this.transform.position, b)) * this.rocketjumpamount * (float)3;
						if (num2 < 0.4f)
						{
							num2 = 0.4f;
						}
						Vector3 b2 = (hit.transform.position + new Vector3((float)0, (float)1, (float)0) - this.transform.position).normalized * num2;
						myControllerScript.gravityforce = b2.y;
						b2.y = (float)0;
						myControllerScript.realrocketjump += b2;
						myControllerScript.CrouchState = false;
					}
					((PlayerHealthManagement)hit.transform.gameObject.GetComponent(typeof(PlayerHealthManagement))).takedamage(this.mydamage / (float)25);
					((LadderUseScript)hit.transform.gameObject.GetComponent(typeof(LadderUseScript))).ignoreladdertoggle = true;
				}
			}
			hit.transform.gameObject.layer = layer;
		}
		else
		{
			DestructibleObjectScript destructibleObjectScript2 = (DestructibleObjectScript)hit.transform.gameObject.GetComponent(typeof(DestructibleObjectScript));
			if (destructibleObjectScript2 && destructibleObjectScript2.allowfire)
			{
				destructibleObjectScript2.onfire = true;
			}
		}
	}

	// Token: 0x0600017E RID: 382 RVA: 0x0000FD2C File Offset: 0x0000DF2C
	public virtual void Main()
	{
	}

	// Token: 0x0400024D RID: 589
	public float lightdecay;

	// Token: 0x0400024E RID: 590
	public float mydamage;

	// Token: 0x0400024F RID: 591
	public float collidertime;

	// Token: 0x04000250 RID: 592
	public float rocketjumpamount;

	// Token: 0x04000251 RID: 593
	public LayerMask jumplayers;

	// Token: 0x04000252 RID: 594
	public LayerMask explosionblockinglayers;

	// Token: 0x04000253 RID: 595
	public bool fireexplosion;

	// Token: 0x04000254 RID: 596
	public bool playerspawnedme;

	// Token: 0x04000255 RID: 597
	[HideInInspector]
	public PersistScript persist;
}
