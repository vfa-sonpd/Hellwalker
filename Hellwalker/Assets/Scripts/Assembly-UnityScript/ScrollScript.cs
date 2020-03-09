using System;
using UnityEngine;

// Token: 0x020000A0 RID: 160
[Serializable]
public class ScrollScript : MonoBehaviour
{
	// Token: 0x060003DB RID: 987 RVA: 0x00024E58 File Offset: 0x00023058
	public virtual void Start()
	{
	}

	// Token: 0x060003DC RID: 988 RVA: 0x00024E5C File Offset: 0x0002305C
	public virtual void Update()
	{
		float x = this.GetComponent<Renderer>().material.mainTextureOffset.x + Time.deltaTime * this.xspeed;
		Vector2 mainTextureOffset = this.GetComponent<Renderer>().material.mainTextureOffset;
		float num = mainTextureOffset.x = x;
		Vector2 vector = this.GetComponent<Renderer>().material.mainTextureOffset = mainTextureOffset;
		float y = this.GetComponent<Renderer>().material.mainTextureOffset.y + Time.deltaTime * this.yspeed;
		Vector2 mainTextureOffset2 = this.GetComponent<Renderer>().material.mainTextureOffset;
		float num2 = mainTextureOffset2.y = y;
		Vector2 vector2 = this.GetComponent<Renderer>().material.mainTextureOffset = mainTextureOffset2;
	}

	// Token: 0x060003DD RID: 989 RVA: 0x00024F2C File Offset: 0x0002312C
	public virtual void Main()
	{
	}

	// Token: 0x040004BD RID: 1213
	public float xspeed;

	// Token: 0x040004BE RID: 1214
	public float yspeed;
}
