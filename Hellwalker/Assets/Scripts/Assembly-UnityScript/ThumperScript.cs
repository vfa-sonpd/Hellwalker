using System;
using UnityEngine;

// Token: 0x020000BB RID: 187
[Serializable]
public class ThumperScript : MonoBehaviour
{
	// Token: 0x06000461 RID: 1121 RVA: 0x00029710 File Offset: 0x00027910
	public virtual void Start()
	{
		this.dascoor = this.startcoor;
		if (this.upcoor > this.downcoor)
		{
			this.updirection = 1;
		}
		else
		{
			this.updirection = -1;
		}
		this.direction = -this.updirection;
		this.aud = (AudioSource)this.GetComponent(typeof(AudioSource));
	}

	// Token: 0x06000462 RID: 1122 RVA: 0x00029778 File Offset: 0x00027978
	public virtual void Update()
	{
		if ((this.direction < 0 && this.updirection < 0) || (this.direction > 0 && this.updirection > 0))
		{
			this.dascoor += Time.deltaTime * this.upspeed * (float)this.direction;
		}
		if ((this.direction < 0 && this.updirection > 0) || (this.direction > 0 && this.updirection < 0))
		{
			this.dascoor += Time.deltaTime * this.downspeed * (float)this.direction;
		}
		if (this.direction == this.updirection && ((this.updirection < 0 && this.dascoor < this.upcoor) || (this.updirection > 0 && this.dascoor > this.upcoor)))
		{
			this.direction = -this.direction;
		}
		if (this.direction != this.updirection && ((this.updirection > 0 && this.dascoor < this.downcoor) || (this.updirection < 0 && this.dascoor > this.downcoor)))
		{
			this.direction = -this.direction;
			this.aud.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
			this.aud.Play();
		}
		if (this.dox)
		{
			float x = this.dascoor;
			Vector3 position = this.transform.position;
			float num = position.x = x;
			Vector3 vector = this.transform.position = position;
		}
		if (this.doy)
		{
			float y = this.dascoor;
			Vector3 position2 = this.transform.position;
			float num2 = position2.y = y;
			Vector3 vector2 = this.transform.position = position2;
		}
		if (this.doz)
		{
			float z = this.dascoor;
			Vector3 position3 = this.transform.position;
			float num3 = position3.z = z;
			Vector3 vector3 = this.transform.position = position3;
		}
	}

	// Token: 0x06000463 RID: 1123 RVA: 0x000299C8 File Offset: 0x00027BC8
	public virtual void OnTriggerEnter(Collider hit)
	{
		if (this.direction != this.updirection)
		{
			if (hit.transform.gameObject.layer == 10)
			{
				((PlayerHealthManagement)hit.transform.gameObject.GetComponent(typeof(PlayerHealthManagement))).takedamage((float)1000);
			}
			DestructibleObjectScript destructibleObjectScript = (DestructibleObjectScript)hit.transform.gameObject.GetComponent(typeof(DestructibleObjectScript));
			if (destructibleObjectScript)
			{
				destructibleObjectScript.myhealth = (float)0;
				destructibleObjectScript.doragdoll = false;
			}
		}
	}

	// Token: 0x06000464 RID: 1124 RVA: 0x00029A64 File Offset: 0x00027C64
	public virtual void Main()
	{
	}

	// Token: 0x04000562 RID: 1378
	public float startcoor;

	// Token: 0x04000563 RID: 1379
	public float upcoor;

	// Token: 0x04000564 RID: 1380
	public float downcoor;

	// Token: 0x04000565 RID: 1381
	public float upspeed;

	// Token: 0x04000566 RID: 1382
	public float downspeed;

	// Token: 0x04000567 RID: 1383
	public bool dox;

	// Token: 0x04000568 RID: 1384
	public bool doy;

	// Token: 0x04000569 RID: 1385
	public bool doz;

	// Token: 0x0400056A RID: 1386
	[HideInInspector]
	public float dascoor;

	// Token: 0x0400056B RID: 1387
	[HideInInspector]
	public int updirection;

	// Token: 0x0400056C RID: 1388
	[HideInInspector]
	public int direction;

	// Token: 0x0400056D RID: 1389
	[HideInInspector]
	public AudioSource aud;
}
