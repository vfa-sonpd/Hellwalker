using System;
using UnityEngine;

// Token: 0x02000088 RID: 136
[Serializable]
public class PickupGravity : MonoBehaviour
{
	// Token: 0x0600035F RID: 863 RVA: 0x0001FEB8 File Offset: 0x0001E0B8
	public virtual void Start()
	{
		this.updatetimer = UnityEngine.Random.Range((float)0, 0.3f);
	}

	// Token: 0x06000360 RID: 864 RVA: 0x0001FECC File Offset: 0x0001E0CC
	public virtual void Update()
	{
		this.updatetimer += Time.deltaTime;
		if (this.updatetimer >= 0.3f)
		{
			this.updatetimer = (float)0;
			float num = this.checkheight();
			if (num > (float)-1)
			{
				if (num > this.targetHeight + 0.1f)
				{
					float y = this.transform.position.y - this.fallSpeed * Time.deltaTime * (float)30;
					Vector3 position = this.transform.position;
					float num2 = position.y = y;
					Vector3 vector = this.transform.position = position;
				}
				else
				{
					this.setheight();
				}
			}
		}
	}

	// Token: 0x06000361 RID: 865 RVA: 0x0001FF84 File Offset: 0x0001E184
	public virtual float checkheight()
	{
		float result = (float)-1;
		RaycastHit raycastHit = default(RaycastHit);
		if (Physics.Raycast(this.transform.position, -Vector3.up, out raycastHit, (float)10000, this.layers))
		{
			result = raycastHit.distance;
		}
		return result;
	}

	// Token: 0x06000362 RID: 866 RVA: 0x0001FFD8 File Offset: 0x0001E1D8
	public virtual void setheight()
	{
		RaycastHit raycastHit = default(RaycastHit);
		if (Physics.Raycast(this.transform.position, -Vector3.up, out raycastHit, (float)10000, this.layers))
		{
			float y = raycastHit.point.y + this.targetHeight;
			Vector3 position = this.transform.position;
			float num = position.y = y;
			Vector3 vector = this.transform.position = position;
		}
	}

	// Token: 0x06000363 RID: 867 RVA: 0x00020068 File Offset: 0x0001E268
	public virtual void Main()
	{
	}

	// Token: 0x04000451 RID: 1105
	public float targetHeight;

	// Token: 0x04000452 RID: 1106
	public float fallSpeed;

	// Token: 0x04000453 RID: 1107
	public LayerMask layers;

	// Token: 0x04000454 RID: 1108
	[HideInInspector]
	public float updatetimer;
}
