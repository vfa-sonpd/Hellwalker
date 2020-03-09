using System;
using UnityEngine;

// Token: 0x020000CA RID: 202
[Serializable]
public class WaterScrollerScript : MonoBehaviour
{
	// Token: 0x060004AD RID: 1197 RVA: 0x0002AC14 File Offset: 0x00028E14
	public virtual void Start()
	{
	}

	// Token: 0x060004AE RID: 1198 RVA: 0x0002AC18 File Offset: 0x00028E18
	public virtual void Update()
	{
		float x = this.watermat.mainTextureOffset.x + Time.deltaTime * this.waterspeed;
		Vector2 mainTextureOffset = this.watermat.mainTextureOffset;
		float num = mainTextureOffset.x = x;
		Vector2 vector = this.watermat.mainTextureOffset = mainTextureOffset;
		if (this.watermat.mainTextureOffset.x > (float)999)
		{
			float x2 = this.watermat.mainTextureOffset.x - (float)999;
			Vector2 mainTextureOffset2 = this.watermat.mainTextureOffset;
			float num2 = mainTextureOffset2.x = x2;
			Vector2 vector2 = this.watermat.mainTextureOffset = mainTextureOffset2;
		}
		this.watermat.SetTextureOffset("_DetailAlbedoMap", new Vector2(-this.watermat.mainTextureOffset.x, 0.3f));
	}

	// Token: 0x060004AF RID: 1199 RVA: 0x0002AD14 File Offset: 0x00028F14
	public virtual void Main()
	{
	}

	// Token: 0x040005A0 RID: 1440
	public float waterspeed;

	// Token: 0x040005A1 RID: 1441
	public Material watermat;
}
