using System;
using UnityEngine;

// Token: 0x02000068 RID: 104
[Serializable]
public class LightTriggerScript : MonoBehaviour
{
	// Token: 0x060002A0 RID: 672 RVA: 0x00019190 File Offset: 0x00017390
	public virtual void Start()
	{
		this.shade = (ShaderScript)GameObject.Find("FilteringSwitcher").GetComponent(typeof(ShaderScript));
		if (RenderSettings.ambientIntensity > (float)0)
		{
			RenderSettings.ambientIntensity = (float)0;
		}
	}

	// Token: 0x060002A1 RID: 673 RVA: 0x000191CC File Offset: 0x000173CC
	public virtual void Update()
	{
	}

	// Token: 0x060002A2 RID: 674 RVA: 0x000191D0 File Offset: 0x000173D0
	public virtual void OnTriggerStay(Collider hit)
	{
		if (!this.shade.dynamiclighting)
		{
			Component[] componentsInChildren = hit.transform.gameObject.GetComponentsInChildren(typeof(Renderer));
			int i = 0;
			Component[] array = componentsInChildren;
			int length = array.Length;
			while (i < length)
			{
				if ((Renderer)array[i] && !((Renderer)array[i]).useLightProbes)
				{
					Material material = ((Renderer)array[i]).material;
					material.SetColor("_EmissionColor", this.myentercolor);
				}
				i++;
			}
		}
	}

	// Token: 0x060002A3 RID: 675 RVA: 0x00019270 File Offset: 0x00017470
	public virtual void Main()
	{
	}

	// Token: 0x04000316 RID: 790
	public Color myentercolor;

	// Token: 0x04000317 RID: 791
	[HideInInspector]
	public ShaderScript shade;
}
