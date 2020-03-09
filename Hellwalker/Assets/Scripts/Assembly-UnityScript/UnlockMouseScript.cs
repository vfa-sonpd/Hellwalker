using System;
using UnityEngine;

// Token: 0x020000C6 RID: 198
[Serializable]
public class UnlockMouseScript : MonoBehaviour
{
	// Token: 0x0600049A RID: 1178 RVA: 0x0002A53C File Offset: 0x0002873C
	public virtual void Start()
	{
		Screen.lockCursor = false;
	}

	// Token: 0x0600049B RID: 1179 RVA: 0x0002A544 File Offset: 0x00028744
	public virtual void Update()
	{
	}

	// Token: 0x0600049C RID: 1180 RVA: 0x0002A548 File Offset: 0x00028748
	public virtual void Main()
	{
	}
}
