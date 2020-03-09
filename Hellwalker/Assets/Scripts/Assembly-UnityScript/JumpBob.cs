using System;
using UnityEngine;

// Token: 0x0200005F RID: 95
[Serializable]
public class JumpBob : MonoBehaviour
{
	// Token: 0x06000274 RID: 628 RVA: 0x00017760 File Offset: 0x00015960
	public virtual void Start()
	{
		this.originaly = this.transform.localPosition.y;
	}

	// Token: 0x06000275 RID: 629 RVA: 0x00017788 File Offset: 0x00015988
	public virtual void Update()
	{
		if (!this.disabled)
		{
			if (this.transform.localPosition.y > this.originaly)
			{
				float y = this.transform.localPosition.y - Time.deltaTime * this.returnspeed;
				Vector3 localPosition = this.transform.localPosition;
				float num = localPosition.y = y;
				Vector3 vector = this.transform.localPosition = localPosition;
			}
			if (this.transform.localPosition.y < this.originaly)
			{
				float y2 = this.transform.localPosition.y + Time.deltaTime * this.returnspeed;
				Vector3 localPosition2 = this.transform.localPosition;
				float num2 = localPosition2.y = y2;
				Vector3 vector2 = this.transform.localPosition = localPosition2;
			}
		}
		else
		{
			float y3 = this.originaly;
			Vector3 localPosition3 = this.transform.localPosition;
			float num3 = localPosition3.y = y3;
			Vector3 vector3 = this.transform.localPosition = localPosition3;
		}
	}

	// Token: 0x06000276 RID: 630 RVA: 0x000178C4 File Offset: 0x00015AC4
	public virtual void Main()
	{
	}

	// Token: 0x040002D7 RID: 727
	public float returnspeed;

	// Token: 0x040002D8 RID: 728
	[HideInInspector]
	public float originaly;

	// Token: 0x040002D9 RID: 729
	[HideInInspector]
	public bool disabled;
}
