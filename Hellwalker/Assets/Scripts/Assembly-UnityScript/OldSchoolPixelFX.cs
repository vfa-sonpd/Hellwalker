using System;
using System.Collections;
using UnityEngine;
using UnityScript.Lang;
using UnityStandardAssets.ImageEffects;

// Token: 0x02000002 RID: 2
[AddComponentMenu("Image Effects/Color Adjustments/OldSchoolPixelFX")]
[ExecuteInEditMode]
[Serializable]
public class OldSchoolPixelFX : PostEffectsBase
{
	// Token: 0x06000001 RID: 1 RVA: 0x000020EC File Offset: 0x000002EC
	public OldSchoolPixelFX()
	{
		this.pixelRes = new Vector2((float)320, (float)200);
		this.width = 320;
		this.height = 200;
		this.useDownscale = true;
		this.useColormap = true;
		this.basedOnTempTex = string.Empty;
	}

	// Token: 0x06000002 RID: 2 RVA: 0x00002148 File Offset: 0x00000348
	public override bool CheckResources()
	{
		this.CheckSupport(false);
		this.material = this.CheckShaderAndCreateMaterial(this.shader, this.material);
		if (!this.isSupported || !SystemInfo.supports3DTextures)
		{
			this.ReportAutoDisable();
		}
		return this.isSupported;
	}

	// Token: 0x06000003 RID: 3 RVA: 0x00002198 File Offset: 0x00000398
	public virtual void OnDisable()
	{
		if (this.material)
		{
			UnityEngine.Object.DestroyImmediate(this.material);
			this.material = null;
		}
	}

	// Token: 0x06000004 RID: 4 RVA: 0x000021C8 File Offset: 0x000003C8
	public virtual void OnDestroy()
	{
		if (this.converted3DLut)
		{
			UnityEngine.Object.DestroyImmediate(this.converted3DLut);
		}
		this.converted3DLut = null;
	}

	// Token: 0x06000005 RID: 5 RVA: 0x000021F8 File Offset: 0x000003F8
	public virtual void SetIdentityLut()
	{
		int num = 8;
		Color[] array = new Color[num * num * num];
		float num2 = 1f / (1f * (float)num - 1f);
		for (int i = 0; i < num; i++)
		{
			for (int j = 0; j < num; j++)
			{
				for (int k = 0; k < num; k++)
				{
					array[i + j * num + k * num * num] = new Color((float)i * 1f * num2, (float)j * 1f * num2, (float)k * 1f * num2, 1f);
				}
			}
		}
		if (this.converted3DLut)
		{
			UnityEngine.Object.DestroyImmediate(this.converted3DLut);
		}
		this.converted3DLut = new Texture3D(num, num, num, TextureFormat.ARGB32, false);
		this.converted3DLut.SetPixels(array);
		this.converted3DLut.Apply();
		this.basedOnTempTex = string.Empty;
	}

	// Token: 0x06000006 RID: 6 RVA: 0x000022F4 File Offset: 0x000004F4
	public virtual bool ValidDimensions(Texture2D tex2d)
	{
		bool result;
		if (!tex2d)
		{
			result = false;
		}
		else
		{
			int num = tex2d.height;
			result = (num == Mathf.FloorToInt(Mathf.Sqrt((float)tex2d.width)));
		}
		return result;
	}

