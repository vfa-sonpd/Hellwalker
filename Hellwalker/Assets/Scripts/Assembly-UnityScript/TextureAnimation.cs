using System;
using UnityEngine;

// Token: 0x020000BA RID: 186
[Serializable]
public class TextureAnimation : MonoBehaviour
{
	// Token: 0x0600045D RID: 1117 RVA: 0x00029620 File Offset: 0x00027820
	public TextureAnimation()
	{
		this.uvAnimationTileX = 24;
		this.uvAnimationTileY = 1;
		this.framesPerSecond = 10f;
	}

	// Token: 0x0600045E RID: 1118 RVA: 0x00029650 File Offset: 0x00027850
	public virtual void Update()
	{
		int num = (int)(Time.time * this.framesPerSecond);
		num %= this.uvAnimationTileX * this.uvAnimationTileY;
		Vector2 value = new Vector2(1f / (float)this.uvAnimationTileX, 1f / (float)this.uvAnimationTileY);
		int num2 = num % this.uvAnimationTileX;
		int num3 = num / this.uvAnimationTileX;
		Vector2 value2 = new Vector2((float)num2 * value.x, 1f - value.y - (float)num3 * value.y);
		this.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", value2);
		this.GetComponent<Renderer>().material.SetTextureScale("_MainTex", value);
	}

	// Token: 0x0600045F RID: 1119 RVA: 0x00029704 File Offset: 0x00027904
	public virtual void Main()
	{
	}

	// Token: 0x0400055F RID: 1375
	public int uvAnimationTileX;

	// Token: 0x04000560 RID: 1376
	public int uvAnimationTileY;

	// Token: 0x04000561 RID: 1377
	public float framesPerSecond;
}
