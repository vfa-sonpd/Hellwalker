using System;
using UnityEngine;
using UnityScript.Lang;

// Token: 0x020000AB RID: 171
[Serializable]
public class ShaderScript : MonoBehaviour
{
	// Token: 0x0600040C RID: 1036 RVA: 0x0002773C File Offset: 0x0002593C
	public virtual void Start()
	{
		this.CreateDynamicLights();
		this.bakedlightmap = LightmapSettings.lightmaps;
		Material[] array = (Material[])Resources.FindObjectsOfTypeAll(typeof(Material));
		for (int i = 0; i < Extensions.get_length(array); i++)
		{
			if (!this.donormalmaps)
			{
				if (array[i].HasProperty("_BumpScale"))
				{
					array[i].SetFloat("_BumpScale", (float)0);
				}
				if (array[i].HasProperty("_DetailNormalMapScale"))
				{
					array[i].SetFloat("_DetailNormalMapScale", (float)0);
				}
			}
			else
			{
				if (array[i].HasProperty("_BumpScale"))
				{
					array[i].SetFloat("_BumpScale", (float)1);
				}
				if (array[i].HasProperty("_DetailNormalMapScale"))
				{
					array[i].SetFloat("_DetailNormalMapScale", (float)1);
				}
			}
			if (!this.dodetailmaps)
			{
				if (array[i].HasProperty("_DetailNormalMapScale"))
				{
					array[i].SetFloat("_DetailNormalMapScale", (float)0);
				}
			}
			else if (array[i].HasProperty("_DetailNormalMapScale"))
			{
				array[i].SetFloat("_DetailNormalMapScale", (float)1);
			}
			if (!this.dospecular)
			{
				if (array[i].HasProperty("_Glossiness"))
				{
					array[i].SetFloat("_Glossiness", (float)1);
				}
			}
			else if (array[i].HasProperty("_Glossiness"))
			{
				array[i].SetFloat("_Glossiness", 0.5f);
			}
			if (this.dynamiclighting)
			{
				this.TurnLightmapsOff();
				this.SetDynamicLights(true);
				this.SetEmissionZero();
			}
			else
			{
				this.SetDynamicLights(false);
			}
		}
	}

	// Token: 0x0600040D RID: 1037 RVA: 0x000278F0 File Offset: 0x00025AF0
	public virtual void Update()
	{
	}

	// Token: 0x0600040E RID: 1038 RVA: 0x000278F4 File Offset: 0x00025AF4
	public virtual void TurnLightmapsOff()
	{
		Renderer[] array = ((Renderer[])UnityEngine.Object.FindObjectsOfType(typeof(Renderer))) as Renderer[];
		int i = 0;
		Renderer[] array2 = array;
		int length = array2.Length;
		while (i < length)
		{
			array2[i].lightmapIndex = -1;
			i++;
		}
	}

	// Token: 0x0600040F RID: 1039 RVA: 0x00027944 File Offset: 0x00025B44
	public virtual void SetDynamicLights(bool a)
	{
		for (int i = 0; i < this.realtimeLights.Length; i++)
		{
			if (this.realtimeLights[i] != null)
			{
				this.realtimeLights[i].active = a;
			}
		}
	}

	// Token: 0x06000410 RID: 1040 RVA: 0x00027990 File Offset: 0x00025B90
	public virtual void CreateDynamicLights()
	{
		Light[] array = ((Light[])UnityEngine.Object.FindObjectsOfType(typeof(Light))) as Light[];
		this.realtimeLights = new GameObject[array.Length];
		int num = 0;
		int i = 0;
		Light[] array2 = array;
		int length = array2.Length;
		while (i < length)
		{
			if (array2[i].alreadyLightmapped && array2[i].type == LightType.Point)
			{
				this.realtimeLights[num] = UnityEngine.Object.Instantiate<GameObject>(this.dynamiclight, array2[i].gameObject.transform.position, Quaternion.identity);
				Light light = (Light)this.realtimeLights[num].GetComponent(typeof(Light));
				light.range = array2[i].range * 1.8f;
				light.color = array2[i].color;
				light.intensity = array2[i].intensity * 1.1f;
				this.realtimeLights[num].active = false;
				num++;
			}
			i++;
		}
	}

	// Token: 0x06000411 RID: 1041 RVA: 0x00027AA4 File Offset: 0x00025CA4
	public virtual void SetEmissionZero()
	{
		Renderer[] array = ((Renderer[])UnityEngine.Object.FindObjectsOfType(typeof(Renderer))) as Renderer[];
		int i = 0;
		Renderer[] array2 = array;
		int length = array2.Length;
		while (i < length)
		{
			if (!array2[i].useLightProbes)
			{
				array2[i].material.SetColor("_EmissionColor", new Color(0.3f, 0.3f, 0.3f));
			}
			i++;
		}
	}

	// Token: 0x06000412 RID: 1042 RVA: 0x00027B1C File Offset: 0x00025D1C
	public virtual void Main()
	{
	}

	// Token: 0x04000516 RID: 1302
	public bool donormalmaps;

	// Token: 0x04000517 RID: 1303
	public bool dospecular;

	// Token: 0x04000518 RID: 1304
	public bool dodetailmaps;

	// Token: 0x04000519 RID: 1305
	public bool dynamiclighting;

	// Token: 0x0400051A RID: 1306
	public GameObject dynamiclight;

	// Token: 0x0400051B RID: 1307
	public Texture2D blacklightmap;

	// Token: 0x0400051C RID: 1308
	[HideInInspector]
	public LightmapData[] bakedlightmap;

	// Token: 0x0400051D RID: 1309
	public GameObject[] realtimeLights;
}
