using System;
using UnityEngine;

// Token: 0x020000C8 RID: 200
[Serializable]
public class WaterAnimator : MonoBehaviour
{
	// Token: 0x060004A2 RID: 1186 RVA: 0x0002A754 File Offset: 0x00028954
	public virtual void Start()
	{
		this.currentframe = (float)0;
		this.frametimer = (float)0;
	}

	// Token: 0x060004A3 RID: 1187 RVA: 0x0002A768 File Offset: 0x00028968
	public virtual void Update()
	{
		float x = this.watermat.mainTextureOffset.x + Time.deltaTime * this.waterspeed;
		Vector2 mainTextureOffset = this.watermat.mainTextureOffset;
		float num = mainTextureOffset.x = x;
		Vector2 vector = this.watermat.mainTextureOffset = mainTextureOffset;
		if (this.watermat.mainTextureOffset.x > (float)2)
		{
			float x2 = this.watermat.mainTextureOffset.x - (float)2;
			Vector2 mainTextureOffset2 = this.watermat.mainTextureOffset;
			float num2 = mainTextureOffset2.x = x2;
			Vector2 vector2 = this.watermat.mainTextureOffset = mainTextureOffset2;
		}
		this.watermat.SetTextureOffset("_DetailAlbedoMap", new Vector2(-this.watermat.mainTextureOffset.x, 0.3f));
		this.frametimer += Time.deltaTime;
		if (this.frametimer > this.wateranimatespeed)
		{
			this.currentframe += (float)1;
			this.frametimer = (float)0;
			if (this.currentframe > (float)(this.waterframes.Length - 1))
			{
				this.currentframe = (float)0;
			}
			this.watermat.SetTexture("_MainTex", this.waterframes[(int)this.currentframe]);
		}
	}

	// Token: 0x060004A4 RID: 1188 RVA: 0x0002A8D4 File Offset: 0x00028AD4
	public virtual void Main()
	{
	}

	// Token: 0x04000593 RID: 1427
	public Material watermat;

	// Token: 0x04000594 RID: 1428
	public float waterspeed;

	// Token: 0x04000595 RID: 1429
	public float wateranimatespeed;

	// Token: 0x04000596 RID: 1430
	public Texture2D[] waterframes;

	// Token: 0x04000597 RID: 1431
	[HideInInspector]
	public float currentframe;

	// Token: 0x04000598 RID: 1432
	[HideInInspector]
	public float frametimer;
}
