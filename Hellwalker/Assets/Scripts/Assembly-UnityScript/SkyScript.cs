using System;
using UnityEngine;

// Token: 0x020000B0 RID: 176
[Serializable]
public class SkyScript : MonoBehaviour
{
	// Token: 0x06000426 RID: 1062 RVA: 0x00027F48 File Offset: 0x00026148
	public virtual void Start()
	{
	}

	// Token: 0x06000427 RID: 1063 RVA: 0x00027F4C File Offset: 0x0002614C
	public virtual void Update()
	{
		float y = this.transform.eulerAngles.y;
		float x = this.transform.eulerAngles.x;
		Material material = ((Renderer)this.GetComponent(typeof(Renderer))).material;
		material.SetTextureOffset("_MainTex", new Vector2(y / (float)360, -x / (float)360));
	}

	// Token: 0x06000428 RID: 1064 RVA: 0x00027FC0 File Offset: 0x000261C0
	public virtual void Main()
	{
	}
}
