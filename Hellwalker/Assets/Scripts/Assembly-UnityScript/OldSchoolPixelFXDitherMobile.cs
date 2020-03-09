using System;
using System.Collections;
using UnityEngine;
using UnityScript.Lang;
using UnityStandardAssets.ImageEffects;

// Token: 0x02000004 RID: 4
[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Color Adjustments/OldSchoolPixelFX Dither Mobile")]
[Serializable]
public class OldSchoolPixelFXDitherMobile : PostEffectsBase
{
	// Token: 0x06000013 RID: 19 RVA: 0x00002F30 File Offset: 0x00001130
	public OldSchoolPixelFXDitherMobile()
	{
		this.pixelRes = new Vector2((float)320, (float)200);
		this.width = 320;
		this.height = 200;
		this.useDownscale = true;
		this.useColormap = true;
		this.useDownscaleRes = true;
		this.basedOnTempTex = string.Empty;
	}

	// Token: 0x06000014 RID: 20 RVA: 0x00002F90 File Offset: 0x00001190
	public override bool CheckResources()
	{
		this.CheckSupport(false);
		this.material = this.CheckShaderAndCreateMaterial(this.shader, this.material);
		if (!this.isSupported)
		{
			this.ReportAutoDisable();
		}
		return this.isSupported;
	}

	// Token: 0x06000015 RID: 21 RVA: 0x00002FCC File Offset: 0x000011CC
	public virtual void OnDisable()
	{
		if (this.material)
		{
			UnityEngine.Object.DestroyImmediate(this.material);
			this.material = null;
		}
	}

	// Token: 0x06000016 RID: 22 RVA: 0x00002FFC File Offset: 0x000011FC
	public virtual void OnDestroy()
	{
		if (this.converted3DLut2D)
		{
			UnityEngine.Object.DestroyImmediate(this.converted3DLut2D);
		}
		this.converted3DLut2D = null;
	}

	// Token: 0x06000017 RID: 23 RVA: 0x0000302C File Offset: 0x0000122C
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
					array[k + i * num + j * (num * num)] = new Color((float)k * num2, 1f - (float)j * num2, (float)i * num2, 1f);
				}
			}
		}
		if (this.converted3DLut2D)
		{
			UnityEngine.Object.DestroyImmediate(this.converted3DLut2D);
		}
		this.converted3DLut2D = new Texture2D(num * num, num, TextureFormat.ARGB32, false);
		this.converted3DLut2D.filterMode = FilterMode.Point;
		this.converted3DLut2D.wrapMode = TextureWrapMode.Clamp;
		this.converted3DLut2D.SetPixels(array);
		this.converted3DLut2D.Apply();
		this.basedOnTempTex = string.Empty;
	}

	// Token: 0x06000018 RID: 24 RVA: 0x00003138 File Offset: 0x00001338
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

	// Token: 0x06000019 RID: 25 RVA: 0x0000317C File Offset: 0x0000137C
	public virtual void Convert(Texture2D temp2DTex, string path)
	{
		if (temp2DTex)
		{
			int num = 8;
			float num2 = (float)num;
			Texture2D texture2D = new Texture2D(num * num, num, TextureFormat.ARGB32, false);
			texture2D.filterMode = FilterMode.Point;
			texture2D.wrapMode = TextureWrapMode.Clamp;
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
						texture2D.SetPixel(k * num + m, l, new Color(vector.x, vector.y, vector.z));
					}
				}
			}
			if (this.converted3DLut2D)
			{
				UnityEngine.Object.DestroyImmediate(this.converted3DLut2D);
			}
			this.converted3DLut2D = texture2D;
			this.converted3DLut2D.filterMode = FilterMode.Point;
			this.converted3DLut2D.wrapMode = TextureWrapMode.Clamp;
			this.converted3DLut2D.Apply();
			this.basedOnTempTex = path;
		}
		else
		{
			Debug.LogError("Couldn't color correct with 3D LUT texture. Image Effect will be disabled.");
		}
	}

	// Token: 0x0600001A RID: 26 RVA: 0x000033D8 File Offset: 0x000015D8
	public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.CheckResources() || (!this.useColormap && !this.useDownscale))
		{
			Graphics.Blit(source, destination);
		}
		else
		{
			if (this.useColormap)
			{
				if (this.converted3DLut2D == null)
				{
					this.SetIdentityLut();
				}
				int num = this.converted3DLut2D.width;
				int num2 = this.converted3DLut2D.height;
				this.material.SetFloat("_ScaleY", (float)num2);
				this.material.SetFloat("_Offset", 1f / (2f * (float)num));
				this.material.SetTexture("_ClutTex", this.converted3DLut2D);
				this.material.SetTexture("_DitherTex", this.ditherTex);
				this.material.SetInt("_DitherTexWith", this.ditherTex.width);
				this.material.SetInt("_DitherTexTotal", this.ditherTex.width * this.ditherTex.height);
				if (this.useDownscaleRes)
				{
					this.material.SetInt("_ScreenWidth", this.width);
					this.material.SetInt("_ScreenHeight", this.height);
				}
				else
				{
					this.material.SetInt("_ScreenWidth", Screen.width);
					this.material.SetInt("_ScreenHeight", Screen.height);
				}
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

	// Token: 0x0600001B RID: 27 RVA: 0x000035F0 File Offset: 0x000017F0
	public virtual void Main()
	{
	}

	// Token: 0x04000019 RID: 25
	public Vector2 pixelRes;

	// Token: 0x0400001A RID: 26
	public int width;

	// Token: 0x0400001B RID: 27
	public int height;

	// Token: 0x0400001C RID: 28
	public float grayScaleSupression;

	// Token: 0x0400001D RID: 29
	public Shader shader;

	// Token: 0x0400001E RID: 30
	public Texture2D ditherTex;

	// Token: 0x0400001F RID: 31
	public bool useDownscale;

	// Token: 0x04000020 RID: 32
	public bool useColormap;

	// Token: 0x04000021 RID: 33
	public bool useDownscaleRes;

	// Token: 0x04000022 RID: 34
	private Material material;

	// Token: 0x04000023 RID: 35
	public Texture2D converted3DLut2D;

	// Token: 0x04000024 RID: 36
	public string basedOnTempTex;
}