	// Token: 0x06000007 RID: 7 RVA: 0x00002338 File Offset: 0x00000538
	public virtual void Convert(Texture2D temp2DTex, string path)
	{
		if (temp2DTex)
		{
			int num = 8;
			float num2 = (float)num;
			this.lutTexture2D = new Texture2D(num * num, num, TextureFormat.ARGB32, false);
			this.lutTexture2D.filterMode = FilterMode.Point;
			this.lutTexture2D.wrapMode = TextureWrapMode.Clamp;
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < temp2DTex.width; i++)
			{
				for (int j = 0; j < temp2DTex.height; j++)
				{
					Color pixel = temp2DTex.GetPixel(i, j);
					if (!arrayList.Contains(pixel))
					{
						arrayList.Add(pixel);
					}
				}
			}
			for (int k = 0; k < num; k++)
			{
				for (int l = 0; l < num; l++)
				{
					for (int m = 0; m < num; m++)
					{
						Vector3 a = new Vector3((float)m / (num2 - (float)1), 1f - (float)l / (num2 - (float)1), (float)k / (num2 - (float)1));
						float num3 = (float)(num * num * num);
						Vector3 vector = Vector3.zero;
						IEnumerator enumerator = UnityRuntimeServices.GetEnumerator(arrayList);
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							Color color = (Color)obj;
							Vector3 vector2 = new Vector3(color.r, color.g, color.b);
							UnityRuntimeServices.Update(enumerator, color);
							float num4 = Vector3.Distance(a, vector2);
							if (num4 < num3)
							{
								if (this.grayScaleSupression != (float)0 && color.r == color.g && color.g == color.b)
								{
									if (num4 + this.grayScaleSupression < num3)
									{
										num3 = num4;
										vector = vector2;
									}
								}
								else
								{
									num3 = num4;
									vector = vector2;
								}
							}
						}
						this.lutTexture2D.SetPixel(k * num + m, l, new Color(vector.x, vector.y, vector.z));
					}
				}
			}
			this.lutTexture2D.Apply();
			int num5 = this.lutTexture2D.width * this.lutTexture2D.height;
			num5 = this.lutTexture2D.height;
			Color[] pixels = this.lutTexture2D.GetPixels();
			Color[] array = new Color[pixels.Length];
			for (int n = 0; n < num5; n++)
			{
				for (int num6 = 0; num6 < num5; num6++)
				{
					for (int num7 = 0; num7 < num5; num7++)
					{
						int num8 = num5 - num6 - 1;
						array[n + num6 * num5 + num7 * num5 * num5] = pixels[num7 * num5 + n + num8 * num5 * num5];
					}
				}
			}
			if (this.converted3DLut)
			{
				UnityEngine.Object.DestroyImmediate(this.converted3DLut);
			}
			this.converted3DLut = new Texture3D(num5, num5, num5, TextureFormat.ARGB32, false);
			this.converted3DLut.SetPixels(array);
			this.converted3DLut.filterMode = FilterMode.Point;
			this.converted3DLut.wrapMode = TextureWrapMode.Clamp;
			this.converted3DLut.Apply();
			this.basedOnTempTex = path;
		}
		else
		{
			Debug.LogError("Couldn't color correct with 3D LUT texture. Image Effect will be disabled.");
		}
	}

	// Token: 0x06000008 RID: 8 RVA: 0x00002690 File Offset: 0x00000890
	public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.CheckResources() || !SystemInfo.supports3DTextures || (!this.useColormap && !this.useDownscale))
		{
			Graphics.Blit(source, destination);
		}
		else
		{
			if (this.useColormap)
			{
				if (this.converted3DLut == null)
				{
					this.SetIdentityLut();
				}
				this.material.SetTexture("_ClutTex", this.converted3DLut);
			}
			if (this.useDownscale)
			{
				source.filterMode = FilterMode.Point;
				RenderTexture temporary = RenderTexture.GetTemporary(this.width, this.height, 0, source.format);
				temporary.filterMode = FilterMode.Point;
				Graphics.Blit(source, temporary);
				if (this.useColormap)
				{
					Graphics.Blit(temporary, destination, this.material, (QualitySettings.activeColorSpace != ColorSpace.Linear) ? 0 : 1);
				}
				else
				{
					Graphics.Blit(temporary, destination);
				}
				RenderTexture.ReleaseTemporary(temporary);
			}
			else
			{
				Graphics.Blit(source, destination, this.material, (QualitySettings.activeColorSpace != ColorSpace.Linear) ? 0 : 1);
			}
		}
	}

	// Token: 0x06000009 RID: 9 RVA: 0x000027A8 File Offset: 0x000009A8
	public virtual void Main()
	{
	}

	// Token: 0x04000001 RID: 1
	public Vector2 pixelRes;

	// Token: 0x04000002 RID: 2
	public int width;

	// Token: 0x04000003 RID: 3
	public int height;

	// Token: 0x04000004 RID: 4
	public float grayScaleSupression;

	// Token: 0x04000005 RID: 5
	public Shader shader;

	// Token: 0x04000006 RID: 6
	public bool useDownscale;

	// Token: 0x04000007 RID: 7
	public bool useColormap;

	// Token: 0x04000008 RID: 8
	private Material material;

	// Token: 0x04000009 RID: 9
	public Texture3D converted3DLut;

	// Token: 0x0400000A RID: 10
	public string basedOnTempTex;

	// Token: 0x0400000B RID: 11
	public Texture2D lutTexture2D;
}
