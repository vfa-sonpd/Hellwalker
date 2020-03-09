using System;
using UnityEngine;

// Token: 0x020000AF RID: 175
[Serializable]
public class SkyColorScript : MonoBehaviour
{
	// Token: 0x06000422 RID: 1058 RVA: 0x00027EAC File Offset: 0x000260AC
	public virtual void Start()
	{
		GameObject gameObject = GameObject.Find("SkyCamera");
		if (gameObject)
		{
			((Camera)gameObject.GetComponent(typeof(Camera))).backgroundColor = this.skycolor;
			if (this.solidcolor)
			{
				((Camera)gameObject.GetComponent(typeof(Camera))).clearFlags = CameraClearFlags.Color;
			}
			else
			{
				((Camera)gameObject.GetComponent(typeof(Camera))).clearFlags = CameraClearFlags.Skybox;
			}
		}
	}

	// Token: 0x06000423 RID: 1059 RVA: 0x00027F38 File Offset: 0x00026138
	public virtual void Update()
	{
	}

	// Token: 0x06000424 RID: 1060 RVA: 0x00027F3C File Offset: 0x0002613C
	public virtual void Main()
	{
	}

	// Token: 0x04000521 RID: 1313
	public Color skycolor;

	// Token: 0x04000522 RID: 1314
	public bool solidcolor;
}
