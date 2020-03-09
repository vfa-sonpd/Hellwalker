using System;
using UnityEngine;

// Token: 0x020000B5 RID: 181
[Serializable]
public class SwitchRoomAfterTime : MonoBehaviour
{
	// Token: 0x06000445 RID: 1093 RVA: 0x00029138 File Offset: 0x00027338
	public virtual void Start()
	{
	}

	// Token: 0x06000446 RID: 1094 RVA: 0x0002913C File Offset: 0x0002733C
	public virtual void Update()
	{
		this.t += Time.deltaTime;
		if (this.t > this.waittime)
		{
			Application.LoadLevel(this.room);
		}
	}

	// Token: 0x06000447 RID: 1095 RVA: 0x00029178 File Offset: 0x00027378
	public virtual void Main()
	{
	}

	// Token: 0x04000550 RID: 1360
	public string room;

	// Token: 0x04000551 RID: 1361
	public float waittime;

	// Token: 0x04000552 RID: 1362
	[HideInInspector]
	public float t;
}
