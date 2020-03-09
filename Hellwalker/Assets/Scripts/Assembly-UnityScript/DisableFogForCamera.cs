using System;
using UnityEngine;

// Token: 0x0200002E RID: 46
[Serializable]
public class DisableFogForCamera : MonoBehaviour
{
	// Token: 0x06000124 RID: 292 RVA: 0x0000DC24 File Offset: 0x0000BE24
	public virtual void OnPreRender()
	{
		this.revertFogState = RenderSettings.fog;
		RenderSettings.fog = false;
	}

	// Token: 0x06000125 RID: 293 RVA: 0x0000DC38 File Offset: 0x0000BE38
	public virtual void OnPostRender()
	{
		RenderSettings.fog = this.revertFogState;
	}

	// Token: 0x06000126 RID: 294 RVA: 0x0000DC48 File Offset: 0x0000BE48
	public virtual void Main()
	{
	}

	// Token: 0x04000201 RID: 513
	private bool revertFogState;
}
