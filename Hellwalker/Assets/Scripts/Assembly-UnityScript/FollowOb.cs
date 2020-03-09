using System;
using UnityEngine;

// Token: 0x0200004C RID: 76
[Serializable]
public class FollowOb : MonoBehaviour
{
	// Token: 0x060001B3 RID: 435 RVA: 0x00010988 File Offset: 0x0000EB88
	public virtual void Start()
	{
		if (this.obisskycam)
		{
			this.ob = GameObject.Find("SkyCamera");
		}
		if (this.obisplayer)
		{
			this.ob = GameObject.Find("Player");
		}
	}

	// Token: 0x060001B4 RID: 436 RVA: 0x000109CC File Offset: 0x0000EBCC
	public virtual void Update()
	{
		if (this.ob)
		{
			float x = this.ob.transform.position.x + this.offsets.x;
			Vector3 position = this.transform.position;
			float num = position.x = x;
			Vector3 vector = this.transform.position = position;
			float y = this.ob.transform.position.y + this.offsets.y;
			Vector3 position2 = this.transform.position;
			float num2 = position2.y = y;
			Vector3 vector2 = this.transform.position = position2;
			float z = this.ob.transform.position.z + this.offsets.z;
			Vector3 position3 = this.transform.position;
			float num3 = position3.z = z;
			Vector3 vector3 = this.transform.position = position3;
			if (this.obisskycam)
			{
				this.transform.parent = null;
				this.transform.eulerAngles = new Vector3((float)90, (float)0, (float)0);
				this.transform.parent = this.ob.transform;
			}
		}
	}

	// Token: 0x060001B5 RID: 437 RVA: 0x00010B34 File Offset: 0x0000ED34
	public virtual void Main()
	{
	}

	// Token: 0x04000275 RID: 629
	public GameObject ob;

	// Token: 0x04000276 RID: 630
	public bool obisskycam;

	// Token: 0x04000277 RID: 631
	public bool obisplayer;

	// Token: 0x04000278 RID: 632
	public Vector3 offsets;
}
