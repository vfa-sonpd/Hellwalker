using System;
using UnityEngine;

// Token: 0x02000075 RID: 117
[Serializable]
public class MainMenuFilterDisable : MonoBehaviour
{
	// Token: 0x060002D8 RID: 728 RVA: 0x0001A1D4 File Offset: 0x000183D4
	public virtual void Start()
	{
	}

	// Token: 0x060002D9 RID: 729 RVA: 0x0001A1D8 File Offset: 0x000183D8
	public virtual void Update()
	{
		PersistScript persistScript = (PersistScript)GameObject.Find("PERSIST").GetComponent(typeof(PersistScript));
		((OldSchoolPixelFX)this.GetComponent(typeof(OldSchoolPixelFX))).enabled = persistScript.pixelfilter;
	}

	// Token: 0x060002DA RID: 730 RVA: 0x0001A224 File Offset: 0x00018424
	public virtual void Main()
	{
	}
}
